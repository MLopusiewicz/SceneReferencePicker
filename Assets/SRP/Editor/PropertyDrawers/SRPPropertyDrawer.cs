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
			if(CheckType(selectType, typeof(Component))) {
				a.data.scenePicker = new ComponentPicker(selectType);
			}
			return new SRPController(a.data, Deserialize());
		}

		protected override void Serialize() {
			if(isSingle) {
				SerializeSingle();
				Reset();
			} else
				SerializeCollection();
		}
		protected override SelectionContainer[] Deserialize() {
			if(isSingle)
				return DeserializeSingle();
			else
				return DeserializeCollection();
		}

		protected void SerializeSingle() {
			Undo.RecordObject(prop.serializedObject.targetObject, "[SRP]");
			if(picker.logic.selection.Count == 0)
				prop.objectReferenceValue = null;
			else {
				if(picker.logic.selection[picker.logic.selection.Count - 1] == null)
					prop.objectReferenceValue = null;
				else
					picker.logic.selection[picker.logic.selection.Count - 1].Serialize(prop);
			}
			prop.serializedObject.ApplyModifiedProperties();

			picker.Clear();
		}

		protected SelectionContainer[] DeserializeSingle() {
			SelectionContainer a = ComponentContainer.Get(prop);
			if(a == null)
				return new ComponentContainer[0];
			else
				return new[] { a };
		}

		protected void SerializeCollection() {
			SerializedProperty collection = prop.FindPropertyRelative("collection");

			Undo.RecordObject(collection.serializedObject.targetObject, "[SRP]");

			if(EditorApplication.isPlayingOrWillChangePlaymode)
				return;
			collection.arraySize = picker.logic.selection.Count;
			for(int i = 0; i < picker.logic.selection.Count; i++) {
				SerializedProperty x = collection.GetArrayElementAtIndex(i);
				picker.logic.selection[i].Serialize(x);
			}
			collection.serializedObject.ApplyModifiedProperties();

		}
		protected SelectionContainer[] DeserializeCollection() {
			SerializedProperty collection = prop.FindPropertyRelative("collection");
			SelectionContainer[] coll = new SelectionContainer[collection.arraySize];
			for(int i = 0; i < collection.arraySize; i++) {
				SelectionContainer a = ComponentContainer.Get(collection.GetArrayElementAtIndex(i));
				coll[i] = a;
			}
			return coll;
		}
	}



}
