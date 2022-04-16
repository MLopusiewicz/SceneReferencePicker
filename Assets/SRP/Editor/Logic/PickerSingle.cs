using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
namespace LoneTower.SRP {

	public class PickerSingle : PickerBase {

		public PickerSingle(ScenePickerBase t, List<object> list = null) : base(t, list) {

		}


		protected override void StartStroke(object obj) {

		}

		protected override void Stroke(object t) {

		}
		protected override void EndStroke(object t) {
			if(selection.Contains(t)) {
				selection.Clear();
				Disable();
				base.EndStroke(null);
			} else {
				selection.Clear();
				selection.Add(t);
				Disable();
				base.EndStroke(t);
			}
		}
	}

}