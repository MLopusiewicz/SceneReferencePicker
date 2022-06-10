using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace LoneTower.SRP {
	public class InterfacePicker : ScenePickerBase {
		public Type t { get; private set; }
		public InterfacePicker(Type t) : base(t) {
			this.t = t;
			

			//Transform[] g = GameObject.FindObjectsOfType(t).Select(x => ((IMonoBehaviourBase)x).transform).ToArray();

			//List<object> s = new List<object>();

			//s.AddRange(g);
			//possible = s.ToArray();

		}

		protected override object[] GetSelection() {
			if(SceneView.mouseOverWindow == null)
				return new object[] { };
			if(SceneView.mouseOverWindow.ToString() == " (UnityEditor.SceneView)") {
				GameObject go = HandleUtility.PickGameObject(Event.current.mousePosition, false);
				if(go != null) {
					IMonoBehaviourBase gg = (IMonoBehaviourBase)go.GetComponentInParent(t);
					if(gg != null)
						return new object[] { gg as Component };
				}
			}
			return new object[] { };
		}

	}


}