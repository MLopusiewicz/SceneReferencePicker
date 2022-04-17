using LoneTower.SRP;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace LoneTower.SRP {
	public abstract class ScenePickerBase {

		public Action<object[]> OnPressed, OnDrag, OnRelease, OnHover;
		public object current;
		public object[] possible;
		public ScenePickerBase(Type t) {

		}
		public virtual void Enable() {
			SceneInput.Instance.inputInterception = true;
			SceneInput.Instance.MouseDown += Click;
			SceneInput.Instance.MousePressing += Pressing;
			SceneInput.Instance.MouseUp += Release;
			SceneInput.Instance.SceneLoop += Update;
		}

		public virtual void Disable() {
			SceneInput.Instance.inputInterception = false;
			SceneInput.Instance.MouseDown -= Click;
			SceneInput.Instance.MousePressing -= Pressing;
			SceneInput.Instance.MouseUp -= Release;
			SceneInput.Instance.MouseLoop -= Update;
		}

		protected abstract object[] GetRaycast();

		protected virtual void Click() {
			OnPressed?.Invoke(GetRaycast());
		}

		protected virtual void Pressing() {
			OnDrag?.Invoke(GetRaycast());
		}

		protected virtual void Release() {
			OnRelease?.Invoke(GetRaycast());
		}

		protected virtual void Update() {
			object[] v = GetRaycast();
			OnHover?.Invoke(v);
		}


		~ScenePickerBase() {
			Disable();
		}
	}
}
