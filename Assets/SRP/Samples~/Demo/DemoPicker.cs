using LoneTower.Utility.SRP;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoPicker : MonoBehaviour {

	[SRP(typeof(SinglePicker))]
	public DemoComponent singlePick;

	[SRP(typeof(PathPicker))]
	public ListDrawer<DemoComponent> pathPick;

	[SRP(typeof(TopDownPicker))]
	public ListDrawer<MeshRenderer> topDown;

	[SRP(typeof(TopDownSinglePicker))]
	public MeshRenderer topDownSingle;

}









