using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_SkillSave : S_Skill {

	[SerializeField] float cooldownTime;
	[SerializeField] float saveValue;
	[SerializeField] int usage;
	public float nextActivationTime;

	// Use this for initialization
	void Start () {
		nextActivationTime = 0.0f;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public override void Activate(GameObject playerObject){
		if (Time.time > nextActivationTime && usage > 0) {
			playerObject.GetComponent<C_Player> ().UpdateSanity (saveValue);
			nextActivationTime = Time.time + cooldownTime;
			usage--;
		}
	}
	public override void Deactivate(){
		
	}



}
