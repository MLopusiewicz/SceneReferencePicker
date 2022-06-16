using UnityEngine;

namespace LoneTower.SRP {
	public interface IDrawer {
		Color color { get; } 
		public bool showSelection { get; }
		public bool showMarks { get; }
		public bool showHandle { get; }
	}
}