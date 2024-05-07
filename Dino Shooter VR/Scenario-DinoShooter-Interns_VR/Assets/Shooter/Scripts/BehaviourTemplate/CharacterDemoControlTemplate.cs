using System.Collections.Generic;
using U4K.BehaviourTemplate.Attr;
using UnityEngine;

namespace U4K.BehaviourTemplate
{
    public class CharacterDemoControlTemplate : BasicBehaviourTemplate
    {
        public override string GetHelpMsg()
        {
            return "Control the character's moving with arrow keys and jumping with space key.";
        }

        private enum ControlMode
        {
            Tank,
            Direct
        }

        [SerializeField] [FieldShownAs("Move speed")] [FieldSortIndex(0)]
        private float m_moveSpeed = 2;

        [SerializeField] [FieldShownAs("Turn speed")] [FieldSortIndex(1)]
        private float m_turnSpeed = 200;

        [SerializeField] [FieldShownAs("Jump force")] [FieldSortIndex(2)]
        private float m_jumpForce = 4;

        private ControlMode m_controlMode = ControlMode.Tank;

        private bool m_isGrounded;
        private List<string> m_collisions = new List<string>();

        private float m_currentV = 0;
        private float m_currentH = 0;

        private readonly float m_interpolation = 10;
        private readonly float m_walkScale = 0.33f;
        private readonly float m_backwardsWalkScale = 0.16f;
        private readonly float m_backwardRunScale = 0.66f;

        private bool m_wasGrounded;
        private float m_currentDirectionX = 0;
        private float m_currentDirectionY = 0;
        private float m_currentDirectionZ = 0;

        private float m_jumpTimeStamp = 0;
        private float m_minJumpInterval = 0.25f;

        private void Update()
        {
            var m_animator = gameObject.GetComponent<Animator>();
            if (m_animator != null)
            {
                m_animator.SetBool("Grounded", m_isGrounded);

                switch (m_controlMode)
                {
                    case ControlMode.Direct:
                        DirectUpdate(gameObject, m_animator);
                        break;

                    case ControlMode.Tank:
                        TankUpdate(gameObject, m_animator);
                        break;

                    default:
                        Debug.LogError("Unsupported state");
                        break;
                }

                m_wasGrounded = m_isGrounded;
            }
        }

        public void OnCollisionEnter(Collision collision)
        {
            ContactPoint[] contactPoints = collision.contacts;
            for (int i = 0; i < contactPoints.Length; i++)
            {
                if (Vector3.Dot(contactPoints[i].normal, Vector3.up) > 0.5f)
                {
                    if (!m_collisions.Contains(collision.collider.name))
                    {
                        m_collisions.Add(collision.collider.name);
                    }

                    m_isGrounded = true;
                }
            }
        }

        public void OnCollisionStay(Collision collision)
        {
            ContactPoint[] contactPoints = collision.contacts;
            bool validSurfaceNormal = false;
            for (int i = 0; i < contactPoints.Length; i++)
            {
                if (Vector3.Dot(contactPoints[i].normal, Vector3.up) > 0.5f)
                {
                    validSurfaceNormal = true;
                    break;
                }
            }

            if (validSurfaceNormal)
            {
                m_isGrounded = true;
                if (!m_collisions.Contains(collision.collider.name))
                {
                    m_collisions.Add(collision.collider.name);
                }
            }
            else
            {
                if (m_collisions.Contains(collision.collider.name))
                {
                    m_collisions.Remove(collision.collider.name);
                }

                if (m_collisions.Count == 0)
                {
                    m_isGrounded = false;
                }
            }
        }

        public void OnCollisionExit(Collision collision)
        {
            if (m_collisions.Contains(collision.collider.name))
            {
                m_collisions.Remove(collision.collider.name);
            }

            if (m_collisions.Count == 0)
            {
                m_isGrounded = false;
            }
        }

        private void TankUpdate(GameObject obj, Animator m_animator)
        {
            if (m_animator != null && obj != null)
            {
                float v = Input.GetAxis("Vertical");
                float h = Input.GetAxis("Horizontal");

                bool walk = Input.GetKey(KeyCode.LeftShift);

                if (v < 0)
                {
                    if (walk)
                    {
                        v *= m_backwardsWalkScale;
                    }
                    else
                    {
                        v *= m_backwardRunScale;
                    }
                }
                else if (walk)
                {
                    v *= m_walkScale;
                }

                m_currentV = Mathf.Lerp(m_currentV, v, Time.deltaTime * m_interpolation);
                m_currentH = Mathf.Lerp(m_currentH, h, Time.deltaTime * m_interpolation);

                obj.transform.position += obj.transform.forward * m_currentV * m_moveSpeed * Time.deltaTime;
                obj.transform.Rotate(0, m_currentH * m_turnSpeed * Time.deltaTime, 0);

                m_animator.SetFloat("MoveSpeed", m_currentV);

                JumpingAndLanding(obj, m_animator);
            }
        }

        private void DirectUpdate(GameObject obj, Animator m_animator)
        {
            if (m_animator != null && obj != null)
            {
                float v = Input.GetAxis("Vertical");
                float h = Input.GetAxis("Horizontal");

                Transform camera = Camera.main.transform;

                if (Input.GetKey(KeyCode.LeftShift))
                {
                    v *= m_walkScale;
                    h *= m_walkScale;
                }

                m_currentV = Mathf.Lerp(m_currentV, v, Time.deltaTime * m_interpolation);
                m_currentH = Mathf.Lerp(m_currentH, h, Time.deltaTime * m_interpolation);

                Vector3 direction = camera.forward * m_currentV + camera.right * m_currentH;

                float directionLength = direction.magnitude;
                direction.y = 0;
                direction = direction.normalized * directionLength;

                if (direction != Vector3.zero)
                {
                    Vector3 m_currentDirection =
                        new Vector3(m_currentDirectionX, m_currentDirectionY, m_currentDirectionZ);
                    m_currentDirection = Vector3.Slerp(m_currentDirection, direction, Time.deltaTime * m_interpolation);
                    m_currentDirectionX = m_currentDirection.x;
                    m_currentDirectionY = m_currentDirection.y;
                    m_currentDirectionZ = m_currentDirection.z;

                    obj.transform.rotation = Quaternion.LookRotation(m_currentDirection);
                    obj.transform.position += m_currentDirection * m_moveSpeed * Time.deltaTime;

                    m_animator.SetFloat("MoveSpeed", direction.magnitude);
                }

                JumpingAndLanding(obj, m_animator);
            }
        }

        private void JumpingAndLanding(GameObject obj, Animator m_animator)
        {
            var m_rigidBody = obj == null ? null : obj.GetComponent<Rigidbody>();
            if (m_animator != null && m_rigidBody != null)
            {
                bool jumpCooldownOver = (Time.time - m_jumpTimeStamp) >= m_minJumpInterval;

                if (jumpCooldownOver && m_isGrounded && Input.GetKey(KeyCode.Space))
                {
                    m_jumpTimeStamp = Time.time;
                    m_rigidBody.AddForce(Vector3.up * m_jumpForce, ForceMode.Impulse);
                }

                if (!m_wasGrounded && m_isGrounded)
                {
                    m_animator.SetTrigger("Land");
                }

                if (!m_isGrounded && m_wasGrounded)
                {
                    m_animator.SetTrigger("Jump");
                }
            }
        }

        public new static string GetName()
        {
            return "Character Control";
        }

        public new static BehaviourType GetBehaviourType()
        {
            return BehaviourType.Control;
        }
    }
}