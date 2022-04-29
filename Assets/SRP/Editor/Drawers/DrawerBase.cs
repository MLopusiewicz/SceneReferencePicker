using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace LoneTower.SRP {
	public abstract class DrawerBase {

		public static SelectionBank<Color> ColorBank {
			get {
				if(colorBank == null)
					colorBank = new SelectionBank<Color>(SRPSettings.colors);
				return colorBank;
			}
			set {
				colorBank = value;
			}
		}
		static SelectionBank<Color> colorBank;

		public Color color { get; private set; }

		public bool showSelection { get; set; }
		public bool showMarks { get; set; }
		public bool showHandle { get; set; }

		public ParserBase drawTarget;

		public DrawerBase() {
			SceneView.duringSceneGui += SceneDraw;

			color = ColorBank.GetClosest(0);
		}

		~DrawerBase() {
			SceneView.duringSceneGui -= SceneDraw;
		}

		public void Hide() {
			showSelection = false;
			showHandle = false;
			showMarks = false;
		}

		public void Show() {
			showHandle = true;
			showSelection = true;
		}

		public void Clear() {
			SceneView.duringSceneGui -= SceneDraw;
			showSelection = false;
			ColorBank.Free(color);
		}


		void SceneDraw(SceneView obj) {
			if(showMarks) {
				Handles.color = SRPSettings.MarkColor;
				DrawMarks(drawTarget.Marks);
			}

			if(!showSelection) {
				return;
			}

			Handles.color = color;
			DrawSelection(drawTarget.Selection);
			if(showHandle) {
				DrawCoursor(MouseRay());
				if(drawTarget.IsHovering) {
					DrawBrush(drawTarget.Hover);
				}
			}
		}

		protected abstract void DrawCoursor(Ray mouseRay);
		protected abstract void DrawBrush(Vector3[] hover);
		protected abstract void DrawSelection(Vector3[] selection);
		protected virtual void DrawMarks(Vector3[] marks) {
			foreach(Vector3 a in marks) {
				Handles.DrawSolidDisc(a, GetCameraDirection(a), SRPSettings.MarkScale);
			}
		}

		protected Vector3 ScreenNormal(Vector3 pos) {
			return pos - Camera.current.transform.position;
		}
		protected Vector3 MousePosAtDepth(float depth) {
			Ray r = HandleUtility.GUIPointToWorldRay(Event.current.mousePosition);
			return r.origin + r.direction * depth;
		}
		protected Ray MouseRay() {
			return HandleUtility.GUIPointToWorldRay(Event.current.mousePosition);
		}

		protected Vector3 GetCameraDirection(Vector3 pos) {
			if(Camera.current.orthographic) {
				return Camera.current.transform.forward;
			}
			return pos - Camera.current.transform.position;
		}
	}
}
