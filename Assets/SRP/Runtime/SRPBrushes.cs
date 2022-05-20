using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
namespace LoneTower.SRP {

	public class SRPPath : SRPAttribute {
		public SRPPath() : base(
			"LoneTower.SRP.BrushPath",
			"LoneTower.SRP.ParserTransform",
			"LoneTower.SRP.DrawerPath",
			"LoneTower.SRP.ComponentPicker",
			"LoneTower.SRP.ComponentSerializer") {
		}
	}


	public class SRPSingle : SRPAttribute {
		public SRPSingle() : base(
			"LoneTower.SRP.BrushSingle",
			"LoneTower.SRP.ParserTransform",
			"LoneTower.SRP.DrawerGeneric",
			"LoneTower.SRP.ComponentPicker",
			"LoneTower.SRP.ComponentSerializer") {

		}
	}
	public class SRPTopDown : SRPAttribute {
		public SRPTopDown() : base(
			"LoneTower.SRP.BrushMain",
			"LoneTower.SRP.ParserTransform",
			"LoneTower.SRP.DrawerTopDown",
			"LoneTower.SRP.ComponentPicker",
			"LoneTower.SRP.ComponentSerializer") {

		}
	}
	public class SRPTopDownSingle : SRPAttribute {
		public SRPTopDownSingle() : base(
			"LoneTower.SRP.BrushSingle",
			"LoneTower.SRP.ParserTransform",
			"LoneTower.SRP.DrawerTopDown",
			"LoneTower.SRP.ComponentPicker",
			"LoneTower.SRP.ComponentSerializer") {
		}
	}


	public class SRPInterface : SRPAttribute {
		public SRPInterface() : base(
			"LoneTower.SRP.BrushMain",
			"LoneTower.SRP.ParserTransform",
			"LoneTower.SRP.DrawerGeneric",
			"LoneTower.SRP.InterfacePicker",
			"LoneTower.SRP.ComponentSerializer") {
			selectType = typeof(IMonoBehaviourBase);
		}
	}
}