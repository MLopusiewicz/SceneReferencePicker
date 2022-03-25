using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
namespace LoneTower.Utility.SRP {

	public class PickerSingle : PickerBase {

		public PickerSingle(Type t, List<Component> list = null) : base(t, list) {

		}


		protected override void StartStroke(Component obj) {

		}

		protected override void Stroke(Component t) {

		}
		protected override void EndStroke(Component t) {
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