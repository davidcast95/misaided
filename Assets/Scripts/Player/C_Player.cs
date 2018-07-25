using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class C_Player : MonoBehaviour {

	public P_Player properties;
	[SerializeField]
	A_Player animationPlayer;
	[SerializeField]
	Slider sanityBar;
	[SerializeField]
	Text scriptText;
	[SerializeField]
	Light observeLight;

	// Use this for initialization
	void Start () {
		//SetFollower (GameObject.Find ("FollowerDummy"));
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Alpha1)) {
			CastSkill (0);
		}
		if (Input.GetKeyDown (KeyCode.Alpha2)) {
			CastSkill (2);
		}
		if (Input.GetKeyDown (KeyCode.Alpha3)) {
			CastSkill (3);
		}

		//update canvas bar
		sanityBar.value = properties.sanity / (properties.sanity_MAX - properties.sanity_MIN) + 0.5f;
	}

	// -- CAMERA PERSON VIEW CONTROLLER --
	public void StartCameraTraverse()
	{
		animationPlayer.SwapCameraToTraverse();
		properties.isCameraTraverse = true;
		properties.isPOV = false;
	}
	public void StopCameraTraverse()
	{
		properties.isCameraTraverse = false;
	}
	public void CameraTraverseTo(Vector3 targetPosition)
	{
		animationPlayer.SetTargetForCameraTraverse(targetPosition);
	}

	public void UpdateSanity(float val){
		if (!(properties.sanity + val > properties.sanity_MAX || properties.sanity + val < properties.sanity_MIN)) {
			properties.sanity += val;
		}
	}

	public void SetFollower(GameObject ghost){
		properties.follower = ghost;
		ghost.transform.SetParent (gameObject.transform);
		CastPassives ();
		//update canvas for skills (passives and actives checking)
	}

	public void CastSkill(int index){
		properties.follower.GetComponent<C_Ghost> ().CastSkill (gameObject, index);
	}

	public void CastPassives(){
		properties.follower.GetComponent<C_Ghost> ().CastPassives (gameObject);
	}

	// -- SKILL BASED FUNCTIONS --
	public void LimboShift(bool c){
		properties.isInLimbo = c;
	}



	public void StartObserve(Transform observeObject)
	{
		GetComponent<S_CrossHair>().SetCrosshairVisibility(false);
		observeObject.GetComponent<C_Observer>().SetFrontPositionOf(Camera.main.transform);
		properties.isObserving = true;
		observeLight.enabled = true;
		sanityBar.gameObject.SetActive(false);
		scriptText.text = observeObject.GetComponent<S_Object_Interractable>().scriptText;
		scriptText.enabled = true;
	}
	public void StopObserve(Transform observeObject)
	{
		observeObject.GetComponent<C_Observer>().Reset();
		properties.isObserving = false;
		observeLight.enabled = false;
		sanityBar.gameObject.SetActive(true);
		scriptText.text = "";
		scriptText.enabled = false;
	}

}
