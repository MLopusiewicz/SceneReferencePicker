using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace LoneTower.Utility.SRP {
	public class SRPSettingsWindow : EditorWindow {
		[MenuItem("Window/SRP settings")]
		static void Init() {
			SRPSettingsWindow window = (SRPSettingsWindow)EditorWindow.GetWindow(typeof(SRPSettingsWindow));
			window.Show();
			window.colors = SRPSettings.colors;
			ScriptableObject target = window;
			SerializedObject so = new SerializedObject(target);
			window.colorCollection = so.FindProperty("colors");

		}
		SerializedProperty colorCollection;
		public Color[] colors;
		void OnGUI() {
			EditorGUI.BeginChangeCheck();
			Color c = EditorGUILayout.ColorField(SRPSettings.choiceColor);

			float scale = EditorGUILayout.FloatField("Scale", SRPSettings.Scale);
			float lineScale = EditorGUILayout.FloatField("Line thickness", SRPSettings.LineScale);
			float choiceScale = EditorGUILayout.FloatField("choice Size", SRPSettings.ChoiceSize);
			EditorGUILayout.PropertyField(colorCollection, true);


			if(EditorGUI.EndChangeCheck()) {
				SRPSettings.colors = colors;
				SRPSettings.choiceColor = c;
				SRPSettings.Scale = scale;
				SRPSettings.ChoiceSize = choiceScale;
				SRPSettings.LineScale = lineScale;

				SRPSettings.Instance.Save();
			}
			EditorGUILayout.LabelField("Changes will be applied on recompile");
			//so.ApplyModifiedProperties(); // Remember to apply modified properties

		}
	}
}