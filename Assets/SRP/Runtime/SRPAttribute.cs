using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace LoneTower.SRP {

	[AttributeUsage(AttributeTargets.Field, AllowMultiple = true, Inherited = true)]
	public class SRPAttribute : PropertyAttribute {

		public string brush;
		public string parser;
		public string drawer;
		public string picker;
		public string serializer;


		public Type selectType;

		public SRPAttribute(string brush, string parser, string drawer, string sceneInput, string serializer) {
			this.brush = brush;
			this.parser = parser;
			this.drawer = drawer;
			this.picker = sceneInput;
			this.serializer = serializer;
		}
		public SRPAttribute(params string[] data) {
			this.brush = data[0];
			this.parser = data[1];
			this.drawer = data[2];
			this.picker = data[3];
			this.serializer = data[4];
		}
		public SRPAttribute() {
			brush = "LoneTower.SRP.BrushMain";
			parser = "LoneTower.SRP.ParserTransform";
			drawer = "LoneTower.SRP.DrawerGeneric";
			picker = "LoneTower.SRP.ComponentPicker";
			serializer = "LoneTower.SRP.ComponentSerializer";
		}
	}

}

