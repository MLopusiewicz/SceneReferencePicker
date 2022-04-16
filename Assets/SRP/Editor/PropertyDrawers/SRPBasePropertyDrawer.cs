using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


namespace LoneTower.SRP {
	public abstract class SRPBasePropertyDrawer : SRPBaseDrawer {

		protected SRPController picker;
		SerializerBase serializer;
		State? state;
		protected bool isSingle;
		protected Type selectType;
		protected override void Awake() {
			if(fieldInfo.FieldType.IsGenericType) {
				if(fieldInfo.FieldType.GetGenericTypeDefinition() == typeof(ArrayDrawer<>) || fieldInfo.FieldType.GetGenericTypeDefinition() == typeof(ListDrawer<>)) {
					isSingle = false;
					selectType = fieldInfo.FieldType.GenericTypeArguments[0];
				}
			} else {

				selectType = fieldInfo.FieldType;
				isSingle = true;
			}

			picker = GetPicker();
			picker.logic.OnStrokeEnd += Serialize;
			if(state != null)
				picker.State = state.Value;

			picker.drawer.Show();
		}

		protected SRPController GetPicker() {
			SRPAttribute a = (attribute as SRPAttribute);
			a.data.selectType = selectType;
			serializer = (SerializerBase)Activator.CreateInstance(a.data.serializer);
			return new SRPController(a.data, Deserialize());
		}

		protected override void Reset() {
			base.Reset();
			if(picker != null) {
				state = picker.State;
				picker.Clear();
				picker = null;
			}
			prop = null; //forces awake
		}

		protected void Serialize() {
			if(isSingle) {
				if(picker.logic.selection.Count > 0)
					serializer.Serialize(picker.logic.selection[picker.logic.selection.Count - 1], prop);
				else
					serializer.Serialize(null, prop);
				picker.Clear();
				Reset();
			} else {
				serializer.SerializeArray(picker.logic.selection.ToArray(), prop.FindPropertyRelative("collection"));
			}
		}

		protected object[] Deserialize() {
			if(isSingle)
				return serializer.Deserialize(prop);
			else
				return serializer.DeserializeArray(prop);
		}

		protected override void OnDestroy() {
			if(picker != null) {
				picker.Clear();
				picker = null;
			}
		}

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