using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_Ghost : MonoBehaviour {

	public string ghostName;
	public int ghostId;
	public Skills[] skillEnum;
	public S_Skill[] skillSet;
	public bool isObtainable;
	public List<GameObject> itemPrerequisites;

	// Use this for initialization
	void Start () {
		isObtainable = false;
	}
	

}
