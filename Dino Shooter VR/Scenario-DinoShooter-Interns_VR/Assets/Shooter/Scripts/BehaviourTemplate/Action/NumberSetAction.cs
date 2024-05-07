using U4K.BehaviourTemplate.Attr;
using U4K.BaseScripts;
using UnityEngine;

namespace U4K.BehaviourTemplate.Action
{
    public class NumberSetAction : BasicAction
    {
        [SerializeField]
        private float set = 1;
        
        [SerializeField]
        [ConditionField]
        public float target;
        
        private NumberContent numberContent;
        
        public override void Execute()
        {
            base.Execute();
            numberContent = actor.GetComponent<NumberContent>();
            if (numberContent != null)
            {
                numberContent.number = set;
                _done = true;
            }
        }

        public override bool Finished(BasicAction conditionAction)
        {
            if (conditionAction is NumberSetAction)
            {
                var ca = conditionAction as NumberSetAction;
                if (numberContent != null && numberContent.number.Equals(ca.target))
                    return true;
            }
            return false;
        }

        public new static string Name
        {
            get { return "Set Number"; }
        }
    }
}