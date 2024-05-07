using U4K.BaseScripts;
using U4K.BehaviourTemplate.Action;
using U4K.BehaviourTemplate.Action.Actor;
using U4K.BehaviourTemplate.Attr;
using UnityEngine;

namespace U4K.BehaviourTemplate
{
    public class ShootTemplate : BasicBehaviourTemplate
    {
        public override string GetHelpMsg()
        {
            return "When the object shoots the <i>Shooted</i> object, the <i>Action</i> will be executed";
        }

        public float fireRate;   
        public int bulletNumber = 1;
        [SerializeField] [FieldSortIndex(0)] private BasicActor Shooted = null;

        [SerializeField] [RuntimeActor("Shooted")] [FieldSortIndex(1)]
        private BasicAction Action = null;

        void Start()
        {
            var shootBase = gameObject.GetComponent<ShootBase>();
            if (shootBase == null)
            {
                shootBase = gameObject.AddComponent<ShootBase>();
            }

            shootBase.BasicActors.Add(Shooted);
            shootBase.BasicActions.Add(Action);
        }

        public new static BehaviourType GetBehaviourType()
        {
            return BehaviourType.Control;
        }

        public new static string GetName()
        {
            return "Shoot";
        }
    }
}