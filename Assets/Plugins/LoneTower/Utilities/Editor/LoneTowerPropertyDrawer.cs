
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;

namespace LoneTower.Utility.Editor {
	public abstract class LoneTowerPropertyDrawer : PropertyDrawer {
		protected SerializedProperty prop;

		protected LoneTowerPropertyDrawer() {
			Selection.selectionChanged += Destroy;
		}

		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
			if(prop == null) {
				prop = property;
				Awake();
			}


		}

		private void Destroy() {
			OnDestroy();
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
			return new T();
		}
	}
}