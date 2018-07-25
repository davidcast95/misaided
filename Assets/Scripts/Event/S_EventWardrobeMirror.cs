using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_EventWardrobeMirror : D_Event {

	[SerializeField] GameObject doorMirror;

	// Use this for initialization
	void Start () {
		doorMirror.GetComponent<C_Door>().open = Open;
		doorMirror.GetComponent<C_Door>().close = Close;
	}
	
	// Update is called once per frame
	void Update () {
	}

	void Open() {
		if (!isActive) {
			doorMirror.GetComponent<A_Door> ().Reset ();
		}
	}

	void Close() {
		if (isActive) {
			doorMirror.GetComponent<A_Door> ().doorSoundSource.mute = true;
			doorMirror.GetComponent<A_Door> ().animationSpeed = 300;
			isActive = false;
			print ("asds");
		}
	}
}
