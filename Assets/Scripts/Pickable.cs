using UnityEngine;
using System.Collections;

public class Pickable : Interaction {

	
	public Vector3 invScale;
	public bool inInventory = false;
	public bool clicked = false;
	Inventory inventory;
	public Sprite[] snowman;

	public override void Update(){
			if (inventory == null)
						inventory = GameObject.FindGameObjectWithTag ("Inventory").GetComponent<Inventory> ();

			if (clicked) {
						inventory.stopUpdate (true);
						transform.position = new Vector3 (Camera.main.ScreenToWorldPoint (Input.mousePosition).x, Camera.main.ScreenToWorldPoint (Input.mousePosition).y, -0.5f);
				}
		}

	public override void action(){

		if (!inInventory) {
			inventory.addItem (gameObject);
			GameObject.FindGameObjectWithTag ("Player").GetComponent<PointClick> ().activate ();
			inInventory = true;
		}
		else{
			clicked = true;
			gameObject.collider.enabled=false;
			GameObject.FindGameObjectWithTag("Player").GetComponent<PointClick>().usingItem(gameObject);
		}

	}

	public override void secondary(){
			//unselect if selected
			if (clicked) {
				clicked= false;
				gameObject.collider.enabled=true;
			inventory.stopUpdate(false);
			//inventory.updateItems();
			}
		}

	public void useWith(GameObject other){
		bool success = false;
		//interact with object
		//Debug.Log("Interact!");
		switch (other.gameObject.name){
		case "Snowman":
			switch (gameObject.name){
			case "Eyes":
				other.gameObject.GetComponent<SpriteRenderer>().sprite = snowman[0];
				success = true;
				break;
			case "Gravel":
				if (other.gameObject.GetComponent<SpriteRenderer>().sprite == snowman[0]){
					other.gameObject.GetComponent<SpriteRenderer>().sprite = snowman[1];
					success = true;

					//change mood to peaceful
					Camera.main.GetComponent<TCPclient>().writeSocket("peaceful");
				}
				else{
					//oh no, it needs eyes before
					other.gameObject.GetComponent<Dialogue>().startDialogue(4);
				}
				break;
			case "Carrot":
				if (other.gameObject.GetComponent<SpriteRenderer>().sprite == snowman[1]){
					other.gameObject.GetComponent<SpriteRenderer>().sprite = snowman[2];
					success = true;
				}
				else{
					//oh no, it needs a mouth before
					other.gameObject.GetComponent<Dialogue>().startDialogue(5);
				}
				break;
			case "Sticks":
				if (other.gameObject.GetComponent<SpriteRenderer>().sprite == snowman[2]){
					other.gameObject.GetComponent<SpriteRenderer>().sprite = snowman[3];
					success = true;

					//change mood to happy
					Camera.main.GetComponent<TCPclient>().writeSocket("happy");
				}
				else{
					//oh no, it needs a nose before
					other.gameObject.GetComponent<Dialogue>().startDialogue(6);
				}
				break;
			}
			break;
		}
	
		//last operations
		if (success){
			clicked = false;
			inventory.removeItem (gameObject);
			//inventory.updateItems ();
			gameObject.collider.enabled=true;
			inventory.stopUpdate (false);
		}
		else
			secondary();
		}
}
