using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityStandardAssets.Characters.FirstPerson;

/**
Jason Jerome
12/24/2017
 */

namespace JJIS {
	public class BasicInspect : MonoBehaviour {

		[Header("Main References")]
		public InspectObject inspectScript;
		public GameObject player;

		[Header("UI References")]
		public TMP_Text scriptTitle;
		public TMP_Text scriptText;
		public TMP_Text scriptSubText;

		[Header("Animation References")]
		public Animator anim;
		public AudioSource sfx_src;
		public AudioClip sfxClip;
		public GameObject dialoguePanel;

		[Header("SFX Pitch Modulation")]
		[Range(0.9f, 1.0f)]
		public float minPitchMod;
		[Range(1.0f, 1.1f)]
		public float maxPitchMod;

		[Header("Character Scroll Time")]
		public float timer = 0.2f;

		//Internal variables to handle script execution.
		private bool canTriggerDialogue;
		private bool scriptIsActive;
		private bool lineIsComplete;
		private int scriptCapacity;
		private int scriptCurrent;

		//Initiates the inspection process.
		public void StartInspection () {
			Debug.Log("Starting Inspection...");
			InspectionTriggerToggle(false);
			player.GetComponent<FirstPersonController>().enabled = false;

			dialoguePanel.SetActive(true);
			scriptCurrent = 0;
			scriptIsActive = true;
			scriptTitle.text = inspectScript.ScriptOwner;
			scriptCapacity = inspectScript.allScriptData.Length;
			scriptSubText.text = "<...>";
			anim.SetTrigger("FadeIn");
			RunInspection();
		}
		//Sets the boolean that checks if inspection can start.
		public void InspectionTriggerToggle(bool val) {
			canTriggerDialogue = val;
		}

		//Gets the boolean that checks if inspection can start.
		public bool GetInspectionTrigger() {
			return canTriggerDialogue;
		}
		
		//Handles user input to initiate inspection.
		private void Update() {
			if(Input.GetMouseButtonDown(0) && scriptIsActive) {
				if(scriptCurrent < scriptCapacity) {
					RunInspection();
				}
				else {
					KillInspection();
				}
			}
		}

		//Handles the inspection process line by line.
		private void RunInspection() {
			StopAllCoroutines();
			if(lineIsComplete) {
				StartCoroutine(NextDialogue(scriptCurrent));
			}
			else {
				CurrentInspection(scriptCurrent);
			}
		}
		
		//Kills an existing inspection process.
		private void KillInspection() {
			StopAllCoroutines();
			anim.SetTrigger("FadeOut");
			scriptCurrent = 0;
			lineIsComplete = false;
			dialoguePanel.SetActive(false);
			
			player.GetComponent<FirstPersonController>().enabled = true;
			scriptIsActive = false;
			InspectionTriggerToggle(true);
		}

		//Handles the current inspection script data when a line isn't finished scrolling.
		private void CurrentInspection(int currentIndex) {
			string fullString;
			fullString = inspectScript.allScriptData[currentIndex];
			scriptText.text = fullString;
			sfx_src.pitch = 1;
			sfx_src.PlayOneShot(sfxClip);
			lineIsComplete = true;
			scriptCurrent++;
			scriptSubText.text = "<...>";
		}

		//Scrolls through all the letters in the line of text
		//and handles html tag formatting.
		IEnumerator NextDialogue(int currentIndex) {
			string fullString = "";
			bool markup = false;
			lineIsComplete = false;

			while(!lineIsComplete) {
				for(int i = 0; i < inspectScript.allScriptData[currentIndex].Length; i++) {
					if(inspectScript.allScriptData[currentIndex][i] == '<') {
						markup = true;
					}
					if(markup) {
						fullString += inspectScript.allScriptData[currentIndex][i];
						if(inspectScript.allScriptData[currentIndex][i] == '>') {
							markup = false;
						}
					}
					else {
						fullString += inspectScript.allScriptData[currentIndex][i];
						scriptText.text = fullString;
						sfx_src.PlayOneShot(sfxClip);
						sfx_src.pitch = Random.Range(minPitchMod,maxPitchMod);
						yield return new WaitForSeconds(timer);
					}
				}
				if(scriptCurrent < scriptCapacity-1) {
					scriptSubText.text = "<...>";
				}
				else {
					scriptSubText.text = "<END>";
				}
				lineIsComplete = true;
			}
			scriptCurrent++;
			yield return 0f;
		}


	}
}
