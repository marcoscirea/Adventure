using UnityEngine;
using System.Collections;
using System;
using System.IO;

public class Logger : MonoBehaviour {

    static Logger instance;

    static ArrayList pick = new ArrayList();
    static ArrayList used = new ArrayList();
    static ArrayList go = new ArrayList();
    static ArrayList interacted = new ArrayList();
    static ArrayList keypoint = new ArrayList();
    static ArrayList dialogues = new ArrayList();

    static string dialogueName;
    static float dialogueStart;

    public int i = 0;

    void Awake()
    {
        if (instance)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;    
        DontDestroyOnLoad(this); 
    }

	void Update(){
        /*if (i > 100)
        {
            pickedUp("test");
            i = 0;
            string[] s = (string[]) pick[0];
            Debug.Log(s[0]+s[1]);
        }
        else
            i++;*/
    }

    void OnApplicationQuit(){
        //log the game is closing
        key("Game closed");

        //save log file
        int subject = 0;
        while (File.Exists("subject"+ subject.ToString()+".txt"))
        {
            subject++;
        }
        StreamWriter sr = File.CreateText("subject"+subject.ToString()+".txt");

        sr.WriteLine("GROUP " + Dialoguer.GetGlobalFloat(1).ToString());
        sr.WriteLine("0 = true foreshadowing, 1 = false foreshadowing, 3 = control group");
        sr.WriteLine("");

        sr.WriteLine ("KEYPOINTS");
        foreach (string[] s in keypoint)
        {
            sr.WriteLine(s[0]+", "+s[1]);
        }
        sr.WriteLine ("");

        sr.WriteLine ("DIALOGUES LENGHTS");
        foreach (string[] s in dialogues)
        {
            sr.WriteLine(s[0]+", "+s[1]);
        }
        sr.WriteLine("");

        sr.WriteLine ("PICKED ITEMS");
        foreach (string[] s in pick)
        {
            sr.WriteLine(s[0]+", "+s[1]);
        }
        sr.WriteLine ("");

        sr.WriteLine ("USED ITEMS");
        foreach (string[] s in used)
        {
            sr.WriteLine(s[0]+", "+s[1]+", "+s[2]);
        }
        sr.WriteLine ("");

        sr.WriteLine ("INTERACTIONS");
        foreach (string[] s in interacted)
        {
            sr.WriteLine(s[0]+", "+s[1]);
        }
        sr.WriteLine ("");

        sr.WriteLine ("CHANGED ROOM");
        foreach (string[] s in go)
        {
            sr.WriteLine(s[0]+", "+s[1]);
        }

        sr.Close();
    }

    static public void pickedUp(string item){
        string[] s = {item, Time.time.ToString()};
        pick.Add(s);
    }

    static public void use(string item1, string item2){
        string[] s = {item1, item2, Time.time.ToString()};
        used.Add(s);
    }

    static public void goTo(string room){
        string[] s = {room, Time.time.ToString()};
        go.Add(s);
    }

    static public void interact(string item){
        string[] s = {item, Time.time.ToString()};
        interacted.Add(s);
    }

    static public void key(string description){
        string[] s = {description, Time.time.ToString()};
        keypoint.Add(s);
    }

    static public void startDialogue(string name){
        dialogueName = name;
        dialogueStart = Time.time;
    }

    static public void endDialogue(){
        string[] s = {dialogueName, (Time.time-dialogueStart).ToString()};
        dialogues.Add(s);
    }
}
