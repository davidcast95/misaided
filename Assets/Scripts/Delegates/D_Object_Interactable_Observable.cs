using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class D_Object_Interactable_Observable : S_Object_Interractable {
	public MouseDownDelegate mouseDown;
	public HoldDelegate hold;

	void Awake()
	{
		mouseDown = MouseDown;
		hold = Hold;
	}

	public void MouseDown()
	{
		Debug.LogWarning("MouseDown for object \"" + transform.name + "\" delegate hasn't been setup yet");
	}
	public void Hold()
	{
		Debug.LogWarning("Hold for object \"" + transform.name + "\" delegate hasn't been setup yet");
	}
}
