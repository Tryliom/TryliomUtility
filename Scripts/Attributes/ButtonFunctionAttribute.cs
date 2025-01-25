using System;
using System.Reflection;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace TryliomUtility
{
    [AttributeUsage(AttributeTargets.Method)]
    public class ButtonFunctionAttribute : PropertyAttribute
    {}

#if UNITY_EDITOR
    [CustomEditor(typeof(MonoBehaviour), true)]
    public class ButtonFunctionEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            var monoBehaviour = (MonoBehaviour)target;
            var methods = monoBehaviour.GetType().GetMethods(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);

            foreach (var method in methods)
            {
                var attributes = method.GetCustomAttributes(typeof(ButtonFunctionAttribute), true);

                if (attributes.Length <= 0) continue;

                GUILayout.Space(5);

                if (GUILayout.Button(method.Name))
                {
                    method.Invoke(monoBehaviour, null);
                }

                GUILayout.Space(5);
            }
        }
    }
#endif
}