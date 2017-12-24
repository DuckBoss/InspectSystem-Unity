using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JJIS {
	public class InspectTrigger : MonoBehaviour {
		
		public BasicInspect scriptOwner;

		public void OnTriggerEnter(Collider col) {
			if(col.CompareTag("Player")) {
				scriptOwner.DialogueTriggerToggle(true);
			}
		}

		private void OnTriggerExit(Collider col) {
			if(col.CompareTag("Player")) {
				if(scriptOwner.GetDialogueTrigger()) {
					scriptOwner.DialogueTriggerToggle(false);
				}
			}
		}

		private void Update() {
			if(Input.GetKeyDown(KeyCode.E) && scriptOwner.GetDialogueTrigger()) {
				GetComponent<BasicInspect>().StartInspection();
			}
		}


	}
}