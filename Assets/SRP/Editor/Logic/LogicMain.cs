using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace LoneTower.SRP {
	public class LogicMain : LogicBase {
		bool subtractive;

		public LogicMain(ScenePickerBase t, List<object> list = null) : base(t, list) {

		}


		protected override void StartStroke(object[] t) {

			if(mode == brushMode.shift) {
				subtractive = true;
			} else
				subtractive = false;

			if(t == null)
				return;  

			if(subtractive) {
				foreach(var a in t) {
					selection.Remove(a);
				}
			} else
				foreach(var a in t) {
					if(!selection.Contains(a))
						selection.Add(a);
				}

		}

		protected override void Stroke(object[] a) {
			if(a == null)
				return;
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