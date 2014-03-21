using UnityEngine;
using System.Collections;

public class MoodTrigger : MonoBehaviour {

    MoodSelector m;
    Animator anim;

	// Use this for initialization
	void Start () {
        m = GameObject.Find("MoodSelector").GetComponent<MoodSelector>();
        anim = gameObject.GetComponent<Animator>();
	}

    void OnMouseUpAsButton(){
        anim.SetTrigger("Click");
        m.SetMood(gameObject.name);
    }

    void OnMouseEnter() {
        anim.SetTrigger("OnMouse");
    }

    void OnMouseExit() {
        anim.SetTrigger("NoMouse");
    }
}
