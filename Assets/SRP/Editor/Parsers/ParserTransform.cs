using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace LoneTower.SRP {
	public class ParserTransform : ParserBase {

		public ParserTransform(PickerBase picker) : base(picker) {
		}

		protected override Vector3 GetPos(SelectionContainer t) {
			if(!(t is ComponentContainer)) {
				throw new System.Exception($"[SRP] Wrong DTO. Expected {typeof(ComponentContainer)} was:  {t.GetType().Name} ");
			}
			return (t.obj as Component).transform.position;
		}
	}
}