using System;
using UnityEngine;
using UnityEditor;
using System.Linq;

namespace TryliomUtility
{
    /**
     * If you want to serialize a class that contains a ScriptableObject, you need to make a field with the name "GUID" at the end of the field name.
     * Example:
     *  public class MyClass
     *  {
     *      public ScriptableObject MyScriptableObject;
     *      public string MyScriptableObjectGUID;
     *  }
     *
     *  This class will serialize the ScriptableObject and store its GUID in the MyScriptableObjectGUID field to be able to load it back independently of the machine.
     */
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
                    ApplyGUIDsRecursively(value);
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
            LoadScriptableObjects();
        }

        /**
         * Update the serialized data from the object.
         */
        public void UpdateSerializedData()
        {
            Value = _value;
        }
        
        private void ApplyGUIDsRecursively(object obj)
        {
            var soFields = obj.GetType().GetFields().Where(f => typeof(ScriptableObject).IsAssignableFrom(f.FieldType));
            foreach (var field in soFields)
            {
                var so = field.GetValue(obj) as ScriptableObject;
                if (so != null)
                {
                    var path = AssetDatabase.GetAssetPath(so);
                    var guid = AssetDatabase.AssetPathToGUID(path);
                    var soGuidField = obj.GetType().GetField(field.Name + "GUID");
                    if (soGuidField != null && soGuidField.FieldType == typeof(string))
                    {
                        soGuidField.SetValue(obj, guid);
                    }
                    ApplyGUIDsRecursively(so);
                }
            }
        }
        
        private void LoadScriptableObjects()
        {
            if (_value != null)
            {
                LoadScriptableObjectsRecursively(_value);
            }
        }

        private void LoadScriptableObjectsRecursively(object obj)
        {
            var soGuidFields = obj.GetType().GetFields().Where(f => f.Name.EndsWith("GUID") && f.FieldType == typeof(string));
            foreach (var guidField in soGuidFields)
            {
                var guid = guidField.GetValue(obj) as string;
                if (!string.IsNullOrEmpty(guid))
                {
                    var path = AssetDatabase.GUIDToAssetPath(guid);
                    var so = AssetDatabase.LoadAssetAtPath<ScriptableObject>(path);
                    var soFieldName = guidField.Name.Replace("GUID", "");
                    var soField = obj.GetType().GetField(soFieldName);
                    if (soField != null && typeof(ScriptableObject).IsAssignableFrom(soField.FieldType))
                    {
                        soField.SetValue(obj, so);
                        LoadScriptableObjectsRecursively(so);
                    }
                }
            }
        }
    }
}