using UnityEngine;
using System.Collections;

public class DialogueManager : MonoBehaviour {

	public CustomGui customDialogue;
	
	private readonly string GLOBAL_VARIABLE_SAVE_KEY = "serialized_global_variable_state";

	//conditionals for scenes
	public static bool intro = true;
	public bool testNoIntro = false;
	public static bool firstSnow = true;
	public static bool interlude1 = true;
	public static bool warm =true;
	public static bool keepCold = true;

	static string dialoguerVariables;
	
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
		//cutscenes
		if (intro && Application.loadedLevelName == "Home") {
			GameObject.FindGameObjectWithTag("Player").GetComponent<PointClick>().canMove=false;
			startDialogue(DialoguerDialogues.Intro); 
			intro= false;
		}

		if (firstSnow && Application.loadedLevelName == "Outside") {
			GameObject.FindGameObjectWithTag("Player").GetComponent<PointClick>().canMove=false;
			startDialogue(DialoguerDialogues.Snow); 
			firstSnow= false;
			}

		if (interlude1 && Application.loadedLevelName == "Narrator") {
			startDialogue(DialoguerDialogues.Interlude); 
			interlude1= false;

				}

		if (warm && !interlude1 && Application.loadedLevelName == "Home") {
			GameObject.FindGameObjectWithTag("Player").GetComponent<PointClick>().canMove=false;
			startDialogue(DialoguerDialogues.Warm);
			warm = false;
		}

		if (keepCold && !warm && Application.loadedLevelName == "Outside") {
			GameObject.FindGameObjectWithTag("Player").GetComponent<PointClick>().canMove=false;
			startDialogue(DialoguerDialogues.KeepCold);
			keepCold = false;
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

	void OnDestroy(){
		PlayerPrefs.SetString(GLOBAL_VARIABLE_SAVE_KEY,Dialoguer.GetGlobalVariablesState());
	}
}
