using UnityEngine;
using System.Collections;

public class Snow : MonoBehaviour {

	
	// Update is called once per frame
	void Update () {
        if (GameObject.Find("Dialogue Manager").GetComponent<DialogueManager>().isWarm())
        {
            GameObject.Destroy(gameObject);
        }
	}
}
