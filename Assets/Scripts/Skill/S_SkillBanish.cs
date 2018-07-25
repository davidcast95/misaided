using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_SkillBanish : S_Skill {

	[SerializeField] float sanityDrainPerPeriod;
	[SerializeField] float period;
	[SerializeField] int usage;

	GameObject player;
	public float nextDrainTime = 0.0f;

	void Start () {
		
	}

	void Update () {
		if (isOn && usage > 0) {
			if (Time.time > nextDrainTime) {
				player.GetComponent<C_Player> ().UpdateSanity (-sanityDrainPerPeriod);
				nextDrainTime = Time.time + period;
			}
		}
	}

	public override void Activate(GameObject playerObject){
		if (!isOn) {
			isOn = true;
			player = playerObject;
			P_Player p = player.GetComponent<P_Player> ();
			player.GetComponent<C_Player> ().LimboShift (true);
			p.postProcessDelegate.GetComponent<D_PostProcess> ().StartInterpolationVector (PostProcessType.blue, p.blueClose, p.blueCloseSpeed);
		} else {
			Deactivate ();
		}
	}

	public override void Deactivate(){
		isOn = false;
		P_Player p = player.GetComponent<P_Player> ();
		player.GetComponent<C_Player> ().LimboShift (false);
		p.postProcessDelegate.GetComponent<D_PostProcess> ().StartInterpolationVector (PostProcessType.blue, p.blueOpen, p.blueOpenSpeed);
	}

}
