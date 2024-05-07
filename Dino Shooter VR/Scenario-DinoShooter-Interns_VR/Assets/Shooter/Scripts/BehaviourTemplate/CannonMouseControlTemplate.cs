using System;
using U4K.BehaviourTemplate.Attr;
using UnityEngine;

namespace U4K.BehaviourTemplate
{
    public class CannonMouseControlTemplate : BasicBehaviourTemplate
    {
        public override string GetHelpMsg()
        {
            return "Control the shooting direction using mouse.";
        }

        public enum RotationAxes
        {
            [FieldShownAs("Horizontal and vertical")]
            MouseXAndY = 0,
            [FieldShownAs("Horizontal only")] MouseX = 1,
            [FieldShownAs("Vertical only")] MouseY = 2
        }

        [SerializeField] [FieldSortIndex(0)] public RotationAxes axes = RotationAxes.MouseXAndY;

        [SerializeField] [FieldShownAs("Turn speed")] [CustomVector2ShownAs("horizontal", "vertical")]
        [FieldSortIndex(1)]
        public Vector2 sensitivity = new Vector2(9f, 9f);

        [SerializeField] [FieldShownAs("Vertical angle range")] [CustomVector2ShownAs("min", "max")]
        [FieldSortIndex(2)]
        public Vector2 verticalRange = new Vector2(-90f, 0f);

        [SerializeField] [FieldShownAs("Horizontal angle range")] [CustomVector2ShownAs("min", "max")]
        [FieldSortIndex(3)]
        public Vector2 horizontalRange = new Vector2(0f, 360f);

        private float _rotationX = 0;

        void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        void Update()
        {
            if (axes == RotationAxes.MouseX)
            {
                float rotationY = Input.GetAxis("Mouse X") * sensitivity.x;
                rotationY = ClampAngle(rotationY, horizontalRange.x, horizontalRange.y);
                transform.Rotate(0, rotationY, 0);
            }
            else if (axes == RotationAxes.MouseY)
            {
                _rotationX -= Input.GetAxis("Mouse Y") * sensitivity.y;
                _rotationX = Mathf.Clamp(_rotationX, verticalRange.x, verticalRange.y);

                float rotationY = transform.localEulerAngles.y;
                transform.localEulerAngles = new Vector3(_rotationX, rotationY, 0);
            }
            else
            {
                _rotationX -= Input.GetAxis("Mouse Y") * sensitivity.y;
                _rotationX = Mathf.Clamp(_rotationX, verticalRange.x, verticalRange.y);

                float delta = Input.GetAxis("Mouse X") * sensitivity.x;
                float rotationY = transform.localEulerAngles.y + delta;
                //rotationY = ClampAngle(rotationY, -110, 110);
                transform.localEulerAngles = new Vector3(_rotationX, rotationY, 0);
            }

            if (Input.GetKeyDown("escape"))
            {
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                //Cursor.lockState = CursorLockMode.Confined;
            }
        }

        public new static string GetName()
        {
            return "Cannon Control (Mouse)";
        }

        public new static BehaviourType GetBehaviourType()
        {
            return BehaviourType.Control;
        }

        // todo : fix bug
        private static float ClampAngle(float angle, float min, float max)
        {
            if (max - min >= 360) return angle;
            if (min > max) return ClampAngle(angle, max, min);
            if (min < 0)
            {
                min = Mathf.Repeat(min, 360);
                max = Mathf.Repeat(max, 360);
                angle = Mathf.Repeat(angle, 360);
                if (Mathf.Abs(max - angle) > Mathf.Abs(min - angle))
                {
                    return Mathf.Clamp(angle, min, 360);
                }

                return Mathf.Clamp(angle, 0, max);
            }

            min = Mathf.Repeat(min, 360);
            max = Mathf.Repeat(max, 360);
            return Mathf.Clamp(angle, min, max);
        }

        private static float ClampOld(float angle, float min, float max)
        {
            angle = Mathf.Repeat(angle, 360);
            min = Mathf.Repeat(min, 360);
            max = Mathf.Repeat(max, 360);
            bool inverse = false;
            var tmin = min;
            var tangle = angle;
            if (min > 180)
            {
                inverse = !inverse;
                tmin -= 180;
            }

            if (angle > 180)
            {
                inverse = !inverse;
                tangle -= 180;
            }

            var result = !inverse ? tangle > tmin : tangle < tmin;
            if (!result)
                angle = min;

            inverse = false;
            tangle = angle;
            var tmax = max;
            if (angle > 180)
            {
                inverse = !inverse;
                tangle -= 180;
            }

            if (max > 180)
            {
                inverse = !inverse;
                tmax -= 180;
            }

            result = !inverse ? tangle < tmax : tangle > tmax;
            if (!result)
                angle = max;
            return angle;
        }
    }
}