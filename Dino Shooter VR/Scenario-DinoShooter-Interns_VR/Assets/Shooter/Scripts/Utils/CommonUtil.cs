using System;
using System.Collections.Generic;
using U4K.BaseScripts;
using U4K.BehaviourTemplate.Action;
using UnityEngine;
using UnityEngine.UI;

namespace U4K.Utils
{
    public class CommonUtil
    {
        public const string MAIN_CAMERA_TAG = "MainCamera";

        public const string CANVAS = "Canvas";
        public const string SHOWUPTEXT = "ShowUpText";
        public const string LOSE = "You Lose";
        public const string WIN = "You Win";
        public const string AIACADEMY = "AIAcademy";
        
        public static GameObject[] FindObjs()
        {
            List<GameObject> objs = new List<GameObject>();
            var customObjectComponents = Resources.FindObjectsOfTypeAll<CustomObjectComponent>();
            foreach (var customObjectComponent in customObjectComponents)
            {
                objs.Add(customObjectComponent.gameObject);
            }
            return objs.ToArray();
        }

        public static GameObject[] FindNumberContentObjs()
        {
            var all = FindObjs();
            List<GameObject> objs = new List<GameObject>();
            foreach (var obj in all)
            {
                var numberContent = obj.GetComponent<NumberContent>();
                if (numberContent != null)
                {
                    objs.Add(obj);
                }
            }
            return objs.ToArray();
        }
        
        public static GameObject[] GetNormalObjects()
        {
            var objs = CommonUtil.FindObjs();
            List<GameObject> normalObjs = new List<GameObject>();
            foreach (var obj in objs)
            {
                if (obj.GetComponent<Text>() == null && obj.GetComponent<NumberContent>() == null)
                {
                    normalObjs.Add(obj);
                }
            }
            return normalObjs.ToArray();
        }

        public static GameObject[] GetUIObjects()
        {
            var objs = CommonUtil.FindObjs();
            List<GameObject> uiObjs = new List<GameObject>();
            foreach (var obj in objs)
            {
                if (obj.GetComponent<Text>() != null)
                {
                    uiObjs.Add(obj);
                }
            }
            return uiObjs.ToArray();
        }

        public static GameObject[] GetVariableObjects()
        {
            var objs = CommonUtil.FindObjs();
            List<GameObject> uiObjs = new List<GameObject>();
            foreach (var obj in objs)
            {
                if (obj.GetComponent<NumberContent>() != null)
                {
                    uiObjs.Add(obj);
                }
            }
            return uiObjs.ToArray();
        }
        
        public static Dictionary<string, Type> actions = new Dictionary<string, Type>();

        static CommonUtil()
        {
            // iniatilize actions
            actions.Add(DisappearAction.Name, typeof(DisappearAction));
            actions.Add(NumberIncreaseAction.Name, typeof(NumberIncreaseAction));
            actions.Add(RotateOnceAction.Name, typeof(RotateOnceAction));
            actions.Add(NumberSetAction.Name, typeof(NumberSetAction));
            actions.Add(TextShowVariableAction.Name, typeof(TextShowVariableAction));
            actions.Add(AppearAction.Name, typeof(AppearAction));
            actions.Add(ResetPositionAction.Name, typeof(ResetPositionAction));
            actions.Add(ResetRandomPositionAction.Name, typeof(ResetRandomPositionAction));
            actions.Add(CopyAction.Name, typeof(CopyAction));
            actions.Add(CopyRandomAction.Name, typeof(CopyRandomAction));
            actions.Add(FollowAction.Name, typeof(FollowAction));
            actions.Add(ShowUpText.Name, typeof(ShowUpText));
            actions.Add(PlayAudioAction.Name, typeof(PlayAudioAction));
            actions.Add(DieAction.Name, typeof(DieAction));
        }

        // used in monobehavior (runtime)
        public static BasicAction[] GetBasicActions(GameObject gameObject)
        {
            var actions = Resources.FindObjectsOfTypeAll(typeof(BasicAction));
            var ret = new List<BasicAction>();
            foreach (var action in actions)
            {
                var a = action as BasicAction;
                if (a != null && a.Actor == gameObject)
                    ret.Add(a);
            }
            return ret.ToArray();
        }

        public static GameObject GetMainCamera()
        {
            var mainCamera = GameObject.FindWithTag(MAIN_CAMERA_TAG);
            if (mainCamera == null)
            {
                mainCamera = new GameObject("Main Camera");
                mainCamera.tag = MAIN_CAMERA_TAG;
                mainCamera.AddComponent<Camera>();
                mainCamera.transform.position = new Vector3(0, 3, -3);
                mainCamera.transform.rotation = Quaternion.Euler(45, 0, 0);
            }
            return mainCamera;
        }

        public static GameObject GetDirectionalLight()
        {
            var directionalLight = GameObject.Find("Directional Light");
            if (directionalLight == null)
            {
                directionalLight = new GameObject("Directional Light");
                var light = directionalLight.AddComponent<Light>();
                light.type = LightType.Directional;
                light.transform.position = new Vector3(0, 3, 0);
                light.transform.rotation = Quaternion.Euler(50, -30, 0);
            }
            return directionalLight;
        }
        
        public static float GetDistance(Vector3 startPosition, Vector3 direction)
        {
            RaycastHit hit;

            if (Physics.Raycast(startPosition, direction, out hit))
            {
                return hit.distance;
            }
            return float.MaxValue;
        }
        
        public static float Range (float minValue, float maxValue, float value)
        {
            if (value < minValue) return 0f;
            if (value > maxValue) return 1f;
            value = Mathf.Clamp(value, minValue, maxValue);
            return (value - minValue) / (maxValue - minValue);
        }
        
        public static GameObject GetExistedParentGameObject(GameObject obj)
        {
            
            if (obj == null)
                return null;

            if (obj.GetComponent<CustomObjectComponent>() != null)
                return obj;

            if (obj.transform.parent != null)
            {
                return GetExistedParentGameObject(obj.transform.parent.gameObject);
            }
            
            return null;
        }
    }
}