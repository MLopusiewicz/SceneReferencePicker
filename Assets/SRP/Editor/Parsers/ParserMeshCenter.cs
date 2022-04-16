using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace LoneTower.SRP {
	public class ParserMeshCenter : ParserBase {

		public ParserMeshCenter(PickerBase picker) : base(picker) {
		}

		protected override Vector3 GetPos(SelectionContainer t) {
			if(!(t.obj is Component)) {
				throw new System.Exception($"[SRP] Wrong DTO. Expected {typeof(Component)} was:  {t.obj.GetType().Name} ");
			}

			Component c = (Component)t.obj;
			var m = c.GetComponent<MeshFilter>();
			if(m == null) {
				Debug.LogError($"MeshFilter not found on: {c.gameObject.name} Falling back to: Transform");
				return c.transform.position;
			}
			return m.sharedMesh.bounds.center + c.transform.position;

		}
	}
}