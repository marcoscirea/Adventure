using UnityEngine;
using System.Collections;

public class PointClick : MonoBehaviour {

	Vector3 target;
	bool move = false;
	RaycastHit hit;
	Interaction interactiveobject;

	public float speed = 1f;
	public float yadjust = 1f;
	public bool canMove = true;

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
						target= interactiveobject.getWalkPoint();
						target.z = -1;
						//hit.collider.gameObject.GetComponent<Interaction>().action();
						//canMove = false;
						move=true;
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
	}

	public void activate(){
		canMove = true;
	}

	private void interaction(){
		interactiveobject.action();
		interactiveobject=null;
	}
}
