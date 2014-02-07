using UnityEngine;
using System.Collections;

public class Pickable : Interaction {

	
	public Vector3 invScale;
	public bool inInventory = false;
	public bool clicked = false;
	
	public override void Update(){
			if (clicked)
						transform.position = new Vector3 (Camera.main.ScreenToWorldPoint (Input.mousePosition).x, Camera.main.ScreenToWorldPoint (Input.mousePosition).y, -0.5f);
		}

	public override void action(){

		if (!inInventory) {
			GameObject.FindGameObjectWithTag ("Inventory").GetComponent<Inventory> ().addItem (gameObject);
			GameObject.FindGameObjectWithTag ("Player").GetComponent<PointClick> ().activate ();
			inInventory = true;
		}
		else{
			clicked = true;
			gameObject.collider.enabled=false;
			GameObject.FindGameObjectWithTag("Player").GetComponent<PointClick>().usingItem(gameObject);
		}

	}

	public void useWith(GameObject other){
		Debug.Log("Interact!");
		clicked = false;
		GameObject.FindGameObjectWithTag ("Inventory").GetComponent<Inventory> ().updateItems ();
		gameObject.collider.enabled=true;
		}
}
