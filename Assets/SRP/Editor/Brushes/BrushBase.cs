using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace LoneTower.SRP {

	public abstract class BrushBase {
		public event Action<object[]> OnStrokeEnd, OnStroke, OnStrokeStart;
		public enum brushMode { normal, shift, ctrl }
		public brushMode mode;
		public object[] hover { get; private set; }

		public bool enabled { get; private set; }

		public List<object> selection;
		public ScenePickerBase input;

		public BrushBase(ScenePickerBase input, List<object> list = null) {
			if(list == null)
				selection = new List<object>();
			selection = list;
			this.input = input;

			SceneInput.Instance.ShiftDown += ShiftMode;
			SceneInput.Instance.ShiftUp += NormalMode;

			SceneInput.Instance.CtrlDown += CtrlMode;
			SceneInput.Instance.CtrlUp += NormalMode;
		}

		private void CtrlMode() {
			mode = brushMode.ctrl;
		}

		private void ShiftMode() {
			mode = brushMode.shift;
		}

		private void NormalMode() {
			mode = brushMode.normal;
		}

		protected virtual void StartStroke(object[] t) {
			OnStroke?.Invoke(t);
		}
		protected virtual void Stroke(object[] t) {
			OnStroke?.Invoke(t);
		}

		protected virtual void EndStroke(object[] t) {
			OnStrokeEnd?.Invoke(t);
		}

		private void Follow(object[] obj) {
			hover = obj;
		}

		public virtual void Enable() {
			if(enabled)
				return;
			enabled = true;
			input.Enable();
			input.OnPressed += StartStroke;
			input.OnDrag += Stroke;
			input.OnRelease += EndStroke;
			input.OnHover += Follow;
		}

		public virtual void Disable() {
			if(!enabled)
				return;
			enabled = false;
			input.Disable();
			input.OnPressed -= StartStroke;
			input.OnDrag -= Stroke;
			input.OnRelease -= EndStroke;
			input.OnHover -= Follow;
		}

	}
}