using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace LoneTower.SRP {
	public class PickerData {

		public Type logic;
		public Type parser;
		public Type drawer;
		public Type sceneInput;
		public Type selectType;


		public static PickerData defaultPicker = new PickerData(typeof(PickerMain), typeof(ParserTransform), typeof(DrawerGeneric), typeof(ComponentPicker));

		public PickerData(Type logic, Type parser, Type drawer, Type picker) {
			this.logic = TryType(logic, typeof(PickerBase), typeof(PickerMain));
			this.parser = TryType(parser, typeof(ParserBase), typeof(ParserTransform));
			this.drawer = TryType(drawer, typeof(DrawerBase), typeof(DrawerGeneric));
			this.sceneInput = TryType(picker, typeof(ScenePickerBase), typeof(ComponentPicker));
		}

		Type TryType(Type current, Type expected, Type fallBack) {
			if(CheckType(current, expected)) {
				return current;
			} else {
				Debug.LogWarning($"[SRP] Wrong type {current}. Expected {expected}. Falling back to: {fallBack}");
				return fallBack;
			}
		}

		public static bool CheckType(Type t, Type g) {
			if(t == null)
				return false;
			while(t != typeof(object)) {
				if(t == g)
					return true;
				else t = t.BaseType;
			}
			return false;
		}

	}


	public class PathPicker : PickerData {
		public PathPicker() : base(typeof(PickerPath), typeof(ParserTransform), typeof(DrawerPath), typeof(ComponentPicker)) {
		}
	}
	public class SinglePicker : PickerData {
		public SinglePicker() : base(typeof(PickerSingle), typeof(ParserTransform), typeof(DrawerGeneric), typeof(ComponentPicker)) { }
	}
	public class TopDownPicker : PickerData {
		public TopDownPicker() : base(typeof(PickerMain), typeof(ParserTransform), typeof(DrawerTopDown), typeof(ComponentPicker)) { }
	}
	public class TopDownSinglePicker : PickerData {
		public TopDownSinglePicker() : base(typeof(PickerSingle), typeof(ParserTransform), typeof(DrawerTopDown), typeof(ComponentPicker)) { }
	}
	public class DefaultPicker : PickerData {
		public DefaultPicker() : base(typeof(PickerMain), typeof(ParserTransform), typeof(DrawerGeneric), typeof(ComponentPicker)) { }
	}

}