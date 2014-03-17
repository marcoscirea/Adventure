using UnityEngine;
using System.Collections;

public class Sky : MonoBehaviour {

    public Material sun;
	
	// Update is called once per frame
	void Update () {
        if (renderer.material != sun && GameObject.Find("Dialogue Manager").GetComponent<DialogueManager>().isWarm())
            renderer.material = sun;
	}
}
