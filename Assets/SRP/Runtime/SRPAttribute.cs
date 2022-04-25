using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace LoneTower.SRP {

	[AttributeUsage(AttributeTargets.Field, AllowMultiple = true, Inherited = true)]
	public class SRPAttribute : PropertyAttribute {

		public string logic;
		public string parser;
		public string drawer;
		public string picker;
		public string serializer;


		public Type selectType;
		 
		public SRPAttribute(string logic, string parser, string drawer, string sceneInput, string serializer) {
			this.logic = logic;
			this.parser = parser;
			this.drawer = drawer;
			this.picker = sceneInput;
			this.serializer = serializer;
		} 
		public SRPAttribute(params string[] data) {
			this.logic = data[0];
			this.parser = data[1];
			this.drawer = data[2];
			this.picker = data[3];
			this.serializer = data[4];
		}


	}

}

