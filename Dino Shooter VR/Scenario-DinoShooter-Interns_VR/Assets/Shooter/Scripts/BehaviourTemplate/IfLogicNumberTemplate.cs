using U4K.BaseScripts;
using U4K.BehaviourTemplate.Action;
using U4K.BehaviourTemplate.Action.Actor;
using U4K.BehaviourTemplate.Attr;
using U4K.BehaviourTemplate.Utils;
using UnityEngine;

namespace U4K.BehaviourTemplate
{
    public class IfLogicNumberTemplate : BasicBehaviourTemplate
    {
        [SerializeField] [FieldSortIndex(0)] public LogicNumberCompare Compare; //non nullable

        [SerializeField] [FieldSortIndex(1)] public CompareNumberActor Target = null;

        [SerializeField] [FieldSortIndex(2)] private BasicAction Action = null;

        private bool done;

        public override string GetHelpMsg()
        {
            return "The object will do the <i>Action</i> when number <i>Compare</i> to <i>Target</i> is true";
        }

        void Update()
        {
            if (isTrue() && !done)
            {
                if (Action != null)
                {
                    if (Action.UseDefaultActor)
                        Action.Actor = gameObject;
                    if (Action.Actor != null)
                        Action.Execute();
                }

                done = true;
            }
        }

        private bool isTrue()
        {
            return LogicCompareUtil.isNumberCompareTrue(gameObject, Target, Compare);
        }

        public new static string GetName()
        {
            return "If (number)... Do...";
        }

        public new static BehaviourType GetBehaviourType()
        {
            return BehaviourType.Logic;
        }
    }
}