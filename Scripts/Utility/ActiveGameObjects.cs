using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace TryliomUtility
{
	/**
	 * A class to keep track of active game objects. It provides a method to remove inactive game objects from the list.
	 */
	public class ActiveGameObjects<T1> where T1 : MonoBehaviour
	{
		public List<T1> List = new ();
		
		/**
		 * Remove inactive and destroyed game objects from the list. This should be called regularly (e.g., in Update) to ensure the list only contains active game objects.
		 */
		public void RemoveInactive(Action<T1> onRemove = null)
		{
			if (List.Count == 0) return;
			
			for (var i = List.Count - 1; i >= 0; i--)
			{
				if (List[i] && List[i].gameObject.activeSelf) continue;
				
				onRemove?.Invoke(List[i]);
				List.RemoveAt(i);
			}
		}
		
		// Common list-like members delegated to the internal list
		public bool TryAdd(T1 item)
		{
			if (List.Contains(item)) return false;
			List.Add(item);
			return true;
		}

		public void Add(T1 item) => List.Add(item);

		public bool Remove(T1 item) => List.Remove(item);

		public bool Contains(T1 item) => List.Contains(item);

		public T1 this[int index]
		{
			get => List[index];
			set => List[index] = value;
		}

		public int Count => List.Count;

		public void Clear() => List.Clear();

		public int IndexOf(T1 item) => List.IndexOf(item);

		public void Insert(int index, T1 item) => List.Insert(index, item);

		public IEnumerator<T1> GetEnumerator() => List.GetEnumerator();
		
		public void Shuffle()
		{
			List = List.OrderBy(x => Random.value).ToList();
		}
	}
	
	/**
	 * A class to keep track of active game objects with an associated value. It provides a method to remove inactive game objects from the dictionary.
	 */
	public class ActiveGameObjects<T1, T2> where T1 : MonoBehaviour
	{
		public Dictionary<T1, T2> Dictionary = new ();
		
		 /**
		 * Remove inactive and destroyed game objects from the dictionary. This should be called regularly (e.g., in Update) to ensure the dictionary only contains active game objects.
		 */
		public void RemoveInactive(Action<T1> onRemove = null)
		{
			if (Dictionary.Count == 0) return;
			
			for (var i = 0; i < Dictionary.Count; i++)
			{
				var key = Dictionary.Keys.ElementAt(i);

				if (key && key.gameObject.activeSelf) continue;
		    
				onRemove?.Invoke(key);
				Dictionary.Remove(key);
			}
		}
		
		// Common dictionary-like members delegated to the internal dictionary
		public bool TryAdd(T1 key, T2 value)
		{
			if (Dictionary.ContainsKey(key)) return false;
			Dictionary.Add(key, value);
			return true;
		}

		public void Add(T1 key, T2 value) => Dictionary.Add(key, value);

		public bool Remove(T1 key) => Dictionary.Remove(key);

		public bool ContainsKey(T1 key) => Dictionary.ContainsKey(key);

		public T2 this[T1 key]
		{
			get => Dictionary[key];
			set => Dictionary[key] = value;
		}
		
		public bool TryGetValue(T1 key, out T2 value) => Dictionary.TryGetValue(key, out value);

		public int Count => Dictionary.Count;

		public ICollection<T1> Keys => Dictionary.Keys;
		public ICollection<T2> Values => Dictionary.Values;

		public void Clear() => Dictionary.Clear();

		public IEnumerator<KeyValuePair<T1, T2>> GetEnumerator() => Dictionary.GetEnumerator();
		
		public void Shuffle()
		{
			Dictionary = Dictionary.OrderBy(x => Random.value).ToDictionary(x => x.Key, x => x.Value);
		}
	}
}