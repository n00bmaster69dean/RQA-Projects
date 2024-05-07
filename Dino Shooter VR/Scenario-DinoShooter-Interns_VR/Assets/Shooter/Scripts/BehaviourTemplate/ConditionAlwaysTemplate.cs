using System;
using U4K.BehaviourTemplate.Action;
using U4K.BehaviourTemplate.Attr;
using UnityEngine;
using U4K.Utils;

namespace U4K.BehaviourTemplate
{
    [Obsolete]
    public class ConditionAlwaysTemplate : MonoBehaviour
    {
        public string GetHelpMsg()
        {
            return "When the object's <i>Condition</i> Action finished, the <i>Action</i> will be always executed";
        }

        [SerializeField] [FieldShownAs("Action as condition")] [ConditionAction]
        private BasicAction condition = null;
        
        [SerializeField] [FieldShownAs("Action as result")]
        private BasicAction Action = null;
        
        private void Update()
        {
            if (condition != null)
            {
                var conditionActionType = condition.GetType();
                var conditionActions = CommonUtil.GetBasicActions(gameObject);
                foreach (var conditionAction in conditionActions)
                {
                    if (conditionAction != condition && conditionAction.GetType() == conditionActionType)
                    {
                        if (conditionAction.Finished(condition))
                        {
                            if (Action != null)
                            {
                                if (Action.UseDefaultActor)
                                    Action.Actor = gameObject;
                                if (Action.Actor != null)
                                {
                                    Action.Execute();
                                }
                            }
                        }
                    }
                }
            }
        }
        
        public static string GetName()
        {
            return "Conditional Repeat Action";
        }
    }
}