using System.Reflection;

#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace TryliomUtility
{
    public static class PropertyConditionUtility
    {
        /**
         * Used to get the value of a field that is a boolean or a method that returns a boolean.
         */
        public static bool GetBoolean(SerializedProperty property, string conditionFieldName, bool inverse = false)
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
                var conditionField = targetObject.GetType().GetField(conditionFieldName, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);

                if (conditionField != null && conditionField.FieldType == typeof(bool))
                {
                    conditionValue = (bool)conditionField.GetValue(targetObject);
                }
                else if (conditionField != null && conditionField.FieldType == typeof(string))
                {
                    var methodName = (string)conditionField.GetValue(targetObject);
                    var conditionMethod = targetObject.GetType().GetMethod(methodName,
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
                            Logger.LogError("Condition method named [" + methodName + "] is null");
                        }
                        else if (conditionMethod.ReturnType != typeof(bool))
                        {
                            Logger.LogError("Condition method named [" + methodName + "] doesn't return a bool");
                        }
                    }
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
                            Logger.LogError("Field named [" + conditionFieldName + "] is null");
                        }
                        else if (conditionMethod.ReturnType != typeof(bool))
                        {
                            Logger.LogError("Field named [" + conditionFieldName + "] doesn't return a bool");
                        }
                    }
                }
            }

            return inverse ? !conditionValue : conditionValue;
        }
        
        /**
         * Used to get the value of a field that is a string or a method that returns a string.
         */
        public static string GetString(SerializedProperty property, string fieldName)
        {
            var targetObject = property.serializedObject.targetObject;

            var conditionProperty = property.serializedObject.FindProperty(fieldName);
            
            if (conditionProperty != null)
            {
                if (conditionProperty.propertyType == SerializedPropertyType.String)
                {
                    return conditionProperty.stringValue;
                }
            }
            else
            {
                var conditionField = targetObject.GetType().GetField(fieldName, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);

                if (conditionField != null && conditionField.FieldType == typeof(string))
                {
                    return (string) conditionField.GetValue(targetObject);
                }

                var conditionMethod = targetObject.GetType().GetMethod(fieldName, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);

                if (conditionMethod != null && conditionMethod.ReturnType == typeof(string))
                {
                    return (string) conditionMethod.Invoke(targetObject, null);
                }

                if (conditionMethod == null)
                {
                    Logger.LogError("Field named [" + fieldName + "] is null");
                }
                else if (conditionMethod.ReturnType != typeof(string))
                {
                    Logger.LogError("Field named [" + fieldName + "] doesn't return a string");
                }
            }  

            return null;
        }
    }
}
#endif