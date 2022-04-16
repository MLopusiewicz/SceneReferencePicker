using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace LoneTower.SRP {
	public class PickerMain : PickerBase {
		bool subtractive;

		public PickerMain(ScenePickerBase t, List<object> list = null) : base(t, list) {

		}


		protected override void StartStroke(object obj) {
			if(obj == null)
				return;
			if(selection.Contains(obj)) {
				subtractive = true;
				selection.Remove(obj);
			} else {
				subtractive = false;
				selection.Add(obj);
			}

		}

		protected override void Stroke(object a) {
			if(a == null)
				return;
			List<object> modifications = new List<object>();

			if(subtractive) {
				selection.Remove(a);
			} else if(!selection.Contains(a))
				selection.Add(a);
		}

	}
}