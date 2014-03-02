using UnityEngine;
using System.Collections;

public class SnowmanChangeDialogue : MonoBehaviour {

    Dialogue d;

	// Use this for initialization
	void Start () {
        d = GetComponent<Dialogue>();

        //activate sprites if the snowman is already been completed
        if (Dialoguer.GetGlobalBoolean(5))
        {
            transform.FindChild("Snow_branches").gameObject.SetActive(true);
            transform.FindChild("Snow_butterfly").gameObject.SetActive(true);
            transform.FindChild("Snow_eyes").gameObject.SetActive(true);
            transform.FindChild("Snow_hat").gameObject.SetActive(true);
            transform.FindChild("Snow_mouth").gameObject.SetActive(true);
            transform.FindChild("Snow_nose").gameObject.SetActive(true);
        }
	}
	
	// Update is called once per frame
	void Update () {
	    //if we're in the warm section change dialogue to the snowman speaks
        if (Dialoguer.GetGlobalBoolean(7))
            d.dialogue = DialoguerDialogues.Thesnowmanspeaks;
	}
}
