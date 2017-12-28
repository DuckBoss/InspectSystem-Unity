using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;
using MovementEffects;
using UnityStandardAssets.Characters.FirstPerson;

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

		[Header("Inspection Properties")]
		// Determines if an object can be inspected multiple times.
		[Tooltip("Determines if an object can be reinspected.")]
		public bool canReinspect;
		// Event will execute at the end of reinspectable objects if true, otherwise it only executes at the end of
		// non reinspectable objects.
		[Tooltip("Event will execute at the end of the reinspectable dialogue if true, otherwise it executes at the end of the non-reinspectable dialogue.")]
		public bool invokeEventReinspectable;
		// This event will execute at the end of the inspection dialogue if used.
		[Tooltip("Event to execute at the end of dialogue.")]
		public UnityEvent inspectEvent;

		private bool firstTimeInspection = true;
		private bool scriptIsActive;
		private bool lineIsComplete;
		private int scriptCapacity;
		private int scriptCurrent;

		//Starts the inspection process.
		public void StartInspection () {
			Debug.Log("Starting Inspection...");
			player.GetComponent<FirstPersonController>().enabled = false;

			firstTimeInspection = false;

			dialoguePanel.SetActive(true);
			scriptCurrent = 0;
			scriptIsActive = true;
			scriptTitle.text = inspectScript.ScriptOwner;
			scriptCapacity = inspectScript.allScriptData.Length;
			scriptSubText.text = "<...>";
			anim.SetTrigger("FadeIn");
			RunDialogue();
		}

		//Handles user input
		private void Update() {
			//If you left click while the text is scrolling, it will skip and display the full text.
			if(Input.GetMouseButtonDown(0) && scriptIsActive) {
				if(scriptCurrent < scriptCapacity) {
					RunDialogue();
				}
				else {
					KillInspection();
				}
			}
		}

		//Checks for first time inspection.
		public bool isFirstTime() {
			return firstTimeInspection;
		}
		//Checks for reinspectable objects.
		public bool isReinspectable() {
			return canReinspect;
		}

		//Runs the dialogue based on the completion of the line of text.
		private void RunDialogue() {
			Timing.KillCoroutines();
			if(lineIsComplete) {
				Timing.RunCoroutine(NextDialogue(scriptCurrent));
			}
			else {
				Timing.RunCoroutine(CurrentDialogue(scriptCurrent));
			}
		}

		//Kills any active dialogue.
		private void KillInspection() {
			Timing.KillCoroutines();
			anim.SetTrigger("FadeOut");
			scriptCurrent = 0;
			lineIsComplete = false;
			dialoguePanel.SetActive(false);
			
			player.GetComponent<FirstPersonController>().enabled = true;
			scriptIsActive = false;

			if(canReinspect) {
				if(invokeEventReinspectable) {
					if(inspectEvent != null)
						inspectEvent.Invoke();
				}
			}
			if(!canReinspect) {
				if(inspectEvent != null)
					inspectEvent.Invoke();
			}
			
		}

		//This method skips the scrolling and displays the full dialogue line.
		IEnumerator<float> CurrentDialogue(int currentIndex) {
			string fullString;
			fullString = inspectScript.allScriptData[currentIndex];
			scriptText.text = fullString;
			sfx_src.pitch = 1;
			sfx_src.PlayOneShot(sfxClip);
			lineIsComplete = true;
			scriptCurrent++;
			scriptSubText.text = "<...>";
			yield return 0f;
		}

		// Handles time between scrolling letters.
		private float timeBetweenLetters = 0.03f;

		//This method scrolls through the dialogue line and displays letter-by-letter.
		IEnumerator<float> NextDialogue(int currentIndex) {
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
						yield return Timing.WaitForSeconds(timeBetweenLetters);
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
