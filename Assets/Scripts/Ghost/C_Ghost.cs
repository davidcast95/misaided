using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C_Ghost : MonoBehaviour {

	// Use this for initialization

	[SerializeField] P_Ghost properties;
	GameObject gameInstance;

	void Start () {
		gameInstance = S_GameInstance.instance.gameObject;
	}
	
	// Update is called once per frame
	void Update () {
		if (!properties.isObtainable) {
			RefreshPrerequisites ();
		}
	}

	public void CastSkill(GameObject playerObject, int index){
		if (index < properties.skillSet.Length) {
			properties.skillSet [index].Activate (playerObject);
		}
	}

	public void CastPassives(GameObject playerObject){
		for (int i = 0; i < properties.skillSet.Length; i++){
			if (properties.skillSet[i].isPassive) {
				properties.skillSet[i].Activate (playerObject);
			}
		}
	}

	void RefreshPrerequisites(){
		bool unlock = true;
		foreach (GameObject item in properties.itemPrerequisites) {
			if (gameInstance.GetComponent<S_GameInstance> ().keyItems.IndexOf (item) < 0) {
				unlock = false;
			}
		}
		properties.isObtainable = unlock;
	}
		
}
