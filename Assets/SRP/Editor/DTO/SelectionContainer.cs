using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

//public abstract class SelectionContainer {
//	public abstract void Serialize(SerializedProperty prop);
//	public object obj;
//	public override bool Equals(object b) {
//		return this.obj == (b as SelectionContainer).obj;
//	}

//	//public static bool operator ==(SelectionContainer a, SelectionContainer b) {

//	//	return a.obj == b.obj;
//	//}

//	//public static bool operator !=(SelectionContainer a, SelectionContainer b) {
//	//	return a.obj != b.obj;
//	//}
//}

//public class ComponentContainer : SelectionContainer {

//	public ComponentContainer(Component c) {
//		obj = c;
//	}

//	//public ComponentContainer(SerializedProperty prop) {
//	//	obj = (Component)prop.objectReferenceValue;

//	//}

//	public static ComponentContainer Get(SerializedProperty prop) {
//		Component obj = (Component)prop.objectReferenceValue;
//		if(obj == null)
//			return null;
//		else
//			return new ComponentContainer(obj);
//	}

//	public override void Serialize(SerializedProperty prop) {
//		prop.objectReferenceValue = (obj as Component);
//	}
//}

