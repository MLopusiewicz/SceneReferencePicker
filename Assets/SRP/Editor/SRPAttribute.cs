using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static LoneTower.SRP.PickerData;

namespace LoneTower.SRP {

	[AttributeUsage(AttributeTargets.Field, AllowMultiple = true, Inherited = true)]
	public class SRPAttribute : PropertyAttribute {

		public PickerData data;
		public SRPAttribute() {
			this.data = PickerData.defaultPicker;
		}
		public SRPAttribute(Type t) {
			if(CheckType(t, typeof(PickerData))) {
				data = (PickerData)Activator.CreateInstance(t);
			} else {
				Debug.LogWarning($"[SRP] Wrong type {t }. Expected type: {typeof(PickerData)}. Falling to default");
				data = defaultPicker;
			}
		}
		public SRPAttribute(Type logic, Type drawer, Type parser) {

			data = new PickerData(logic, parser, drawer);
		}

	}

}

