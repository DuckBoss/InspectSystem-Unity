using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
Jason Jerome
12/24/2017
 */

namespace JJIS {
	//This script is used if a trigger is used to initiate an inspection.
	public class InspectTrigger : MonoBehaviour {

		[Header("References")]
		public BasicInspect scriptOwner;

		//Allows inspection events to occur within trigger area.
		public void OnTriggerEnter(Collider col) {
			if(col.CompareTag("Player")) {
				scriptOwner.InspectionTriggerToggle(true);
			}
		}

		//Prevents inspection events to occur when outside of trigger area.
		private void OnTriggerExit(Collider col) {
			if(col.CompareTag("Player")) {
				if(scriptOwner.GetInspectionTrigger()) {
					scriptOwner.InspectionTriggerToggle(false);
				}
			}
		}
		
		//Handles user input to initiate an inspection.
		private void Update() {
			if(Input.GetKeyDown(KeyCode.E) && scriptOwner.GetInspectionTrigger()) {
				GetComponent<BasicInspect>().StartInspection();
			}
		}


	}
}