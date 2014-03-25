using UnityEngine;
using System.Collections;

public class MoodTrigger : MonoBehaviour {

    MoodSelector m;
    Animator anim;

    public float timer = 0;
    public bool timerOn = false;

    CustomGui gui;

	// Use this for initialization
	void Start () {
        m = GameObject.Find("MoodSelector").GetComponent<MoodSelector>();
        anim = gameObject.GetComponent<Animator>();
        gui = GameObject.FindGameObjectWithTag("Gui").GetComponent<CustomGui>();
	}

    void Update(){
        if (timerOn && timer < Time.time)
        {
            timerOn = false;
            anim.SetTrigger("NoMouse");
            gui.pause = false;
        }
    }

    void OnMouseUpAsButton(){
        anim.SetTrigger("Click");
        m.SetMood(gameObject.name);
    }

    /*void OnMouseEnter() {
        anim.SetTrigger("OnMouse");
    }

    void OnMouseExit() {
        anim.SetTrigger("NoMouse");
    }*/

    void OnMouseOver() {
        if (!timerOn)
            anim.SetTrigger("OnMouse");
        timerOn = true;
        timer = Time.time + 1;
        gui.pause = true;

        m.ProlongTimer();
    }
}
