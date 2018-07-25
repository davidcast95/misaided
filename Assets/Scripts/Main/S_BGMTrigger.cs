using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_BGMTrigger : MonoBehaviour {

	[SerializeField] AudioClip BGM;
	[SerializeField] AudioSource bgmSound;
	[SerializeField] float delay;
	[SerializeField] bool isTriggerActive = true;

	void OnTriggerEnter() {
		if (isTriggerActive) {
			isTriggerActive = false;
			StartCoroutine (Play (delay));
		}
	}

	IEnumerator Play(float withDelay) {
		yield return new WaitForSeconds (withDelay);
		bgmSound.clip = BGM;
		bgmSound.Play ();

	}
}
