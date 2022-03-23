
using LoneTower.Utility.SRP;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoPicker : MonoBehaviour {

	[SRP(typeof(PathPicker), "Default")]
	public MeshRenderer asdf;

	[SRP(typeof(PathPicker), "Default")]
	public ListDrawer<MeshRenderer> rends;

 
	//public ListDrawer<DemoComponent> demo;
}
