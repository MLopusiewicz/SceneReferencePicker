using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static LoneTower.SRP.BrushBase;

namespace LoneTower.SRP {
	public class DrawerPath : DrawerPathBase {
		protected override void DrawCoursor(Ray mouseRay) {

		}

		protected override void DrawBrush(Vector3[] hover) {
			base.DrawBrush(hover);
			foreach(var a in hover)
				Handles.DrawWireDisc(a, Vector3.up, 0.2f * SRPSettings.Scale, 3f * SRPSettings.LineScale);
		}

		protected override void DrawSelection(Vector3[] selection) {
			base.DrawSelection(selection);
			for(int i = 0; i < selection.Length; i++) {
				Handles.DrawWireDisc(selection[i], Vector3.up, 0.3f * SRPSettings.Scale, 3 * SRPSettings.LineScale);
			}

		}

	}
}