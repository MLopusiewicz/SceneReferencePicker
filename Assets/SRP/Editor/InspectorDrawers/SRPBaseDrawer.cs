
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;

namespace LoneTower.SRP {
	public abstract class SRPBaseDrawer : PropertyDrawer {
		protected SerializedProperty prop;

		protected SRPBaseDrawer() {
			Selection.selectionChanged += Destroy;
			EditorApplication.playModeStateChanged += OnChange;
			Undo.undoRedoPerformed += Reset;
		}

		protected virtual void Reset() {

		}

		private void OnChange(PlayModeStateChange obj) {
			Reset();
		}

		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
			if(prop == null) {
				prop = property;
				Awake();
			}


		}

		private void Destroy() {
			OnDestroy();
			Undo.undoRedoPerformed -= Reset;
			Selection.selectionChanged -= Destroy;
		}

		protected abstract void Awake();
		protected abstract void OnDestroy();
		
		protected T GetAttribute<T>(SerializedProperty property) where T : Attribute, new() {
			Type objType = prop.serializedObject.targetObject.GetType();
			MemberInfo[] myMembers = objType.GetMembers();
			foreach(var a in myMembers) {
				if(a.Name == property.name) {
					foreach(object g in a.GetCustomAttributes(true)) {
						if(g is T)
							return (g as T);
					}
				}
			}
			Debug.LogWarning($"<b><color=#ED1E79>[SRP]</color></b> SRPAttribute not found <color=#4ec9b0>{property.name}</color>");
			return new T();
		}
	}
}