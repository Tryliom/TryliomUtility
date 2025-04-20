using System;
using System.Reflection;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace TryliomUtility
{
    [AttributeUsage(AttributeTargets.Method)]
    public class ButtonFunctionAttribute : Attribute
    {}

#if UNITY_EDITOR
    [CustomPropertyDrawer(typeof(ButtonFunctionAttribute), true)]
    public class ButtonFunctionDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var targetObject = property.serializedObject.targetObject;
            var targetType = targetObject.GetType();
            var methods = targetType.GetMethods(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);

            foreach (var method in methods)
            {
                var attributes = method.GetCustomAttributes(typeof(ButtonFunctionAttribute), true);

                if (attributes.Length > 0)
                {
                    if (GUI.Button(new Rect(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight), method.Name))
                    {
                        method.Invoke(targetObject, null);
                    }
                }
            }
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUIUtility.singleLineHeight * 2; // Ajuste la hauteur si nécessaire
        }
    }
#endif
}