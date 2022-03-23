using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static LoneTower.Utility.SRP.PickerBase;

namespace LoneTower.Utility.SRP {
	public class DrawerPath : DrawerBase {
		protected override void DrawEmptyHandle(Ray mouseRay) {

		}

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

			Handles.DrawWireDisc(hover, Vector3.up, 0.2f);
		}

		protected override void DrawSelection(Vector3[] selection) {
			if(selection.Length == 0)
				return;
			Handles.color = color;
			Handles.DrawSolidDisc(selection[0], Vector3.up, 0.1f);

			for(int i = 0; i < selection.Length; i++) {
				Handles.DrawWireDisc(selection[i], Vector3.up, 0.3f);
				//Handles.Label(selection[i], i.ToString());
			}
			for(int i = 1; i < selection.Length; i++) {
				Handles.color = color;
				if(drawTarget.mode == brushMode.ctrl) {
					if(drawTarget.IsHovering)
						if(selection[i] == drawTarget.Hover) {
							Handles.DrawLine(selection[i], selection[i - 1], 3);
						}
				}

				Handles.DrawLine(selection[i], selection[i - 1]);
			}
		}
		protected override void DrawChoices(Vector3[] choices) {
			Handles.color = color;
			foreach(var a in choices) {
				Handles.DrawWireDisc(a, Vector3.up, 0.1f);
			}
		}
	}
}