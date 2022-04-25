using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
namespace LoneTower.SRP {

	public class SRPDefaultBrush : SRPAttribute {
		public SRPDefaultBrush() : base("LogicMain", "ParserTransform", "DrawerGeneric", "ComponentPicker", "ComponentSerializer") {

		}
	}
	public class SRPPath : SRPAttribute {
		public SRPPath() : base("LogicPath", "ParserTransform", "DrawerPath", "ComponentPicker", "ComponentSerializer") {

		}
	}
	public class SRPPathSingle : SRPAttribute {
		public SRPPathSingle() : base("LogicSingle", "ParserTransform", "DrawerPath", "ComponentPicker", "ComponentSerializer") {

		}
	}
	public class SRPSingle : SRPAttribute {
		public SRPSingle() : base("LogicSingle", "ParserTransform", "DrawerGeneric", "ComponentPicker", "ComponentSerializer") {

		}
	}
	public class SRPTopDown : SRPAttribute {
		public SRPTopDown() : base("LogicMain", "ParserTransform", "DrawerTopDown", "ComponentPicker", "ComponentSerializer") {

		}
	}
	public class SRPTopDownSingle : SRPAttribute {
		public SRPTopDownSingle() : base("LogicSingle", "ParserTransform", "DrawerTopDown", "ComponentPicker", "ComponentSerializer") {
		}
	}
}