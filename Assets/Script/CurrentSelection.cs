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

		int len = formText.Length;
		if(len > 20) {
			InfoPanel.fontSize = 36;
			InfoPanel2.fontSize = 50;
		} else {		
			InfoPanel.fontSize = 48;
			InfoPanel2.fontSize = 60;
		}
			
		InfoPanel.SetText (noteText + "  " + formText);
		InfoPanel2.SetText (noteText + "  " + formText);
	}
}
