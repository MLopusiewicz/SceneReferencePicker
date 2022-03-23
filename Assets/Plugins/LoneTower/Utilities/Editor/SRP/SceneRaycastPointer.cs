using LoneTower.Utility.Editor;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace LoneTower.Utility.SRP {
	public class SceneRaycastPointer {

		public Action<Component> OnPressed, OnDrag, OnRelease, OnCurrentChange;
		public Component current;
		LayerMask mask;
		public Type t { get; private set; }
		public SceneRaycastPointer(LayerMask mask, Type t) {
			this.mask = mask;
			this.t = t;
			EditorSceneInput.Instance.MouseDown += Click;
			EditorSceneInput.Instance.MousePressing += Pressing;
			EditorSceneInput.Instance.MouseUp += Release;
			EditorSceneInput.Instance.SceneLoop += Update;
		}

		public void Enable() {
			EditorSceneInput.Instance.inputInterception = true;
		}

		public void Disable() {
			EditorSceneInput.Instance.inputInterception = false;
		}

		protected virtual Component GetRaycast() {
			Ray r = HandleUtility.GUIPointToWorldRay(Event.current.mousePosition);
			RaycastHit info;

			if(Physics.Raycast(r, out info, 1000, mask)) {
				return info.collider.gameObject.GetComponentInParent(t);
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



		~SceneRaycastPointer() {
			EditorSceneInput.Instance.MouseDown -= Click;
			EditorSceneInput.Instance.MousePressing -= Pressing;
			EditorSceneInput.Instance.MouseUp -= Release;
			EditorSceneInput.Instance.SceneLoop -= Update;
		}
	}
}