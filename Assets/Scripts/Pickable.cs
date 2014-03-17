using UnityEngine;
using System.Collections;

public class Pickable : Interaction
{

    
    public Vector3 invScale;
    public bool inInventory = false;
    public bool clicked = false;
    Inventory inventory;
    public DialoguerDialogues dialogue;
    public bool isActive = true;

    protected override void doStart()
    {
        if (inventory == null)
            inventory = GameObject.FindGameObjectWithTag("Inventory").GetComponent<Inventory>();

        //check if object already picked up
        if (inventory.hasBeenPickedUp(gameObject.name))
            Destroy(gameObject);
    }

    public override void Update()
    {

        checkForChangedObjects();

        if (clicked)
        {
            inventory.stopUpdate(true);
            transform.position = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y, -0.5f);
        }
    }

    public override void action()
    {

        if (!inInventory)
        {
            if (isActive)
            {
                inventory.addItem(gameObject);
                //GameObject.FindGameObjectWithTag ("Player").GetComponent<PointClick> ().activate ();
                inInventory = true;

                //Picking up animation
                GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>().SetTrigger("Pickup");

                //log picking up action
                Logger.pickedUp(gameObject.name);
            }

            //dialogue when picking up item
            dm.startDialogue(dialogue);
        } else
        {
            clicked = true;
            gameObject.collider.enabled = false;
            GameObject.FindGameObjectWithTag("Player").GetComponent<PointClick>().usingItem(gameObject);
        }

    }

    public override void secondary()
    {
        //unselect if selected
        if (clicked)
        {
            clicked = false;
            gameObject.collider.enabled = true;
            inventory.stopUpdate(false);
            //inventory.updateItems();
        }
    }

    public void useWith(GameObject other)
    {

        //log trying to use an the item on something
        Logger.use(gameObject.name, other.name);

        bool success = false;
        //interact with object
        //Debug.Log("Interact!");
        switch (other.gameObject.name)
        {

        //interactions with Snowman
            case "Snowman":
                switch (gameObject.name)
                {
                    case "Eyes":
                //other.gameObject.GetComponent<SpriteRenderer>().sprite = snowman[0];
                        Dialoguer.SetGlobalBoolean(2, true);
                        other.GetComponent<Snowman>().Eyes();
                        success = true;
                        break;
                    case "Gravel":
                        Dialoguer.SetGlobalBoolean(1, true);
                        other.GetComponent<Snowman>().Mouth();
                        success = true;

                //change mood to peaceful
                        Camera.main.GetComponent<TCPclient>().writeSocket("peaceful");
                        break;
                    case "Carrot":
                //other.gameObject.GetComponent<SpriteRenderer>().sprite = snowman[2];
                        Dialoguer.SetGlobalBoolean(3, true);
                        other.GetComponent<Snowman>().Nose();
                        success = true;
                        break;
                    case "Sticks":
                //other.gameObject.GetComponent<SpriteRenderer>().sprite = snowman[3];
                        Dialoguer.SetGlobalBoolean(4, true);
                        other.GetComponent<Snowman>().Arms();
                        success = true;
                        break;
                    case "Hat":
                //other.gameObject.GetComponent<SpriteRenderer>().sprite = snowman[3];
                        Dialoguer.SetGlobalBoolean(0, true);
                        other.GetComponent<Snowman>().Hat();
                        success = true;
                        break;

                //umbrella 
                    case "Umbrella":
                        //dm.startDialogue(DialoguerDialogues.Thesnowmanspeaks);
                        Dialoguer.SetGlobalBoolean(6, true);
                        dm.startDialogue(DialoguerDialogues.Thesnowmanspeaks);
                        other.GetComponent<Snowman>().Umbrella();
                        success = true;
                        break;
                }
                break;
     
        //Combining items template
        /*
            case "Hat":
                switch (gameObject.name){
                    case "Eyes":
                        Debug.Log("combining items!");
                        break;
                }
                break; */
        }
    
        //last operations
        if (success)
        {
            clicked = false;
            inventory.removeItem(gameObject);
            //inventory.updateItems ();
            gameObject.collider.enabled = true;
            inventory.stopUpdate(false);
        } else
            secondary();
    }

    void checkForChangedObjects()
    {
        //needed when changing scene to have the new dialogue manager etc
        if (inventory == null)
            inventory = GameObject.FindGameObjectWithTag("Inventory").GetComponent<Inventory>();
        if (dm == null)
            dm = GameObject.Find("Dialogue Manager").GetComponent<DialogueManager>();
    }
}
