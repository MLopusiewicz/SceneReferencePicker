using LoneTower.SRP;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoPicker : MonoBehaviour {

	[SRP(typeof(SinglePicker))]
	public DemoComponent singlePick;

	[SRP(typeof(PathPicker))]
	public PickableList<DemoComponent> pathPick;

	[SRP(typeof(TopDownPicker))]
	public PickableList<MeshRenderer> topDown;

	[SRP(typeof(TopDownSinglePicker))]
	public MeshRenderer topDownSingle;

} 








