namespace U4K.BehaviourTemplate.Action
{
    public class AppearAction : BasicAction
    {
        public override void Execute()
        {
            base.Execute();
            actor.SetActive(true);
            _done = true;
        }
        
        public new static string Name
        {
            get { return "Appear"; }
        }
    }
}