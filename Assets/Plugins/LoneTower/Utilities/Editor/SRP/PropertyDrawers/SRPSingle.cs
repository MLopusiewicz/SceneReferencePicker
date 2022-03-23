using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
namespace LoneTower.Utility.SRP {
	[CustomPropertyDrawer(typeof(SRPAttribute))]
	public class SRPSingle : SRPSinglePropertyDrawer {

		protected override RayPickerController GetPicker() {
			SRPAttribute a = (attribute as SRPAttribute);
			return new RayPickerController(a.data, selectType, Deserialize());
		}

		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
			base.OnGUI(position, property, label);

			EditorGUI.BeginProperty(position, label, property);
			if(picker != null)
				position = picker.InspectorDraw(position, prop.displayName);
			if(isSingle)
				EditorGUI.ObjectField(position, prop, new GUIContent(""));

			EditorGUI.EndProperty();
		}
	}
}