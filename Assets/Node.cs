using UnityEngine;
using System.Collections;

public class Node : MonoBehaviour {

	public GameObject[] neighbor_nodes;
	public Vector3 pos;
    public float dist;
    public Node previous;

	// Use this for initialization
	void Start () {
		pos = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
	    foreach (GameObject g in neighbor_nodes)
        {
            Debug.DrawLine(transform.position, g.transform.position, Color.red);
        }
	}
}
