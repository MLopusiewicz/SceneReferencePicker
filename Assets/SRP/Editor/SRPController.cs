using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace LoneTower.Utility.SRP {
	public class SRPController {

		public PickerBase logic;
		public DrawerBase drawer;


		VisibilityButton visibility;
		PaintButton paintButton;

		public SRPController(PickerData data, Type t, Component[] arr = null) {
			List<Component> c = new List<Component>();
			if(arr != null)
				c.AddRange(arr);

			logic = (PickerBase)Activator.CreateInstance(data.logic, new object[] { t, c });
			drawer = (DrawerBase)Activator.CreateInstance(data.drawer);
			ParserBase parser = (ParserBase)Activator.CreateInstance(data.parser, logic);
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
			drawer.showChoices = false;
		}

		void VisibilityOff() {
			drawer.Show();
		}

		void PaintOn() {
			logic.Enable();
			drawer.Show();
			drawer.showChoices = true;
		}

		void PaintOff() {
			logic.Disable();
			drawer.showChoices = false;

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