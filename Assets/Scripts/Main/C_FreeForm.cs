using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C_FreeForm : MonoBehaviour {

	// Use this for initialization
	[SerializeField]
	float moveSpeed;

	[SerializeField]
	float rotationSpeed;
	float rotationY, rotationX;

	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		KeyboardControl ();
		MouseControl ();
	}

	void KeyboardControl(){
		float dz = Input.GetAxis ("Vertical");
		float dx = Input.GetAxis ("Horizontal");
		transform.position += transform.forward * dz + transform.right * dx;
	}

	void MouseControl(){
		rotationY += Input.GetAxis ("Mouse X");
		rotationX -= Input.GetAxis ("Mouse Y");
		transform.eulerAngles = new Vector3 (rotationX, rotationY, 0);
	}
}
