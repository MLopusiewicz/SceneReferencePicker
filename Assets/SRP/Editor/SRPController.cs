using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace LoneTower.SRP {
	public class SRPController : IDisposable {

		public BrushBase brush;
		public DrawerBase drawer;

		VisibilityButton visibility;
		PaintButton paintButton;

		public SRPController(SRPAttribute attr, object[] arr = null) {
			SRPTypeParser types = new SRPTypeParser(attr);
			List<object> c = new List<object>();
			if(arr != null)
				c.AddRange(arr);
			ScenePickerBase pi = (ScenePickerBase)Activator.CreateInstance(types.sceneInput, new object[] { types.selectType });
			brush = (BrushBase)Activator.CreateInstance(types.brush, new object[] { pi, c });
			drawer = (DrawerBase)Activator.CreateInstance(types.drawer);
			ParserBase parser = (ParserBase)Activator.CreateInstance(types.parser, brush);
			drawer.drawTarget = parser;

			visibility = new VisibilityButton();
			paintButton = new PaintButton();

			visibility.OnEnable += VisibilityOn;
			visibility.OnDisable += VisibilityOff;


			paintButton.OnEnable += PaintOn;
			paintButton.OnDisable += PaintOff;

			brush.Disable();
			drawer.Hide();

		}
		void VisibilityOn() {
			drawer.Hide();
			brush.Disable();
			drawer.showMarks = false;
		}

		void VisibilityOff() {
			drawer.Show();
		}

		void PaintOn() {
			brush.Enable();
			drawer.Show();
			drawer.showMarks = true;
			drawer.showHandle = true;
		}

		void PaintOff() {
			brush.Disable();
			drawer.showMarks = false;
			drawer.showHandle = false;
		}

		public Rect InspectorDraw(Rect position, string label) {
			visibility.state = !drawer.showSelection;
			paintButton.state = brush.enabled;
			position = EditorGUI.PrefixLabel(position, UnityEngine.GUIUtility.GetControlID(FocusType.Passive), new GUIContent(label));

			GUITools.ColorField(position, drawer.color);
			position = paintButton.PropertyDraw(position);
			position = visibility.PropertyDraw(position);

			return position;
		}

		public void Dispose() {
			brush.Disable();
			drawer.Dispose();
		}

		public State State {
			get { return new State(visibility.state, brush.enabled); }
			set {
				drawer.showSelection = !value.visible;
				if(value.enabled) {
					brush.Enable();
				} else
					brush.Disable();
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