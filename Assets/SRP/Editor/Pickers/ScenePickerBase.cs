using LoneTower.SRP;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace LoneTower.SRP {
	public abstract class ScenePickerBase {
		protected bool enabled = false;
		public Action<object[]> OnPressed, OnDrag, OnRelease, OnHover;
		public object[] possible;
		public ScenePickerBase(Type t) {

		}
		public virtual void Enable() {
			if(enabled)
				return;
			enabled = true;
			SceneInput.Instance.MouseDown += Click;
			SceneInput.Instance.MousePressing += Pressing;
			SceneInput.Instance.MouseUp += Release;
			SceneInput.Instance.MouseLoop += Update;
		}

		public virtual void Disable() {
			SceneInput.Instance.MouseDown -= Click;
			SceneInput.Instance.MousePressing -= Pressing;
			SceneInput.Instance.MouseUp -= Release;
			SceneInput.Instance.MouseLoop -= Update;

			enabled = false;
		}
		public void EnableHover() { 
			SceneInput.Instance.MouseLoop += Update;
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
