using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace LoneTower.Utility.SRP {
	public abstract class DrawerBase {

		static SelectionBank<Color> colorBank = new SelectionBank<Color>(new Color[]{
			Color.blue, Color.red, Color.green, Color.magenta, Color.yellow,  Color.cyan, Color.black
		});

		public Color color { get; private set; }

		public bool showSelection { get; set; }
		public bool showChoices { get; set; }

		public IDrawable drawTarget;

		public DrawerBase() {
			SceneView.duringSceneGui += SceneDraw;
			color = colorBank.GetClosest(0);
		}

		~DrawerBase() {
			SceneView.duringSceneGui -= SceneDraw;
		}

		public void Hide() {
			showSelection = false;
		}

		public void Show() {
			showSelection = true;
		}

		public void Clear() {
			SceneView.duringSceneGui -= SceneDraw;
			showSelection = false;
			colorBank.Free(color);
		}


		void SceneDraw(SceneView obj) {
			if(showChoices) 
				DrawChoices(drawTarget.Choices);

			if(!showSelection) {
				return;
			}
			DrawSelection(drawTarget.Selection);
			if(drawTarget.IsHovering)
				DrawHandle(drawTarget.Hover, drawTarget.IsHoverSelected);
			else {
				DrawHandle();
			}
		}

		protected abstract void DrawEmptyHandle(Ray mouseRay);
		protected abstract void DrawHandle(Vector3 hover, bool contained);
		protected abstract void DrawSelection(Vector3[] selection);
		protected abstract void DrawChoices(Vector3[] choices);

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

		protected void DrawHandle() {
			DrawEmptyHandle(MouseRay());
		}
		protected Vector3 GetCameraDirection(Vector3 pos) {
			if(Camera.current.orthographic) {
				return Camera.current.transform.forward;
			}
			return pos - Camera.current.transform.position;
		}
	}
}
