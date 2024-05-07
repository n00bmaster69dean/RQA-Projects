using U4K.BaseScripts;
using UnityEngine;
using UnityEngine.UI;

namespace U4K.BehaviourTemplate.Action
{
    public class TextShowVariableAction : BasicAction
    {
        [SerializeField]
        private GameObject variable = null;
        
        public override void Execute()
        {
            base.Execute();
            Text text = actor.GetComponent<Text>();
            if (text != null)
            {
                var number = variable.GetComponent<NumberContent>();
                if (number != null)
                {
                    text.text = number.number.ToString();
                }
            }
            _done = true;
        }
        
        public new static string Name
        {
            get { return "Show Variable"; }
        }
    }
}