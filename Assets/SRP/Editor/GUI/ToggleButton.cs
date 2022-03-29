using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace LoneTower.SRP {
	public abstract class ToggleButton {
		public string name;
		public bool state;
		public Vector2Int size = new Vector2Int(30, 30);
		string offIcon, onIcon;
		public Action OnDisable, OnEnable;
		public ToggleButton(string name, string on, string off) {
			this.name = name;
			onIcon = on;
			offIcon = off;
		}

		public void Draw() {
			bool newState = false;
			GUIContent g;
			if(!state)
				g = EditorGUIUtility.IconContent(onIcon);
			else
				g = EditorGUIUtility.IconContent(offIcon);


			if(name == "")
				newState = GUILayout.Toggle(state, g, "Button", GUILayout.Width(size.x), GUILayout.Height(size.y));
			else {
				GUILayout.BeginHorizontal();
				GUILayout.Label(name);
				newState = GUILayout.Toggle(state, g, "Button", GUILayout.Width(size.x), GUILayout.Height(size.y));
				GUILayout.EndHorizontal();
			}


			if(newState != state) {
				state = newState;
				if(state == true)
					OnEnable?.Invoke();
				else
					OnDisable?.Invoke();
			}
		}
		public Rect PropertyDraw(Rect pos) {
			bool newState = false;
			GUIContent g;
			if(!state)
				g = EditorGUIUtility.IconContent(onIcon);
			else
				g = EditorGUIUtility.IconContent(offIcon);
			Rect r = pos;
			r.width = size.x;
			if(name == "") {
				newState = UnityEngine.GUI.Toggle(r, state, g, UnityEngine.GUI.skin.button);
			} else {
				r = EditorGUI.PrefixLabel(r, UnityEngine.GUIUtility.GetControlID(FocusType.Passive), new GUIContent(name));
				newState = UnityEngine.GUI.Toggle(r, state, g, UnityEngine.GUI.skin.button);
			}


			if(newState != state) {
				state = newState;
				if(state == true)
					OnEnable?.Invoke();
				else
					OnDisable?.Invoke();
			}
			pos.x += size.x;
			pos.width -= size.x;
			return pos;
		}

	}

	public class VisibilityButton : ToggleButton {
		public VisibilityButton(string name = "") : base(name, "animationvisibilitytoggleon@2x", "animationvisibilitytoggleoff@2x") {

		}
	}
	public class HierarchyButton : ToggleButton {
		public HierarchyButton(string name = "") : base(name, "d_SceneViewSnap-Off@2x", "d_SceneViewSnap-Off@2x") {

		}
	}

	public class PaintButton : ToggleButton {
		public PaintButton(string name = "") : base(name, "Grid.Default@2x", "Grid.Default@2x") {

		}
	}
}