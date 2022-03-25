using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LoneTower.Utility.SRP {
	[System.Serializable]
	public class ListDrawer<T> {
		[SerializeField]
		List<T> collection;

		public ListDrawer() {
			collection = new List<T>();
		}

		public ListDrawer(List<T> a) {
			collection = a;
		}

		public T this[int i] {
			get { return collection[i]; }
			set { collection[i] = value; }
		}

		public static implicit operator List<T>(ListDrawer<T> list) {
			return list.collection;
		}

		public static implicit operator ListDrawer<T>(List<T> list) {
			return new ListDrawer<T>(list);
		}



		//------------------- ListControl
		public int Count { get { return collection.Count; } }
		public void RemoveAt(int i) {
			collection.RemoveAt(i);
		}
		public void AddRange(List<T> arr) {
			collection.AddRange(arr);
		}


	}

}