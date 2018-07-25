using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class D_SceneLoader : MonoBehaviour {

	// Use this for initialization

	[SerializeField] GameObject postProcessDelegate;
	[SerializeField] float blackCloseSpeed, blackClose;
	[SerializeField] float vignetteCloseSpeed, vignetteClose;

	S_GameInstance gameInstance;

	AsyncOperation async;

	void Start(){
	
	}

	public void LoadLevel(string level, LoadSceneMode mode = LoadSceneMode.Single, bool animated = true){
		if (animated) {
			postProcessDelegate.GetComponent<D_PostProcess> ().StartInterpolation (PostProcessType.blackout, blackClose, blackCloseSpeed); 
			postProcessDelegate.GetComponent<D_PostProcess> ().StartInterpolation (PostProcessType.vignette, vignetteClose, vignetteCloseSpeed); 
		}
		StartCoroutine (Load (level, mode));
	}
		

	IEnumerator Load(string level, LoadSceneMode mode){
		async = SceneManager.LoadSceneAsync (level, mode);
		async.allowSceneActivation = false;

		while (postProcessDelegate.GetComponent<D_PostProcess> ().isFading && !async.isDone) {
			yield return new WaitForEndOfFrame ();
		}

		async.allowSceneActivation = true;
	}

}
