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

    static string logDirectory = @"Logs";
    static string groupDir = System.IO.Path.Combine(logDirectory, "Group.txt");
    static string keyDir = System.IO.Path.Combine(logDirectory, "Keypoints.txt");
    //static string dialDir = System.IO.Path.Combine(logDirectory, "DialogueLenghts.txt");
    static string pickedDir = System.IO.Path.Combine(logDirectory, "PickedItems.txt");
    static string usedDir = System.IO.Path.Combine(logDirectory,"UsedItems.txt");
    //static string interactionDir = System.IO.Path.Combine(logDirectory,"Interactions.txt");
    //static string roomDir = System.IO.Path.Combine(logDirectory,"ChangedRoom.txt");

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

        //save log file (human readable)
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

        //save csv logs
        csvLogs();
    }

    void csvLogs(){
        //save log file (csv)
        checkLogDirectories();
        Guid guid = Guid.NewGuid();
        string id = guid.ToString();
        
        //group entry
        StreamWriter sr = System.IO.File.AppendText(groupDir);
        sr.WriteLine(id + ","+Dialoguer.GetGlobalFloat(1).ToString());
        sr.Close();
        
        //keypoint entry
        string[] tmp = new string[8];
        foreach (string[] s in keypoint)
        {
            switch (s[0]){
                case "First time outside":
                    tmp[0]=s[1];
                    break;
                case "Snowman Complete":
                    tmp[1]=s[1];
                    break;
                case "Narrator dialogue":
                    tmp[2]=s[1];
                    break;
                case "Warm":
                    tmp[3]=s[1];
                    break;
                case "Back Outside":
                    tmp[4]=s[1];
                    break;
                case "Last Dialogue":
                    tmp[5]=s[1];
                    break;
                case "Ending":
                    tmp[6]=s[1];
                    break;
                case "Game closed":
                    tmp[7]=s[1];
                    break;
            }
        }
        sr = System.IO.File.AppendText(keyDir);
        sr.Write(id + ",");
        for (int i = 0; i<tmp.Length; i++)
        {
            sr.Write(tmp[i]);
            if (i!=tmp.Length-1)
                sr.Write(",");
        }
        sr.WriteLine("");
        sr.Close();
        
        //picked items entry
        tmp = new string[6];
        foreach (string[] s in pick)
        {
            switch (s[0]){
                case "Eyes":
                    tmp[0]=s[1];
                    break;
                case "Hat":
                    tmp[1]=s[1];
                    break;
                case "Carrot":
                    tmp[2]=s[1];
                    break;
                case "Sticks":
                    tmp[3]=s[1];
                    break;
                case "Gravel":
                    tmp[4]=s[1];
                    break;
                case "Umbrella":
                    tmp[5]=s[1];
                    break;
            }
        }
        sr = System.IO.File.AppendText(pickedDir);
        sr.Write(id + ",");
        for (int i = 0; i<tmp.Length; i++)
        {
            sr.Write(tmp[i]);
            if (i!=tmp.Length-1)
                sr.Write(",");
        }
        sr.WriteLine("");
        sr.Close();
        
        //used items on snowman entry
        tmp = new string[6];
        foreach (string[] s in used)
        {
            if (s[1]=="Snowman"){
                switch (s[0]){
                    case "Eyes":
                        tmp[0]=s[2];
                        break;
                    case "Hat":
                        tmp[1]=s[2];
                        break;
                    case "Carrot":
                        tmp[2]=s[2];
                        break;
                    case "Sticks":
                        tmp[3]=s[2];
                        break;
                    case "Gravel":
                        tmp[4]=s[2];
                        break;
                    case "Umbrella":
                        tmp[5]=s[2];
                        break;
                }
            }
        }
        sr = System.IO.File.AppendText(usedDir);
        sr.Write(id + ",");
        for (int i = 0; i<tmp.Length; i++)
        {
            sr.Write(tmp[i]);
            if (i!=tmp.Length-1)
                sr.Write(",");
        }
        sr.WriteLine("");
        sr.Close();
    }
    
    void checkLogDirectories(){
        if (!System.IO.Directory.Exists(logDirectory))
        {
            System.IO.Directory.CreateDirectory(logDirectory);
            //System.IO.File.Create(keyDir);
            //System.IO.File.Create(pickedDir);
            //System.IO.File.Create(usedDir);
        }
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
        if (dialogueStart != 0 || dialogueName=="Intro")
        {
            string[] s = {dialogueName, (Time.time - dialogueStart).ToString()};
            dialogues.Add(s);
            dialogueStart = 0;
        }
    }
}
