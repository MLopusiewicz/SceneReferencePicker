using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static LoneTower.Utility.SRP.PickerBase;

namespace LoneTower.Utility.SRP {
	public class DrawerPath : DrawerPathBase {
		protected override void DrawEmptyHandle(Ray mouseRay) {

		}

		protected override void DrawHandle(Vector3 hover, bool contained) {
			base.DrawHandle(hover, contained);
			Handles.DrawWireDisc(hover, Vector3.up, 0.2f * SRPSettings.Scale, 3f * SRPSettings.LineScale);
		}

		protected override void DrawSelection(Vector3[] selection) {
			base.DrawSelection(selection);
			for(int i = 0; i < selection.Length; i++) {
				Handles.DrawWireDisc(selection[i], Vector3.up, 0.3f * SRPSettings.Scale, 3 * SRPSettings.LineScale);
			}

		}

	}
}