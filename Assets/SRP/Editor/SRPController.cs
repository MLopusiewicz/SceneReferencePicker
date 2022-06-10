using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace LoneTower.SRP {
	public class SRPController : IDisposable {

		public IStroke brush { get { return _brush; } }

		public DrawerBase drawer;
		BrushBase _brush;

		public object[] GetSelection() {
			return _brush.selection.ToArray();
		}
		public void SetSelection(List<object> o) {
			_brush.selection = o;
		}
		public int SelectionCount { get { return _brush.selection.Count; } }

		public SRPController(SRPAttribute attr, object[] arr = null) {
			SRPFactory types = new SRPFactory(attr);
			List<object> c = new List<object>();
			if(arr != null)
				c.AddRange(arr);

			_brush = types.GetBrush(c);
			drawer = types.GetDrawer(_brush);

			_brush.Disable();
			drawer.Hide();
		}

		public void VisibilityOn() {
			drawer.Show();
		}

		public void VisibilityOff() {
			PaintOff();
			drawer.Hide();
		}

		public void PaintOn() {
			_brush.Enable();
			drawer.Show();
			drawer.showMarks = true;
			drawer.showHandle = true;
		}

		public void PaintOff() {
			_brush.Disable();
			drawer.showMarks = false;
			drawer.showHandle = false;
		}

		public void PaintToggle(bool obj) {
			if(obj)
				PaintOn();
			else
				PaintOff();
		}

		public void Dispose() {
			_brush.Disable();
			drawer.Dispose();
		}

		public State State {
			get { return new State(drawer.showSelection, _brush.enabled); }
			set {
				drawer.showSelection = !value.visible;
				if(value.enabled) {
					_brush.Enable();
				} else
					_brush.Disable();
			}
		}

	}


	public class SRPGUIDrawer {
		SRPController srp;
		public SRPGUIDrawer(SRPController srp) {
			this.srp = srp;

			visibility = new VisibilityButton();
			paintButton = new PaintButton();

			visibility.OnEnable += srp.VisibilityOn;
			visibility.OnDisable += srp.VisibilityOff;


			paintButton.OnEnable += srp.PaintOn;
			paintButton.OnDisable += srp.PaintOff;

		}
		VisibilityButton visibility;
		PaintButton paintButton; 
		public Rect InspectorDraw(Rect position, string label) {
			visibility.state = !srp.State.visible;
			paintButton.state = srp.State.enabled;
			position = EditorGUI.PrefixLabel(position, UnityEngine.GUIUtility.GetControlID(FocusType.Passive), new GUIContent(label));

			GUITools.ColorField(position, srp.drawer.color);
			position = paintButton.PropertyDraw(position);
			position = visibility.PropertyDraw(position);

			return position;
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