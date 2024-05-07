using UnityEngine;

namespace U4K.BehaviourTemplate.Action
{
    public class RotateOnceAction : BasicAction
    {
        [SerializeField] private Vector3 angle;
        
        public override void Execute()
        {
            base.Execute();
            actor.transform.Rotate(angle);
            _done = true;
        }
        
        public new static string Name
        {
            get { return "Rotate Once"; }
        }
    }
}