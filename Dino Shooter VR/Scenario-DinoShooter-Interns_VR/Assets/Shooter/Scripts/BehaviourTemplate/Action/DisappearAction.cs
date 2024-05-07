using U4K.Utils;
using UnityEngine;

namespace U4K.BehaviourTemplate.Action
{
    public class DisappearAction : BasicAction
    {
        public override void Execute()
        {
            base.Execute();
           // actor.SetActive(false);
            var mainCamera = CommonUtil.GetMainCamera();
            mainCamera.SetActive(true);
            _done = true;
        }
        
        public new static string Name
        {
            get { return "Disappear"; }
        }
    }
}