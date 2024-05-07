using System;
using U4K.BehaviourTemplate.Action;
using U4K.BehaviourTemplate.Attr;
using UnityEngine;

namespace U4K.BehaviourTemplate
{
    public class CollideAnyTemplate : BasicBehaviourTemplate
    {
        public override string GetHelpMsg()
        {
            return "When the object collides anything, the <i>Action</i> will be executed";
        }

        [SerializeField] [RuntimeActor("Collidee")]
        private BasicAction Action = null;
        
        private Boolean usingCollision = true;
        
        private void Start()
        {
            var c = GetComponent<Collider>();
            if (c != null && c.isTrigger)
                usingCollision = false;
        }
        
        public void OnCollisionEnter(Collision collision)
        {
            if (usingCollision)
            {
                if (collision != null)
                {
                    ExecuteAction(collision.gameObject);
                }
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!usingCollision)
            {
                if (other != null)
                {
                    ExecuteAction(other.gameObject);
                }
            }
        }

        private void ExecuteAction(GameObject actor)
        {
            if (Action != null)
            {
                if (Action.UseDefaultActor)
                    Action.Actor = gameObject;
                if (Action.IsRuntimeActor)
                    Action.Actor = actor;
                if (Action.Actor != null)
                    Action.Execute();
            }
        }
        
        public new static string GetName()
        {
            return "Collide Anything then Do";
        }
        
        public new static BehaviourType GetBehaviourType()
        {
            return BehaviourType.Event;
        }
    }
}