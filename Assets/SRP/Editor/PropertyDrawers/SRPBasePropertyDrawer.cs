using LoneTower.Utility.Editor;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


namespace LoneTower.Utility.SRP {
	public abstract class SRPBasePropertyDrawer : LoneTowerPropertyDrawer {
		protected SRPController picker;
		State? state;
		protected bool isSingle;
		protected Type selectType;
		protected override void Awake() {
			if(CheckType(fieldInfo.FieldType, typeof(Component))) {
				isSingle = true;
				selectType = fieldInfo.FieldType;
			} else {
				if(fieldInfo.FieldType.GetGenericTypeDefinition() == typeof(ArrayDrawer<>) || fieldInfo.FieldType.GetGenericTypeDefinition() == typeof(ListDrawer<>)) {
					isSingle = false;
					selectType = fieldInfo.FieldType.GenericTypeArguments[0];
				} else
					Debug.LogError($"[SRP] Type not supported by attribute: {typeof(SRPAttribute)}");
			}
			picker = GetPicker();
			picker.logic.OnStrokeEnd += Serialize;
			if(state != null)
				picker.State = state.Value;

		}

		protected abstract SRPController GetPicker();
		protected override void Reset() {
			base.Reset();
			if(picker != null) {
				state = picker.State;
				picker.Clear();
				picker = null;
			}
			prop = null; //forces awake
		}

		protected abstract void Serialize();

		protected override void OnDestroy() {
			if(picker != null) {
				picker.Clear();
				picker = null;
			}
		}

		protected abstract Component[] Deserialize();
		public static bool CheckType(Type t, Type g) {
			while(t != typeof(object)) {
				if(t == g)
					return true;
				else t = t.BaseType;
			}
			return false;

		}


	}
}