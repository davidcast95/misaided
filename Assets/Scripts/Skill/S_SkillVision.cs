using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_SkillVision : S_Skill {

	[SerializeField] float sanityDrainPerPeriod;
	[SerializeField] float period;
	public float nextDrainTime = 0.0f;

	GameObject player;

	void Start () {
		nextDrainTime = 0.0f;
	}

	void Update () {
		if (isOn) {
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
			player.GetComponent<P_Player> ().postProcessDelegate.GetComponent<D_PostProcess> ().StartInterpolation (PostProcessType.chromatic, 1f, 0.1f);
		}
		else {
			Deactivate ();
		}

	}

	public override void Deactivate(){
		isOn = false;
		player.GetComponent<P_Player> ().postProcessDelegate.GetComponent<D_PostProcess> ().StartInterpolation (PostProcessType.chromatic, 0f, 0.1f);
	}
}
