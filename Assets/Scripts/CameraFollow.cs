﻿using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

	GameObject player;
	public float dampTime = 0.15f;
	private Vector3 velocity = Vector3.zero;
	public float border = 0.15f;
	public Vector3 target;
	public bool move = false;

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {
		if (!move){
			//float x = transform.position.x;
			float px = player.transform.position.x;

//			Debug.Log(px);
//			Debug.Log(camera.ViewportToWorldPoint(new Vector3(0.8f, 1, camera.nearClipPlane)).x);

			//border check
			if (px < camera.ViewportToWorldPoint(new Vector3(border, 1, camera.nearClipPlane)).x
			    ){
				target= new Vector3(px, transform.position.y, transform.position.z);
				move= true;
			}
			if (px > camera.ViewportToWorldPoint(new Vector3(0.8f, 1, camera.nearClipPlane)).x
			    ){
				target= new Vector3(px, transform.position.y, transform.position.z);
				move= true;
			}
		}
		else{
			Ray rl = new Ray(camera.ViewportToWorldPoint(new Vector3(0, 0.5f, camera.nearClipPlane)) - new Vector3(0.001f,0,0), transform.forward);
			Ray rr = new Ray(camera.ViewportToWorldPoint(new Vector3(1, 0.5f, camera.nearClipPlane)) + new Vector3(0.001f,0,0), transform.forward);

			if((transform.position.x-target.x < 0.01 && transform.position.x-target.x > -0.01) ||
			   (!Physics.Raycast(rl) && target.x <transform.position.x ) ||
			   (!Physics.Raycast(rr) && target.x >transform.position.x )
			   ){
				
				move=false;
			}
			else{
				goTo(target);
			}

		}
	}

	void goTo(Vector3 t){
		//Debug.Log("goto");
		Vector3 point = camera.WorldToViewportPoint(t);
		Vector3 delta = t - camera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, point.z)); //(new Vector3(0.5, 0.5, point.z));
		Vector3 destination = transform.position + delta;
		transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, dampTime);
	}
}