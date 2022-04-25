
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
namespace LoneTower.SRP {
	public class ComponentRectPicker : ScenePickerBase {
		public Type t { get; private set; }
		Vector2 startpos;
		bool pressing = false;
		public ComponentRectPicker(Type t) : base(t) {
			this.t = t;
			Transform[] g = GameObject.FindObjectsOfType(t).Select(x => ((Component)x).transform).ToArray();
			List<object> s = new List<object>();

			s.AddRange(g);
			possible = s.ToArray();
		}

		Vector3 GetPoint(Vector2 v) {
			Ray r = HandleUtility.GUIPointToWorldRay(v);
			return r.origin + r.direction;
		}
		protected override void Click() {
			startpos = Event.current.mousePosition;
			pressing = true;
			base.Click();
		}
		protected override void Release() {
			base.Release();
			startpos = Vector2.zero;
			pressing = false;
			OnHover?.Invoke(null);
		}

		protected override object[] GetRaycast() {
			{
				if(SceneView.mouseOverWindow == null)
					return null;
				if(SceneView.mouseOverWindow.ToString() == " (UnityEditor.SceneView)") {
					GameObject[] go = HandleUtility.PickRectObjects(GetSelectionRect(startpos, Event.current.mousePosition), false);

					List<object> objs = new List<object>();
					foreach(var a in go) {
						if(a != null) {
							Component cc = a.GetComponentInParent(t);
							if(cc != null)
								objs.Add(cc);
						}
					}
					return objs.ToArray();
				}
				return null;
			}
		}
		Rect GetSelectionRect(Vector2 start, Vector2 current) {
			Vector2 d = new Vector2(Mathf.Min(start.x, current.x), Mathf.Min(start.y, current.y));
			Vector2 p2 = new Vector2(Mathf.Max(start.x, current.x), Mathf.Max(start.y, current.y));
			return new Rect(d, p2 - d);

		}

		protected override void Update() {
			if(pressing)
				base.Update();
		}
		public override void Enable() {
			base.Enable();
			SceneInput.Instance.SceneLoop += Draw;
		}
		public override void Disable() {
			base.Disable();
			SceneInput.Instance.SceneLoop -= Draw;
		}

		void Draw() {
			if(!pressing)
				return;
			Vector2 v = Event.current.mousePosition;
			Vector3[] p = new Vector3[5];
			p[0] = GetPoint(startpos);
			p[1] = GetPoint((startpos) + (v - startpos).x * Vector2.right);
			p[2] = GetPoint(v);
			p[3] = GetPoint((startpos) + (v - startpos).y * Vector2.up);
			p[4] = GetPoint(startpos);
			Handles.DrawPolyLine(p);
			Handles.color = Color.white * 0.3f;
			Handles.DrawAAConvexPolygon(p[0], p[1], p[2], p[3]);
		}
	}
}
