﻿using UnityEngine;
using System.Collections;

public class DialogueManager : MonoBehaviour {

	public CustomGui customDialogue;
	
	private readonly string GLOBAL_VARIABLE_SAVE_KEY = "serialized_global_variable_state";

	public static bool intro = true;
	public bool testNoIntro = false;
	
	//private string returnedString = string.Empty;
	
	void Start () {

		customDialogue = GameObject.FindGameObjectWithTag ("Gui").GetComponent<CustomGui> ();
		
		// Initialize the Dialoguer
		Dialoguer.Initialize();
		
		// If the Global Variables state already exists, LOAD it into Dialoguer
		if(PlayerPrefs.HasKey(GLOBAL_VARIABLE_SAVE_KEY)){
			Dialoguer.SetGlobalVariablesState(PlayerPrefs.GetString(GLOBAL_VARIABLE_SAVE_KEY));
			//returnedString = PlayerPrefs.GetString(GLOBAL_VARIABLE_SAVE_KEY);
		}
		//		This can be saved anywhere, and loaded from anywhere the user wishes
		//		To save the Global Variable State, get it with Dialoguer.GetGlobalVariableState() and save it where you wish
	
		if (testNoIntro)
			intro = false;
	}
	
	void Update () {
		//returnedString = Dialoguer.GetGlobalVariablesState();
		if (intro) {
			GameObject.FindGameObjectWithTag("Player").GetComponent<PointClick>().canMove=false;
			startDialogue(DialoguerDialogues.Intro); 
			intro= false;
		}

		//test for dialoguer variables
		/*for (int i=0; i<5; i++) {
			Debug.Log("variable "+i+ " " + Dialoguer.GetGlobalBoolean(i));
			}*/
	}

	public void startDialogue(int n){
		Dialoguer.events.ClearAll();
		customDialogue.addDialoguerEvents();
		Dialoguer.StartDialogue(n);	
	}

	public void startDialogue(DialoguerDialogues n){
		Dialoguer.events.ClearAll();
		customDialogue.addDialoguerEvents();
		Dialoguer.StartDialogue(n);	
	}
}
