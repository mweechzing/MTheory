using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CurrentSelection : MonoBehaviour 
{
	public TMP_Text InfoPanel;
	public TMP_Text InfoPanel2;

	public static CurrentSelection Instance = null;

	void Awake () 
	{
		Instance = this;

	}
		
	public void RefreshSelectedForm()
	{
		int formIndex = NeckDraw.Instance.GetCurrentFormIndex ();
		string formText = FormData.Instance.gFormText [formIndex];
		int keyIndex = NeckDraw.Instance.GetCurrentKey ();
		string noteText = FormData.Instance.gKeyNamesSharp[keyIndex];

		InfoPanel.SetText (noteText + " " + formText);
		InfoPanel2.SetText (noteText + " " + formText);
	}
}
