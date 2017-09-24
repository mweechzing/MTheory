using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyButton : MonoBehaviour 
{
	public int KeyIndex = 0;

	void Start () 
	{
		string formText = FormData.Instance.gKeyNames1 [KeyIndex];

		Text[] buttonText = GetComponentsInChildren <Text> ();

		buttonText[0].text = formText;
	}

	void Update () 
	{

	}

	public void ButtonSelected()
	{
		//Debug.LogError ("Send Message form index = " + KeyIndex);
		NeckDraw.Instance.SetCurrentKey (KeyIndex);
		CurrentSelection.Instance.RefreshSelectedForm();
	}
}


