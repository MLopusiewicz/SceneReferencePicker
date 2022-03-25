using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static LoneTower.Utility.SRP.PickerBase;

namespace LoneTower.Utility.SRP {
	public abstract class DrawerPathBase : DrawerBase {


		protected override void DrawHandle(Vector3 hover, bool contained) {
			switch(drawTarget.mode) {
				case brushMode.ctrl:
					Handles.color = Color.green;
					break;
				case brushMode.shift:
					Handles.color = Color.red;
					break;
				default:
					Handles.color = Color.white;
					break;
			}
		}

		protected override void DrawSelection(Vector3[] selection) {
			if(selection.Length == 0)
				return;

			Handles.DrawSolidDisc(selection[0], Vector3.up, 0.1f * SRPSettings.Scale);

			for(int i = 1; i < selection.Length; i++) {
				Handles.color = color;
				if(drawTarget.mode == brushMode.ctrl) {
					if(drawTarget.IsHovering)
						if(selection[i] == drawTarget.Hover) {
							Handles.DrawLine(selection[i], selection[i - 1], 4 * SRPSettings.LineScale);
						}
				}
				Handles.DrawLine(selection[i], selection[i - 1], SRPSettings.LineScale);
			}
		}
	}
}