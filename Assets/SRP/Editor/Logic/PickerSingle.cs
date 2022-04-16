using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
namespace LoneTower.SRP {

	public class PickerSingle : PickerBase {

		public PickerSingle(ScenePickerBase t, List<SelectionContainer> list = null) : base(t, list) {

		}


		protected override void StartStroke(SelectionContainer obj) {

		}

		protected override void Stroke(SelectionContainer t) {

		}
		protected override void EndStroke(SelectionContainer t) {
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