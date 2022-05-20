using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LoneTower.SRP {
	public interface IMonoBehaviourBase {
		GameObject gameObject { get; }
		Transform transform { get; }
	}
}