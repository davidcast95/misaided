using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class A_Player : MonoBehaviour
{

	[SerializeField]
	Transform cameraPOV, cameraTraverse;

	public float cameraTraverseSpeed = 100f;
	[SerializeField]
	C_Player controller;


	Vector3 targetCameraTraversePosition;
	float durationCameraTraverse = 0f;

	void Start()
	{
		if (controller.properties.isPOV)
		{
			SwapCameraToPOV();
		}
	}

	void Update()
	{
		if (!controller.properties.isPOV)
		{
			if (controller.properties.isCameraTraverse)
			{
				durationCameraTraverse = durationCameraTraverse + Time.deltaTime * cameraTraverseSpeed > 100 ? 100 : durationCameraTraverse + Time.deltaTime * cameraTraverseSpeed;
				cameraTraverse.position = Vector3.Lerp(cameraPOV.position, targetCameraTraversePosition, durationCameraTraverse / 100);
			}
			else {
				durationCameraTraverse = durationCameraTraverse - Time.deltaTime * cameraTraverseSpeed < 0 ? 0 : durationCameraTraverse - Time.deltaTime * cameraTraverseSpeed;
				if (durationCameraTraverse < 0.1)
				{
					SwapCameraToPOV();
					controller.properties.isPOV = true;
				}
				else {
					cameraTraverse.position = Vector3.Lerp(cameraPOV.position, targetCameraTraversePosition, durationCameraTraverse / 100);
				}
			}
		}
	}

	public void SwapCameraToTraverse()
	{
		cameraPOV.gameObject.SetActive(false);
		cameraTraverse.gameObject.SetActive(true);
		durationCameraTraverse = 0f;
	}

	public void SwapCameraToPOV()
	{
		cameraPOV.gameObject.SetActive(true);
		cameraTraverse.gameObject.SetActive(false);
	}

	public void SetTargetForCameraTraverse(Vector3 targetPos)
	{
		targetCameraTraversePosition = targetPos;
	}
}

