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
            var conditional = (ConditionalFieldAttribute)attribute;
            var enabled = GetConditionValue(property, conditional.ConditionFieldName, conditional.Inverse);
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
            var enabled = GetConditionValue(property, conditional.ConditionFieldName, conditional.Inverse);

            return enabled ? EditorGUI.GetPropertyHeight(property, label) : 0f;
        }

        public static bool GetConditionValue(SerializedProperty property, string conditionFieldName, bool inverse)
        {
            var conditionValue = false;
            var conditionProperty = property.serializedObject.FindProperty(conditionFieldName);

            if (conditionProperty != null)
            {
                if (conditionProperty.propertyType == SerializedPropertyType.Boolean)
                {
                    conditionValue = conditionProperty.boolValue;
                }
                else if (conditionProperty.propertyType == SerializedPropertyType.ObjectReference)
                {
                    conditionValue = conditionProperty.objectReferenceValue != null;
                }
            }
            else
            {
                var targetObject = property.serializedObject.targetObject;
                var conditionField = targetObject.GetType().GetField(conditionFieldName,
                    BindingFlags.NonPublic | BindingFlags.Public |
                    BindingFlags.Instance);

                if (conditionField != null && conditionField.FieldType == typeof(bool))
                {
                    conditionValue = (bool)conditionField.GetValue(targetObject);
                }
                else
                {
                    var conditionMethod = targetObject.GetType().GetMethod(conditionFieldName,
                        BindingFlags.NonPublic | BindingFlags.Public |
                        BindingFlags.Instance);

                    if (conditionMethod != null && conditionMethod.ReturnType == typeof(bool))
                    {
                        conditionValue = (bool)conditionMethod.Invoke(targetObject, null);
                    }
                    else
                    {
                        if (conditionMethod == null)
                        {
                            Logger.LogError("Condition method named [" + conditionFieldName + "] is null");
                        }
                        else if (conditionMethod.ReturnType != typeof(bool))
                        {
                            Logger.LogError("Condition method named [" + conditionFieldName +
                                            "] doesn't return a bool");
                        }
                    }
                }
            }

            return inverse ? !conditionValue : conditionValue;
        }
    }
#endif
}