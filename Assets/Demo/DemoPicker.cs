using LoneTower.SRP;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoPicker : MonoBehaviour {

	[SRPDefaultBrush]
	public DemoComponent singlePick;

	[SRPTopDown]
	public PickableList<DemoComponent> pathPick;
}








