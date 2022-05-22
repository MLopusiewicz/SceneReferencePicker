using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace LoneTower.SRP {
	public abstract class SerializerBase {
		protected Type serializeType;
		public SerializerBase(Type t) {
			serializeType = t;
		}
		public abstract void Serialize(object o, SerializedProperty prop);

		public abstract void SerializeArray(object[] o, SerializedProperty prop);

		public abstract object[] Deserialize(SerializedProperty prop);

		public abstract object[] DeserializeArray(SerializedProperty prop);
	}
}