using U4K.BehaviourTemplate.Attr;
using UnityEngine;

namespace U4K.BehaviourTemplate.Action
{
    public class ResetRandomPositionAction : BasicAction
    {
        [SerializeField] [FieldShownAs("x axis")] [CustomVector2ShownAs("min", "max")] private Vector2 x = new Vector2(0,0);
        
        [SerializeField] [FieldShownAs("y axis")] [CustomVector2ShownAs("min", "max")] private Vector2 y = new Vector2(0, 0);
        
        [SerializeField] [FieldShownAs("z axis")] [CustomVector2ShownAs("min", "max")] private Vector2 z = new Vector2(0, 0);

        [SerializeField] [ConditionField] [FieldShownAs("position target")] public Vector3 target = new Vector3(0, 0, 0);

        private float rx;
        private float ry;
        private float rz;
        
        public override void Execute()
        {
            base.Execute();
            rx = Random.Range(x.x, x.y);
            ry = Random.Range(y.x, y.y);
            rz = Random.Range(z.x, z.y);
            actor.transform.position = new Vector3(rx, ry, rz);
            _done = true;
        }
        
        public override bool Finished(BasicAction conditionAction)
        {
            if (conditionAction is ResetRandomPositionAction)
            {
                var ca = conditionAction as ResetRandomPositionAction;
                if (rx.Equals(ca.target.x) && ry.Equals(ca.target.y) && rz.Equals(ca.target.z))
                    return true;
            }
            return false;
        }
        
        public new static string Name
        {
            get { return "Reset Random Position"; }
        }
    }
}