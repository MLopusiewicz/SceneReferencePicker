using LoneTower.Utility.SRP;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LoneTower.Utility.SRP {
	public interface IDrawable {
		public bool IsHoverSelected { get; }
		public bool IsHovering { get; }
		public PickLogic.brushMode mode { get; }
		public Vector3[] Selection { get; }
		public Vector3 Hover { get; }
	}
}
