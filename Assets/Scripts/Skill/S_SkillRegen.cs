using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_SkillRegen : S_Skill {

	[SerializeField] float regenRate;
	[SerializeField] float regenValue;
	public float nextActivationTime;

	GameObject player;

	void Start () {
		isPassive = true;
		nextActivationTime = 0.0f;
	}
	
	// Update is called once per frame
	void Update () {
		if (isOn) {
			if(Time.time > nextActivationTime){
				player.GetComponent<C_Player> ().UpdateSanity (regenValue);
				nextActivationTime = Time.time + regenRate;
			}
		}
	}

	public override void Activate(GameObject playerObject){
		print ("wow");
		player = playerObject;
		isOn = true;
	}

	public override void Deactivate(){
		isOn = false;
	}
}
