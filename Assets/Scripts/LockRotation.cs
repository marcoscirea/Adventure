using UnityEngine;
using System.Collections;

public class LockRotation : MonoBehaviour {

	Quaternion initRot;

	// Use this for initialization
	void Start () {
		initRot = transform.rotation;
	}
	
	// Update is called once per frame
	void LateUpdate () {
		transform.rotation=initRot;
	}
}
