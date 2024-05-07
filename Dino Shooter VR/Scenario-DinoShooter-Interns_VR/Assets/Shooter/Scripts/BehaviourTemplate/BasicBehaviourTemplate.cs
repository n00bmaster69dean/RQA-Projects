using UnityEngine;

namespace U4K.BehaviourTemplate
{
    public class BasicBehaviourTemplate : MonoBehaviour
    {
        public virtual string GetHelpMsg()
        {
            return "";
        }
        
        public static string GetName()
        {
            return "BasicBehaviour";
        }

        public static BehaviourType GetBehaviourType()
        {
            return BehaviourType.Base;
        }
    }

    public enum BehaviourType
    {
        Base,
        Control,
        Event,
        Logic,
        Motion,
        AI,
        UI
    }
}