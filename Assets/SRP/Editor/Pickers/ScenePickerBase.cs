using LoneTower.SRP;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace LoneTower.SRP {
	public abstract class ScenePickerBase {

		public Action<SelectionContainer> OnPressed, OnDrag, OnRelease, OnCurrentChange;
		public SelectionContainer current;
		public SelectionContainer[] possible;

		public void Enable() {
			SceneInput.Instance.inputInterception = true;
			SceneInput.Instance.MouseDown += Click;
			SceneInput.Instance.MousePressing += Pressing;
			SceneInput.Instance.MouseUp += Release;
			SceneInput.Instance.MouseLoop += Update;
		}

		public void Disable() {
			SceneInput.Instance.inputInterception = false;
			SceneInput.Instance.MouseDown -= Click;
			SceneInput.Instance.MousePressing -= Pressing;
			SceneInput.Instance.MouseUp -= Release;
			SceneInput.Instance.MouseLoop -= Update;
		}

		protected abstract SelectionContainer GetRaycast();

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
			SelectionContainer v = GetRaycast();
			if(v != current) {
				OnCurrentChange?.Invoke(v);
				current = v;
			}
		}

		~ScenePickerBase() {
			Disable();
		}
	}
}
