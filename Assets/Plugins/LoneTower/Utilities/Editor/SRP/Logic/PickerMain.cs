using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace LoneTower.Utility.SRP {
	public class PickerMain : PickLogic  {
		bool subtractive;

		public PickerMain(LayerMask m, Type t, List<Component> list = null) : base(m, t, list) {
		}


		protected override void StartStroke(Component obj) {
			if(selection.Contains(obj)) {
				subtractive = true;
			} else
				subtractive = false;
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