using System;
using U4K.BehaviourTemplate.Attr;
using UnityEngine;

namespace U4K.BehaviourTemplate.Action.Actor
{
    [Serializable]
    public class BasicActor
    {
        [SerializeField]
        [FieldShownAs("single actor")]
        public GameObject singleActor;

        [SerializeField]
        [FieldShownAs("tag")]
        [TagSelection]
        public String tagContent = "Untagged";

        public BasicActorType basicActorType;

    }

    public enum BasicActorType
    {
        Single = 0,
        Tagged = 1
    }
}