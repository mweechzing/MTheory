using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonNoteDisplay : MonoBehaviour 
{
	public int NoteDisplayIndex = 0;

	void Start () 
	{
		string formText = FormData.Instance.gDisplayStyleText [NoteDisplayIndex];

		Text[] buttonText = GetComponentsInChildren <Text> ();

		buttonText[0].text = formText;
	}

	void Update () 
	{

	}

	public void ButtonSelected()
	{

		Debug.LogError ("Send Message form index = " + NoteDisplayIndex);


		NeckDraw.Instance.SetNoteDisplayStyle (NoteDisplayIndex);
	}
}

