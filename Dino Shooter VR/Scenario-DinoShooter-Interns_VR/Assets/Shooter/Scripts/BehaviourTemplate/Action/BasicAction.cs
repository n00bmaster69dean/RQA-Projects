using U4K.BehaviourTemplate.Attr;
using U4K.Utils;
using UnityEngine;

namespace U4K.BehaviourTemplate.Action
{
    public class BasicAction : MonoBehaviour
    {
        protected bool _done = false;
        
        [SerializeField]
        [FieldShownAs("Use default actor (self)")]
        protected bool useDefaultActor = true;

        [SerializeField]
        [FieldShownAs("Actor")]
        protected GameObject actor;

        [SerializeField]
        [SettingIgnore]
        protected bool isRuntimeActor = false;

        public virtual void Execute()
        {
            if (isRuntimeActor)
                actor = CommonUtil.GetExistedParentGameObject(actor);
        }
        
        public virtual bool Finished()
        {
            return _done;
        }

        public virtual bool Finished(BasicAction conditionAction)
        {
            return _done;
        }

        public GameObject Actor
        {
            get { return actor; }
            set { actor = value; }
        }

        public bool IsRuntimeActor
        {
            get { return isRuntimeActor; }
            set { isRuntimeActor = value; }
        }

        public bool UseDefaultActor
        {
            get { return useDefaultActor; }
            set { useDefaultActor = value; }
        }

        public static string Name
        {
            get { return "Basic Action"; }
        }
    }
}