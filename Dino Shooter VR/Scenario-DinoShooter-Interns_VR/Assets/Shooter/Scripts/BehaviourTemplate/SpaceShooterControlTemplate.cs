using System;
using U4K.BehaviourTemplate.Attr;
using UnityEngine;

namespace U4K.BehaviourTemplate
{
    public class SpaceShooterControlTemplate : BasicBehaviourTemplate
    {
        public override string GetHelpMsg()
        {
            return "Control the Spaceship's moving with arrow keys";
        }

        public new static string GetName()
        {
            return "Spaceship Control";
        }

        [SerializeField] [FieldSortIndex(0)]
        private float speed = 10;

        [SerializeField] [FieldShownAs("horizontal boundary")] [CustomVector2ShownAs("min", "max")]
        private Vector2 x = new Vector2(-10, 10);

        [SerializeField] [FieldShownAs("vertical boundary")] [CustomVector2ShownAs("min", "max")]
        private Vector2 z = new Vector2(-10, 10);

        private float tilt = -25;

        private Rigidbody rb;

        private void Start()
        {
            rb = GetComponent<Rigidbody>();
        }

        private void FixedUpdate()
        {
            float moveHorizontal = 0;
            float moveVertical = 0;

            if (rb != null)
            {
                moveHorizontal = Input.GetAxis("Horizontal");
                moveVertical = Input.GetAxis("Vertical");
                Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
                rb.AddForce(movement * speed);
            }

            var currentPosition = gameObject.transform.position;
            if (currentPosition.x < x.x)
            {
                gameObject.transform.position = new Vector3(x.x, currentPosition.y, currentPosition.z);
            }

            if (currentPosition.x > x.y)
            {
                gameObject.transform.position = new Vector3(x.y, currentPosition.y, currentPosition.z);
            }

            if (currentPosition.z < z.x)
            {
                gameObject.transform.position = new Vector3(currentPosition.x, currentPosition.y, z.x);
            }

            if (currentPosition.z > z.y)
            {
                gameObject.transform.position = new Vector3(currentPosition.x, currentPosition.y, z.y);
            }

            gameObject.transform.rotation = Quaternion.Euler(0.0f, 0.0f, tilt * moveHorizontal);
        }

        public new static BehaviourType GetBehaviourType()
        {
            return BehaviourType.Control;
        }
    }
}