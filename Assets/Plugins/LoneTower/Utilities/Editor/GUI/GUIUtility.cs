using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LoneTower.Utility.GUI {
	public static class GUIUtility {
		public static Rect ColorField(Rect r, Color c) {
			Rect t = r;
			t.size = new Vector2(10, 10);
			t.y += 5;
			t.x -= 20;

			UnityEngine.GUI.contentColor = c;
			UnityEngine.GUI.backgroundColor = c;
			UnityEngine.GUI.color = c;
			GUIStyle a = new GUIStyle() {
				stretchHeight = true,
				stretchWidth = true
			};

			UnityEngine.GUI.Box(t, new GUIContent(Tex(c)), a);//GUI.skin.button);


			UnityEngine.GUI.backgroundColor = Color.white;
			UnityEngine.GUI.color = Color.white;
			UnityEngine.GUI.contentColor = Color.white;
			return r;
		}
		static Texture Tex(Color c) {
			Texture2D a = new Texture2D(20, 20);
			for(int x = 0; x < 20; x++)
				for(int y = 0; y < 20; y++)
					a.SetPixel(x, y, c);

			a.Apply();
			return a;
		}
	}
}