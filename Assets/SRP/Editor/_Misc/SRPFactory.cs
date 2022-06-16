using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
namespace LoneTower.SRP {
	public class SRPFactory
		{


		Type brush;
		Type parser;
		Type drawer;
		Type sceneInput;
		Type selectType;
		public static SerializerBase GetSerializer(SRPAttribute atr) {
			Type g = TryType(atr.serializer, typeof(SerializerBase), typeof(ComponentSerializer));
			return (SerializerBase)Activator.CreateInstance(g, atr.selectType);
		}

		public SRPFactory(string brush, string parser, string drawer, string picker, string serializer) {
			this.brush = TryType(brush, typeof(BrushBase), typeof(BrushMain));
			this.parser = TryType(parser, typeof(ParserBase), typeof(ParserTransform));
			this.drawer = TryType(drawer, typeof(DrawerBase), typeof(DrawerGeneric));
			this.sceneInput = TryType(picker, typeof(ScenePickerBase), typeof(ComponentPicker));
		}

		public SRPFactory(SRPAttribute attr) {
			this.brush = TryType(attr.brush, typeof(BrushBase), typeof(BrushMain));
			this.parser = TryType(attr.parser, typeof(ParserBase), typeof(ParserTransform));
			this.drawer = TryType(attr.drawer, typeof(DrawerBase), typeof(DrawerGeneric));
			this.sceneInput = TryType(attr.picker, typeof(ScenePickerBase), typeof(ComponentPicker));
			selectType = attr.selectType;
		}

		static Type TryType(string objType, Type expected, Type fallBack) {

			Type current = Type.GetType(objType);

			if(CheckType(current, expected)) {
				return current;
			} else {
				if(fallBack != null)
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

		public ScenePickerBase GetPicker() {
			return (ScenePickerBase)Activator.CreateInstance(sceneInput, new object[] { selectType });
		}
		public BrushBase GetBrush(List<object> c) {
			return (BrushBase)Activator.CreateInstance(brush, new object[] { GetPicker(), c });
		}
		public DrawerBase GetDrawer(BrushBase brush) {
			DrawerBase t = (DrawerBase)Activator.CreateInstance(drawer);
			ParserBase _p = (ParserBase)Activator.CreateInstance(parser, brush);
			t.drawTarget = _p;
			return t;
		}
	}
}