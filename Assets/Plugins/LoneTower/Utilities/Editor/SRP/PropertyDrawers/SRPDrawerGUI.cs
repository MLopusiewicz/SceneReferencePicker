using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
namespace LoneTower.Utility.SRP {
	[CustomPropertyDrawer(typeof(SRPAttribute))]
	public class SRPDrawerGUI : SRPPropertyDrawer {

		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
			base.OnGUI(position, property, label);
			EditorGUI.BeginProperty(position, label, property);
			if(picker != null) {
				if(isSingle)
					position = picker.InspectorDraw(position, prop.displayName);
				else
					position = picker.InspectorDraw(position, $"{prop.displayName} ({picker.logic.selection.Count})");
			}
			if(isSingle)
				EditorGUI.ObjectField(position, prop, new GUIContent(""));

			EditorGUI.EndProperty();
		}
	}
}