using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JJIS {
	[System.Serializable]
	public class InspectObject : ScriptableObject {

		
		[SerializeField]
		private string scriptOwner;
		public string ScriptOwner {
			get {return scriptOwner;}
			set {scriptOwner = value;}
		}

		[Multiline]
		public string[] allScriptData;
				
	}
}
