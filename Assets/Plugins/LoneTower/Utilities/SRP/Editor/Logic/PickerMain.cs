using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace LoneTower.Utility.SRP {
	public class PickerMain : PickerBase {
		bool subtractive;

		public PickerMain(Type t, List<Component> list = null) : base(t, list) {
		}


		protected override void StartStroke(Component obj) {
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

		protected override void Stroke(Component a) {
			if(a == null)
				return;
			List<Component> modifications = new List<Component>();

			if(subtractive) {
				selection.Remove(a);
			} else if(!selection.Contains(a))
				selection.Add(a);
		}

	}
}