using UnityEngine;
using System.Collections;

public class Snowman : MonoBehaviour {

    static bool eyes = false;
    static bool mouth = false;
    static bool arms = false;
    static bool nose = false;
    static bool hat = false;

	// Use this for initialization
	void Start () {
	    //if warm show melted snowman 
        if (GameObject.Find("Dialogue Manager").GetComponent<DialogueManager>().isWarm())
        {
            //deactivate all te sprites for the normal snowman
            gameObject.transform.FindChild("Snow_base").gameObject.SetActive(false);
            gameObject.transform.FindChild("Snow_eyes").gameObject.SetActive(false);
            gameObject.transform.FindChild("Snow_mouth").gameObject.SetActive(false);
            gameObject.transform.FindChild("Snow_nose").gameObject.SetActive(false);
            gameObject.transform.FindChild("Snow_branches").gameObject.SetActive(false);
            gameObject.transform.FindChild("Snow_hat").gameObject.SetActive(false);
            gameObject.transform.FindChild("Snow_butterfly").gameObject.SetActive(false);

            //activate melted snowman sprites
            gameObject.transform.FindChild("Snow_melted").gameObject.SetActive(true);
        }

        //else show current state of the snowman
        else
        {
            if (eyes)
                Eyes();
            if (mouth)
                Mouth();
            if (arms)
                Arms();
            if (nose)
                Nose();
            if (hat)
                Hat();
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Eyes(){
        gameObject.transform.FindChild("Snow_eyes").gameObject.SetActive(true);
        eyes = true;
    }

    public void Mouth(){
        gameObject.transform.FindChild("Snow_mouth").gameObject.SetActive(true);
        mouth = true;
    }

    public void Nose(){
        gameObject.transform.FindChild("Snow_nose").gameObject.SetActive(true);
        nose = true;
    }

    public void Arms(){
        gameObject.transform.FindChild("Snow_branches").gameObject.SetActive(true);
        arms = true;
    }

    public void Hat(){
        gameObject.transform.FindChild("Snow_hat").gameObject.SetActive(true);
        gameObject.transform.FindChild("Snow_butterfly").gameObject.SetActive(true);
        hat = true;
    }

    public void Umbrella(){
        gameObject.transform.FindChild("Snow_umbrella").gameObject.SetActive(true);
        gameObject.transform.FindChild("Snow_melted").gameObject.SetActive(false);
    }

}
