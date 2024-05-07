using U4K.BaseScripts;
using U4K.BehaviourTemplate.Attr;
using UnityEngine;

namespace U4K.BehaviourTemplate.Action
{
    public class DieAction : BasicAction
    {
        [SerializeField] [FieldShownAs("Disappearance delay")]
        private float delay = 3;
        
        public override void Execute()
        {
            if (actor != null)
            {
                var mortal = actor.GetComponent<MortalComponent>();
                if (mortal != null)
                {
                    mortal.Die(delay);
                }
                else
                {
                    actor.SetActive(false);
                }
                _done = true;
            }
        }

        public new static string Name
        {
            get { return "Die"; }
        }
    }
}