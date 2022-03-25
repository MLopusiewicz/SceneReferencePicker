using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
namespace LoneTower.Utility.SRP {
	[CustomPropertyDrawer(typeof(SRPAttribute))]
	public class SRPInspectorDrawer : SRPPropertyDrawer {
		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
			base.OnGUI(position, property, label);
			EditorGUI.BeginProperty(position, label, property);
			if(picker != null) {
				if(isSingle)
					position = picker.InspectorDraw(position, prop.displayName);
				else
					position = picker.InspectorDraw(position, $"{prop.displayName} ({picker.logic.selection.Count})");
			}
			if(isSingle) {
				EditorGUI.BeginChangeCheck();
				EditorGUI.ObjectField(position, prop, new GUIContent(""));
				if(EditorGUI.EndChangeCheck()) {
					Reset();
				}
			} else {
				position.x += 5;
				position.width -= 5;
				EditorGUI.LabelField(position, picker.logic.input.t.Name);
			}
			EditorGUI.EndProperty();
		}
	}
}