using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace LoneTower.SRP {
	public class SceneInput {
		public static SceneInput Instance {
			get {
				if(instance == null)
					instance = new SceneInput();
				return instance;
			}
		}
		static SceneInput instance;

		public bool inputInterception;
		public bool shiftPressed { get; private set; }
		public bool ctrlPressed { get; private set; }
		public event Action MouseDown, MouseUp, MousePressing, ShiftDown, ShiftUp, SceneLoop;
		public event Action CtrlDown, CtrlUp;
		public event Action<float> MouseScroll;
		public event Action MouseLoop;


		public bool LMBpressed { get; private set; }

		Dictionary<KeyCode, Action> keys = new Dictionary<KeyCode, Action>();


		public SceneInput() {
			SceneView.duringSceneGui += SceneFunc;
		}

		void SceneFunc(SceneView sceneView) {
			SceneLoop?.Invoke();
			if(!inputInterception)
				return;
			MouseInput(Event.current);
			shiftPressed = CheckButton(KeyCode.LeftShift, shiftPressed, ShiftDown, ShiftUp, Event.current);
			ctrlPressed = CheckButton(KeyCode.LeftControl, ctrlPressed, CtrlDown, CtrlUp, Event.current);

			if(Event.current.type == EventType.KeyDown) {
				if(keys.ContainsKey(Event.current.keyCode))
					keys[Event.current.keyCode]();
			}
		}

		public void RegisterKey(KeyCode k, Action callback) {
			if(!keys.ContainsKey(k)) {
				keys.Add(k, callback);
			} else
				keys[k] += callback;
		}
		public void UnregisterKey(KeyCode k, Action callback) {
			if(keys.ContainsKey(k)) {
				keys[k] -= callback;
				if(keys[k] == null)
					keys.Remove(k);
			}
		}

		void MouseInput(Event e) {
			HandleUtility.AddDefaultControl(GUIUtility.GetControlID(FocusType.Passive));//disables LBM 
			if(!e.isMouse)
				return;

			MouseLoop?.Invoke();

			if(e.type == EventType.MouseDown && e.button == 0) {
				MouseDown?.Invoke();
				LMBpressed = true;

			} else if(e.type == EventType.MouseUp && e.button == 0) {
				MouseUp?.Invoke();
				LMBpressed = false;
			} else {
				if(LMBpressed) {
					MousePressing?.Invoke();
				}
			}

			if(MouseScroll != null)
				if(e.type == EventType.ScrollWheel) {
					MouseScroll?.Invoke(e.delta.y);
					e.Use();
				}
		}

		bool CheckButton(KeyCode key, bool state, Action Down, Action Up, Event e) {
			if(e.type == EventType.KeyDown && Event.current.keyCode == key) {
				if(!state) {

					Down?.Invoke();
					return true;
				}
			}
			if(e.type == EventType.KeyUp && Event.current.keyCode == key) {
				if(state) {

					Up?.Invoke();
					return false;
				}
			}
			return state;
		}
	}

}