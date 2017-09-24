using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FormGridList : MonoBehaviour 
{
	public GameObject GridLayout;

	public GameObject GoButton;

	public ColorSet FormColorCodes;

	private int _selectedformIndex = 0;
	public int SelectedFormIndex {
		get {return _selectedformIndex; } 
		set {_selectedformIndex = value; }
	}

	public static FormGridList Instance = null;

	void Awake () 
	{
		Instance = this;

	}

	void Start () 
	{
		//LoadFormButtonObjects ();
		SetFormButtonObjects();
	}
	
	void Update () 
	{}

	//deprecated
	private void LoadFormButtonObjects()
	{
		for (int t = 0; t < 100; t++) {

			GameObject _sfObj = Instantiate (Resources.Load ("Prefabs/FormButton", typeof(GameObject))) as GameObject;

			if (_sfObj != null) {

				if (GridLayout != null) {
					_sfObj.transform.parent = GridLayout.transform;
				}
				_sfObj.name = "FormButton_" + t.ToString ();

				FormButton objectScript = _sfObj.GetComponent<FormButton> ();
				objectScript.FormIndex = t;

			} else {

				Debug.Log ("Couldn't load marker object prefab");
			}
		}
	}

	private void SetFormButtonObjects()
	{
		GridLayoutGroup glg = GetComponentInChildren<GridLayoutGroup> ();

		Button[] glgButtons = glg.GetComponentsInChildren<Button> ();

		int colorIndex = 0;
		int findex = 0;
		foreach(Button bObj in glgButtons)
		{
			string formText = FormData.Instance.gFormText [findex];
			if (formText == "space") {
				colorIndex = FormData.Instance.gFormData [findex * Globals.FormQuantum];
				findex++;
			}
				
			FormButton objectScript = bObj.GetComponent<FormButton> ();
			objectScript.FormIndex = findex;
			objectScript.ColorIndex = colorIndex;
			objectScript.Refresh ();
			findex++;
		}
	}
		
	public void ButtonGO()
	{
		//Debug.LogError ("Send Message form index = " + _selectedformIndex);
		//NeckDraw.Instance.ReApplyForm (_selectedformIndex);
		gameObject.SetActive (false);
		TapPad.Instance.DragEnabled = true;
	}

	public void ButtonShowSelf()
	{
		TapPad.Instance.DragEnabled = false;
		gameObject.SetActive (true);
	}

	public Color GetColorCodeForIndex(int index)
	{
		return( GetColorFromSet(FormColorCodes, index));
	}

	
	private Color GetColorFromSet(ColorSet cs, int index)
	{
		Color rc = cs.Color1;
		switch(index)
		{
		case 0:
			rc = cs.Color1;
			break;
		case 1:
			rc = cs.Color2;
			break;
		case 2:
			rc = cs.Color3;
			break;
		case 3:
			rc = cs.Color4;
			break;
		case 4:
			rc = cs.Color5;
			break;
		case 5:
			rc = cs.Color6;
			break;
		case 6:
			rc = cs.Color7;
			break;
		case 7:
			rc = cs.Color8;
			break;
		case 8:
			rc = cs.Color9;
			break;
		case 9:
			rc = cs.Color10;
			break;
		case 10:
			rc = cs.Color11;
			break;
		case 11:
			rc = cs.Color12;
			break;
		case 12:
			rc = cs.ColorText;
			break;
		}

		return rc;
	}

}
