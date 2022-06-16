using UnityEditor;
using UnityEngine;

namespace LoneTower.SRP {
	public class SRPGUIDrawer {
		SRPController srp;
		VisibilityButton visibility;
		PaintButton paintButton;
		public SRPGUIDrawer(SRPController srp) {
			this.srp = srp;

			visibility = new VisibilityButton();
			paintButton = new PaintButton();

			visibility.OnEnable += srp.VisibilityOff;
			visibility.OnDisable += srp.VisibilityOn;


			paintButton.OnEnable += srp.PaintOn;
			paintButton.OnDisable += srp.PaintOff;

		}
		public Rect InspectorDraw(Rect position, string label) {

			visibility.state = !srp.State.visible;
			paintButton.state = srp.State.enabled;

			position = EditorGUI.PrefixLabel(position, UnityEngine.GUIUtility.GetControlID(FocusType.Passive), new GUIContent(label));

			GUITools.ColorField(position, srp.drawer.color);
			position = paintButton.PropertyDraw(position);
			position = visibility.PropertyDraw(position);

			return position;
		}
		~SRPGUIDrawer() {
			visibility.OnEnable -= srp.VisibilityOff;
			visibility.OnDisable -= srp.VisibilityOn;


			paintButton.OnEnable -= srp.PaintOn;
			paintButton.OnDisable -= srp.PaintOff;
		}

	}

}