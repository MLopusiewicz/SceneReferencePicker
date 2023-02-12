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
			selection.Clear();
			selection.AddRange(obj);
			clicked = selection.ToArray();
			base.StartStroke(clicked);
		}

		protected override void Stroke(object[] t) {
			base.Stroke(clicked);
		}
		protected override void EndStroke(object[] a) {
			base.EndStroke(clicked);
		}

		protected override void SetHover(object[] obj) {
			if(stroking) {
				hover = clicked;
				return;
			}
			base.SetHover(obj);

		}
	}

}