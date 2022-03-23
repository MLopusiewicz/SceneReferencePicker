using LoneTower.Utility;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SRPSettings : Singleton<SRPSettings> {
	static string assetPath = @"Assets\Plugins\LoneTower\Utilities\Editor\SRP\Settings\Settings.asset";


	public static Color choiceColor {
		get {
			return Instance.data.choiceColor;
		}
		set {
			Instance.data.choiceColor = value;
			Instance.Save();
		}
	}

	public SelectionBank<Color> colorBank;

	SRPData data;
	public SRPSettings() {
		Load();
		colorBank = new SelectionBank<Color>(data.colorBank);
	}

	public void Save() {
		TextAsset text = new TextAsset(JsonUtility.ToJson(data));
		AssetDatabase.CreateAsset(text, assetPath);
	}

	public void Load() {
		TextAsset a = AssetDatabase.LoadAssetAtPath<TextAsset>(assetPath);

		if(a == null) {
			data = SRPData.defaultData;
			Save();
		} else {
			try {
				data = JsonUtility.FromJson<SRPData>(a.text);
			} catch {
				data = SRPData.defaultData;
				Save();
				Debug.LogError("[SRP] Settings can't be loaded: RESETED");
			}
		}

	}
}

[Serializable]
struct SRPData {
	public Color choiceColor;
	public Color[] colorBank;

	public SRPData(Color choiceColor, Color[] colorBank) {
		this.choiceColor = choiceColor;
		this.colorBank = colorBank;
	}
	public static SRPData defaultData = new SRPData(Color.black, new Color[] { Color.blue, Color.red, Color.green, Color.magenta, Color.yellow, Color.cyan, Color.black });
}
