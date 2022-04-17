using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
namespace LoneTower.SRP {
	[CustomPropertyDrawer(typeof(SRPAttribute))]
	public class SRPInspectorDrawer : SRPBasePropertyDrawer {
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
				if(CheckType(selectType, typeof(Component)))
					EditorGUI.ObjectField(position, prop, new GUIContent(""));
				else {
					if(picker.logic.selection.Count > 0)
						EditorGUI.LabelField(position, picker.logic.selection[0].ToString());
					else
						EditorGUI.LabelField(position, selectType.Name);
				}
				if(EditorGUI.EndChangeCheck()) {
					Reset();
				}
			} else {
				position.x += 5;
				position.width -= 5;
				EditorGUI.LabelField(position, selectType.Name);
			}
			EditorGUI.EndProperty();
		}
	}
}