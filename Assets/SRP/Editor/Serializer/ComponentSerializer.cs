using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace LoneTower.SRP {
	public class ComponentSerializer : SerializerBase {
		public ComponentSerializer(Type t) : base(t) { }
		public override void Serialize(object o, SerializedProperty prop) {
			if(o == null) {
				Serialize((Component)null, prop);
				return;
			}

			if(CheckType(o.GetType(), typeof(Component))) {
				Serialize((Component)o, prop);
				return;
			}

		}

		public override void SerializeArray(object[] o, SerializedProperty prop) {
			SerializeCollectionComponent(o, prop);
			return;
		}

		public override object[] Deserialize(SerializedProperty prop) {

			Component obj = (Component)prop.objectReferenceValue;
			if(obj == null)
				return null;
			else
				return new object[] { obj };

		}

		public override object[] DeserializeArray(SerializedProperty prop) {
			List<object> coll = new List<object>();
			//object[] coll = new object[prop.arraySize];
			for(int i = 0; i < prop.arraySize; i++) {
				var g = prop.GetArrayElementAtIndex(i).objectReferenceValue;
				if(g != null)
					coll.Add(g);
				//coll[i] = prop.GetArrayElementAtIndex(i).objectReferenceValue;
			}
			return coll.ToArray();
		}


		static void Serialize(Component c, SerializedProperty prop) {
			Undo.RecordObject(prop.serializedObject.targetObject, "[SRP]");

			prop.objectReferenceValue = c;
			prop.serializedObject.ApplyModifiedProperties();
		}

		static void SerializeCollectionComponent(object[] c, SerializedProperty collection) {
			Undo.RecordObject(collection.serializedObject.targetObject, "[SRP]");

			if(EditorApplication.isPlayingOrWillChangePlaymode)
				return;
			collection.arraySize = c.Length;
			for(int i = 0; i < c.Length; i++) {
				SerializedProperty x = collection.GetArrayElementAtIndex(i);
				x.objectReferenceValue = (c[i] as Component);
			}
			collection.serializedObject.ApplyModifiedProperties();
		}

		static bool CheckType(Type t, Type g) {
			while(t != typeof(object)) {
				if(t == g)
					return true;
				else t = t.BaseType;
			}
			return false;

		}
	}
}