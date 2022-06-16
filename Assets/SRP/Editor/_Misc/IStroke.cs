using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static LoneTower.SRP.BrushBase;

namespace LoneTower.SRP {
	public interface IStroke {
		public event Action<object[]> OnStrokeEnd, OnStroke, OnStrokeStart;
		public brushMode mode { get; }
	}
}