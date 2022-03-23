using LoneTower.Utility.View;
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

			visibility.OnEnable += drawer.Hide;
			visibility.OnEnable += logic.Disable;

			visibility.OnDisable += drawer.Show;


			paintButton.OnEnable += logic.Enable;
			paintButton.OnEnable += drawer.Show;

			paintButton.OnDisable += logic.Disable;

			logic.Disable();
			drawer.Hide();

		}

		public Rect InspectorDraw(Rect position, string label) {
			visibility.state = !drawer.showSelection;
			paintButton.state = logic.enabled;
			position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), new GUIContent(label));

			ViewControls.ColorField(position, drawer.color);
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