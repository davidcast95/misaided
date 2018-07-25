using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C_Drawer : MonoBehaviour {

	public P_Drawer properties;
	[SerializeField]
	D_Object_Interractable_Motion motionScript;
	[SerializeField] A_Drawer animationDrawer;
	public Transform container;

	void Start()
	{
		motionScript.mouseDown = ToogleOpen;
	}


	void ToogleOpen()
	{
		if (properties.isOpen)
		{
			properties.isOpen = false;
			animationDrawer.Close();
		}
		else
		{
			properties.isOpen = true;
			animationDrawer.Open();
		}
	}

}
