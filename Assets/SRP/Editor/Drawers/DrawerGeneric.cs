using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace LoneTower.SRP {
	public class DrawerGeneric : DrawerBase {

		protected override void DrawEmptyHandle(Ray r) {
		}

		protected override void DrawHandle(Vector3[] hover) {
			if(drawTarget.picker.mode == BrushBase.brushMode.shift)
				Handles.color = Color.red;
			else
				Handles.color = Color.white;
			foreach(var a in hover) {
				Handles.DrawWireDisc(a, GetCameraDirection(a), 0.3f * SRPSettings.Scale, 3f * SRPSettings.LineScale);
			}
		}

		protected override void DrawSelection(Vector3[] Selection) {
			Handles.color = color;
			foreach(var a in Selection) {
				Handles.DrawWireDisc(a, GetCameraDirection(a), 0.3f * SRPSettings.Scale, 3f * SRPSettings.LineScale);
			}
		}


	}

}