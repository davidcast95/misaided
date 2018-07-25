using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_GameInstance : MonoBehaviour {
	
	public static S_GameInstance instance = null;

	public List<GameObject> keyItems;

	void Awake(){
		if (instance == null) {
			instance = this;
		} else if (instance != this) {
			Destroy (gameObject);
		}
		DontDestroyOnLoad (gameObject);
	}

}
