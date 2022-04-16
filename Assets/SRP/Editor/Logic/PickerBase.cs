using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace LoneTower.SRP {

	public abstract class PickerBase {
		public event Action OnStrokeEnd;
		public enum brushMode { normal, shift, ctrl }
		public brushMode mode;
		public SelectionContainer hover { get; private set; }

		public bool enabled { get; private set; }

		public List<SelectionContainer> selection;

		public ScenePickerBase input;

		public PickerBase(ScenePickerBase input, List<SelectionContainer> list = null) {
			if(list == null)
				selection = new List<SelectionContainer>();
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

		protected abstract void StartStroke(SelectionContainer t);
		protected abstract void Stroke(SelectionContainer t);
		protected virtual void EndStroke(SelectionContainer t) {
			OnStrokeEnd?.Invoke();
		}

		private void Follow(SelectionContainer obj) {
			hover = obj;
		}

		public virtual void Enable() {
			enabled = true;
			input.Enable();
			input.OnPressed += StartStroke;
			input.OnDrag += Stroke;
			input.OnRelease += EndStroke;
			input.OnCurrentChange += Follow;

		}

		public virtual void Disable() {
			enabled = false;
			input.Disable();
			input.OnPressed -= StartStroke;
			input.OnDrag -= Stroke;
			input.OnRelease -= EndStroke;
			input.OnCurrentChange -= Follow;
		}

		public void Toggle(bool state) {
			if(state) {
				Enable();
			} else
				Disable();
		}
	}
}