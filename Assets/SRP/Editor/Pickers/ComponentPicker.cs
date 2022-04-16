using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace LoneTower.SRP {
	public class ComponentPicker : ScenePickerBase {
		public Type t { get; private set; }
		public ComponentPicker(Type t) {
			this.t = t;
			Transform[] g = GameObject.FindObjectsOfType(t).Select(x => ((Component)x).transform).ToArray();
			List<SelectionContainer> s = new List<SelectionContainer>();

			foreach(Transform a in g) {
				s.Add(new ComponentContainer(a));
			}

			possible = s.ToArray();
		}

		protected override SelectionContainer GetRaycast() {
			if(SceneView.mouseOverWindow == null)
				return null;
			if(SceneView.mouseOverWindow.ToString() == " (UnityEditor.SceneView)") {
				GameObject go = HandleUtility.PickGameObject(Event.current.mousePosition, false);
				if(go != null) {
					Component cc = go.GetComponentInParent(t);
					if(cc != null)
						return new ComponentContainer(cc);
				}
			}

			return null;
		}

	}
}