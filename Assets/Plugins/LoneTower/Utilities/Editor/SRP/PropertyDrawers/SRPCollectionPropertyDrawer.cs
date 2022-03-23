
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace LoneTower.Utility.SRP {
	public abstract class SRPCollectionPropertyDrawer : SRPBasePropertyDrawer {

		protected override void Serialize(Component t) {
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
		protected override RayPickerController GetPicker() {
			BrushAttribute g = GetAttribute<BrushAttribute>(prop);
			return new RayPickerController(g.data, fieldInfo.FieldType.GenericTypeArguments[0], Deserialize());
		}
		protected override Component[] Deserialize() {
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