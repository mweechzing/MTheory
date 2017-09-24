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
	void Start () 
	{
		Refresh ();
	}
	
	public void Refresh () 
	{
		string formText = FormData.Instance.gFormText [FormIndex];

		Text[] buttonText = GetComponentsInChildren <Text> ();
		buttonText[0].text = formText;

		if (formText == "space") {

			buttonText[0].text = " ";

			Button b = GetComponent<Button> ();
			b.interactable = false;

		}

	}

	public void ButtonSelected()
	{
		//Debug.LogError ("Send Message form index = " + _formIndex);
		NeckDraw.Instance.SetCurrentFormIndex(FormIndex);
	}
}
