using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace LoneTower.SRP {
	public class InterfaceSerializer : ComponentSerializer {

		public InterfaceSerializer(Type t) : base(t) { }
		public override object[] Deserialize(SerializedProperty prop) {

			var des = base.Deserialize(prop);
			List<object> objs = new List<object>();
			foreach(Component g in des) {
				if(g is IMonoBehaviourBase)
					objs.Add(g);
			}
			return objs.ToArray();

		}
		public override object[] DeserializeArray(SerializedProperty prop) {

			var des = base.DeserializeArray(prop);
			List<object> objs = new List<object>();
			foreach(Component g in des) {
				if(g is IMonoBehaviourBase)
					objs.Add(g);
			}
			return objs.ToArray();
		}
	}
}