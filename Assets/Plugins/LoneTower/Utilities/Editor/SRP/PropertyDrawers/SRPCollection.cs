 
using System;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace LoneTower.Utility.SRP {
	[CustomPropertyDrawer(typeof(ListDrawer<>), true)]
	[CustomPropertyDrawer(typeof(ArrayDrawer<>), true)]
	class SRPCollection : SRPCollectionPropertyDrawer {

		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
			base.OnGUI(position, property, label);

			EditorGUI.BeginProperty(position, label, property);
			if(picker != null)
				position = picker.InspectorDraw(position, $"{prop.displayName} ({picker.logic.selection.Count})");
			EditorGUI.EndProperty();

		}

	}


}