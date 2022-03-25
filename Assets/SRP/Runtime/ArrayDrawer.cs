using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace LoneTower.Utility.SRP {
	[System.Serializable]
	public class ArrayDrawer<T> {

		[SerializeField]
		T[] collection;

		ArrayDrawer(T[] arr) {
			collection = arr;
		}

		public T this[int i] {
			get { return collection[i]; }
			set { collection[i] = value; }
		}

		public static implicit operator T[](ArrayDrawer<T> arr) {
			return arr.collection;
		}

		public static implicit operator ArrayDrawer<T>(T[] arr) {
			return new ArrayDrawer<T>(arr);
		}

		public static SerializedProperty GetCollection(SerializedProperty prop) {
			return prop.FindPropertyRelative("collection");
		} 
	}

}