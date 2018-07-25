using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_FootstepMaterial : MonoBehaviour {

	[SerializeField]
	AudioClip[] material;

	void OnTriggerEnter(Collider other) {
		if (other.CompareTag ("Player")) {
			other.GetComponent<FirstPersonController> ().materialFootStep = material;
		}
	}

	void OnTriggerExit(Collider other) {
		if (other.CompareTag ("Player")) {
			other.GetComponent<FirstPersonController> ().materialFootStep = new AudioClip[0];
		}
	}
}
