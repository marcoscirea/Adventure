using UnityEngine;
using System.Collections;

public class PointClick : MonoBehaviour {

	Vector3 target;
	bool move = false;
	RaycastHit hit;
	Interaction interactiveobject;
	GameObject selectedItem;

	public float speed = 1f;
	public float yadjust = 1f;
	public bool canMove = true;
	bool nowMove = false;

	// Use this for initialization
	void Start () {
		interactiveobject=null;
	}
	
	// Update is called once per frame
	void Update () {
		if (canMove){
			if (Input.GetMouseButtonDown(0))
			{
				Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);

				if (Physics.Raycast(ray, out hit)) {

					if (hit.collider.tag=="Walkable"){
						move = true;	
						target = new Vector3(hit.point.x, hit.point.y + yadjust, -1);
					}

					if (hit.collider.tag=="Interactive"){
						interactiveobject = hit.collider.gameObject.GetComponent<Interaction>();
						if (hit.collider.gameObject.GetComponent<Pickable>()==null || 
							!hit.collider.gameObject.GetComponent<Pickable>().inInventory){
							target= interactiveobject.getWalkPoint();
							target.z = -1;
							//hit.collider.gameObject.GetComponent<Interaction>().action();
							//canMove = false;
							move=true;
						}
						else {
							canMove= false;
							interaction();
						}
					}
				}
				
			}
			
			
			
			if(move){
				
				transform.LookAt(target);

				//Ray diff = new Ray(transform.position, target);

				//Debug.Log(diff.direction);

				transform.Translate(Vector3.forward *  Time.deltaTime * speed);


				
				if(Vector3.Distance(transform.position, target) < 0.01){
					
					move = false;

					if (interactiveobject!=null){
						canMove=false;
						interaction();
					}
					
				}
				
				
				
			}
		}
		else{
			if (selectedItem!=null){
				if (Input.GetMouseButtonDown(0))
				{
					Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
					
					if (Physics.Raycast(ray, out hit)) {
						
						if (hit.collider.tag=="Interactive"){
							//call interaction code
							selectedItem.GetComponent<Pickable>().useWith(hit.collider.gameObject);
							activate();
						}
					}
					
				}
				if (Input.GetMouseButtonDown(1)){
					selectedItem.GetComponent<Pickable>().secondary();
					activate();
				}
			}
		}
	}

	void LateUpdate(){
		if (nowMove) {
			canMove = true;
			nowMove = false;
		}
	}

	public void activate(){
		nowMove = true;
	}

	private void interaction(){
		interactiveobject.action();
		interactiveobject=null;
	}

	public void usingItem(GameObject item){
		selectedItem = item;
	}
}
