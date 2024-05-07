using U4K.BaseScripts;
using U4K.BehaviourTemplate.Attr;
using UnityEngine;

namespace U4K.BehaviourTemplate
{
    public class Move2DirectionTemplate : MotionBehaviourTemplate
    {
        [SerializeField] [FieldSortIndex(0)] private Direction Direction; //non-nullable type

        [SerializeField] [FieldSortIndex(1)] private float Speed =0f;

        [SerializeField] [FieldSortIndex(2)] private float Distance = 0f;

        [SerializeField] [FieldShownAs("World reference")] [FieldSortIndex(3)]
        private bool worldReference = true;

        [SerializeField] [FieldShownAs("Back & Forth")] [FieldSortIndex(4)]
        private bool backAndForth = false; //set default to avoid inspector error
        
        [SerializeField] [FieldShownAs("Disappear after move?")] [FieldSortIndex(5)]
        private bool disapper = false;

        private bool moving = true;

        private Vector3 originalPosition = new Vector3(0,0,0);

        private CustomObjectComponent targetComponent = null;

        private CustomObjectComponent actorComponent = null;

        public override string GetHelpMsg()
        {
            return "The object will move to the <i>Direction</i> using the <i>Speed</i> with the <i>Distance</i>";
        }

        private void Start()
        {
            originalPosition = gameObject.transform.position;
        }

        private void Update()
        {
            if (moving)
            {
                Vector3 d = Vector3.zero;
                if (worldReference)
                {
                    switch (Direction)
                    {
                        case Direction.Forward:
                            d = Vector3.forward;
                            break;
                        case Direction.Back:
                            d = Vector3.back;
                            break;
                        case Direction.Right:
                            d = Vector3.right;
                            break;
                        case Direction.Left:
                            d = Vector3.left;
                            break;
                        case Direction.Up:
                            d = Vector3.up;
                            break;
                        case Direction.Down:
                            d = Vector3.down;
                            break;
                    }
                }
                else
                {
                    switch (Direction)
                    {
                        case Direction.Forward:
                            d = gameObject.transform.forward;
                            break;
                        case Direction.Back:
                            d = gameObject.transform.forward * -1f;
                            break;
                        case Direction.Right:
                            d = gameObject.transform.right;
                            break;
                        case Direction.Left:
                            d = gameObject.transform.right * -1;
                            break;
                        case Direction.Up:
                            d = gameObject.transform.up;
                            break;
                        case Direction.Down:
                            d = gameObject.transform.up * -1;
                            break;
                    }
                }

                var hasMoved = Vector3.Distance(originalPosition, gameObject.transform.position);
                if (hasMoved >= Distance && disapper)
                {
                    gameObject.SetActive(false);
                }
                if (hasMoved < Distance)
                {
                    gameObject.transform.position += d * Speed * Time.deltaTime;
                }
                else if (backAndForth)
                {
                    Speed *= -1;
                    originalPosition = gameObject.transform.position;
                }
            }
        }

        public new static string GetName()
        {
            return "Move to Direction";
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

    public enum Direction
    {
        Forward,
        Back,
        Right,
        Left,
        Up,
        Down
    }
}