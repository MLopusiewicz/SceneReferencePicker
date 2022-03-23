using LoneTower.Utility.Editor;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace LoneTower.Utility.SRP {

	public abstract class PickerBase {
		public event Action<Component> OnStrokeEnd;
		public enum brushMode { normal, shift, ctrl }
		public brushMode mode;
		public Component hover { get; private set; }

		public bool enabled { get; private set; }

		public List<Component> selection;

		public SceneMousePicker input;

		public PickerBase(Type t, List<Component> list = null) {
			if(list == null)
				selection = new List<Component>();
			selection = list;
			input = new SceneMousePicker(t);

			EditorSceneInput.Instance.ShiftDown += ShiftMode;
			EditorSceneInput.Instance.ShiftUp += NormalMode;

			EditorSceneInput.Instance.CtrlDown += CtrlMode;
			EditorSceneInput.Instance.CtrlUp += NormalMode;
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

		protected abstract void StartStroke(Component t);
		protected abstract void Stroke(Component t);
		protected virtual void EndStroke(Component t) {
			OnStrokeEnd?.Invoke(t);
		}

		private void Follow(Component obj) {
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