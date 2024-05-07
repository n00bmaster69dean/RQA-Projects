//using U4K.BehaviourTemplate.Attr;
//using UnityEngine;
//
//namespace U4K.BehaviourTemplate.Action
//{
//    public class MoveForwardAction : BasicAction
//    {
//        [SerializeField] private float speed;
//
//        [SerializeField] [FieldShownAs("back and forth?")]
//        private bool backAndForth = false;
//
//        [SerializeField] [FieldShownAs("maximum distance")]
//        private float maxDistance;
//
//        private int direction = 1;
//        private float traveledDistance = 0;
//
//        public override void Execute()
//        {
//            base.Execute();
//            if (actor != null)
//            {
//                // control direction
//                if (backAndForth && (traveledDistance > maxDistance || traveledDistance < 0)) direction *= -1;
//                else if (traveledDistance > maxDistance || traveledDistance < 0) direction = 0;
//                // move
//                actor.transform.position += actor.transform.forward * direction * speed * Time.deltaTime;
//                traveledDistance += direction * speed * Time.deltaTime;
//            }
//        }
//
//        public new static string Name
//        {
//            get { return "Move Forward"; }
//        }
//    }
//}