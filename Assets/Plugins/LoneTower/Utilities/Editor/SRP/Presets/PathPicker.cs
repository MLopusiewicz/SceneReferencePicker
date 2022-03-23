using LoneTower.Utility.SRP;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace LoneTower.Utility.SRP {
	public class PickerData {


		public Type logic;
		public Type parser;
		public Type drawer;
		public LayerMask mask;

		public static PickerData defaultPicker = new PickerData(typeof(PickerMain), typeof(ParserTransform), typeof(GenericDrawer), 1 << LayerMask.NameToLayer("Default"));

		public PickerData(Type logic, Type parser, Type drawer, LayerMask mask) {
			this.mask = mask;

			this.logic = TryType(logic, typeof(PickLogic), typeof(PickerMain));
			this.parser = TryType(parser, typeof(ParserBase), typeof(ParserTransform));
			this.drawer = TryType(drawer, typeof(PickDrawer), typeof(GenericDrawer));

		}

		Type TryType(Type current, Type expected, Type fallBack) {
			if(CheckType(current, expected)) {
				return current;
			} else {
				Debug.LogWarning($"[PICKER] Wrong type {current}. Expected {expected}. Falling back to: {fallBack}");
				return fallBack;
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

	public class PathPicker : PickerData {
		public PathPicker() : base(typeof(PickerOrder), typeof(ParserTransform), typeof(OrderDrawer), 1 << LayerMask.NameToLayer("Default")) {
		}
	}

	public class SinglePicker : PickerData {
		public SinglePicker() : base(typeof(PickerSingle), typeof(ParserTransform), typeof(GenericDrawer), 1 << LayerMask.NameToLayer("Default")) { }
	}
}