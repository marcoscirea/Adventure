using UnityEngine;
using System.Collections;

public abstract class Interaction : MonoBehaviour {

	public DialogueManager dm;
	Vector3 walkpoint;
	//Vector3 initScale;

	// Use this for initialization
	void Start () {
		dm = GameObject.Find("Dialogue Manager").GetComponent<DialogueManager>();
		walkpoint=transform.FindChild("Walk Point").transform.position;
		//initScale = transform.localScale;
	}
	
	// Update is called once per frame
	public abstract void Update ();

	public abstract void action ();

	public Vector3 getWalkPoint(){
		return walkpoint;
	}
}
