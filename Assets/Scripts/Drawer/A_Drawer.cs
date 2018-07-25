using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[ExecuteInEditMode]
public class A_Drawer : MonoBehaviour {
	
	public float speed;
	float defaultSpeed;
	[SerializeField]
	C_Drawer controller;
	[SerializeField]
	Transform drawerObject;
	[SerializeField]
	float minSliding, maxSliding;
	[SerializeField]
	AudioClip openSound,closeSound;
	public AudioSource drawerSound;

	Transform playerTransform;
	Vector3 defaultPosition;


	void Start () {
		defaultSpeed = speed;
		defaultPosition = new Vector3(drawerObject.transform.localPosition.x, drawerObject.transform.localPosition.y, minSliding);
	}

	public void Reset() {
		speed = defaultSpeed;
	}

	// Update is called once per frame
	void Update () {
		if (controller.properties.isAnimate) {
			if (controller.properties.isOpen && !controller.properties.isLocked) {
				controller.properties.slidingPercentage = controller.properties.slidingPercentage + speed * Time.deltaTime > 100 ? 100 : controller.properties.slidingPercentage + speed * Time.deltaTime;
			} else {
				controller.properties.slidingPercentage = controller.properties.slidingPercentage - speed * Time.deltaTime < 0 ? 0 : controller.properties.slidingPercentage - speed * Time.deltaTime;
			}
		}
		drawerObject.transform.localPosition = Vector3.Lerp(defaultPosition,
			new Vector3(defaultPosition.x,defaultPosition.y,defaultPosition.z + (maxSliding - minSliding)),
			controller.properties.slidingPercentage / 100f);

		if (controller.properties.slidingPercentage >= 100f)
		{
			for (int i = 0; i < controller.container.childCount; i++)
			{
				if (controller.container.GetChild(i).GetComponent<P_Observer>())
				{
					controller.container.GetChild(i).GetComponent<P_Observer>().isObservable = true;
				}
				if (controller.container.GetChild(i).GetComponent<BoxCollider>())
				{
					controller.container.GetChild(i).GetComponent<BoxCollider>().enabled = true;
				}
			}
		}
		if (controller.properties.slidingPercentage <= 0f)
		{
			for (int i = 0; i < controller.container.childCount; i++)
			{
				if (controller.container.GetChild(i).GetComponent<P_Observer>())
				{
					controller.container.GetChild(i).GetComponent<P_Observer>().isObservable = false;
				}
				if (controller.container.GetChild(i).GetComponent<BoxCollider>())
				{
					controller.container.GetChild(i).GetComponent<BoxCollider>().enabled = false;
				}
			}
		}

	}

	public void Open()
	{
		drawerSound.clip = openSound;
		drawerSound.Play();
	}
	public void Close()
	{
		drawerSound.clip = closeSound;
		drawerSound.Play();
	}

}
