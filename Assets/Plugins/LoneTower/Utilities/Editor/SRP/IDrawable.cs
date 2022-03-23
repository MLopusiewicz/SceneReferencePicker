using LoneTower.Utility.SRP;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LoneTower.Utility.SRP {
	public interface IDrawable {
		public bool IsHoverSelected { get; }
		public bool IsHovering { get; }
		public PickerBase.brushMode mode { get; }
		public Vector3[] Selection { get; }
		public Vector3[] Choices { get; }
		public Vector3 Hover { get; }
	}
}
