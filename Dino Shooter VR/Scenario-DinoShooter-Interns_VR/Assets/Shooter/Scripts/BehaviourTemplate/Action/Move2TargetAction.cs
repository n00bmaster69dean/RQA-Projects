//using U4K.BaseScripts;
//using UnityEngine;
//
//namespace U4K.BehaviourTemplate.Action
//{
//    public class Move2TargetAction : BasicAction
//    {
//        [SerializeField] private GameObject target;
//        
//        [SerializeField] private float speed;
//        
//        public override void Execute()
//        {
//            base.Execute();
//            if (actor != null && target != null)
//            {
//                var targetComponent = target.GetComponent<CustomObjectComponent>();
//                var actorComponent = actor.GetComponent<CustomObjectComponent>();
//                actor.transform.position += Vector3.Normalize(targetComponent.CenterPosition() - actorComponent.CenterPosition()) * speed * Time.deltaTime;
//                _done = true;
//            }
//        }
//
//        public override bool Finished(BasicAction conditionAction)
//        {
//            if (conditionAction is Move2TargetAction)
//            {
//                if (actor != null && target != null && actor.transform.position == target.transform.position)
//                    return true;
//            }
//            return false;
//        }
//
//        public new static string Name
//        {
//            get { return "Move to Target"; }
//        }
//    }
//}