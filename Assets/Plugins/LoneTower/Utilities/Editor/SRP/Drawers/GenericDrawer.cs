using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace LoneTower.Utility.SRP {
	public class GenericDrawer : PickDrawer {



		protected override void EmptyHandleDrawer(Ray r) {
		}

		protected override void HandleDrawer(Vector3 hover, bool contained) {
			if(contained)
				Handles.color = Color.red;
			else
				Handles.color = Color.white;

			Handles.DrawWireDisc(hover, Vector3.up, 0.3f, 3f);
		}

		protected override void SelectionDrawer(Vector3[] Selection) {
			Handles.color = color;
			foreach(var a in Selection) {
				Handles.DrawWireDisc(a, GetCameraDirection(a), 0.3f, 3f);
			}
		}

	}

}