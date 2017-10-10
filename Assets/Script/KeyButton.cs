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

		Color hiliteColor = FormGridList.Instance.GetColorCodeForIndex(11);
		Button button = GetComponent<Button>();
		ColorBlock cb = button.colors;
		cb.highlightedColor = hiliteColor;
		button.colors = cb;
	}

	void Update () 
	{

	}

	public void ButtonSelected()
	{
		if(AudioController.Instance != null)
			AudioController.Instance.PlayButtonClick(0);

		//Debug.LogError ("Send Message form index = " + KeyIndex);
		NeckDraw.Instance.SetCurrentKey (KeyIndex);
	}
}


