using U4K.BaseScripts;
using U4K.BehaviourTemplate.Attr;
using UnityEngine;

namespace U4K.BehaviourTemplate
{
    public class Move2TargetTemplate : MotionBehaviourTemplate
    {
        [SerializeField] [FieldSortIndex(0)] private GameObject Target = null;
        
        [SerializeField] [FieldSortIndex(1)] private float Speed = 0f;
        
        [SerializeField] [FieldSortIndex(2)] private float Distance = 0f;

        private Vector3 originalPosition;

        private CustomObjectComponent targetComponent;

        private CustomObjectComponent actorComponent;

        private bool moving = true;


        public override string GetHelpMsg()
        {
            return "The object will move to the <i>Target</i> using the <i>Speed</i> with the <i>Distance</i>";
        }

        private void Start()
        {

            targetComponent = Target.GetComponent<CustomObjectComponent>();
            actorComponent = gameObject.GetComponent<CustomObjectComponent>();
            originalPosition = actorComponent == null ? gameObject.transform.position : actorComponent.CenterPosition();
        }

        private void Update()
        {
            if (moving)
            {
                if (Target != null && Vector3.Distance(gameObject.transform.position, originalPosition) < Distance)
                {
                    var tposition = targetComponent == null
                        ? Target.transform.position
                        : targetComponent.CenterPosition();
                    var apostion = actorComponent == null
                        ? gameObject.transform.position
                        : actorComponent.CenterPosition();
                    gameObject.transform.position += Vector3.Normalize(tposition - apostion) * Speed * Time.deltaTime;
                    gameObject.transform.LookAt(Target.transform);
                }
            }
        }

        public new static string GetName()
        {
            return "Move to Target";
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