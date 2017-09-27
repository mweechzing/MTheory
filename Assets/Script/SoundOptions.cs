using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundOptions : MonoBehaviour 
{

	void Start () 
	{
		
	}
	

	public void ButtonGO()
	{
		gameObject.SetActive (false);
		TapPad.Instance.DragEnabled = true;
	}

	public void ButtonShowSelf()
	{
		TapPad.Instance.DragEnabled = false;
		gameObject.SetActive (true);
	}

}
