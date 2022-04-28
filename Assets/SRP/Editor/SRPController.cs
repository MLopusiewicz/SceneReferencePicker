using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace LoneTower.SRP {
	public class SRPController {

		public BrushBase logic;
		public DrawerBase drawer;

		VisibilityButton visibility;
		PaintButton paintButton; 
	
		public SRPController(SRPTypeParser types, object[] arr = null) {
			List<object> c = new List<object>();
			if(arr != null)
				c.AddRange(arr);
			ScenePickerBase pi = (ScenePickerBase)Activator.CreateInstance(types.sceneInput, new object[] { types.selectType });
			logic = (BrushBase)Activator.CreateInstance(types.logic, new object[] { pi, c });
			drawer = (DrawerBase)Activator.CreateInstance(types.drawer);
			ParserBase parser = (ParserBase)Activator.CreateInstance(types.parser, logic);
			drawer.drawTarget = parser;

			visibility = new VisibilityButton();
			paintButton = new PaintButton();

			visibility.OnEnable += VisibilityOn;
			visibility.OnDisable += VisibilityOff;


			paintButton.OnEnable += PaintOn;
			paintButton.OnDisable += PaintOff;

			logic.Disable();
			drawer.Hide();
		}

		void VisibilityOn() {
			drawer.Hide();
			logic.Disable();
			drawer.showMarks = false;
		}

		void VisibilityOff() {
			drawer.Show();
		}

		void PaintOn() {
			logic.Enable();
			drawer.Show();
			drawer.showMarks = true;
			drawer.showHandle = true;
		}

		void PaintOff() {
			logic.Disable();
			drawer.showMarks = false;
			drawer.showHandle = false;
		}

		public Rect InspectorDraw(Rect position, string label) {
			visibility.state = !drawer.showSelection;
			paintButton.state = logic.enabled;
			position = EditorGUI.PrefixLabel(position, UnityEngine.GUIUtility.GetControlID(FocusType.Passive), new GUIContent(label));

			GUITools.ColorField(position, drawer.color);
			position = paintButton.PropertyDraw(position);
			position = visibility.PropertyDraw(position);

			return position;
		}

		public void Clear() {
			logic.Disable();
			drawer.Clear();
		}

		public State State {
			get { return new State(visibility.state, logic.enabled); }
			set {
				drawer.showSelection = !value.visible;
				logic.Toggle(value.enabled);
			}
		}

	}

	public struct State {
		public bool visible;
		public bool enabled;

		public State(bool visible, bool enabled) {
			this.visible = visible;
			this.enabled = enabled;
		}
	}

}