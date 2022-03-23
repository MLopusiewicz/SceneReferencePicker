using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PickerData;

namespace LoneTower.Utility.SRP {

	[AttributeUsage(AttributeTargets.Field, AllowMultiple = true, Inherited = true)]
	public class BrushAttribute : PropertyAttribute {

		public PickerData data;
		public BrushAttribute() {
			this.data = PickerData.defaultPicker;
		}

		public BrushAttribute(Type t) {
			if(CheckType(t, typeof(PickerData))) {
				data = (PickerData)Activator.CreateInstance(t);
			} else {
				Debug.LogError($"Wrong type {t }. Expected type: {typeof(PickerData)}. Falling to default");
				data = defaultPicker;
			}
		}
		public BrushAttribute(Type t, params string[] layers) {
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

