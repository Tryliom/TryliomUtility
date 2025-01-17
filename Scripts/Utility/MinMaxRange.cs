using System;
using Random = UnityEngine.Random;

#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
#endif

namespace TryliomUtility
{
    public enum MinMaxRangeType
    {
        ZeroTo1000,
        ZeroTo100,
        ZeroTo10,
        ZeroTo360,
        ZeroTo50,
        ZeroTo25
    }

    [Serializable]
    public class MinMaxRange<T> where T : struct, IComparable<T>
    {
        public T MinValue;
        public T MaxValue;
        public float MinRange;
        public float MaxRange;

        public MinMaxRange(T minValue, T maxValue, MinMaxRangeType rangeType = MinMaxRangeType.ZeroTo1000)
        {
            MinValue = minValue;
            MaxValue = maxValue;

            MinRange = 0f;
            MaxRange = rangeType switch
            {
                MinMaxRangeType.ZeroTo1000 => 1000,
                MinMaxRangeType.ZeroTo100 => 100,
                MinMaxRangeType.ZeroTo10 => 10,
                MinMaxRangeType.ZeroTo360 => 360,
                MinMaxRangeType.ZeroTo50 => 50,
                MinMaxRangeType.ZeroTo25 => 25,
                _ => throw new ArgumentOutOfRangeException(nameof(rangeType), rangeType, null)
            };
        }

        public MinMaxRange(T minValue, T maxValue, float minRange, float maxRange)
        {
            MinValue = minValue;
            MaxValue = maxValue;

            MinRange = minRange;
            MaxRange = maxRange;
        }

        public T RandomInRange()
        {
            if (MinValue.CompareTo(MaxValue) == 0)
            {
                return MaxValue;
            }

            if (typeof(T) == typeof(int))
            {
                return (T)(object)Random.Range((int)(object)MinValue, (int)(object)MaxValue);
            }

            if (typeof(T) == typeof(float))
            {
                return (T)(object)Random.Range((float)(object)MinValue, (float)(object)MaxValue);
            }

            throw new InvalidOperationException("Unsupported type");
        }
    }

#if UNITY_EDITOR
    [CustomPropertyDrawer(typeof(MinMaxRange<>))]
    public class MinMaxRangeDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var minValue = property.FindPropertyRelative("MinValue");
            var maxValue = property.FindPropertyRelative("MaxValue");
            var minRange = property.FindPropertyRelative("MinRange").floatValue;
            var maxRange = property.FindPropertyRelative("MaxRange").floatValue;

            EditorGUI.BeginProperty(position, label, property);
            var range = EditorGUI.PrefixLabel(position, label);

            var minFieldRect = new Rect(range.x, range.y, 50, range.height);
            var maxFieldRect = new Rect(range.x + range.width - 50, range.y, 50, range.height);
            var sliderRect = new Rect(range.x + 55, range.y, range.width - 110, range.height);

            if (minValue.propertyType == SerializedPropertyType.Integer &&
                maxValue.propertyType == SerializedPropertyType.Integer)
            {
                var min = minValue.intValue;
                var max = maxValue.intValue;

                min = EditorGUI.IntField(minFieldRect, min);
                max = EditorGUI.IntField(maxFieldRect, max);

                if (min > max)
                {
                    max = min;
                }
                else if (max < min)
                {
                    min = max;
                }

                float minFloat = min;
                float maxFloat = max;

                EditorGUI.MinMaxSlider(sliderRect, ref minFloat, ref maxFloat, minRange, maxRange);

                minValue.intValue = Mathf.Clamp((int)minFloat, (int)minRange, (int)maxFloat);
                maxValue.intValue = Mathf.Clamp((int)maxFloat, (int)minFloat, (int)maxRange);
            }
            else if (minValue.propertyType == SerializedPropertyType.Float &&
                     maxValue.propertyType == SerializedPropertyType.Float)
            {
                var min = minValue.floatValue;
                var max = maxValue.floatValue;

                min = EditorGUI.FloatField(minFieldRect, min);
                max = EditorGUI.FloatField(maxFieldRect, max);

                if (min > max)
                {
                    max = min;
                }
                else if (max < min)
                {
                    min = max;
                }

                EditorGUI.MinMaxSlider(sliderRect, ref min, ref max, minRange, maxRange);

                minValue.floatValue = Mathf.Clamp(min, minRange, max);
                maxValue.floatValue = Mathf.Clamp(max, min, maxRange);

                // Round to 2 decimal places
                minValue.floatValue = (float)Math.Round(minValue.floatValue, 2);
                maxValue.floatValue = (float)Math.Round(maxValue.floatValue, 2);
            }
            else
            {
                EditorGUI.LabelField(position, label.text, "Use MinMaxRange with float or int.");
            }

            EditorGUI.EndProperty();
        }
    }
#endif
}