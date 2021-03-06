using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using UnityEditor;
using UnityEditor.Compilation;
using UnityEngine;

namespace LoneTower.SRP {
	public class SRPSettings {

		public static SRPSettings Instance {
			get {
				if(instance == null)
					instance = new SRPSettings();
				return instance;
			}
		}
		static SRPSettings instance;


		static string assetPath = "Assets\\Editor Default Resources\\LoneTower\\SRP";
		static string assetName = @"Settings.asset";

		public static Color MarkColor {
			get {
				return Instance.data.choiceColor;
			}
			set {
				Instance.data.choiceColor = value;
			}
		}

		public static Color[] colors {
			get { return Instance.data.colorBank; }
			set {
				Instance.data.colorBank = value;
			}
		}
		public static float Scale {
			get { return Instance.data.scale; }
			set {
				Instance.data.scale = value;
			}

		}
		public static float LineScale {
			get { return Instance.data.lineScale; }
			set {
				Instance.data.lineScale = value;
			}
		}
		public static float MarkScale {
			get { return Instance.data.choiceSize; }
			set {
				Instance.data.choiceSize = value;
			}
		}

		SRPData data;

		public SRPSettings() {
			Load();
		}

		public void Save() {
			TextAsset text = new TextAsset(JsonUtility.ToJson(data));
			if(!Directory.Exists(assetPath))
				Directory.CreateDirectory(assetPath);
			AssetDatabase.CreateAsset(text, assetPath + "\\" + assetName);
		}

		public void Load() {
			if(!Directory.Exists(assetPath))
				Directory.CreateDirectory(assetPath);
			TextAsset a = AssetDatabase.LoadAssetAtPath<TextAsset>(assetPath + "\\" + assetName);

			if(a == null) {
				data = SRPData.defaultData;
				Save();
				Debug.LogError("<color=#ED1E79>[SRP]</color> Settings can't be loaded: restored defaults");
			} else {
				try {
					data = JsonUtility.FromJson<SRPData>(a.text);
				} catch {
					data = SRPData.defaultData;
					Save();
				Debug.LogError("<color=#ED1E79>[SRP]</color> Settings can't be loaded: restored defaults");
				}
			}
		}
	}

	[Serializable]
	struct SRPData {
		public Color choiceColor;
		public Color[] colorBank;
		public float scale;
		public float choiceSize;
		public float lineScale;
		public SRPData(Color choiceColor, Color[] colorBank, float scale, float lineScale, float choiceSize) {
			this.choiceColor = choiceColor;
			this.colorBank = colorBank;
			this.scale = scale;
			this.lineScale = lineScale;
			this.choiceSize = choiceSize;
		}
		public static SRPData defaultData = new SRPData(Color.blue, new Color[] { Color.blue, Color.red, Color.green, Color.magenta, Color.yellow, Color.cyan, Color.black }, 1, 0.1f, 0.2f);
	}
}