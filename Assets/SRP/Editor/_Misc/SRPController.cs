using System;
using System.Collections;
using System.Collections.Generic;

namespace LoneTower.SRP {
	public class SRPController : IDisposable {

		public IStroke brush { get { return _brush; } }
		public IDrawer drawer { get { return _drawer; } }

		DrawerBase _drawer;
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
			_drawer = types.GetDrawer(_brush);

			_brush.Disable();
			_drawer.Hide();
		}

		public void VisibilityOn() {
			_drawer.Show();
		}

		public void VisibilityOff() {
			PaintOff();
			_drawer.Hide();
		}

		public void PaintOn() {
			_brush.Enable();
			_drawer.Show();
			_drawer.showMarks = true;
			_drawer.showHandle = true;
		}

		public void PaintOff() {
			_brush.Disable();
			_drawer.showMarks = false;
			_drawer.showHandle = false;
		}

		public void PaintToggle(bool obj) {
			if(obj)
				PaintOn();
			else
				PaintOff();
		}

		public void Dispose() {
			_brush.Disable();
			_drawer.Dispose();
		}

		public State State {
			get { return new State(_drawer.showSelection, _brush.enabled); }
			set {
				_drawer.showSelection = !value.visible;
				if(value.enabled) {
					_brush.Enable();
				} else
					_brush.Disable();
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