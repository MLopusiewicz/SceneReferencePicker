using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
namespace LoneTower.SRP {

	public class BrushSingle : BrushBase {

		object[] clicked;
		public BrushSingle(ScenePickerBase t, List<object> list = null) : base(t, list) {

		}


		protected override void StartStroke(object[] obj) {
			base.StartStroke(obj);
			if(obj != null)
				base.Stroke(clicked = obj);
		}

		protected override void Stroke(object[] t) {
			if(clicked != null)
				base.Stroke(clicked);
		}
		protected override void EndStroke(object[] a) {
			if(a == null)
				return;

			object t = a[0];

			if(t == null)
				return;

			if(selection.Contains(t)) {
				selection.Clear();
				Disable();
				base.EndStroke(null);
			} else {
				selection.Clear();
				selection.Add(t);
				Disable();
				base.EndStroke(new object[] { t });
			}
		}
	}

}