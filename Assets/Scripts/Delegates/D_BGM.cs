using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class D_BGM : MonoBehaviour {

	void Awake(){
		DontDestroyOnLoad (this.gameObject);
	}
	// Use this for initialization
	void Start () {
		this.gameObject.GetComponent<AudioSource> ().Play ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
