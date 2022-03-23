using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace LoneTower.Utility.SRP {
	public abstract class SRPSinglePropertyDrawer : SRPBasePropertyDrawer {
		protected override void Awake() {
			base.Awake();
			picker.drawer.Show();

		}
		protected override void Serialize(Component t) {
			if(isSingle)
				SerializeSingle(t);
			else
				SerializeCollection(t);
		}
		protected override Component[] Deserialize() {
			if(isSingle)
				return DeserializeSingle();
			else
				return DeserializeCollection();
		}

		protected void SerializeSingle(Component t) {
			Undo.RecordObject(prop.serializedObject.targetObject, "[RayPicker]");
			prop.objectReferenceValue = t;
			prop.serializedObject.ApplyModifiedProperties();

			picker.Clear();
		}

		protected Component[] DeserializeSingle(
			) {
			Component a = (Component)prop.objectReferenceValue;
			if(a == null)
				return new Component[0];
			else
				return new[] { a };
		}

		protected void SerializeCollection(Component t) {
			SerializedProperty collection = prop.FindPropertyRelative("collection");

			Undo.RecordObject(collection.serializedObject.targetObject, "[RayPicker]");

			if(EditorApplication.isPlayingOrWillChangePlaymode)
				return;
			collection.arraySize = picker.logic.selection.Count;
			for(int i = 0; i < picker.logic.selection.Count; i++) {
				SerializedProperty x = collection.GetArrayElementAtIndex(i);
				x.objectReferenceValue = picker.logic.selection[i];
			}
			collection.serializedObject.ApplyModifiedProperties();

		}
		protected Component[] DeserializeCollection() {
			SerializedProperty collection = prop.FindPropertyRelative("collection");
			Component[] coll = new Component[collection.arraySize];
			for(int i = 0; i < collection.arraySize; i++) {
				Component a = (Component)collection.GetArrayElementAtIndex(i).objectReferenceValue;
				coll[i] = a;
			}
			return coll;
		}
	}



}
