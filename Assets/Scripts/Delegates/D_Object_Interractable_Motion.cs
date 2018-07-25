using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class D_Object_Interractable_Motion : S_Object_Interractable {
	public HoldDelegate hold;
	public MouseDownDelegate mouseDown;
	public MouseUpDelegate mouseUp;
	private Transform playerTransform;

	void Awake() {

		playerTransform = GameObject.FindGameObjectWithTag ("Player").transform;
		hold = Hold;
		mouseDown = MouseDown;
		mouseUp = MouseUp;
		active = Active;
	}

	public void MouseDown() {
		Debug.LogWarning("MouseDown for object \"" + transform.name + "\" delegate hasn't been setup yet");
	}

	public void MouseUp() {
		Debug.LogWarning("MouseUp for object \"" + transform.name + "\" delegate hasn't been setup yet");
	}

	public void Hold() {
		Debug.LogWarning("Hold for object \"" + transform.name + "\" delegate hasn't been setup yet");
	}


}
