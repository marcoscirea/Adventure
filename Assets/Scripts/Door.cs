using UnityEngine;
using System.Collections;

public class Door : Interaction {

	public string to;

	// Update is called once per frame
	public override void Update () {
	
	}

	public override void action ()
	{
		Application.LoadLevel (to);
	}

	public override void secondary ()
	{
		throw new System.NotImplementedException ();
	}
}
