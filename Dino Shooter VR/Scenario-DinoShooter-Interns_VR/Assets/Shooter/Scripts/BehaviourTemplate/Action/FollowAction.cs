using System;
using U4K.BaseScripts;
using U4K.BehaviourTemplate.Attr;
using UnityEngine;

namespace U4K.BehaviourTemplate.Action
{
    public class FollowAction : BasicAction
    {
        [SerializeField] private GameObject target = null;

        [SerializeField] private float gap = 1;

        public float Gap
        {
            get { return gap; }
        }

        public override void Execute()
        {
            base.Execute();
            if (actor != null && target != null)
            {
                var customObjectComponent = target.GetComponent<CustomObjectComponent>();
                if (customObjectComponent != null)
                {
                    if (!customObjectComponent.followers.Contains(actor))
                    {
                        var followed = target;
                        if (customObjectComponent.followers.Count > 0)
                        {
                            followed = customObjectComponent.followers[customObjectComponent.followers.Count - 1];
                        }

                        customObjectComponent.followers.Add(actor);
                        var followedObjectComponent = followed.GetComponent<CustomObjectComponent>();
                        if (followedObjectComponent != null)
                        {
                            actor.transform.position = followedObjectComponent.CenterPosition() -
                                                       Vector3.Normalize(followed.transform.forward) * gap;
                        }
                    }
                }
            }
        }

        public new static string Name
        {
            get { return "Start Following"; }
        }
    }
}