using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_Door : MonoBehaviour {
	
	public bool isAnimate, isPush, isLocked, isOpen, isPeekable, isPeeking;
	[Range(0f, 100f)]
	public float anglePercentage;
	public bool directPlay = false;
}
