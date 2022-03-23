using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace LoneTower.Utility.SRP {
	public class ParserMeshCenter : ParserBase {

		public ParserMeshCenter(PickLogic picker) : base(picker) {
		}


		public override Vector3[] Selection {
			get {
				if(picker.selection.ToArray() is MeshFilter[]) {
					return (picker.selection.ToArray() as MeshFilter[]).Select(x => x.sharedMesh.bounds.center + x.transform.position).ToArray();
				} else {
					Vector3[] a = new Vector3[picker.selection.Count];
					for(int i = 0; i < a.Length; i++)
						a[i] = GetPos(picker.selection[i]);

					return a;
				}
			}
		}

		public override Vector3 Hover {
			get {
				if(picker.hover != null)
					return GetPos(picker.hover);
				else
					return Vector3.zero;
			}
		}

		Vector3 GetPos(Component c) {
			var m = c.GetComponent<MeshFilter>();
			if(m == null) {
				Debug.LogError($"MeshFilter not found on: {c.gameObject.name} Falling back to: Transform");
				return c.transform.position;
			}
			return m.sharedMesh.bounds.center + c.transform.position;
		}
	}
}