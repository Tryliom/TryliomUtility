using System;
using UnityEngine;
using UnityEditor;
using System.Linq;
using System.Collections.Generic;

namespace TryliomUtility
{
    [Serializable]
    public class SerializableObject
    {
        [SerializeField] private string _serializedData;
        [SerializeField] private string _typeName;
        [SerializeReference] private object _value;
        [SerializeField] private List<string> _scriptableObjectGUIDs = new List<string>();

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
                    _scriptableObjectGUIDs.Clear();
                    ApplyGUIDsRecursively(value, new HashSet<object>());
                }
                _serializedData = JsonUtility.ToJson(value);
                _typeName = value?.GetType().AssemblyQualifiedName;
            }
        }

        public Type ObjectType
        {
            get => Type.GetType(_typeName);
            set => _typeName = value.AssemblyQualifiedName;
        }

        public SerializableObject() {}

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
            LoadScriptableObjects();
        }

        /**
         * Update the serialized data from the object.
         */
        public void UpdateSerializedData()
        {
            Value = _value;
        }
        
        private void ApplyGUIDsRecursively(object obj, HashSet<object> visited)
        {
            if (obj == null || visited.Contains(obj)) return;
            visited.Add(obj);

            var fields = obj.GetType().GetFields();
            foreach (var field in fields)
            {
                var fieldValue = field.GetValue(obj);
                if (fieldValue is ScriptableObject so && so != null)
                {
                    var path = AssetDatabase.GetAssetPath(so);
                    var guid = AssetDatabase.AssetPathToGUID(path);
                    _scriptableObjectGUIDs.Add(guid);
                }
                else if (fieldValue != null && fieldValue.GetType().IsClass && !field.FieldType.IsPrimitive && !field.FieldType.IsValueType)
                {
                    ApplyGUIDsRecursively(fieldValue, visited);
                }
            }
        }

        private void LoadScriptableObjects()
        {
            if (_value != null)
            {
                var guids = new List<string>(_scriptableObjectGUIDs);
                guids.Reverse();
                LoadScriptableObjectsRecursively(_value, new Stack<string>(guids), new HashSet<object>());
            }
        }

        private void LoadScriptableObjectsRecursively(object obj, Stack<string> guids, HashSet<object> visited)
        {
            if (obj == null || guids.Count == 0 || visited.Contains(obj)) return;
            visited.Add(obj);

            var objectType = obj.GetType();
            if (!objectType.IsClass || objectType.IsPrimitive || objectType.IsValueType) return;

            var fields = obj.GetType().GetFields();
            foreach (var field in fields)
            {
                if (guids.Count == 0) return;

                var fieldValue = field.GetValue(obj);
                if (fieldValue == null && typeof(ScriptableObject).IsAssignableFrom(field.FieldType))
                {
                    var guid = guids.Pop();
                    var path = AssetDatabase.GUIDToAssetPath(guid);
                    if (!string.IsNullOrEmpty(path))
                    {
                        var loadedObject = AssetDatabase.LoadAssetAtPath<ScriptableObject>(path);
                        field.SetValue(obj, loadedObject);
                        LoadScriptableObjectsRecursively(loadedObject, guids, visited);
                    }
                    else
                    {
                        Debug.LogError($"Failed to load ScriptableObject with GUID: {guid}");
                    }
                }
                else if (fieldValue != null && fieldValue.GetType().IsClass && !field.FieldType.IsPrimitive && !field.FieldType.IsValueType)
                {
                    LoadScriptableObjectsRecursively(fieldValue, guids, visited);
                }
            }
        }
    }
}