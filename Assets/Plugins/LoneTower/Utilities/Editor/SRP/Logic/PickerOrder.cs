using LoneTower.Utility.Editor;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace LoneTower.Utility.SRP {
	public class PickerOrder : PickLogic {

		bool drag;
		int index;


		public PickerOrder(LayerMask m, Type t, List<Component> list = null) : base(m, t, list) {
		}

		protected override void StartStroke(Component t) {
			drag = false;
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
			}

		}

		protected override void Stroke(Component t) {
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

		void Insert(Component t, int i) {
			selection.Insert(i, t);
			selection.RemoveAt(i + 1);
		}

		protected override void EndStroke(Component t) {

			if(drag) {
				if(selection.Contains(t)) {
					return;
				} else if(t != null) {
					Insert(t, index);
				}
			}
			if(mode == brushMode.shift)
				selection.Remove(t);
			base.EndStroke(t);
		}
	}
}