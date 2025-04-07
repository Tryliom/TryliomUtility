using System;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace TryliomUtility
{
     [Serializable]
     public class Reference<ConstantType> : IResettable
     {
         public bool UseConstant = false;
         public ConstantType ConstantValue;
         public Variable<ConstantType> Variable;
     
         public Reference() {}
     
         public Reference(ConstantType value)
         {
             UseConstant = true;
             ConstantValue = value;
         }
         
         public Reference(bool useConstant)
         {
             UseConstant = useConstant;
         }
     
         public ConstantType Value
         {
             get => UseConstant ? ConstantValue : Variable.Value;
             set
             {
                 if (UseConstant)
                 {
                     ConstantValue = value;
                 }
                 else
                 {
                     Variable.Value = value;
                 }
             }
         }
     
         public static implicit operator ConstantType(Reference<ConstantType> reference)
         {
             return reference.Value;
         }
         
         public static implicit operator Reference<ConstantType>(Variable<ConstantType> value)
         {
             return new Reference<ConstantType> {Variable = value};
         }
         
         public static implicit operator Reference<ConstantType>(ConstantType value)
         {
             return new Reference<ConstantType>(value);
         }
         
         public Type GetConstantType()
         {
             return typeof(ConstantType);
         }
         
         public void SetVariable(ScriptableObject variable)
         {
             Variable = (Variable<ConstantType>) variable;
             UseConstant = false;
         }
         
         public void SetValue(ConstantType value)
         {
             Value = value;
         }
         
         public ConstantType GetValue()
         {
             return Value;
         }
         
         public void ResetValue()
         {
             Value = default;
         }
     }

#if UNITY_EDITOR
    [CustomPropertyDrawer(typeof(Reference<>), useForChildren: true)]
    public class ReferenceDrawer : PropertyDrawer
    {
        private GUIStyle _buttonStyle;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            _buttonStyle ??= new GUIStyle
            {
                imagePosition = ImagePosition.ImageOnly,
                fixedWidth = 20
            };

            label = EditorGUI.BeginProperty(position, label, property);
            position = EditorGUI.PrefixLabel(position, label);
        
            EditorGUI.BeginChangeCheck();

            // Get properties
            var useConstant = property.FindPropertyRelative("UseConstant");
            var constantValue = property.FindPropertyRelative("ConstantValue");
            var variable = property.FindPropertyRelative("Variable");
            
            // Add button to open inspector if not using constant
            if (!useConstant.boolValue && variable.objectReferenceValue != null)
            {
                var variableObject = variable.objectReferenceValue;
                var displayField = variableObject.GetType().GetField("DisplayedInInspector", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
                
                bool currentValue = (bool) displayField.GetValue(variableObject);
                var rect = new Rect(position);

                rect.yMin += _buttonStyle.margin.top - 3;
                rect.width = _buttonStyle.fixedWidth + _buttonStyle.margin.right - 5;
                
                currentValue = EditorGUI.Foldout(rect, currentValue, "");
                displayField.SetValue(variableObject, currentValue);
                
                if (currentValue)
                {
                    EditorGUI.indentLevel++;
                    var serializedObject = new SerializedObject(variable.objectReferenceValue);
                    var prop = serializedObject.GetIterator();

                    prop.NextVisible(true);

                    while (prop.NextVisible(false))
                    {
                        EditorGUILayout.PropertyField(prop, true);
                    }

                    serializedObject.ApplyModifiedProperties();
                    EditorGUI.indentLevel--;
                }
                
                position.xMin += 15;
            }

            var refreshIcon = new GUIContent(EditorGUIUtility.IconContent("Refresh").image, "Toggle between constant and variable");
            var buttonRect = new Rect(position);

            buttonRect.yMin += _buttonStyle.margin.top;
            buttonRect.width = _buttonStyle.fixedWidth + _buttonStyle.margin.right;
            position.xMin = buttonRect.xMax;

            // Store old indent level and set it to 0, the PrefixLabel takes care of it
            var indent = EditorGUI.indentLevel;
            EditorGUI.indentLevel = 0;

            if (GUI.Button(buttonRect, refreshIcon, _buttonStyle))
            {
                useConstant.boolValue = !useConstant.boolValue;
            }

            EditorGUI.PropertyField(position, useConstant.boolValue ? constantValue : variable, GUIContent.none);

            EditorGUI.indentLevel = indent;
        }
    }
#endif
     
    [Serializable]
    public class FloatReference : Reference<float> {}

    [Serializable]
    public class IntReference : Reference<int> {}

    [Serializable]
    public class BoolReference : Reference<bool> {}

    [Serializable]
    public class StringReference : Reference<string> {}

    [Serializable]
    public class Vector2Reference : Reference<UnityEngine.Vector2> {}
    
    [Serializable]
    public class Vector3Reference : Reference<UnityEngine.Vector3> {}

    [Serializable]
    public class GameObjectReference : Reference<GameObject> {}   

    [Serializable]
    public class TransformReference : Reference<Transform> {}
    
    [Serializable]
    public class ColorReference : Reference<Color> {}
    
    [Serializable]
    public class QuaternionReference : Reference<Quaternion> {}
}