using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace LoneTower.Utility.SRP {
	public class DrawerTopDown : DrawerBase {


		protected override void DrawEmptyHandle(Ray mouseRay) {

		}

		protected override void DrawHandle(Vector3 hover, bool contained) {
			if(contained)
				Handles.color = Color.red;
			Vector3 dot = hover + Vector3.up * 0.3f * SRPSettings.Scale;
			Handles.DrawSolidDisc(dot, GetCameraDirection(dot), 0.05f * SRPSettings.Scale);
			Handles.DrawWireDisc(hover, Vector3.up, 0.3f * SRPSettings.Scale, 6 * SRPSettings.Scale);
			Handles.DrawLine(hover, dot, 3f * SRPSettings.LineScale);
		}

		protected override void DrawSelection(Vector3[] selection) {
			foreach(Vector3 a in selection) {
				Handles.DrawWireDisc(a, Vector3.up, 0.4f * SRPSettings.Scale, 3f * SRPSettings.LineScale);
			}
		}
	}
}