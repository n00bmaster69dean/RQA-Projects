//using UnityEngine;
//
//namespace U4K.BehaviourTemplate.Action
//{
//    public class RotateTimedlyAction : BasicAction
//    {
//        [SerializeField] private Vector3 angle;
//        
//        public override void Execute()
//        {
//            base.Execute();
//            actor.transform.Rotate(angle * Time.deltaTime);
//            _done = true;
//        }
//        
//        public new static string Name
//        {
//            get { return "Rotate Timedly"; }
//        }
//    }
//}