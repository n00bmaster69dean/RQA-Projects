using System.Linq;
using U4K.BehaviourTemplate.Attr;
using UnityEngine;

namespace U4K.BehaviourTemplate
{
    public class CarDemoControlTemplate : BasicBehaviourTemplate
    {
        public override string GetHelpMsg()
        {
            return "Control the car's moving with arrow keys";
        }
        
        public new static string GetName()
        {
            return "Car Control";
        }
        
        [SerializeField]
        [FieldShownAs("Max motor torque")] [FieldSortIndex(0)]
        public float maxMotorTorque = 25;
        
        [SerializeField]
        [FieldShownAs("Max steering angle")] [FieldSortIndex(1)]
        public float maxSteeringAngle = 25;

        private void FixedUpdate()
        {
            float motor = maxMotorTorque * Input.GetAxis("Vertical");
            float steering = maxSteeringAngle * Input.GetAxis("Horizontal");
            bool brake = Input.GetKey(KeyCode.B);
            float brakeTorque = 0;
            if (brake) {
                brakeTorque = maxMotorTorque * 2;
                motor = 0;
            } else {
                brakeTorque = 0;
            }

            for (int i = 0; i < transform.childCount; i++)
            {
                var child = transform.GetChild(i);
                var wheelCollider = child.GetComponent<WheelCollider>();
                if (wheelCollider != null)
                {
                    if (i < 2)
                        wheelCollider.steerAngle = steering;
                    else
                        wheelCollider.motorTorque = motor;
                    wheelCollider.brakeTorque = brakeTorque;

                    if (!brake)
                    {
                        Quaternion rot;
                        Vector3 pos;
                        wheelCollider.GetWorldPose(out pos, out rot);
                        child.transform.rotation = rot;
                    }
                }
            }
        }
        
        public new static BehaviourType GetBehaviourType()
        {
            return BehaviourType.Control;
        }
    }
}