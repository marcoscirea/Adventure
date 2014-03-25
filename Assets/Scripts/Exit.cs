using UnityEngine;
using System.Collections;

public class Exit : MonoBehaviour {

    int width = 300;
    int height = 90;
    int center_x;
    int center_y;

    bool playerMoveStatus;
    PointClick playerControl;
    CustomGui gui;

    static GameObject instance;

	// Use this for initialization
	void Start () {
        if (instance != null)
            Destroy(gameObject);
        else
            instance = gameObject;
        playerControl = GameObject.FindGameObjectWithTag("Player").GetComponent<PointClick>();
        gui = GameObject.FindGameObjectWithTag("Gui").GetComponent<CustomGui>();

        playerMoveStatus = playerControl.canMove;
        playerControl.canMove = false;
        gui.pause = true;

        Time.timeScale = 0;
        center_x = (Screen.width / 2);
        center_y = (Screen.height / 2);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnGUI(){
        GUI.Box(new Rect (center_x-(width/2),center_y-(height/2),width,height), "Exit To Desktop?");
        if (GUI.Button(new Rect(center_x - 90,center_y - 10 ,80,20), "Yeah")) {
            Application.Quit();
        }
        if (GUI.Button(new Rect(center_x + 10, center_y -10 , 80, 20), "Not yet"))
        {
            playerControl.canMove = playerMoveStatus;
            gui.pause = false;
            Time.timeScale = 1f;
            instance = null;
            Destroy(gameObject);
        }
    }
}
