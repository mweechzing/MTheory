using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StyleButton : MonoBehaviour 
{
	public int StyleIndex = 0;

	void Start () 
	{
		string styleText = FormData.Instance.gNoteGraphicStyleText [StyleIndex];

		Text[] buttonText = GetComponentsInChildren <Text> ();

		buttonText[0].text = styleText;

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
		NeckDraw.Instance.SetCurrentStyleIndex (StyleIndex);
	}
}

