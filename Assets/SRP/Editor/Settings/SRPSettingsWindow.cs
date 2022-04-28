using UnityEditor;
using UnityEngine;

namespace LoneTower.SRP {
	public class SRPSettingsWindow : EditorWindow {
		[MenuItem("Window/SRP settings")]
		static void Init() {
			SRPSettingsWindow window = (SRPSettingsWindow)EditorWindow.GetWindow<SRPSettingsWindow>("SRP Settings");
			window.Show();
			SRPSettings.Instance.Load();
			window.colors = SRPSettings.colors;
			ScriptableObject target = window;
			SerializedObject so = new SerializedObject(target);
			window.colorCollection = so.FindProperty("colors");

		}

		SerializedProperty colorCollection;
		public Color[] colors;
		bool foldMark = true;
		bool foldDrawers = true;
		Vector2 scrollPos = Vector2.zero;

		void OnGUI() {
			EditorGUI.BeginChangeCheck();
			Color markColor = SRPSettings.MarkColor;
			float scale = SRPSettings.Scale;
			float markScale = SRPSettings.MarkScale;
			float lineScale = SRPSettings.LineScale;

			if(foldMark = EditorGUILayout.Foldout(foldMark, "Mark")) {
				EditorGUI.indentLevel++;
				markColor = EditorGUILayout.ColorField("color", SRPSettings.MarkColor);
				markScale = EditorGUILayout.FloatField("choice Size", SRPSettings.MarkScale);
				EditorGUI.indentLevel--;
			}

			if(foldDrawers = EditorGUILayout.Foldout(foldDrawers, "Drawer")) {
				EditorGUI.indentLevel++;
				scale = EditorGUILayout.FloatField("Scale", SRPSettings.Scale);
				lineScale = EditorGUILayout.FloatField("Line thickness", SRPSettings.LineScale);
				EditorGUI.indentLevel--;
			}
			;
			using(var scrollView = new EditorGUILayout.ScrollViewScope(scrollPos)) {
				scrollPos = scrollView.scrollPosition;
				EditorGUILayout.PropertyField(colorCollection, true);
			}


			if(EditorGUI.EndChangeCheck()) {
				SRPSettings.colors = colors;
				SRPSettings.MarkColor = markColor;
				SRPSettings.Scale = scale;
				SRPSettings.MarkScale = markScale;
				SRPSettings.LineScale = lineScale;

				SRPSettings.Instance.Save();
				DrawerBase.ColorBank = new SelectionBank<Color>(SRPSettings.colors);
			}

		}
	}
}