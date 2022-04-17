using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace LoneTower.SRP {
	public abstract class DrawerBase {

		static SelectionBank<Color> colorBank = new SelectionBank<Color>(SRPSettings.colors);

		public Color color { get; private set; }

		public bool showSelection { get; set; }
		public bool showChoices { get; set; }
		public bool showHandle { get; set; }

		public ParserBase drawTarget;

		public DrawerBase() {
			SceneView.duringSceneGui += SceneDraw;

			color = colorBank.GetClosest(0);
		}

		~DrawerBase() {
			SceneView.duringSceneGui -= SceneDraw;
		}

		public void Hide() {
			showSelection = false;
			showHandle = false;
			showChoices = false;
		}

		public void Show() {
			showHandle = true;
			showSelection = true;
		}

		public void Clear() {
			SceneView.duringSceneGui -= SceneDraw;
			showSelection = false;
			colorBank.Free(color);
		}


		void SceneDraw(SceneView obj) {
			if(showChoices) {
				Handles.color = SRPSettings.choiceColor;
				DrawChoices(drawTarget.Choices);
			}

			if(!showSelection) {
				return;
			}

			Handles.color = color;
			DrawSelection(drawTarget.Selection);
			if(showHandle)
				if(drawTarget.IsHovering) {
					DrawHandle(drawTarget.Hover);
				} else {
					DrawHandle();
				}
		}

		protected abstract void DrawEmptyHandle(Ray mouseRay);
		protected abstract void DrawHandle(Vector3[] hover);
		protected abstract void DrawSelection(Vector3[] selection);
		protected virtual void DrawChoices(Vector3[] choices) {
			foreach(Vector3 a in choices) {
				Handles.DrawSolidDisc(a, GetCameraDirection(a), SRPSettings.ChoiceSize);
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
