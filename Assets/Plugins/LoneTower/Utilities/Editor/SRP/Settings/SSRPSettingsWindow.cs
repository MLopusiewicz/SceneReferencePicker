using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SSRPSettingsWindow : EditorWindow {
	[MenuItem("Window/SRP settings")]
	static void Init() {
		SSRPSettingsWindow window = (SSRPSettingsWindow)EditorWindow.GetWindow(typeof(SSRPSettingsWindow));
		window.Show();
	}

	void OnGUI() {
		SRPSettings.choiceColor = EditorGUILayout.ColorField(SRPSettings.choiceColor);
	}
}
