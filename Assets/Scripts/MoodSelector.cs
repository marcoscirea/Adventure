using UnityEngine;
using System.Collections;

public class MoodSelector : MonoBehaviour {

    static string activeMood = "Sad";
    bool idle = true;
    float timerStart;
    float timerLenght = 2f;

    private Animator animator;

	// Use this for initialization
	void Start () {
        animator = GetComponent<Animator>();

        transform.FindChild(activeMood).renderer.sortingOrder = 2;

        transform.position = Camera.main.ViewportToWorldPoint(new Vector3(0.95f, 0.075f));
        transform.position += new Vector3(0, 0, 1f);
	}
	
	// Update is called once per frame
	void Update () {
	    if (!idle && Time.time >= timerLenght + timerStart)
        {
            animator.SetTrigger("BackToIdle");
            idle = true;
        }
	}

    void OnMouseEnter(){
        if (idle)
        {
            idle = false;
            timerStart = Time.time;
            animator.SetTrigger("OnMouse");
        }
    }

    public void SetMood(string mood){
        Logger.mood(mood);

        transform.FindChild(activeMood).renderer.sortingOrder = 1;
        activeMood = mood;
        transform.FindChild(activeMood).renderer.sortingOrder = 2;

        timerStart = 0;
        animator.SetTrigger("BackToIdle");
        idle = true;
    }

    public void ProlongTimer(){
        timerStart = Time.time;
    }
}
