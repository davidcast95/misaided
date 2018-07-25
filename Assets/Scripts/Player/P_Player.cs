using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PostProcessing;

public class P_Player : MonoBehaviour {

	public bool isCameraTraverse, isPOV, isViewLocked, isObserving;

	public float sanity, sanity_MAX, sanity_MIN;
	public GameObject follower;
	public GameObject postProcessDelegate;
	public bool isInLimbo;

	//open = normal
	public Vector3 blueOpen, blueClose;
	public float blueOpenSpeed, blueCloseSpeed;
	public float chromaOpen, chromaClose, chromaOpenSpeed, chromaCloseSpeed;

	// Use this for initialization
	void Start () {
		sanity = 0;
		postProcessDelegate.GetComponent<D_PostProcess> ().SetInitialPostProcessVector (PostProcessType.blue, blueOpen);
		postProcessDelegate.GetComponent<D_PostProcess> ().SetInitialPostProcess (PostProcessType.chromatic, chromaOpen);
	}


}
