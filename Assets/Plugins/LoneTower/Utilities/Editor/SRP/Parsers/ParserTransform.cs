using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace LoneTower.Utility.SRP {
	public class ParserTransform : ParserBase {

		public ParserTransform(PickerBase picker) : base(picker) {
		}

		protected override Vector3 GetPos(Component t) {
			return t.transform.position;
		}
	}
}