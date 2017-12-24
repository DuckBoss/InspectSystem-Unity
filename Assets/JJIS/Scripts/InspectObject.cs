using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
Jason Jerome
12/24/2017
 */

namespace JJIS {
	[System.Serializable]
	public class InspectObject : ScriptableObject {

		//Owner of the inspection script.
		[SerializeField]
		private string scriptOwner;
		public string ScriptOwner {
			get {return scriptOwner;}
			set {scriptOwner = value;}
		}

		//Individual strings of inspection lines.
		[Multiline]
		public string[] allScriptData;
		
	}
}
