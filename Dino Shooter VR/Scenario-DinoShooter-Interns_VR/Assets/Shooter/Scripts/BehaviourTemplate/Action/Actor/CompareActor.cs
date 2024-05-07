using System;
using UnityEngine;

namespace U4K.BehaviourTemplate.Action.Actor
{
    [Serializable]
    public class CompareActor
    {
        public GameObject SingleTarget;

        public CompareType CompareType;
    }

    public enum CompareType
    {
        Input,
        Object
    }
}