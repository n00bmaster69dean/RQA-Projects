using System;
using U4K.BehaviourTemplate.Action;
using U4K.BehaviourTemplate.Action.Actor;
using U4K.BehaviourTemplate.Attr;
using U4K.Utils;
using UnityEngine;

namespace U4K.BehaviourTemplate
{
    public class CollideTemplate : BasicBehaviourTemplate
    {
        public override string GetHelpMsg()
        {
            return "When the object collides <i>Collidee</i>, the <i>Action</i> will be executed";
        }
        
        [SerializeField] [FieldSortIndex(0)]
        private BasicActor Collidee = null;

        [SerializeField] [RuntimeActor("Collidee")] [FieldSortIndex(1)]
        private BasicAction Action = null;

        private Boolean usingCollision = true;

        private void Start()
        {
            var collider = GetComponent<Collider>();
            if (collider != null && collider.isTrigger)
                usingCollision = false;
        }

        public void OnCollisionEnter(Collision collision)
        {
            if (usingCollision && collision != null)
            {
                var filteredGo = CommonUtil.GetExistedParentGameObject(collision.gameObject);
                if (Collidee.basicActorType == BasicActorType.Single 
                    && filteredGo == Collidee.singleActor)
                {
                    ExecuteAction(filteredGo);
                }
                if (Collidee.basicActorType == BasicActorType.Tagged 
                    && filteredGo.CompareTag(Collidee.tagContent))
                {
                    ExecuteAction(filteredGo);
                }
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!usingCollision)
            {
                var filteredGo = CommonUtil.GetExistedParentGameObject(other.gameObject);
                if (Collidee.basicActorType == BasicActorType.Single 
                    && filteredGo == Collidee.singleActor)
                {
                    ExecuteAction(filteredGo);
                }
                if (Collidee.basicActorType == BasicActorType.Tagged 
                    && filteredGo.CompareTag(Collidee.tagContent))
                {
                    ExecuteAction(filteredGo);
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
            return "Collide Something then Do";
        }
        
        public new static BehaviourType GetBehaviourType()
        {
            return BehaviourType.Event;
        }
    }
}