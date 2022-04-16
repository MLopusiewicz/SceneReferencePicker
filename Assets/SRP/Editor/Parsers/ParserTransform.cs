using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace LoneTower.SRP {
	public class ParserTransform : ParserBase {

		public ParserTransform(LogicBase picker) : base(picker) {
		}

		protected override Vector3 GetPos(object t) {
			if(!(t is Component)) {
				throw new System.Exception($"[SRP] Wrong DTO. Expected {typeof(Component)} was:  {t.GetType().Name} ");
			}
			return (t as Component).transform.position;
		}
	}
}