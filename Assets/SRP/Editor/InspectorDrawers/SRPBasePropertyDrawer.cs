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
				if(fieldInfo.FieldType.GetGenericTypeDefinition() == typeof(PickableArray<>) || fieldInfo.FieldType.GetGenericTypeDefinition() == typeof(PickableList<>)) {
					isSingle = false;
					selectType = fieldInfo.FieldType.GenericTypeArguments[0];
				}
			} else {

				selectType = fieldInfo.FieldType;
				isSingle = true;
			}

			picker = GetPicker();
			picker.brush.OnStrokeEnd += Serialize;
			if(state != null)
				picker.State = state.Value;

			picker.drawer.Show();
		}

		protected SRPController GetPicker() {
			SRPAttribute attr = GetAttribute<SRPAttribute>(prop);
			if(attr.selectType == null)
				attr.selectType = selectType;
			SRPTypeParser types = new SRPTypeParser(attr);

			serializer = (SerializerBase)Activator.CreateInstance(types.serializer);
			return new SRPController(attr, Deserialize());
		}

		protected override void Reset() {
			base.Reset();
			if(picker != null) {
				state = picker.State;
				picker.Dispose();
				picker = null;
			}
			prop = null; //forces awake
		}

		protected void Serialize(object[] t) {
			if(isSingle) {
				if(picker.brush.selection.Count > 0)
					serializer.Serialize(picker.brush.selection[picker.brush.selection.Count - 1], prop);
				else
					serializer.Serialize(null, prop);
				picker.Dispose();
				Reset();
			} else {
				serializer.SerializeArray(picker.brush.selection.ToArray(), prop.FindPropertyRelative("collection"));
			}
		}

		protected object[] Deserialize() {
			if(isSingle)
				return serializer.Deserialize(prop);
			else
				return serializer.DeserializeArray(prop.FindPropertyRelative("collection"));
		}

		protected override void OnDestroy() {
			if(picker != null) {
				picker.Dispose();
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