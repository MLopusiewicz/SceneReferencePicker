using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace LoneTower.SRP {
	public abstract class SRPPropertyDrawer : SRPBasePropertyDrawer {
		protected override void Awake() {
			base.Awake();
			picker.drawer.Show();

		}
		protected override SRPController GetPicker() {
			SRPAttribute a = (attribute as SRPAttribute);
			return new SRPController(a.data, selectType, Deserialize());
		}
		protected override void Serialize() {
			if(isSingle) {
				SerializeSingle();
				Reset();
			} else
				SerializeCollection();
		}
		protected override Component[] Deserialize() {
			if(isSingle)
				return DeserializeSingle();
			else
				return DeserializeCollection();
		}

		protected void SerializeSingle() {
			Undo.RecordObject(prop.serializedObject.targetObject, "[RayPicker]");
			if(picker.logic.selection.Count == 0)
				prop.objectReferenceValue = null;
			else
				prop.objectReferenceValue = picker.logic.selection[picker.logic.selection.Count - 1];
			prop.serializedObject.ApplyModifiedProperties();

			picker.Clear();
		}

		protected Component[] DeserializeSingle() {
			Component a = (Component)prop.objectReferenceValue;
			if(a == null)
				return new Component[0];
			else
				return new[] { a };
		}

		protected void SerializeCollection() {
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
