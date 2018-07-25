using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void DoorOpenDelegate();
public delegate void DoorDraggingDelegate(float anglePercentage);
public delegate void DoorClosedDelegate();
public delegate void DoorStartPeekDelegate();
public delegate void DoorStopPeekDelegate();
public delegate void DoorLockedDelegate();


public class C_Door : MonoBehaviour
{
	public P_Door properties;
	[SerializeField] D_Object_Interractable_Motion motionScript;
	[SerializeField] A_Door animationDoor;
	[SerializeField] Transform keyHoleCamera;
	C_Player playerController;

	public DoorOpenDelegate open;
	public DoorDraggingDelegate drag;
	public DoorClosedDelegate close;
	public DoorLockedDelegate locked;
	public DoorStartPeekDelegate startPeek;
	public DoorStopPeekDelegate stopPeek;

	void Awake() {
		open = Open;
		drag = Drag;
		close = Close;
		locked = Locked;
		startPeek = StartPeek;
		stopPeek = StopPeek;
	}

	void Start()
	{
		motionScript.mouseDown = MouseDown;
		playerController = GameObject.FindWithTag("Player").transform.GetComponent<C_Player>();
	}

	void Update()
	{
		if (Input.GetMouseButtonUp(0))
		{
			MouseUp ();
		}
	}

	void MouseDown()
	{
		if (properties.isPeekable)
		{
			if (properties.isPeeking)
			{
				stopPeek ();
				properties.isPeeking = false;
				playerController.StopCameraTraverse();
			}
			else {
				startPeek ();
				properties.isPeeking = true;
				playerController.StartCameraTraverse();
				playerController.CameraTraverseTo(keyHoleCamera.position);
			}
		}
		else if (properties.isLocked)
		{
			animationDoor.SoundLocked();
		}
		else
		{
			if (properties.isAnimate)
			{
				if (properties.isOpen)
				{
					close ();
					properties.isOpen = false;
					if (properties.directPlay)
					{
						animationDoor.SoundClosed();
					}
				}
				else
				{
					open ();
					properties.isOpen = true;
					animationDoor.SoundOpen();
				}

			}
			else
			{
				playerController.properties.isViewLocked = true;
				animationDoor.StartDragging();

			}
		}

	}

	void MouseUp()
	{
		playerController.properties.isViewLocked = false;
		animationDoor.StopDragging();
	}

	public void Open() {}
	public void Drag(float anglePercentage) {}
	public void Close() {}
	public void Locked() {}
	public void StartPeek() {}
	public void StopPeek() {}

}
