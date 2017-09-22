using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FormGridList : MonoBehaviour 
{
	public GameObject GridLayout;

	public GameObject GoButton;
	public TMP_Text InfoPanel;

	private int _selectedformIndex = 0;
	public int SelectedFormIndex {
		get {return _selectedformIndex; } 
		set {_selectedformIndex = value; }
	}

	public static FormGridList Instance;

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

	//reprecated
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

		int findex = 0;
		foreach(Button bObj in glgButtons)
		{
			FormButton objectScript = bObj.GetComponent<FormButton> ();

			objectScript.FormIndex = findex;

			objectScript.Refresh ();

			findex++;
		}

	}
		

	public void SetSelectedFormIndex(int fIndex)
	{
		_selectedformIndex = fIndex;

		string formText = FormData.Instance.gFormText [fIndex];

		int keyIndex = NeckDraw.Instance.GetCurrentKey ();

		string noteText = FormData.Instance.gKeyNamesSharp[keyIndex];


		InfoPanel.SetText (noteText + " " + formText);
	}

	public void ButtonGO()
	{

		Debug.LogError ("Send Message form index = " + _selectedformIndex);

		NeckDraw.Instance.ReApplyForm (_selectedformIndex);

		gameObject.SetActive (false);

		TapPad.Instance.DragEnabled = true;

	}

	public void ButtonShowSelf()
	{
		TapPad.Instance.DragEnabled = false;

		gameObject.SetActive (true);
	}


}
