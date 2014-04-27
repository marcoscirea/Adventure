using UnityEngine;
using System.Collections;

public class PointClick : MonoBehaviour
{

    //moving related variables
    Vector3 target;
    public float speed = 1f;
    public float yadjust = 1f;
    public bool canMove = true;
    RaycastHit hit;

    //interaction variables
    Interaction interactiveobject = null;
    GameObject selectedItem;

    //state control variables
    bool move = false;
    bool objectInteraction = false;
    //bool nowMove = false; //nowMove was a fix for dialoguer, probably won't need this anymore
    //bool wait = true; //same for wait

    //external references
    Exit exitGui;
    private Animator animator;

    //exit point for next scene
    static Vector3 exitDoor = Vector3.zero;


    // Use this for initialization
    void Start()
    {

        //get references
        animator = GetComponent<Animator>();
        exitGui = GameObject.FindGameObjectWithTag("ExitPrompt").GetComponent<Exit>();

        //move player to starting position if existing
        if (exitDoor != Vector3.zero)
        {
            transform.position = new Vector3(exitDoor.x, exitDoor.y, -1);
            exitDoor = Vector3.zero;
        }

    }
    
    // Update is called once per frame
    void Update()
    {
        //management of interruptable actions (mostly walk to a new place while walking towards another destination)
        if (canMove)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out hit))
                {

                    //clicked on a walkable area
                    if (hit.collider.tag == "Walkable")
                    {
                        move = true;    
                        target = new Vector3(hit.point.x, hit.point.y + yadjust, -1);
                    }

                    //clicked on an interactive object
                    if (hit.collider.tag == "Interactive")
                    {
                        interactiveobject = hit.collider.gameObject.GetComponent<Interaction>();

                        //if the object is not an item in the inventory walk to its walkpoint
                        if (hit.collider.gameObject.GetComponent<Pickable>() == null || 
                            !hit.collider.gameObject.GetComponent<Pickable>().inInventory)
                        {
                            target = interactiveobject.getWalkPoint();
                            target.z = -1;
                            move = true;
                        }
                        //else just start interaction
                        else
                        {
                            canMove = false;
                            interaction();
                        }
                    }
                }   
            }
        } 
        //management of ations not interruptable by walking
        else
        {
            //if the player is trying to use an item on something else, we control where the raycast hits
            if (selectedItem != null)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    
                    if (Physics.Raycast(ray, out hit))
                    {
                        //if the ray hits an other interactive object we walk to that object and set the flag objectInteraction
                        if (hit.collider.tag == "Interactive")
                        {
                            interactiveobject = hit.collider.gameObject.GetComponent<Interaction>();
                            target = interactiveobject.getWalkPoint();
                            target.z = -1;
                            move = true;
                            objectInteraction = true;
                        }
                    }
                }
                //if the player presses the right mouse button we call the secondary function for the selected object and re-enable movement
                if (Input.GetMouseButtonDown(1))
                {
                    selectedItem.GetComponent<Pickable>().secondary();
                    activate();
                }
            }
        }

        //if we have to move to target
        if (move)
        {
            //move to target with specified speed
            transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
            
            //if we are close to target
            if (Vector3.Distance(transform.position, target) < 0.01)
            {

                move = false;

                //if we clicked on an interactive object
                if (interactiveobject != null)
                {
                    if (!objectInteraction)
                    {
                        //normal interaction (dialogue, pickup, etc.)
                        canMove = false;
                        interaction();
                    } else
                    {
                        //Object interaction
                        selectedItem.GetComponent<Pickable>().useWith(interactiveobject.gameObject);
                        activate();
                        selectedItem = null;
                        objectInteraction = false;
                    }

                }
                
            }   
        }

        //ANIMATOR CODE
        if (move)
        {
            if (target.x > transform.position.x)
                animator.SetBool("isMovingLeft", false);
            else
                animator.SetBool("isMovingLeft", true);
                
            animator.SetBool("isMoving", true);
        } else
            animator.SetBool("isMoving", false);

        //EXIT PROMPT
        if (Input.GetKeyUp(KeyCode.Escape))
            exitGui.Activate();
    }

    /*void LateUpdate()
    {
        if (nowMove)
        {
            canMove = true;
            nowMove = false;
        }
    }*/

    public void activate()
    {
        //nowMove = true;
        canMove = true;
    }

    private void interaction()
    {
        interactiveobject.action();
        interactiveobject = null;
    }

    public void usingItem(GameObject item)
    {
        selectedItem = item;
    }

    //function to set up when going through a door the exit point in the next scene
    public static void exitThroughDoor(Vector3 door)
    {
        exitDoor = door;
    }

}
