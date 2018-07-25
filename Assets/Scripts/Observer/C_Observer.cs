using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C_Observer : MonoBehaviour {

	[SerializeField]
	P_Observer properties;
	[SerializeField]
	D_Object_Interactable_Observable observeScript;
	public GameObject playerGameObject;
	Vector3 originalPosition, originalScale;
	Quaternion originalRotation;

	void Start()
	{
		originalPosition = transform.localPosition;
		originalScale = transform.localScale;
		originalRotation = transform.localRotation;
		playerGameObject = GameObject.FindWithTag("Player");
		observeScript.mouseDown = MouseDown;
	}

	void Update()
	{
		if (Input.GetMouseButtonUp(0) && playerGameObject.GetComponent<P_Player>().isObserving)
		{
			playerGameObject.GetComponent<C_Player>().StopObserve(transform);
		}
	}

	void MouseDown()
	{
		if (properties.isObservable)
		{
			playerGameObject.GetComponent<C_Player>().StartObserve(transform);
		}
	}

	public void Reset()
	{
		transform.localPosition = originalPosition;
		transform.localScale = originalScale;
		transform.localRotation = originalRotation;
	}

	public void SetFrontPositionOf(Transform cameraTransform)
	{
		transform.position = cameraTransform.position + (cameraTransform.forward * 1.5f);
		transform.LookAt(playerGameObject.transform);
		transform.localScale = Vector3.one;
	}


}
