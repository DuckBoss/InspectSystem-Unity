using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JJIS {
	public class InspectRayCast : MonoBehaviour {
		
		public Camera mainCam;
		
		[SerializeField]
		private float rayDist = 3.0f;

		private void Start() {
			if(!mainCam) {
				mainCam = Camera.main;
			}
		}

		// Update is called once per frame
		private void Update () {
			if(Input.GetKeyDown(KeyCode.E)) {
				InspectRay();
			}
		}

		private void InspectRay() {
			RaycastHit hit;
			Ray ray = mainCam.ScreenPointToRay(Input.mousePosition);
			if(Physics.Raycast(ray, out hit, rayDist)) {
				if(hit.collider.CompareTag("InspectSignPost")) {
					BasicInspect inspectSignScript = hit.collider.GetComponent<BasicInspect>();
					
					if(inspectSignScript.isReinspectable())
						inspectSignScript.StartInspection();
					else if(inspectSignScript.isFirstTime())
						inspectSignScript.StartInspection();
					else
						return;
				}
			}
		}

	}
}
