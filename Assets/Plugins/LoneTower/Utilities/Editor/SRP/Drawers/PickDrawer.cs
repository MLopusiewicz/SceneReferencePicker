using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
namespace LoneTower.Utility.SRP {

	public abstract class PickDrawer {

		static SelectionBank<Color> colorBank = new SelectionBank<Color>(new Color[]{
			Color.blue, Color.red, Color.green, Color.magenta, Color.yellow,  Color.cyan, Color.black
		});

		public Color color { get; private set; }

		public bool showSelection { get; set; }

		public IDrawable drawTarget;

		public PickDrawer() {
			SceneView.duringSceneGui += SceneDraw;
			color = colorBank.GetClosest(0);
		}

		~PickDrawer() {
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
			if(showSelection) {
				SelectionDrawer(drawTarget.Selection);
				if(drawTarget.IsHovering)
					HandleDrawer(drawTarget.Hover, drawTarget.IsHoverSelected);
				else {
					DrawHandle();
				}
			}
		}

		protected abstract void EmptyHandleDrawer(Ray mouseRay);
		protected abstract void HandleDrawer(Vector3 hover, bool contained);
		protected abstract void SelectionDrawer(Vector3[] Selection);

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
			EmptyHandleDrawer(MouseRay());
		}
		protected Vector3 GetCameraDirection(Vector3 pos) {
			if(Camera.current.orthographic) {
				return Camera.current.transform.forward;
			}
			return pos - Camera.current.transform.position;
		}
	}
}
