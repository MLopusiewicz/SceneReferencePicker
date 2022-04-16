using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using static LoneTower.SRP.PickerBase;

namespace LoneTower.SRP {
	public abstract class ParserBase {
		public PickerBase picker;
		protected ParserBase(PickerBase picker) {
			this.picker = picker;
		}

		public bool IsHovering { get { return picker.hover != null; } }

		public brushMode mode { get { return picker.mode; } }

		public Vector3[] Selection {
			get {
				Vector3[] v = new Vector3[picker.selection.Count];
				for(int i = 0; i < picker.selection.Count; i++) {
					v[i] = GetPos(picker.selection[i]);
				}
				return v;
			}
		}
		public Vector3[] Hover {
			get {
				Vector3[] pos = new Vector3[picker.hover.Length];
				for(int i = 0; i < picker.hover.Length; i++) {
					pos[i] = GetPos(picker.hover[i]);
				}
				return pos;
			}
		}

		public Vector3[] Choices {
			get {
				if(picker.input.possible == null)
					return new Vector3[0];
				Vector3[] v = new Vector3[picker.input.possible.Length];
				for(int i = 0; i < picker.input.possible.Length; i++) {
					v[i] = GetPos(picker.input.possible[i]);
				}
				return v;
			}
		}

		protected abstract Vector3 GetPos(object t);
	}

}