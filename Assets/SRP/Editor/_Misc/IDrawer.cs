using UnityEngine;

namespace LoneTower.SRP {
	public interface IDrawer {
		Color color { get; } 
		public bool userShowSelection { get; set; }
		public bool showMarks { get; set; }
		public bool showHandle { get; set;  }
	}
}