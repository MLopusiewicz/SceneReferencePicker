using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace LoneTower.SRP {
	public class ParserTransform : ParserBase {

		public ParserTransform(BrushBase brush) : base(brush) {
		}

		protected override Vector3 GetPos(object t) {
			if(!(t is Component)) {
				throw new System.Exception($"[SRP] Wrong type. Expected {typeof(Component)} was:  {t.GetType().Name} ");
			}
			return (t as Component).transform.position; 
		}
	}
}