using System;
using U4K.BehaviourTemplate.Attr;
using UnityEngine;

namespace U4K.BehaviourTemplate.Utils
{
    public enum AIRuntimeType
    {
        Trainning,
        Complete
    }
    
    public class AIUtil
    {
    }
    
    [Serializable]
    public class MLAgentBrainUtil
    {
        public AIRuntimeType rType;
            
        public TextAsset internalModel;
    }
}