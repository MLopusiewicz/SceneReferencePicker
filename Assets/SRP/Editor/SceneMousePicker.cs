using LoneTower.Utility.Editor;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace LoneTower.Utility.SRP {
	public class SceneMousePicker {

		public Action<Component> OnPressed, OnDrag, OnRelease, OnCurrentChange;
		public Component current;
		public Type t { get; private set; }
		public GameObject[] possible;
		public SceneMousePicker(Type t) {
			this.t = t;
			possible = GameObject.FindObjectsOfType(t).Select(x => ((Component)x).gameObject).ToArray();
		}

		public void Enable() {
			EditorSceneInput.Instance.inputInterception = true;
			EditorSceneInput.Instance.MouseDown += Click;
			EditorSceneInput.Instance.MousePressing += Pressing;
			EditorSceneInput.Instance.MouseUp += Release;
			EditorSceneInput.Instance.MouseLoop += Update;
		}

		public void Disable() {
			EditorSceneInput.Instance.inputInterception = false;
			EditorSceneInput.Instance.MouseDown -= Click;
			EditorSceneInput.Instance.MousePressing -= Pressing;
			EditorSceneInput.Instance.MouseUp -= Release;
			EditorSceneInput.Instance.MouseLoop -= Update;
		}

		protected virtual Component GetRaycast() {
			if(SceneView.mouseOverWindow == null)
				return null;
			if(SceneView.mouseOverWindow.ToString() == " (UnityEditor.SceneView)") {
				GameObject go = HandleUtility.PickGameObject(Event.current.mousePosition, false);
				if(go != null) {
					return go.GetComponentInParent(t);
				}
			}


			return null;
		}

		private void Click() {
			OnPressed?.Invoke(GetRaycast());
		}

		protected virtual void Pressing() {
			OnDrag?.Invoke(GetRaycast());
		}

		private void Release() {
			OnRelease?.Invoke(GetRaycast());
		}

		private void Update() {

			Component v = GetRaycast();
			if(v != current) {
				OnCurrentChange?.Invoke(v);
				current = v;
			}
		}



		~SceneMousePicker() {
			Disable();
		}
	}
}