using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
namespace LoneTower.SRP {
	public class SRPTypeParser {

		public Type logic;
		public Type parser;
		public Type drawer;
		public Type sceneInput;
		public Type selectType;
		public Type serializer;

		public SRPTypeParser(string logic, string parser, string drawer, string picker, string serializer) {
			this.logic = TryType(logic, typeof(BrushBase), typeof(BrushMain));
			this.parser = TryType(parser, typeof(ParserBase), typeof(ParserTransform));
			this.drawer = TryType(drawer, typeof(DrawerBase), typeof(DrawerGeneric));
			this.sceneInput = TryType(picker, typeof(ScenePickerBase), typeof(ComponentPicker));
			this.serializer = TryType(serializer, typeof(SerializerBase), typeof(ComponentSerializer));
		}

		public SRPTypeParser(SRPAttribute attr) {
			this.logic = TryType(attr.brush, typeof(BrushBase), typeof(BrushMain));
			this.parser = TryType(attr.parser, typeof(ParserBase), typeof(ParserTransform));
			this.drawer = TryType(attr.drawer, typeof(DrawerBase), typeof(DrawerGeneric));
			this.sceneInput = TryType(attr.picker, typeof(ScenePickerBase), typeof(ComponentPicker));
			this.serializer = TryType(attr.serializer, typeof(SerializerBase), typeof(ComponentSerializer));
			this.selectType = attr.selectType;

		}

		Type TryType(string objType, Type expected, Type fallBack) {
			objType = "LoneTower.SRP." + objType;
			Type current = Type.GetType(objType);

			if(CheckType(current, expected)) {
				return current;
			} else {
				Debug.LogWarning($"<b><color=#ED1E79>[SRP]</color></b> Wrong type <color=#4ec9b0>{objType}</color>. Expected <color=#4ec9b0>{expected.Name}</color>. Falling back to: <color=#4ec9b0>{fallBack.Name} </color>");
				return fallBack;
			}
		}

		static bool CheckType(Type t, Type g) {
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
}