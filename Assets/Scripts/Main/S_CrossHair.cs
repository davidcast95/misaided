using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class S_CrossHair : MonoBehaviour {

	[SerializeField] private Image crosshair;
	private bool isOn;

	private Transform currentObject, playerTransform;


	// Use this for initialization
	void Start () {
		isOn = false;
		crosshair.GetComponent<Image> ().CrossFadeAlpha (0f, 0f, true);
		playerTransform = GameObject.FindGameObjectWithTag ("Player").transform;
	}
	
	// Update is called once per frame
	void Update () {
		Cast ();
		if (Input.GetKey (KeyCode.Mouse0)) {
//			Shoot ();
			//Dewe
			Hold ();
		}
		if (Input.GetKeyDown(KeyCode.Mouse0)) {
			MouseDown ();
		}
		if (Input.GetKeyUp (KeyCode.Mouse0)) {
			MouseUp ();
		}
	}

	void Cast(){
		if (playerTransform.GetComponent<P_Player>().isPOV && !playerTransform.GetComponent<P_Player>().isObserving)
		{
			Ray ray = Camera.main.ViewportPointToRay(new Vector3(.5f, .5f, 0f));
			int[] layerTest = { 8, 9 };
			for (int layer = 0; layer < layerTest.Length; layer++)
			{
				RaycastHit[] hits;
				hits = Physics.RaycastAll(ray, 15, 1 << layerTest[layer]);
				for (int i = 0; i < hits.Length; i++)
				{
					if (hits[i].transform.GetComponent<S_Object_Interractable>())
					{
						SetCrosshairVisibility(true);
						currentObject = hits[i].transform;
						currentObject.GetComponent<S_Object_Interractable> ().active(currentObject);
						return;
					}
				}
				if (hits.Length == 0) {
					SetCrosshairVisibility(false);
					if (currentObject)
						if (currentObject.GetComponent<S_Object_Interractable>())
							currentObject.GetComponent<S_Object_Interractable> ().inactive(currentObject);
					currentObject = null;
				}
			}
		}
	}

	public void SetCrosshairVisibility(bool status){
		if (status != isOn) {
			isOn = status;
			if (status) {
				crosshair.GetComponent<Image> ().CrossFadeAlpha (1f, .2f, true);
			} else {
				crosshair.GetComponent<Image> ().CrossFadeAlpha (0f, .2f, true);
			}
		}
	}

	void Hold() {
		StartCoroutine (Blip());
		if (currentObject) {
			if (currentObject.GetComponent<D_Object_Interactable_Observable>())
				currentObject.GetComponent<D_Object_Interactable_Observable>().hold();
			if (currentObject.GetComponent<D_Object_Interractable_Motion>())
				currentObject.GetComponent<D_Object_Interractable_Motion> ().hold ();
		}
	}
	void MouseUp() {
		SetCrosshairVisibility(false);
		if (currentObject) {
			if (currentObject.GetComponent<D_Object_Interractable_Motion>())
				currentObject.GetComponent<D_Object_Interractable_Motion> ().mouseUp ();
		}

		StartCoroutine (Blip());

	}
	void MouseDown() {
		if (currentObject) {
			if (currentObject.GetComponent<D_Object_Interactable_Observable>())
				currentObject.GetComponent<D_Object_Interactable_Observable>().mouseDown();
			if (currentObject.GetComponent<D_Object_Interractable_Motion>())
				currentObject.GetComponent<D_Object_Interractable_Motion> ().mouseDown ();
		}
		StartCoroutine (Blip());
	}

	void Shoot(){
		StartCoroutine (Blip());
		if (currentObject) {
//			currentObject.GetComponent<Object_Interractable_Motion> ().Test ();
		}
	}

	IEnumerator Blip(){
		crosshair.GetComponent<Image> ().CrossFadeColor (Color.black, .2f, true, true);
		yield return new WaitForSeconds (.2f);
		crosshair.GetComponent<Image> ().CrossFadeColor (Color.red, .2f, true, true);
	}
}
