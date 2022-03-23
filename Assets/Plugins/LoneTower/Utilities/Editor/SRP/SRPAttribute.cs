using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static LoneTower.Utility.SRP.PickerData;

namespace LoneTower.Utility.SRP {

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
				Debug.LogError($"Wrong type {t }. Expected type: {typeof(PickerData)}. Falling to default");
				data = defaultPicker;
			}
		}
		public SRPAttribute(Type t, params string[] layers) {
			if(CheckType(t, typeof(PickerData))) {
				data = (PickerData)Activator.CreateInstance(t);
				data.mask = LayerMask.GetMask(layers);
			} else {
				Debug.LogError($"Wrong type {t }. Expected type: {typeof(PickerData)}. Falling to default");
				data = defaultPicker;
			}
		}

	}

}

