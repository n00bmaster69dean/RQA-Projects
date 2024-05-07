using U4K.BehaviourTemplate.Action;
using U4K.BehaviourTemplate.Attr;
using UnityEngine;

namespace U4K.BehaviourTemplate
{
    public class SingleStartActionTemplate : BasicBehaviourTemplate
    {
        public override string GetHelpMsg()
        {
            return "The object will do the <i>Action</i> at the beginning";
        }
        
        [SerializeField]
        private BasicAction Action = null;

        private void Start()
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
            return "On-Start Do";
        }
        
        public new static BehaviourType GetBehaviourType()
        {
            return BehaviourType.Event;
        }
    }
}