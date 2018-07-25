using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class S_Loading : MonoBehaviour {

	[Header("Delegates")]
	[SerializeField] GameObject postProcessDelegate;
	[SerializeField] GameObject sceneDelegate;
	bool isOpen, isLoading;

	[Header("Scene FX")]
	[SerializeField] float startDelay, waitDelay;
	[SerializeField] float startSpeed;
	[SerializeField] float vignetteOpen, vignetteClose;
	[SerializeField] float vignetteOpenSpeed, vignetteCloseSpeed;

	[SerializeField] float blackOpen;
	[SerializeField] float blackClose, blackCloseSpeed;

	S_GameInstance gameInstance;

	[Header("Loading Section")]
	[SerializeField] Canvas loadingCanvas;
	[SerializeField] Camera mainCamera;
	AsyncOperation asyncGame, asyncEnv;


	void Start () {
		isOpen = false; isLoading = false;
		postProcessDelegate.GetComponent<D_PostProcess> ().SetInitialPostProcess (PostProcessType.vignette, vignetteClose);
		postProcessDelegate.GetComponent<D_PostProcess> ().SetInitialPostProcess (PostProcessType.blackout, blackOpen);


		StartCoroutine (LoadMainLevels ());
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (!postProcessDelegate.GetComponent<D_PostProcess> ().isFading && !isLoading) {
			if (isOpen) {
				postProcessDelegate.GetComponent<D_PostProcess> ().StartInterpolation (PostProcessType.vignette, vignetteClose, vignetteCloseSpeed);
			} else if (!isOpen) {
				postProcessDelegate.GetComponent<D_PostProcess> ().StartInterpolation (PostProcessType.vignette, vignetteOpen, vignetteOpenSpeed, 0.002f, InterpolationType.lerp);
			}
			isOpen = !isOpen;
		}
	}

	IEnumerator LoadMainLevels(){
		asyncEnv = SceneManager.LoadSceneAsync ("Environment", LoadSceneMode.Additive);
		asyncEnv.allowSceneActivation = false;
		asyncGame = SceneManager.LoadSceneAsync ("Game", LoadSceneMode.Additive);
		asyncGame.allowSceneActivation = false;

		yield return new WaitForSeconds (startDelay);


		while ((asyncEnv.progress < 0.89f) && (asyncGame.progress < 0.89f)) {
			yield return new WaitForEndOfFrame ();	
		}
		isLoading = true;
		postProcessDelegate.GetComponent<D_PostProcess> ().StartInterpolation (PostProcessType.vignette, vignetteClose, vignetteCloseSpeed);
		postProcessDelegate.GetComponent<D_PostProcess> ().StartInterpolation (PostProcessType.blackout, blackClose, blackCloseSpeed);



		yield return new WaitForSeconds (waitDelay);
		loadingCanvas.gameObject.SetActive (false);

		asyncEnv.allowSceneActivation = true;
		asyncGame.allowSceneActivation = true;

		Scene gameScene = SceneManager.GetSceneByBuildIndex (3);



		while (!SceneManager.SetActiveScene (gameScene)) {
			yield return new WaitForEndOfFrame ();
			mainCamera.gameObject.SetActive (false);
		}



	}
		
}
