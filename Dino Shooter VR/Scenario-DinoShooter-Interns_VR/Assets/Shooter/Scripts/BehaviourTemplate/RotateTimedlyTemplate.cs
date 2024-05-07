using UnityEngine;

namespace U4K.BehaviourTemplate
{
    public class RotateTimedlyTemplate : MotionBehaviourTemplate
    {
        [SerializeField] private Vector3 Angle = new Vector3(0,0,0);

        private bool moving = true;
        
        public override string GetHelpMsg()
        {
            return "The object will rotate by the <i>Angle</i> every seconds";
        }

        private void Update()
        {
            if (moving)
            gameObject.transform.Rotate(Angle * Time.deltaTime);
        }

        public new static string GetName()
        {
            return "Rotate Timedly";
        }
        
        public new static BehaviourType GetBehaviourType()
        {
            return BehaviourType.Motion;
        }

        public override void ToggleActive()
        {
            moving = !moving;
        }
    }
}