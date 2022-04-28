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
		public object[] possible;
		public ScenePickerBase(Type t) {

		}
		public virtual void Enable() {
			SceneInput.Instance.inputInterception = true;
			SceneInput.Instance.MouseDown += Click;
			SceneInput.Instance.MousePressing += Pressing;
			SceneInput.Instance.MouseUp += Release;
			SceneInput.Instance.MouseLoop += Update;
		}

		public virtual void Disable() {
			SceneInput.Instance.inputInterception = false;
			SceneInput.Instance.MouseDown -= Click;
			SceneInput.Instance.MousePressing -= Pressing;
			SceneInput.Instance.MouseUp -= Release;

			SceneInput.Instance.MouseLoop -= Update;
		}

		protected abstract object[] GetSelection();

		protected virtual void Click() {
			OnPressed?.Invoke(GetSelection());
		}

		protected virtual void Pressing() {
			OnDrag?.Invoke(GetSelection());
		}

		protected virtual void Release() {
			OnRelease?.Invoke(GetSelection());
		}

		protected virtual void Update() {
			object[] v = GetSelection();
			OnHover?.Invoke(v);
		}
		
		~ScenePickerBase() {
			Disable();
		}
	}
}
