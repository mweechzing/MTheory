using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FormButton : MonoBehaviour 
{
	private int _formIndex = 7;
	public int FormIndex {
		get {return _formIndex; } 
		set {_formIndex = value; }
	}
	void Start () 
	{
		string formText = FormData.Instance.gFormText [FormIndex];

		Text[] buttonText = GetComponentsInChildren <Text> ();

		buttonText[0].text = formText;
	}
	
	void Update () 
	{
		
	}

	public void ButtonSelected()
	{

		Debug.LogError ("Send Message form index = " + _formIndex);


		FormGridList.Instance.SetSelectedFormIndex (_formIndex);
	}
}
