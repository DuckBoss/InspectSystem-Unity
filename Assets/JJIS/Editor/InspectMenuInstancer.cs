﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace JJIS {
	public class InspectMenuInstancer : MonoBehaviour {

		[MenuItem("JJ_Systems/JJIS/New Inspect Script Asset")]
		public static void CreateMyAsset_Simple() {
			InspectObject asset = ScriptableObject.CreateInstance<InspectObject>();

			AssetDatabase.CreateAsset(asset, "Assets/JJIS/Inspect_Assets/NewInspectObject.asset");
			AssetDatabase.SaveAssets();
			AssetDatabase.Refresh();
			EditorUtility.FocusProjectWindow();
			Selection.activeObject = asset;
		}

	}
}
