using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LoneTower.SRP {
	[System.Serializable]
	public class PickableArray<T> {

		[SerializeField]
		T[] collection;

		PickableArray(T[] arr) {
			collection = arr;
		}

		public T this[int i] {
			get { return collection[i]; }
			set { collection[i] = value; }
		}

		public static implicit operator T[](PickableArray<T> arr) {
			return arr.collection;
		}

		public static implicit operator PickableArray<T>(T[] arr) {
			return new PickableArray<T>(arr);
		}
	}

}