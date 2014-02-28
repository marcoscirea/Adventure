using UnityEngine;
using System.Collections;

public class SnowmanChangeDialogue : MonoBehaviour {

    Dialogue d;

	// Use this for initialization
	void Start () {
        d = GetComponent<Dialogue>();
	}
	
	// Update is called once per frame
	void Update () {
	    //if we're in the warm section change dialogue to the snowman speaks
        if (Dialoguer.GetGlobalBoolean(7))
            d.dialogue = DialoguerDialogues.Thesnowmanspeaks;
	}
}
