//using UnityEngine;
//
//namespace U4K.BehaviourTemplate.Action
//{
//    public class Move2DirectionAction : BasicAction
//    {
//        [SerializeField] private Direction direction;
//        
//        [SerializeField] private float speed;
//        
//        public override void Execute()
//        {
//            base.Execute();
//            if (actor != null)
//            {
//                Vector3 d = Vector3.zero;
//                switch (direction)
//                {
//                    case Direction.Forward:
//                        d = Vector3.forward;
//                        break;
//                    case Direction.Back:
//                        d = Vector3.back;
//                        break;
//                    case Direction.Right:
//                        d = Vector3.right;
//                        break;
//                    case Direction.Left:
//                        d = Vector3.left;
//                        break;
//                    case Direction.Up:
//                        d = Vector3.up;
//                        break;
//                    case Direction.Down:
//                        d = Vector3.down;
//                        break;
//                }
//                actor.transform.position += d * speed * Time.deltaTime;
//                _done = true;
//            }
//        }
//
//        public new static string Name
//        {
//            get { return "Move to Direction"; }
//        }
//    }
//
//    public enum Direction
//    {
//        Forward,
//        Back,
//        Right,
//        Left,
//        Up,
//        Down
//    }
//}