using UnityEngine;

namespace U4K.BehaviourTemplate
{
    public class HorizontallyMoveTemplate : BasicBehaviourTemplate
    {
        public override string GetHelpMsg()
        {
            return "Move the object forcedly by using arrow keys with the <i>Force</i>.";
        }
        
        [SerializeField]
        private float Force = 1;
        
        private Rigidbody rb;

        private void Start()
        {
            rb = GetComponent<Rigidbody>();
            if (rb == null)
            {
                gameObject.AddComponent<Rigidbody>();
            }
        }

        private void FixedUpdate()
        {
            rb = GetComponent<Rigidbody>();
            if (rb != null)
            {
                float moveHorizontal = Input.GetAxis ("Horizontal");
                float moveVertical = Input.GetAxis("Vertical");
                Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
                rb.AddForce(movement * Force);
            }
            else
            {
                Debug.Log("no rigidbody found");
            }
        }

        public new static string GetName()
        {
            return "Forced horizontally Move";
        }
        
        public new static BehaviourType GetBehaviourType()
        {
            return BehaviourType.Control;
        }
    }
}