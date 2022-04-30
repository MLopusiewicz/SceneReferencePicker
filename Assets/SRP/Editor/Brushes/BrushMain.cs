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

		protected override void Stroke(object[] t) {
			if(t == null)
				return;
			if(subtractive) {
				foreach(var a in t) {
					if(selection.Remove(t)) {
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
		protected override void EndStroke(object[] t) {
			foreach(var a in lastModified)
				Debug.Log(a.ToString());
			base.EndStroke(lastModified.ToArray());
		}
	}
}