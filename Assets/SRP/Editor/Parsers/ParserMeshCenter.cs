using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace LoneTower.SRP {
	public class ParserMeshCenter : ParserBase {

		public ParserMeshCenter(PickerBase picker) : base(picker) {
		}

		protected override Vector3 GetPos(Component t) {
			var m = t.GetComponent<MeshFilter>();
			if(m == null) {
				Debug.LogError($"MeshFilter not found on: {t.gameObject.name} Falling back to: Transform");
				return t.transform.position;
			}
			return m.sharedMesh.bounds.center + t.transform.position;

		}
	}
}