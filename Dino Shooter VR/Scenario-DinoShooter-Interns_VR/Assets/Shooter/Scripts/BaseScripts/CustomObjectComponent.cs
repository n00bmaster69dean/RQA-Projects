using System.Collections.Generic;
using U4K.BehaviourTemplate.Action;
using U4K.Utils;
using UnityEngine;

namespace U4K.BaseScripts
{
    public class CustomObjectComponent : MonoBehaviour
    {
        [HideInInspector] public List<GameObject> followers = new List<GameObject>();
        [HideInInspector] public Object originalPrefab;

        public string originalTag;

        public Vector3 CenterPosition()
        {
            var render = gameObject.GetComponent<Renderer>();
            if (render != null)
                return render.bounds.center;
            var c = gameObject.GetComponent<Collider>();
            if (c != null)
                return c.bounds.center;
            return gameObject.transform.position;
        }

        public Vector3 FrontMostPosition()
        {
            var render = gameObject.GetComponent<Renderer>();
            if (render != null)
            {
                var center = render.bounds.center;
                var extents = render.bounds.extents;
                return center + Vector3.Normalize(gameObject.transform.forward) * extents.z;
            }

            var c = gameObject.GetComponent<Collider>();
            if (c != null)
            {
                var center = c.bounds.center;
                var extents = c.bounds.extents;
                return center + Vector3.Normalize(gameObject.transform.forward) * extents.z;
            }

            return gameObject.transform.position;
        }

        public Vector3 LeftMostPosition()
        {
            var render = gameObject.GetComponent<Renderer>();
            if (render != null)
            {
                var center = render.bounds.center;
                var extents = render.bounds.extents;
                return center - Vector3.Normalize(gameObject.transform.right) * extents.x / 2;
            }

            var c = gameObject.GetComponent<Collider>();
            if (c != null)
            {
                var center = c.bounds.center;
                var extents = c.bounds.extents;
                return center - Vector3.Normalize(gameObject.transform.right) * extents.x / 2;
            }

            return gameObject.transform.position;
        }

        public Vector3 RightMostPosition()
        {
            var render = gameObject.GetComponent<Renderer>();
            if (render != null)
            {
                var center = render.bounds.center;
                var extents = render.bounds.extents;
                return center + Vector3.Normalize(gameObject.transform.right) * extents.x / 2;
            }

            var c = gameObject.GetComponent<Collider>();
            if (c != null)
            {
                var center = c.bounds.center;
                var extents = c.bounds.extents;
                return center + Vector3.Normalize(gameObject.transform.right) * extents.x / 2;
            }

            return gameObject.transform.position;
        }

        public Vector3 FrontMostLeftPosition()
        {
            var render = gameObject.GetComponent<Renderer>();
            if (render != null)
            {
                var center = render.bounds.center;
                var extents = render.bounds.extents;
                return center
                       + Vector3.Normalize(gameObject.transform.forward) * extents.z
                       - Vector3.Normalize(gameObject.transform.right) * extents.x / 2;
            }

            var c = gameObject.GetComponent<Collider>();
            if (c != null)
            {
                var center = c.bounds.center;
                var extents = c.bounds.extents;
                return center
                       + Vector3.Normalize(gameObject.transform.forward) * extents.z
                       - Vector3.Normalize(gameObject.transform.right) * extents.x / 2;
            }

            return gameObject.transform.position;
        }

        public Vector3 FrontMostRigthPosition()
        {
            var render = gameObject.GetComponent<Renderer>();
            if (render != null)
            {
                var center = render.bounds.center;
                var extents = render.bounds.extents;
                return center
                       + Vector3.Normalize(gameObject.transform.forward) * extents.z
                       + Vector3.Normalize(gameObject.transform.right) * extents.x / 2;
            }

            var c = gameObject.GetComponent<Collider>();
            if (c != null)
            {
                var center = c.bounds.center;
                var extents = c.bounds.extents;
                return center
                       + Vector3.Normalize(gameObject.transform.forward) * extents.z
                       + Vector3.Normalize(gameObject.transform.right) * extents.x / 2;
            }

            return gameObject.transform.position;
        }

        private void Start()
        {
            if (string.IsNullOrEmpty(originalTag)) originalTag = "Untagged";
            gameObject.tag = originalTag;
        }

        private void Update()
        {
            for (int i = 0; i < followers.Count; i++)
            {
                var follower = followers[i];
                GameObject target;
                if (i == 0)
                {
                    target = gameObject;
                }
                else
                {
                    target = followers[i - 1];
                }

                var targetComponent = target.GetComponent<CustomObjectComponent>();
                var followerComponent = follower.GetComponent<CustomObjectComponent>();
                if (targetComponent != null && followerComponent != null)
                {
                    follower.transform.LookAt(targetComponent.CenterPosition());
                    float distance = Vector3.Distance(targetComponent.CenterPosition(),
                        followerComponent.CenterPosition());
                    float gap = 1;
                    var actions = CommonUtil.GetBasicActions(follower);
                    foreach (var action in actions)
                    {
                        if (action is FollowAction)
                        {
                            gap = ((FollowAction) action).Gap;
                        }
                    }

                    if (distance > gap)
                    {
                        follower.transform.position += Vector3.Normalize(follower.transform.forward) * (distance - gap);
                    }
                }
            }
        }
    }
}