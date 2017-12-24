using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
Jason Jerome
12/24/2017
 */

namespace JJIS {
	//This script is used if a raycast is used to initiate an inspection. 
	public class InspectRayCast : MonoBehaviour {
		
		[Header("References")]
		public Camera mainCam;
		
		[Header("Properties")]
		[SerializeField]
		private float rayDist = 3.0f;

		//Initializes the script.
		private void Start() {
			if(!mainCam) {
				mainCam = Camera.main;
			}
		}

		//Handles user input to start inspection.
		private void Update () {
			if(Input.GetKeyDown(KeyCode.E)) {
				InspectRay();
			}
		}

		//Raycast to trigger inspection events.
		private void InspectRay() {
			RaycastHit hit;
			Ray ray = mainCam.ScreenPointToRay(Input.mousePosition);
			if(Physics.Raycast(ray, out hit, rayDist)) {
				if(hit.collider.CompareTag("InspectSignPost")) {
					BasicInspect inspectSignScript = hit.collider.GetComponent<BasicInspect>();
					inspectSignScript.InspectionTriggerToggle(true);
					inspectSignScript.StartInspection();
				}
			}
		}

	}
}
