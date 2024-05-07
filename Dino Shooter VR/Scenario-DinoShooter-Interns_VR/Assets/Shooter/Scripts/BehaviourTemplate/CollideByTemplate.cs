using System;
using U4K.BehaviourTemplate.Action;
using U4K.BehaviourTemplate.Attr;
using UnityEngine;

namespace U4K.BehaviourTemplate
{
    [Obsolete]
    public class CollideByTemplate : MonoBehaviour
    {
        public string GetHelpMsg()
        {
            return "When the object is collided by <i>Collider</i>, its <i>Action</i> will be executed";
        }
        
        [SerializeField]
        [FieldShownAs("Collider")]
        private GameObject myCollider = null;

        [SerializeField]
        private BasicAction Action = null;
        
        public void OnCollisionEnter(Collision collision)
        {
            if (collision != null && myCollider != null && myCollider.name.Equals(collision.gameObject.name))
                if (Action != null)
                {
                    if (Action.UseDefaultActor)
                        Action.Actor = gameObject;
                    if (Action.Actor != null)
                        Action.Execute();
                }
        }
        
        public static string GetName()
        {
            return "Collide By";
        }
    }
}