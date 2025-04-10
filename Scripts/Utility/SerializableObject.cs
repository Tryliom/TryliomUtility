using System;
using UnityEngine;

namespace TryliomUtility
{
    /**
     * Don't work if the object has a ScriptableObject inside, in that case, find another way with Interface or Abstract class.
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
                }

                return _value;
            }
            set
            {
                _value = value;
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
        }

        /**
         * Update the serialized data from the object.
         */
        public void UpdateSerializedData()
        {
            Value = _value;
        }
    }
}