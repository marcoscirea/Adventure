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
	bool objectInteraction = false;

    bool wait = true;

    //exit point for next scene
    static Vector3 exitDoor = Vector3.zero;

	TCPclient client;

    private Animator animator;

	// Use this for initialization
	void Start () {

        animator = GetComponent<Animator>();

		interactiveobject=null;

        if (exitDoor != Vector3.zero)
        {
            transform.position=new Vector3(exitDoor.x, exitDoor.y, -1);
            exitDoor = Vector3.zero;
        }
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
			
			
			

		}
		else{
			if (selectedItem!=null){
				if (Input.GetMouseButtonDown(0))
				{
					Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
					
					if (Physics.Raycast(ray, out hit)) {
						
						if (hit.collider.tag=="Interactive"){
							//call interaction code
							//selectedItem.GetComponent<Pickable>().useWith(hit.collider.gameObject);
							//activate();

							interactiveobject = hit.collider.gameObject.GetComponent<Interaction>();
							target= interactiveobject.getWalkPoint();
							target.z = -1;
							move=true;
							objectInteraction = true;
						}
					}
					
				}
				if (Input.GetMouseButtonDown(1)){
					selectedItem.GetComponent<Pickable>().secondary();
					activate();
				}
			}
		}

		if(move){
			
            //old way
			/*transform.LookAt(target);
			transform.Translate(Vector3.forward *  Time.deltaTime * speed);
			*/

            transform.position = Vector3.MoveTowards(transform.position, target, speed);
			
			
			if(Vector3.Distance(transform.position, target) < 0.01){
				
                //patch to avoid skipping first line of dialogue when already close to the walkpoint
                if(wait){
                    wait = false;
                    return;
                }
                else{
                    wait = true;
                }

				move = false;
				
				if (interactiveobject!=null){
					if (!objectInteraction){
						//normal interaction (dialogue, pickup, etc.
						canMove=false;
						interaction();
					}
					else {
						//Object interaction
						selectedItem.GetComponent<Pickable>().useWith(interactiveobject.gameObject);
						activate();
						selectedItem=null;
						objectInteraction=false;
                    }

				}
				
			}	
		}

        //ANIMATOR CODE
        if (move)
        {
            if (target.x >transform.position.x)
                animator.SetBool("isMovingLeft", false);
            else
                animator.SetBool("isMovingLeft", true);
                
            animator.SetBool("isMoving", true);
        }
        else
            animator.SetBool("isMoving", false);
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

    //function to set up when going through a door the exit point in the next scene
    public static void exitThroughDoor(Vector3 door){
        exitDoor = door;
    }

}
