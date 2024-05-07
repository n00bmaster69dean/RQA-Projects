using U4K.BehaviourTemplate.Attr;
using UnityEngine;

namespace U4K.BehaviourTemplate.Action
{
    public class CopyAction : BasicAction
    {
        [SerializeField] private Vector3 position = new Vector3(0,0,0);

        [SerializeField] [ConditionField] [FieldShownAs("position target")]
        public Vector3 target = new Vector3(0, 0, 0);

        public override void Execute()
        {
            base.Execute();
            var go = GameObject.Instantiate(actor, position, Quaternion.identity);
            go.SetActive(true);
            _done = true;
        }

        public override bool Finished(BasicAction conditionAction)
        {
            if (conditionAction is CopyAction)
            {
                var ca = conditionAction as CopyAction;
                if (position.x.Equals(ca.target.x) && position.y.Equals(ca.target.y) && position.z.Equals(ca.target.z))
                    return true;
            }

            return false;
        }

        public new static string Name
        {
            get { return "Copy"; }
        }
    }
}