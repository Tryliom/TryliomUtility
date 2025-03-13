// Simple helper class that allows you to serialize System.Type objects.
// Use it however you like, but crediting or even just contacting the author would be appreciated (Always 
// nice to see people using your stuff!)
//
// Written by Bryan Keiren (http://www.bryankeiren.com)

using UnityEngine;

namespace TryliomUtility
{
	[System.Serializable]
	public class SerializableSystemType
	{
		[SerializeField] private string m_Name;

		public string Name
		{
			get { return m_Name; }
		}

		[SerializeField] private string m_AssemblyQualifiedName;

		public string AssemblyQualifiedName
		{
			get { return m_AssemblyQualifiedName; }
		}

		[SerializeField] private string m_AssemblyName;

		public string AssemblyName
		{
			get { return m_AssemblyName; }
		}

		private System.Type m_SystemType;

		public System.Type SystemType
		{
			get
			{
				if (m_SystemType == null)
				{
					GetSystemType();
				}

				return m_SystemType;
			}
		}

		private void GetSystemType()
		{
			m_SystemType = System.Type.GetType(m_AssemblyQualifiedName);
		}

		public SerializableSystemType(System.Type systemType)
		{
			m_SystemType = systemType;
			m_Name = systemType.Name;
			m_AssemblyQualifiedName = systemType.AssemblyQualifiedName;
			m_AssemblyName = systemType.Assembly.FullName;
		}

		public override bool Equals(object obj)
		{
			var temp = obj as SerializableSystemType;
			return (object)temp != null && Equals(temp);
		}

		public bool Equals(SerializableSystemType @object)
		{
			return @object.SystemType == SystemType;
		}

		public static bool operator ==(SerializableSystemType a, SerializableSystemType b)
		{
			// If both are null, or both are the same instance, return true.
			if (ReferenceEquals(a, b))
			{
				return true;
			}

			// If one is null, but not both, return false.
			if ((object)a == null || (object)b == null)
			{
				return false;
			}

			return a.Equals(b);
		}

		public static bool operator !=(SerializableSystemType a, SerializableSystemType b)
		{
			return !(a == b);
		}

		public static bool operator ==(SerializableSystemType a, System.Type b)
		{
			// If one is null, but not both, return false.
			if (((object)a == null) || ((object)b == null))
			{
				return false;
			}

			return a.SystemType == b;
		}

		public static bool operator !=(SerializableSystemType a, System.Type b)
		{
			return !(a == b);
		}

		public static bool operator ==(System.Type a, SerializableSystemType b)
		{
			// If one is null, but not both, return false.
			if (((object)a == null) || ((object)b == null))
			{
				return false;
			}

			return a == b.SystemType;
		}

		public static bool operator !=(System.Type a, SerializableSystemType b)
		{
			return !(a == b);
		}

		public static implicit operator SerializableSystemType(System.Type systemType)
		{
			return new SerializableSystemType(systemType);
		}

		public static implicit operator System.Type(SerializableSystemType serializableSystemType)
		{
			return serializableSystemType.SystemType;
		}
	}
}