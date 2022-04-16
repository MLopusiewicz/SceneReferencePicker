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
		public Type serializer;

		public static PickerData defaultPicker = new PickerData(typeof(LogicMain), typeof(ParserTransform), typeof(DrawerGeneric), typeof(ComponentPicker), typeof(ComponentSerializer));

		public PickerData(Type logic, Type parser, Type drawer, Type picker, Type serializer) {
			this.logic = TryType(logic, typeof(LogicBase), typeof(LogicMain));
			this.parser = TryType(parser, typeof(ParserBase), typeof(ParserTransform));
			this.drawer = TryType(drawer, typeof(DrawerBase), typeof(DrawerGeneric));
			this.sceneInput = TryType(picker, typeof(ScenePickerBase), typeof(ComponentPicker));
			this.serializer = TryType(serializer, typeof(SerializerBase), typeof(ComponentSerializer));
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
		public PathPicker() : base(typeof(LogicPath), typeof(ParserTransform), typeof(DrawerPath), typeof(ComponentPicker), typeof(ComponentSerializer)) {
		}
	}
	public class SinglePicker : PickerData {
		public SinglePicker() : base(typeof(LogicSingle), typeof(ParserTransform), typeof(DrawerGeneric), typeof(ComponentPicker), typeof(ComponentSerializer)) { }
	}
	public class TopDownPicker : PickerData {
		public TopDownPicker() : base(typeof(LogicMain), typeof(ParserTransform), typeof(DrawerTopDown), typeof(ComponentPicker), typeof(ComponentSerializer)) { }
	}
	public class TopDownSinglePicker : PickerData {
		public TopDownSinglePicker() : base(typeof(LogicSingle), typeof(ParserTransform), typeof(DrawerTopDown), typeof(ComponentPicker), typeof(ComponentSerializer)) { }
	}
	public class DefaultPicker : PickerData {
		public DefaultPicker() : base(typeof(LogicMain), typeof(ParserTransform), typeof(DrawerGeneric), typeof(ComponentPicker), typeof(ComponentSerializer)) { }
	}

}