using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class A_Door : MonoBehaviour
{

	public float distanceAngle;
	float defaultDistanceAngle;
	public float draggingSpeed, animationSpeed;
	float defaultAnimationSpeed;
	//editor
	[SerializeField]
	C_Door controller;
	[SerializeField]
	Transform doorObject;
	[SerializeField]
	AudioClip opening, closing, locked;
	public AudioSource doorSoundSource;

	bool isDragging;


	//own class properties
	Transform playerTransform;
	bool isOpeningSoundDone = false, isClosingSoundDone = true;
	float crackingPauseDuration = 0f;
	Vector3 defaultRotation;

	//Mono Behaviour
	void Start()
	{
		defaultAnimationSpeed = animationSpeed;
		defaultDistanceAngle = distanceAngle;
		defaultRotation = new Vector3(doorObject.localEulerAngles.x, 180f, doorObject.localEulerAngles.z);
	}

	public void Reset() {
		distanceAngle = defaultDistanceAngle;
		animationSpeed = defaultAnimationSpeed;
	}

	void Update()
	{
		//change automatically angle percentage when AnimateType is Animate
		if (controller.properties.isAnimate)
		{
			if (controller.properties.isOpen && !controller.properties.isLocked)
			{
				controller.properties.anglePercentage = controller.properties.anglePercentage + animationSpeed * Time.deltaTime > 100 ? 100 : controller.properties.anglePercentage + animationSpeed * Time.deltaTime;
				isClosingSoundDone = false;
			}
			else
			{
				controller.properties.anglePercentage = controller.properties.anglePercentage - animationSpeed * Time.deltaTime < 0 ? 0 : controller.properties.anglePercentage - animationSpeed * Time.deltaTime;
			}

			if (!controller.properties.isOpen)
			{
				if (Mathf.FloorToInt(controller.properties.anglePercentage) <= 5 && !isClosingSoundDone)
				{
					SoundClosed();
				}
			}
		}
		else
		{
			if (isDragging) Hold();
		}

		//change rotation according to DoorType
		if (controller.properties.isPush)
		{
			doorObject.localEulerAngles = Vector3.Lerp(defaultRotation,
				new Vector3(defaultRotation.x, defaultRotation.y + distanceAngle, defaultRotation.z),
				controller.properties.anglePercentage / 100);
		}
		else
		{
			doorObject.localEulerAngles = Vector3.Lerp(defaultRotation,
				new Vector3(defaultRotation.x, defaultRotation.y - distanceAngle, defaultRotation.z),
				controller.properties.anglePercentage / 100);
		}

	}

	public void SoundOpen()
	{
		if (!isOpeningSoundDone)
		{
			doorSoundSource.clip = opening;
			doorSoundSource.Play();
			isOpeningSoundDone = true;
			isClosingSoundDone = false;
		}
	}

	public void SoundClosed()
	{
		if (!isClosingSoundDone)
		{
			doorSoundSource.clip = closing;
			doorSoundSource.Play();
			isClosingSoundDone = true;
			isOpeningSoundDone = false;
		}
	}
	public void SoundLocked()
	{
		doorSoundSource.clip = locked;
		doorSoundSource.Play();
	}

	public void StartDragging()
	{
		isDragging = true;
	}
	public void StopDragging()
	{
		isDragging = false;
	}

	void Hold()
	{
		controller.drag (controller.properties.anglePercentage);
		float dy = Input.GetAxis("Mouse Y") * draggingSpeed * Mathf.Sign(Camera.main.transform.forward.z);
		if (!controller.properties.isLocked && !controller.properties.isAnimate)
		{
			Vector3 lastRotation = doorObject.localEulerAngles;
			if (controller.properties.isPush)
			{
				if (lastRotation.y - dy >= defaultRotation.y && lastRotation.y - dy <= defaultRotation.y + distanceAngle)
					lastRotation.y -= dy;
			}
			else {
				if (lastRotation.y - dy <= defaultRotation.y && lastRotation.y - dy >= defaultRotation.y - distanceAngle)
					lastRotation.y -= dy;
			}

			controller.properties.anglePercentage = Mathf.Abs((lastRotation.y - defaultRotation.y) / distanceAngle * 100);
		}
		if (Mathf.FloorToInt(controller.properties.anglePercentage) <= 20 && Mathf.FloorToInt(controller.properties.anglePercentage) >= 11)
		{
			isOpeningSoundDone = false;
			isClosingSoundDone = false;
		}

		if (controller.properties.isPush)
		{
			if (Mathf.FloorToInt(controller.properties.anglePercentage) >= 2 && Mathf.FloorToInt(controller.properties.anglePercentage) <= 10 && dy < 0)
			{
				SoundOpen();
			}

			//close sound play in 95-100
			if (Mathf.FloorToInt(controller.properties.anglePercentage) <= 5 && dy > 0)
			{
				SoundClosed();
			}

		}
		else
		{
			if (Mathf.FloorToInt(controller.properties.anglePercentage) >= 2 && Mathf.FloorToInt(controller.properties.anglePercentage) <= 10 && dy > 0)
			{
				SoundOpen();
			}

			if (Mathf.FloorToInt(controller.properties.anglePercentage) <= 5 && dy < 0)
			{
				SoundClosed();
			}
		}

	}


}
