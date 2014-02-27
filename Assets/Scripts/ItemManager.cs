using UnityEngine;
using System.Collections;

public class ItemManager : MonoBehaviour {

	static ArrayList commands = new ArrayList();

	// Use this for initialization
	void Start () {
		foreach (string command in commands) {
				activateObjects (command);
		}
	}
	
	// Update is called once per frame
	void Update () {
	}

	public void activateObjects(string message){
		switch (message) {
		case "snowmanComplete":
			Debug.Log(Dialoguer.GetGlobalBoolean(5));
			if (GameObject.Find("Umbrella")!=null)
				GameObject.Find("Umbrella").GetComponent<Pickable>().isActive=true;
			break;
		}

		if (!commands.Contains(message))
			commands.Add(message);
	}
}
