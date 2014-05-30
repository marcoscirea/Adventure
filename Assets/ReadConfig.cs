using UnityEngine;
using System.Collections;
using System;
using System.IO;

public class ReadConfig : MonoBehaviour
{

		string config;
		// Use this for initialization
		void Start ()
		{
				if (File.Exists ("config.txt")) {
						string line = "";
						System.IO.StreamReader file = new System.IO.StreamReader ("config.txt");
						while ((line = file.ReadLine()) != null) {
								if (line.StartsWith ("Group=")) {
				
										string S = line.Substring (6).Replace (" ", "");
										Debug.Log ("Setting up game for group " + S);
										gameObject.GetComponent<DialogueManager> ().group = Convert.ToInt32 (S);
								}
						}
		
						file.Close ();
				} else {
			Debug.Log("Writing default config file");
			string[] lines = {"#Set here which group playthrough do you want:",
					"#Group 0 is true foreshadowing",
					"#Group 1 is false foreshadowing",
					"#Group 2 is the control group",
					"Group=0"};
					System.IO.File.WriteAllLines(@"config.txt", lines);
				}

		}
	
		// Update is called once per frame
		void Update ()
		{
	
		}
}
