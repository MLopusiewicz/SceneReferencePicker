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

			base.StartStroke(lastModified.ToArray());
		}

		protected override void Stroke(object[] t) {
			
			List<object> change = new List<object>();
			if(subtractive) {
				foreach(var a in t) {
					if(selection.Remove(a)) {
						change.Add(a);
					}
				}
			} else
				foreach(var a in t) {
					if(!selection.Contains(a)) {
						selection.Add(a);
						change.Add(a);
					}
				}

			lastModified.AddRange(change);
			base.Stroke(change.ToArray());
		}
		protected override void EndStroke(object[] t) {
			base.EndStroke(lastModified.ToArray());
		}
	}
}