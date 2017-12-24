using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
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

		[Header("Character Scroll Time")]
		public float timer = 0.2f;


		private bool canTriggerDialogue;
		private bool scriptIsActive;
		private bool lineIsComplete;
		private int scriptCapacity;
		private int scriptCurrent;

		public void StartInspection () {
			Debug.Log("Starting Inspection...");
			DialogueTriggerToggle(false);
			player.GetComponent<FirstPersonController>().enabled = false;

			dialoguePanel.SetActive(true);
			scriptCurrent = 0;
			scriptIsActive = true;
			scriptTitle.text = inspectScript.ScriptOwner;
			scriptCapacity = inspectScript.allScriptData.Length;
			scriptSubText.text = "<...>";
			anim.SetTrigger("FadeIn");
			RunDialogue();
		}
		public void KillInspection() {
			KillDialogue();
		}

		public void DialogueTriggerToggle(bool val) {
			canTriggerDialogue = val;
		}
		public bool GetDialogueTrigger() {
			return canTriggerDialogue;
		}

		private void Update() {
			if(Input.GetMouseButtonDown(0) && scriptIsActive) {
				if(scriptCurrent < scriptCapacity) {
					RunDialogue();
				}
				else {
					KillDialogue();
				}
			}
		}

		private void RunDialogue() {
			StopAllCoroutines();
			if(lineIsComplete) {
				StartCoroutine(NextDialogue(scriptCurrent));
			}
			else {
				CurrentDialogue(scriptCurrent);
			}
		}
		private void KillDialogue() {
			StopAllCoroutines();
			anim.SetTrigger("FadeOut");
			scriptCurrent = 0;
			lineIsComplete = false;
			dialoguePanel.SetActive(false);
			
			player.GetComponent<FirstPersonController>().enabled = true;
			scriptIsActive = false;
			DialogueTriggerToggle(true);
		}

		private void CurrentDialogue(int currentIndex) {
			string fullString;
			fullString = inspectScript.allScriptData[currentIndex];
			scriptText.text = fullString;
			sfx_src.pitch = 1;
			sfx_src.PlayOneShot(sfxClip);
			lineIsComplete = true;
			scriptCurrent++;
			scriptSubText.text = "<...>";
		}


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
