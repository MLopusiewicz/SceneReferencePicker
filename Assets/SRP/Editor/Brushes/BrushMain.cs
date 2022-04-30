using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace LoneTower.SRP {
	public class BrushMain : BrushBase {
		bool subtractive;

		public List<object> lastModified;
		public BrushMain(ScenePickerBase t, List<object> list = null) : base(t, list) {

		}


		protected override void StartStroke(object[] t) {
			lastModified = new List<object>();
			if(mode == brushMode.shift) {
				subtractive = true;
			} else
				subtractive = false;

			if(t == null)
				return;

			if(subtractive) {
				foreach(var a in t) {
					if(selection.Remove(a)) {
						lastModified.Add(a);
					}
				}
			} else
				foreach(var a in t) {
					if(!selection.Contains(a)) {
						selection.Add(a);
						lastModified.Add(a);
					}
				}

		}

		protected override void Stroke(object[] a) {
			if(a == null)
				return;
			if(subtractive) {
				foreach(var t in a) {
					if(selection.Remove(a)) {
						lastModified.Add(a);
					}
				}
			} else
				foreach(var t in a) {
					if(!selection.Contains(t)) {
						selection.Add(t);
						lastModified.Add(a);
					}
				}
		}
	}
}