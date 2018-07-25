using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class A_Observer : MonoBehaviour {

	Transform playerTransform;

	void Start()
	{
		playerTransform = GameObject.FindWithTag("Player").transform;
	}

	void Update () {
		if (playerTransform.GetComponent<P_Player>().isObserving)
		{
			float horizontal = Input.GetAxis("Mouse X");
			float vertical = Input.GetAxis("Mouse Y");
			print(horizontal);
			print(vertical);

			Vector3 oldRotation = transform.eulerAngles;

			transform.rotation = Quaternion.Euler(new Vector3((oldRotation.x - (vertical * 2f)), oldRotation.y, (oldRotation.z + horizontal)));
		}
	}
}
