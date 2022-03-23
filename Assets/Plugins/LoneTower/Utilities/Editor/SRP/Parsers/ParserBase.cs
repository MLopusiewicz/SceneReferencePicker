using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using static LoneTower.Utility.SRP.PickerBase;

namespace LoneTower.Utility.SRP {
	public abstract class ParserBase : IDrawable {
		public PickerBase picker;
		protected ParserBase(PickerBase picker) {
			this.picker = picker;
		}

		public bool IsHovering { get { return picker.hover != null; } }

		public bool IsHoverSelected {
			get { return picker.selection.Contains(picker.hover); }
		}

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
		public Vector3 Hover { get { return GetPos(picker.hover); } }

		public Vector3[] Choices {
			get {
				Vector3[] v = new Vector3[picker.input.possible.Length];
				for(int i = 0; i < picker.input.possible.Length; i++) {
					v[i] = GetPos(picker.selection[i]);
				}
				return v;
			}
		}

		protected abstract Vector3 GetPos(Component t);
	}

}