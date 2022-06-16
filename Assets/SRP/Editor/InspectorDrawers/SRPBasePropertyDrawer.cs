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
		protected SRPGUIDrawer srpDrawer;
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
			srpDrawer = new SRPGUIDrawer(picker);
			picker.brush.OnStrokeEnd += Serialize;
			if(state != null)
				picker.State = state.Value;

			picker.VisibilityOn();
		}

		protected SRPController GetPicker() {
			SRPAttribute attr = (SRPAttribute)attribute;
			if(attr.selectType == null)
				attr.selectType = selectType;
			serializer = SRPFactory.GetSerializer(attr);
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
			var selection = picker.GetSelection();
			if(isSingle) {
				if(selection.Length > 0)
					serializer.Serialize(selection[selection.Length - 1], prop);
				else
					serializer.Serialize(null, prop);
				picker.Dispose();
				Reset();
			} else {
				serializer.SerializeArray(selection, prop.FindPropertyRelative("collection"));
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