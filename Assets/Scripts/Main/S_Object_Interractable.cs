using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void UnHoverDelegate(Transform hoverObject);
public delegate void HoverDelegate(Transform hoverObject);
public delegate void HoldDelegate();
public delegate void MouseDownDelegate();
public delegate void MouseUpDelegate();

public class S_Object_Interractable : MonoBehaviour {
	public HoverDelegate active;
	public UnHoverDelegate inactive;
	[TextArea(3, 10)]
	public string scriptText;
	public AudioClip voiceOverText;

	// Use this for initialization
	void Start () {
		active = Active;
		inactive = InActive;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public virtual void Active(Transform activeObject) {
		Debug.LogWarning("Active for object \"" + activeObject.name + "\" delegate hasn't been setup yet");
	}

	public virtual void InActive(Transform activeObject) {
		Debug.LogWarning("InActive for object \"" + activeObject.name + "\" delegate hasn't been setup yet");
	}

	public virtual void Test(){}

}
