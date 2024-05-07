using U4K.BehaviourTemplate.Action;
using U4K.BehaviourTemplate.Attr;
using UnityEngine;

namespace U4K.BehaviourTemplate
{
    public class RepeatTemplate : BasicBehaviourTemplate
    {
        [SerializeField] [FieldSortIndex(0)] public float Delay = 3f;

        [SerializeField] [FieldSortIndex(1)] private BasicAction Action = null;

        public override string GetHelpMsg()
        {
            return "The object will do the <i>Action</i> repeatedly each <i>Delay</i> seconds";
        }

        void Start()
        {
            // Call the Spawn function after a delay of the spawnTime and then continue to call after the same amount of time.
            InvokeRepeating("Do", Delay, Delay);
        }

        void Do()
        {
            if (Action != null)
            {
                if (Action.UseDefaultActor)
                    Action.Actor = gameObject;
                if (Action.Actor != null)
                    Action.Execute();
            }
        }

        public new static string GetName()
        {
            return "Delayed Repeat Do";
        }
        
        public new static BehaviourType GetBehaviourType()
        {
            return BehaviourType.Event;
        }
    }
}