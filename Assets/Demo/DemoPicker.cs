
using LoneTower.Utility.SRP;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoPicker : MonoBehaviour {

	[SRP(typeof(SinglePicker))]
	public DemoComponent asdf;

	[SRP(typeof(PathPicker))]
	public ListDrawer<DemoComponent> asdfasdfasd;

	//[SRP(typeof(PathPicker))]
	//public ListDrawer<MeshRenderer> rends;

}
