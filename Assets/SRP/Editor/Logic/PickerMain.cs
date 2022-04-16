using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace LoneTower.SRP {
	public class PickerMain : PickerBase {
		bool subtractive;

		public PickerMain(ScenePickerBase t, List<SelectionContainer> list = null) : base(t, list) {

		}


		protected override void StartStroke(SelectionContainer obj) {
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

		protected override void Stroke(SelectionContainer a) {
			if(a == null)
				return;
			List<SelectionContainer> modifications = new List<SelectionContainer>();

			if(subtractive) {
				selection.Remove(a);
			} else if(!selection.Contains(a))
				selection.Add(a);
		}

	}
}