using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LoneTower.SRP {
	[System.Serializable]
	public class PickableList<T> {
		[SerializeField]
		List<T> collection;

		public PickableList() {
			collection = new List<T>();
		}

		public PickableList(List<T> a) {
			collection = a;
		}

		public T this[int i] {
			get { return collection[i]; }
			set { collection[i] = value; }
		}

		public static implicit operator List<T>(PickableList<T> list) {
			return list.collection;
		}

		public static implicit operator PickableList<T>(List<T> list) {
			return new PickableList<T>(list);
		}



		//------------------- ListControl
		public int Count { get { return collection.Count; } }
		public void RemoveAt(int i) {
			collection.RemoveAt(i);
		}
		public void AddRange(List<T> arr) {
			collection.AddRange(arr);
		}
		public List<T> GetList() {
			return collection;
		}

	}

}