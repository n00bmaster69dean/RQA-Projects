namespace U4K.BehaviourTemplate
{
    public class MotionBehaviourTemplate : BasicBehaviourTemplate
    {
        public virtual void ToggleActive()
        {
        }

        public new static BehaviourType GetBehaviourType()
        {
            return BehaviourType.Base;
        }
    }
}