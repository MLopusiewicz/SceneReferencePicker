using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using static LoneTower.Utility.SRP.PickLogic;

namespace LoneTower.Utility.SRP {
	public abstract class ParserBase : IDrawable {
		public PickLogic picker;
		protected ParserBase(PickLogic picker) {
			this.picker = picker;
		}
		protected ParserBase() {
		}

		public bool IsHovering { get { return picker.hover != null; } }

		public bool IsHoverSelected {
			get { return picker.selection.Contains(picker.hover); }
		}

		public brushMode mode { get { return picker.mode; } }

		public abstract Vector3[] Selection { get; }

		public abstract Vector3 Hover { get; }
	}
}