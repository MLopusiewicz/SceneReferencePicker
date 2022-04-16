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


		protected override void StartStroke(object[] t) {
			if(mode == brushMode.shift) {
				subtractive = true;
				foreach(var a in t) {
					selection.Remove(a);
				}
			} else
				foreach(var a in t) {
					subtractive = false;
					if(!selection.Contains(a))
						selection.Add(a);
				}

		}

		protected override void Stroke(object[] a) {

			if(subtractive) {
				foreach(var t in a) {
					selection.Remove(t);
				}
			} else
				foreach(var t in a) {
					if(!selection.Contains(t))
						selection.Add(t);
				}
		} 
	}
}