using UnityEngine;
using System.Collections;

public class Pickable : Interaction {

	
	public Vector3 invScale;
	public bool inInventory = false;
	public bool clicked = false;
	Inventory inventory;
	
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
		Debug.Log("Interact!");
		clicked = false;
		inventory.removeItem (gameObject);
		//inventory.updateItems ();
		gameObject.collider.enabled=true;
		inventory.stopUpdate (false);
		}
}
