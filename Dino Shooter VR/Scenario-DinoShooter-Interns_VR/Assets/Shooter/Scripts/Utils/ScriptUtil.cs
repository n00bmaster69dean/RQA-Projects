#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using U4K.BehaviourTemplate;
using U4K.BehaviourTemplate.Action;
using UnityEditor;
using UnityEngine;

namespace U4K.Utils
{
    public class ScriptUtil
    {
        private static string _controllerPath = "Assets/Controller/";
        private static string _dataPath = "Assets/Data";
        private static string _scriptSubfix = "Controller";
        private static string _dataSubfix = "Data";
        private static string _scriptExtension = ".cs";
        private static string _dataExtension = ".data";

        public static void CreateScript(string gameObjectName)
        {
            if (!Directory.Exists(_controllerPath))
            {
                Directory.CreateDirectory(_controllerPath);
            }

            string className = GetControllerName(gameObjectName);
            string path = _controllerPath + className + _scriptExtension;
            if (File.Exists(path))
            {
                File.Delete(path);
            }
            using (StreamWriter outfile =
                new StreamWriter(path))
            {
                outfile.WriteLine("using UnityEngine;");
                outfile.WriteLine("");
                outfile.WriteLine("public class " + className + " : MonoBehaviour {");
//            outfile.WriteLine("    void Start () {}");
//            outfile.WriteLine("    void Update () {}");
                outfile.WriteLine("}");
            }
            AssetDatabase.Refresh();
        }

        public static string ReadScript(string gameObjectName)
        {
            if (!Directory.Exists(_controllerPath))
            {
                return "";
            }
            string className = GetControllerName(gameObjectName);
            string path = _controllerPath + className + _scriptExtension;
            if (!File.Exists(path))
            {
                return "";
            }
            string text = File.ReadAllText(path);
            return text;
        }

        public static void WriteScript(string gameObjectName, string text)
        {
            if (!Directory.Exists(_controllerPath))
            {
                Directory.CreateDirectory(_controllerPath);
            }
            string className = GetControllerName(gameObjectName);
            string path = _controllerPath + className + _scriptExtension;
            if (File.Exists(path))
            {
                File.Delete(path);
            }
            using (StreamWriter outfile =
                new StreamWriter(path, false))
            {
                outfile.Write(text);
            }
            AssetDatabase.Refresh();
        }

        public static void WriteScript(string gameObjectName)
        {
            if (!Directory.Exists(_controllerPath))
            {
                Directory.CreateDirectory(_controllerPath);
            }
            string dataName = GetDataName(gameObjectName);
            string path = _dataPath + "/" + dataName + _dataExtension;
            if (!File.Exists(path))
            {
                Debug.LogError("Object: " + gameObjectName + " does not exist.");
                return;
            }

            string className = GetControllerName(gameObjectName);
            path = _controllerPath + className + _scriptExtension;
            if (File.Exists(path))
            {
                File.Delete(path);
            }
            using (StreamWriter outfile =
                new StreamWriter(path))
            {
                outfile.WriteLine(GenerateScriptPlainCode(gameObjectName));
            }
            AssetDatabase.Refresh();
        }

        public static void RemoveScript(string gameObjectName)
        {
            string className = GetControllerName(gameObjectName);
            string path = _controllerPath + className + _scriptExtension;
            if (File.Exists(path))
            {
                File.Delete(path);
            }
            string dataName = GetDataName(gameObjectName);
            path = _dataPath + "/" + dataName + _dataExtension;
            if (File.Exists(path))
            {
                File.Delete(path);
            }
            AssetDatabase.Refresh();
        }

        public static string GenerateScriptPlainCode(string gameObjectName)
        {
            HashSet<string> onImport = new HashSet<string>();
            string onStart = "";
            string onUpdate = "";
            string onFixedUpdate = "";
            string onTriggerEnter = "";
            string onCollisionEnter = "";

            string className = GetControllerName(gameObjectName);
            string code = "using UnityEngine;\n";
            foreach (var import in onImport)
            {
                code += import;
            }
            code += "\n";
            code += "public class " + className + " : MonoBehaviour {\n";
            code += "\n";

            if (onStart.Length > 0)
            {
                code += "    void Start () {\n";
                code += onStart;
                code += "    }\n\n";
            }

            if (onUpdate.Length > 0)
            {
                code += "    void Update () {\n";
                code += onUpdate;
                code += "    }\n\n";
            }

            if (onFixedUpdate.Length > 0)
            {
                code += "    void FixedUpdate () {\n";
                code += onFixedUpdate;
                code += "    }\n\n";
            }

            if (onTriggerEnter.Length > 0)
            {
                code += "    void OnTriggerEnter (Collider other) {\n";
                code += onTriggerEnter;
                code += "    }\n\n";
            }

            if (onCollisionEnter.Length > 0)
            {
                code += "    void OnCollisionEnter (Collision collision) {\n";
                code += onCollisionEnter;
                code += "    }\n\n";
            }

            code += "}\n";
            return code;
        }

        public static string GetControllerName(string objectName)
        {
            return objectName.Replace(" ", "") + _scriptSubfix;
        }

        public static string GetDataName(string objectName)
        {
            return objectName.Replace(" ", "") + _dataSubfix;
        }

        public static bool ExistController(string gameObjectName)
        {
            if (!Directory.Exists(_controllerPath))
            {
                return false;
            }
            string className = GetControllerName(gameObjectName);
            string path = _controllerPath + className + _scriptExtension;
            if (File.Exists(path))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static Type[] GetBehaviourTemplates()
        {
            var subclassTypes = Assembly
                .GetAssembly(typeof(BasicBehaviourTemplate))
                .GetTypes()
                .Where(t => t.IsSubclassOf(typeof(BasicBehaviourTemplate))).ToArray();
            return subclassTypes;
        }

        public static Type[] GetActions()
        {
            var subclassTypes = Assembly
                .GetAssembly(typeof(BasicAction))
                .GetTypes()
                .Where(t => t.IsSubclassOf(typeof(BasicAction))).ToArray();
            return subclassTypes;
        }

        public static string GetTemplateName(Type t)
        {
            string templateName;
            var methodInfo = t.GetMethod("GetName");
            if (methodInfo != null)
            {
                var getName = methodInfo.Invoke(null, null);
                if (getName == null)
                {
                    templateName = t.Name;
                }
                else
                {
                    templateName = getName.ToString();
                    if (String.IsNullOrEmpty(templateName))
                    {
                        templateName = t.Name;
                    }
                }
            }
            else
            {
                templateName = t.Name;
            }
            return templateName;
        }
    }
}
#endif