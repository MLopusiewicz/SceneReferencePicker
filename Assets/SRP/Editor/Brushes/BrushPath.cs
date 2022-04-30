using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace LoneTower.SRP {
	public class BrushPath : BrushBase {

		bool drag;
		int index;

		public BrushPath(ScenePickerBase t, List<object> list = null) : base(t, list) {

		}

		protected override void StartStroke(object[] a) {
			drag = false;

			if(a == null)
				return;

			object t = a[0];

			if(t == null)
				return;

			if(mode == brushMode.shift) {
				selection.Remove(t);
				return;
			}

			if(mode == brushMode.ctrl) {
				if(selection.Contains(t)) {
					selection.Insert(index = selection.IndexOf(t), t);
					drag = true;
					return;
				}
			}
			if(selection.Contains(t)) {
				drag = true;
				index = selection.IndexOf(t);
				return;
			} else
				selection.Add(t);

		}

		protected override void Stroke(object[] a) {
			if(a == null)
				return;

			object t = a[0];

			if(t == null)
				return;

			if(mode == brushMode.shift) {
				selection.Remove(t);
				return;
			}

			if(drag) {
				Insert(t, index);
				return;
			}

			if(!selection.Contains(t))
				selection.Add(t);

		}

		void Insert(object t, int i) {
			selection.Insert(i, t);
			selection.RemoveAt(i + 1);
		}

		protected override void EndStroke(object[] a) {
			if(a == null)
				return;

			object t = a[0];

			if(t == null)
				return;
			if(drag) {
				if(selection.Contains(t)) {
					return;
				} else if(t != null) {
					Insert(t, index);
				}
			}
			if(mode == brushMode.shift)
				selection.Remove(t);

			base.EndStroke(new object[] { t });
		}
	}
}