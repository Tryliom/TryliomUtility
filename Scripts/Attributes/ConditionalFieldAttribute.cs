using System;
using System.Reflection;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace TryliomUtility
{
    [AttributeUsage(AttributeTargets.Field, Inherited = true, AllowMultiple = true)]
    public class ConditionalFieldAttribute : PropertyAttribute
    {
        public string ConditionFieldName { get; }
        public bool Inverse { get; }

        public ConditionalFieldAttribute(string conditionFieldName, bool inverse = false)
        {
            ConditionFieldName = conditionFieldName;
            Inverse = inverse;
        }
    }

#if UNITY_EDITOR
    [CustomPropertyDrawer(typeof(ConditionalFieldAttribute))]
    public class ConditionalFieldDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (property.serializedObject.targetObject is not MonoBehaviour and not ScriptableObject)
            {
                Logger.LogError("ConditionalFieldAttribute can only be used on MonoBehaviour or ScriptableObject");
                return;
            }
            
            var conditional = (ConditionalFieldAttribute)attribute;
            var enabled = PropertyConditionUtility.GetBoolean(property, conditional.ConditionFieldName, conditional.Inverse);
            var wasEnabled = GUI.enabled;

            GUI.enabled = enabled;

            if (enabled)
            {
                EditorGUI.PropertyField(position, property, label, true);
            }

            GUI.enabled = wasEnabled;
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            var conditional = (ConditionalFieldAttribute)attribute;
            var enabled = PropertyConditionUtility.GetBoolean(property, conditional.ConditionFieldName, conditional.Inverse);

            return enabled ? EditorGUI.GetPropertyHeight(property, label) : 0f;
        }
    }
#endif
}