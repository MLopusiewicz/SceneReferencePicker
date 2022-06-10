using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace LoneTower.SRP {

	public abstract class BrushBase : IStroke {
		public event Action<object[]> OnStrokeEnd, OnStroke, OnStrokeStart;
		public enum brushMode { normal, shift, ctrl }
		public brushMode mode { get; protected set; }
		public object[] hover { get; protected set; }
		public bool enabled { get; private set; }

		public List<object> selection;

		public ScenePickerBase input;
		public bool stroking { get; private set; }
		public BrushBase(ScenePickerBase input, List<object> list = null) {
			if(list == null)
				selection = new List<object>();
			selection = list;
			this.input = input;
			hover = new object[] { };
			stroking = false;
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
			OnStrokeStart?.Invoke(t);
			stroking = true;
		}
		protected virtual void Stroke(object[] t) {
			OnStroke?.Invoke(t);
		}

		protected virtual void EndStroke(object[] t) {
			OnStrokeEnd?.Invoke(t);
			stroking = false;
		}

		protected virtual void SetHover(object[] obj) {
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
			input.OnHover += SetHover;
		}

		public virtual void Disable() {
			if(!enabled)
				return;
			enabled = false;
			input.Disable();
			input.OnPressed -= StartStroke;
			input.OnDrag -= Stroke;
			input.OnRelease -= EndStroke;
			input.OnHover -= SetHover;
		}

	}
}