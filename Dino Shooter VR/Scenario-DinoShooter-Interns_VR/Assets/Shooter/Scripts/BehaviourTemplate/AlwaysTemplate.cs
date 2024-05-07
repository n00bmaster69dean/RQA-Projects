using System;
using U4K.BehaviourTemplate.Action;
using UnityEngine;

namespace U4K.BehaviourTemplate
{
    [Obsolete]
    public class AlwaysTemplate : MonoBehaviour
    {
        public string GetHelpMsg()
        {
            return "The object will always do the <i>Action</i>";
        }
        
        [SerializeField]
        private BasicAction Action = null;

        private void Update()
        {
            if (Action != null)
            {
                if (Action.UseDefaultActor)
                    Action.Actor = gameObject;
                if (Action.Actor != null)
                    Action.Execute();
            }
        }

        public static string GetName()
        {
            return "Always Do";
        }
        
        public static BehaviourType GetBehaviourType()
        {
            return BehaviourType.Event;
        }
    }
}