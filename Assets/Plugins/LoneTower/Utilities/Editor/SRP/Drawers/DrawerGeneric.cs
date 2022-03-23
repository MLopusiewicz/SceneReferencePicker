using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace LoneTower.Utility.SRP {
	public class DrawerGeneric : DrawerBase {

		protected override void DrawEmptyHandle(Ray r) {
		}

		protected override void DrawHandle(Vector3 hover, bool contained) {
			if(contained)
				Handles.color = Color.red;
			else
				Handles.color = Color.white;

			Handles.DrawWireDisc(hover, Vector3.up, 0.3f, 3f);
		}

		protected override void DrawSelection(Vector3[] Selection) {
			Handles.color = color;
			foreach(var a in Selection) {
				Handles.DrawWireDisc(a, GetCameraDirection(a), 0.3f, 3f);
			}
		}
		protected override void DrawChoices(Vector3[] choices) {
			Handles.color = SRPSettings.Instance.choiceColor;
			foreach(var a in choices) {
				Handles.DrawWireDisc(a, GetCameraDirection(a), 0.1f);
			}
		}

	}

}