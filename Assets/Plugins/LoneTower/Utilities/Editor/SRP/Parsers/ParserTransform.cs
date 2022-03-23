using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace LoneTower.Utility.SRP {
	public class ParserTransform : ParserBase {

		public ParserTransform(PickLogic picker) : base(picker) {
		}
		public ParserTransform() {

		}

		public override Vector3[] Selection {
			get { return picker.selection.Select(x => x.transform.position).ToArray(); }
		}

		public override Vector3 Hover {
			get {
				if(picker.hover != null)
					return picker.hover.transform.position;
				else return Vector3.zero;
			}
		}

	}
}