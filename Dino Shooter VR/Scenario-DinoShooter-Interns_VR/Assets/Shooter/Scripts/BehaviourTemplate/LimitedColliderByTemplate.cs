using System;
using U4K.BehaviourTemplate.Action;
using U4K.BehaviourTemplate.Attr;
using UnityEngine;

namespace U4K.BehaviourTemplate
{
    public class LimitedColliderByTemplate : BasicBehaviourTemplate
    {
        public override string GetHelpMsg()
        {
            return "When the object is collided by <i>Collider</i>, "
                   + "its <i>Action</i> will be executed at most <i>Limit times</i> times. "
                   + "After the times expire, another Action <i>Afterwards Action</i> will be executed immediately.";
        }

        [SerializeField] [FieldShownAs("Collider")] [FieldSortIndex(0)]
        private GameObject myCollider = null;

        [SerializeField] [FieldShownAs("Limit times")] [FieldSortIndex(1)]
        private int limitTimes = 1;

        [SerializeField] [FieldSortIndex(2)] private BasicAction Action = null;

        [SerializeField] [FieldShownAs("Afterwards will happen")] [FieldSortIndex(3)]
        private BasicAction afterwardsAction = null;

        private int currentTimes;
        private bool afterwardExecuted;

        private Boolean usingCollision = true;

        private void Start()
        {
            currentTimes = 0;
            afterwardExecuted = false;

            var c = GetComponent<Collider>();
            if (c != null && c.isTrigger)
                usingCollision = false;
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (usingCollision)
            {
                if (currentTimes < limitTimes)
                {
                    if (myCollider != null && myCollider == collision.gameObject)
                    {
                        if (Action != null)
                        {
                            if (Action.UseDefaultActor)
                                Action.Actor = gameObject;
                            if (Action.Actor != null)
                                Action.Execute();
                        }

                        currentTimes++;
                    }
                }

                if (currentTimes == limitTimes) // execute afterward Action
                {
                    if (afterwardsAction != null && !afterwardExecuted)
                    {
                        if (afterwardsAction.UseDefaultActor)
                            afterwardsAction.Actor = gameObject;
                        if (afterwardsAction.Actor != null)
                            afterwardsAction.Execute();
                        afterwardExecuted = true;
                    }
                }
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!usingCollision)
            {
                if (currentTimes < limitTimes)
                {
                    if (myCollider != null && myCollider == other.gameObject)
                    {
                        if (Action != null)
                        {
                            if (Action.UseDefaultActor)
                                Action.Actor = gameObject;
                            if (Action.Actor != null)
                                Action.Execute();
                        }

                        currentTimes++;
                    }
                }

                if (currentTimes == limitTimes) // execute afterward Action
                {
                    if (afterwardsAction != null && !afterwardExecuted)
                    {
                        if (afterwardsAction.UseDefaultActor)
                            afterwardsAction.Actor = gameObject;
                        if (afterwardsAction.Actor != null)
                            afterwardsAction.Execute();
                        afterwardExecuted = true;
                    }
                }
            }
        }

        public new static string GetName()
        {
            return "Collide with Times then Do";
        }

        public new static BehaviourType GetBehaviourType()
        {
            return BehaviourType.Event;
        }
    }
}