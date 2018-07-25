using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class S_Menu : MonoBehaviour {

	[SerializeField] GameObject gameInstance;
	// Use this for initialization
	[SerializeField] GameObject postProcessDelegate;
	[SerializeField] GameObject sceneDelegate;
	bool isOpen;

	[SerializeField] float blackOpenSpeed, blackCloseSpeed;
	[SerializeField] float blackOpen, blackClose;

	[SerializeField] string loadLevel;


	float startDelay;

	void Awake(){
		if (S_GameInstance.instance == null) {
			Instantiate (gameInstance);
		}
	}
	void Start () {
		isOpen = true;
		startDelay = 0.2f;
		postProcessDelegate.GetComponent<D_PostProcess> ().SetInitialPostProcess (PostProcessType.blackout, 0.3f);
		postProcessDelegate.GetComponent<D_PostProcess> ().SetInitialPostProcess (PostProcessType.vignette, 0.15f);
		StartCoroutine (Delay());
	}

	// Update is called once per frame
	void FixedUpdate () {
		if (!postProcessDelegate.GetComponent<D_PostProcess> ().isFading) {
			if (isOpen) {
				postProcessDelegate.GetComponent<D_PostProcess> ().StartInterpolation (PostProcessType.blackout, blackClose, blackCloseSpeed);
			} else if (!isOpen) {
				postProcessDelegate.GetComponent<D_PostProcess> ().StartInterpolation (PostProcessType.blackout, blackOpen, blackOpenSpeed, 0.002f, InterpolationType.lerp);
			}
			isOpen = !isOpen;
		}
	}

	IEnumerator Delay(){
		yield return new WaitForSeconds (startDelay);
	}

	public void StartGame(){
		sceneDelegate.GetComponent<D_SceneLoader> ().LoadLevel (loadLevel);
	}
}
