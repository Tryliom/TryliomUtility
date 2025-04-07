using System;
using UnityEngine;

namespace TryliomUtility
{
    [Serializable]
    public class Variable<TType> : ScriptableObject
    {
#if UNITY_EDITOR
        [Multiline] public string DeveloperDescription = "";
#endif
        public TType Value;
        
        protected bool DisplayedInInspector = false;
    }
}