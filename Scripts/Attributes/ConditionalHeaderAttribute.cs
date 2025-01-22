using System;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace TryliomUtility
{
    [AttributeUsage(AttributeTargets.Field, Inherited = true, AllowMultiple = true)]
    public class ConditionalHeaderAttribute : PropertyAttribute
    {
        public string Header { get; }
        public string ConditionFieldName { get; }
        public bool Inverse { get; }

        public ConditionalHeaderAttribute(string header, string conditionFieldName, bool inverse = false)
        {
            Header = header;
            ConditionFieldName = conditionFieldName;
            Inverse = inverse;
        }
    }

#if UNITY_EDITOR
    [CustomPropertyDrawer(typeof(ConditionalHeaderAttribute))]
    public class ConditionalHeaderDrawer : PropertyDrawer
    {
        private const float MarginTop = 20f;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (property.serializedObject.targetObject is not MonoBehaviour and not ScriptableObject)
            {
                Logger.LogError("ConditionalHeaderAttribute can only be used on MonoBehaviour or ScriptableObject");
                return;
            }
            
            var conditional = (ConditionalHeaderAttribute) attribute;
            var enabled = PropertyConditionUtility.GetBoolean(property, conditional.ConditionFieldName, conditional.Inverse);

            if (!enabled) return;

            // Draw the header
            var headerContent = new GUIContent(conditional.Header);
            var headerPosition = new Rect(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight + MarginTop);
            EditorGUI.LabelField(headerPosition, headerContent, EditorStyles.boldLabel);

            // Adjust position for the property field
            position.y += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing + MarginTop * 0.5f;
            position.height = EditorGUI.GetPropertyHeight(property, label, true);
            EditorGUI.PropertyField(position, property, label, true);
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            var conditional = (ConditionalHeaderAttribute)attribute;
            var enabled = PropertyConditionUtility.GetBoolean(property, conditional.ConditionFieldName, conditional.Inverse);

            if (!enabled) return 0f;

            return EditorGUI.GetPropertyHeight(property, label, true) + EditorGUIUtility.singleLineHeight + MarginTop * 0.5f + EditorGUIUtility.standardVerticalSpacing;
        }
    }
#endif
}