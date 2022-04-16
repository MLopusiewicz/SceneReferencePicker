using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace LoneTower.SRP {
	public class ComponentSerializer : SerializerBase {

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

		public override object[] Deserialize(Type t, SerializedProperty prop) {
			if(t == typeof(Component[]))
				return DeserializeComponentCollection(prop.FindPropertyRelative("collection"));
			if(t == typeof(Component))
				return DeserializeComponent(prop);
			return null;
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

		static object[] DeserializeComponent(SerializedProperty prop) {
			Component obj = (Component)prop.objectReferenceValue;
			if(obj == null)
				return null;
			else
				return new object[] { obj };
		}

		static object[] DeserializeComponentCollection(SerializedProperty collection) {
			object[] coll = new object[collection.arraySize];
			for(int i = 0; i < collection.arraySize; i++) {
				coll[i] = collection.GetArrayElementAtIndex(i).objectReferenceValue;
			}
			return coll;
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