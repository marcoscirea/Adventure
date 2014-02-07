using UnityEngine;
using System.Collections;

public class Interaction : MonoBehaviour {

	public DialogueManager dm;
	Vector3 walkpoint;
	public bool dialogue = true;
	Vector3 initScale;
	public Vector3 invScale;

	// Use this for initialization
	void Start () {
		dm = GameObject.Find("Dialogue Manager").GetComponent<DialogueManager>();
		walkpoint=transform.FindChild("Walk Point").transform.position;
		initScale = transform.localScale;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void action(){
		if (dialogue)
			//interactive object is NPC
			dm.startDialogue(3);
		else {
			//interactive object is item
			GameObject.FindGameObjectWithTag("Inventory").GetComponent<Inventory>().addItem(gameObject);
		}
	}

	public Vector3 getWalkPoint(){
		return walkpoint;
	}
}
