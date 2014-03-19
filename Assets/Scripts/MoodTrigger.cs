using UnityEngine;
using System.Collections;

public class MoodTrigger : MonoBehaviour {

    MoodSelector m;

	// Use this for initialization
	void Start () {
        m = GameObject.Find("MoodSelector").GetComponent<MoodSelector>();
	}

    void OnMouseUpAsButton(){
        m.SetMood(gameObject.name);
    }
}
