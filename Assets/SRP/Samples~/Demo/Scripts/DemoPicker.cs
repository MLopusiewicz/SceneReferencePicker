using LoneTower.SRP;
using UnityEngine;

public class DemoPicker : MonoBehaviour {

	[SRP]
	public PickableList<DemoComponent> defaultBrush;

	[SRPSingle]
	public DemoComponent component;

	[SRPPath]
	public PickableList<DemoComponent> pathPick;


	[SRPTopDownSingle]
	public DemoComponent topDownSingle;

	[SRPTopDown]
	public PickableList<DemoComponent> topDownasdfas;

	[SRP("LoneTower.SRP.BrushPath",
			"LoneTower.SRP.ParserTransform",
			"LoneTower.SRP.DrawerPath",
			"LoneTower.SRP.ComponentPicker",
			"LoneTower.SRP.ComponentSerializer")]
	public PickableList<DemoComponent> pick;


	[SRPInterface(typeof(IDemoInterface))]
	public Component interfac;

	[SRPInterfaces(typeof(IDemoInterface))]
	public PickableList<Component> interfaces;
}








