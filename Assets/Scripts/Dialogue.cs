using UnityEngine;
using System.Collections;

public class Dialogue : Interaction {

	public int dialogueNum = 3;

	public override void Update ()
	{

	}
	public override void action(){
		dm.startDialogue(dialogueNum);
	}
}
