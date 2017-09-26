using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapPad : MonoBehaviour 
{
	public float ScrollSpeed = 1f;
	private float camMaxY = -13.0f;
	private Vector3 startPoint;
	private Vector3 camDefault;
	private Vector3 dragOffset;
	private bool firstDrag = true;

	[HideInInspector]
	public bool DragEnabled = true;



	public static TapPad Instance;

	void Awake () 
	{
		Instance = this;
	}

	void Start () 
	{
		camDefault = Camera.main.transform.position;


		//float targetScale = ResolutionManager.Instance.GetTargetScale();

		camMaxY  = ResolutionManager.Instance.GetCamLimY();

	}
	
	void Update () 
	{}

	void OnMouseDown()
	{
		if (DragEnabled == false)
			return;


		//Vector3 ms = Input.mousePosition;
		//startPoint = Camera.main.ScreenToWorldPoint(ms);
		startPoint = Camera.main.transform.position;

		//Vector3 currentPoint = Camera.main.ScreenToWorldPoint(ms);
		//Debug.Log (" MDown startPoint.y = " + startPoint.y + " currentPoint.y = " + currentPoint.y + " ms.y = " + ms.y);

		firstDrag = true;
	}
	void OnMouseDrag()
	{
		if (DragEnabled == false)
			return;


		Vector3 ms;

		if (firstDrag == true) {
			firstDrag = false;

			ms = Input.mousePosition;
			dragOffset = Camera.main.ScreenToWorldPoint(ms);

			//dragOffset.y *= -1f;

			//Debug.Log ("dragOffset.y = " + dragOffset.y);
		}

		ms = Input.mousePosition;
		Vector3 currentPoint = Camera.main.ScreenToWorldPoint(ms);

		float dOffset = dragOffset.y - currentPoint.y;
		//float dOffset = currentPoint.y - dragOffset.y;

		//Debug.Log ("...........startPoint.y = " + startPoint.y + " currentPoint.y = " + currentPoint.y + " dOffset = " + dOffset);


		float dy = startPoint.y - (dOffset * ScrollSpeed);


		//Debug.Log ("...........startPoint.y = " + startPoint.y + " currentPoint.y = " + currentPoint.y + " dOffset = " + dOffset + " dy = " + dy);

		if (dy < camMaxY) {
			
			dy = camMaxY;

		} else if (dy > 0) {

			//Debug.Log ("dy > 0 startPoint.y = " + startPoint.y + " currentPoint.y = " + currentPoint.y + " ms.y = " + ms.y);

			dy = 0f;
		}


		Camera.main.transform.position = new Vector3 (camDefault.x, dy, camDefault.z);

		//Debug.Log ("dy = " + dy );

	}
	void OnMouseUp()
	{
		if (DragEnabled == false)
			return;


		//Debug.Log ("Mouse up");

	}

}
