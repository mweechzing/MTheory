using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FormButton : MonoBehaviour 
{
	private int _formIndex = 0;
	public int FormIndex {
		get {return _formIndex; } 
		set {_formIndex = value; }
	}

	private int _colorIndex = 0;
	public int ColorIndex {
		get {return _colorIndex; } 
		set {_colorIndex = value; }
	}


	void Start () 
	{
		//Refresh ();
	}
	
	public void Refresh () 
	{
		string formText = FormData.Instance.gFormText [FormIndex];

		int len = formText.Length;
		//Debug.LogError("formText = " + formText + "  len = " + len + " FormIndex = " + FormIndex);
		Text[] buttonText = GetComponentsInChildren <Text> ();
		buttonText[0].text = formText;

		if(len > 10) {
			buttonText[0].fontSize = 30;
			buttonText[0].resizeTextMaxSize = 30;
		}

		Color hiliteColor = FormGridList.Instance.GetColorCodeForIndex(11);

		Color newColor = FormGridList.Instance.GetColorCodeForIndex(ColorIndex);
		Button button = GetComponent<Button>();
		ColorBlock cb = button.colors;
		cb.normalColor = newColor;
		cb.highlightedColor = hiliteColor;

		button.colors = cb;

	}

	public void ButtonSelected()
	{
		if(AudioController.Instance != null)
			AudioController.Instance.PlayButtonClick(0);
		
		//Debug.LogError ("Send Message form index = " + _formIndex);
		NeckDraw.Instance.SetCurrentFormIndex(FormIndex);
	}
}
