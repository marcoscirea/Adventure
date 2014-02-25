using UnityEngine;
using System.Collections;

public class Dialogue : Interaction {

	//public int dialogueNum = 3;
	public DialoguerDialogues dialogue;

	public override void Update ()
	{

	}
	public override void action(){
		//dm.startDialogue(dialogueNum);
		dm.startDialogue (dialogue);
	}

	public override void secondary(){
		//description code if necessary
	}

	//Deprecated
	public void startDialogue(int n){
		dm.startDialogue(n);
		Debug.Log ("called deprecated startDialogue for n=" + n);
	}

	public void startDialogue(DialoguerDialogues d){
		dm.startDialogue (d);
		}
}
