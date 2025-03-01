using System;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace TryliomUtility
{

    [AttributeUsage(AttributeTargets.Field, Inherited = true, AllowMultiple = false)]
    public class DisplayScriptableObjectAttribute : PropertyAttribute
    {}

#if UNITY_EDITOR
    [CustomPropertyDrawer(typeof(DisplayScriptableObjectAttribute))]
    public class DisplayScriptableObjectDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);

            // Draw the object field for the ScriptableObject
            EditorGUI.PropertyField(position, property, label);

            // If the property is not null, draw its properties
            if (property.objectReferenceValue != null)
            {
                EditorGUI.indentLevel++;
                var serializedObject = new SerializedObject(property.objectReferenceValue);
                var prop = serializedObject.GetIterator();
                
                prop.NextVisible(true);
                
                while (prop.NextVisible(false))
                {
                    EditorGUILayout.PropertyField(prop, true);
                }
                
                serializedObject.ApplyModifiedProperties();
                EditorGUI.indentLevel--;
            }

            EditorGUI.EndProperty();
        }
    }
#endif
}
