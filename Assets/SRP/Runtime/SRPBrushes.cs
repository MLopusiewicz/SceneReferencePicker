using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
namespace LoneTower.SRP {

	public class SRPDefaultBrush : SRPAttribute {
		public SRPDefaultBrush() : base("BrushMain", "ParserTransform", "DrawerGeneric", "ComponentPicker", "ComponentSerializer") {

		}
	}
	public class SRPPath : SRPAttribute {
		public SRPPath() : base("BrushPath", "ParserTransform", "DrawerPath", "ComponentPicker", "ComponentSerializer") {

		}
	}


	public class SRPSingle : SRPAttribute {
		public SRPSingle() : base("BrushSingle", "ParserTransform", "DrawerGeneric", "ComponentPicker", "ComponentSerializer") {

		}
	}
	public class SRPTopDown : SRPAttribute {
		public SRPTopDown() : base("BrushMain", "ParserTransform", "DrawerTopDown", "ComponentPicker", "ComponentSerializer") {

		}
	}
	public class SRPTopDownSingle : SRPAttribute {
		public SRPTopDownSingle() : base("BrushSingle", "ParserTransform", "DrawerTopDown", "ComponentPicker", "ComponentSerializer") {
		}
	}
}