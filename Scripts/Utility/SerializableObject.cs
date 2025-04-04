using System;
using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.Linq;

namespace TryliomUtility
{
    [Serializable]
    public class SerializableObject
    {
        [SerializeField] private string _serializedData;
        [SerializeField] private string _typeName;
        [SerializeReference] private object _value;

        public object Value
        {
            get
            {
                if (_value == null && !string.IsNullOrEmpty(_serializedData) && !string.IsNullOrEmpty(_typeName))
                {
                    _value = JsonUtility.FromJson(_serializedData, Type.GetType(_typeName));
                    LoadScriptableObjects();
                }

                return _value;
            }
            set
            {
                _value = value;
                if (value != null)
                {
                    var soFields = value.GetType().GetFields().Where(f => typeof(ScriptableObject).IsAssignableFrom(f.FieldType));
                    foreach (var field in soFields)
                    {
                        var so = field.GetValue(value) as ScriptableObject;
                        if (so != null)
                        {
                            var path = AssetDatabase.GetAssetPath(so);
                            var guid = AssetDatabase.AssetPathToGUID(path);
                            var soGuidField = value.GetType().GetField(field.Name + "GUID");
                            if (soGuidField != null && soGuidField.FieldType == typeof(string))
                            {
                                soGuidField.SetValue(value, guid);
                            }
                        }
                    }
                }
                _serializedData = JsonUtility.ToJson(value);
                _typeName = value.GetType().AssemblyQualifiedName;
            }
        }

        public Type ObjectType
        {
            get => Type.GetType(_typeName);
            set => _typeName = value.AssemblyQualifiedName;
        }

        public SerializableObject()
        {
        }

        public SerializableObject(object value)
        {
            Value = value;
        }

        /**
         * Load the serialized data into the object.
         */
        public void LoadSerializedData()
        {
            _value = JsonUtility.FromJson(_serializedData, Type.GetType(_typeName));
        }

        /**
         * Update the serialized data from the object.
         */
        public void UpdateSerializedData()
        {
            Value = _value;
        }
        
        private void LoadScriptableObjects()
        {
            if (_value != null)
            {
                var soGuidFields = _value.GetType().GetFields().Where(f => f.Name.EndsWith("GUID") && f.FieldType == typeof(string));
                foreach (var guidField in soGuidFields)
                {
                    var guid = guidField.GetValue(_value) as string;
                    if (!string.IsNullOrEmpty(guid))
                    {
                        var path = AssetDatabase.GUIDToAssetPath(guid);
                        var so = AssetDatabase.LoadAssetAtPath<ScriptableObject>(path);
                        var soFieldName = guidField.Name.Replace("GUID", "");
                        var soField = _value.GetType().GetField(soFieldName);
                        if (soField != null && typeof(ScriptableObject).IsAssignableFrom(soField.FieldType))
                        {
                            soField.SetValue(_value, so);
                        }
                    }
                }
            }
        }
    }
}