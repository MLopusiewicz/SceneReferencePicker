using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LoneTower.Utility.Editor {
	public class LoneTowerGUI {
		public static Texture2D MakeTex(int width, int height, Color col) {
			Color[] pix = new Color[width * height];
			for(int i = 0; i < pix.Length; ++i) {
				pix[i] = col;
			}
			Texture2D result = new Texture2D(width, height);
			result.SetPixels(pix);
			result.Apply();
			return result;
		}
	}
}