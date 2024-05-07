using System;
using U4K.BehaviourTemplate.Attr;
using U4K.Utils;
using UnityEngine;

namespace U4K.BehaviourTemplate
{
    public class ControllerTemplate : BasicBehaviourTemplate
    {
        public override string GetHelpMsg()
        {
            return "Control the character's moving with arrow keys.";
        }

        [SerializeField] [FieldShownAs("Move speed")] [FieldSortIndex(0)]
        private float moveSpeed = 2;

        [SerializeField] [FieldShownAs("Turn speed")] [FieldSortIndex(1)]
        private float turnSpeed = 200;

        private float interpolation = 10;
        private float currentH;
        private float currentV;

        private void Update()
        {
            TankUpdate(gameObject);
        }
        
        private void TankUpdate(GameObject obj)
        {
            float h = Input.GetAxis("Horizontal");
            float v = Input.GetAxis("Vertical");

            currentH = Mathf.Lerp(currentH, h, Time.deltaTime * interpolation);
            currentV = Mathf.Lerp(currentV, v, Time.deltaTime * interpolation);

            obj.transform.position += obj.transform.forward * moveSpeed * currentV * Time.deltaTime;
            obj.transform.Rotate(0, currentH * turnSpeed * Time.deltaTime, 0);
        }

        public new static string GetName()
        {
            return "Unforced horizontally Move";
        }
        
        public new static BehaviourType GetBehaviourType()
        {
            return BehaviourType.Control;
        }
    }
}