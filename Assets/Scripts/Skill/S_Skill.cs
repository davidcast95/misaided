using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Skills {Save, Regen, Banish, Vision}

public class S_Skill: MonoBehaviour {

	public bool isPassive;
	public bool isOn;

	// Use this for initialization
	void Start () {
		isPassive = false;
		isOn = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public virtual void Activate (GameObject playerObject){
	}

	public virtual void Deactivate (){

	}

}
