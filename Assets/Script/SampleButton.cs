using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SampleButton : MonoBehaviour 
{

	public int SampleIndex = 0;
	public int SampleType = 0;

	void Start () 
	{
		/*
		string formText;
		if (SampleType == 0) {
			formText = FormData.Instance.gSampleText [SampleIndex];
		} else {
			formText = FormData.Instance.gDroneText [SampleIndex];		
		}
			
		Text[] buttonText = GetComponentsInChildren <Text> ();

		buttonText[0].text = formText;
		*/
	}

	void Update () 
	{

	}

	public void ButtonSelected()
	{
		SoundOptions.Instance.SetSampleOptions (SampleType, SampleIndex);

		/*
		AudioController.Instance.PlayButtonClick(0);
		if (SampleType == 0) {
			AudioController.Instance.SetSampleBank (SampleIndex);
		} else {
			AudioController.Instance.SetDroneBank (SampleIndex);		
		}
		*/
		//Debug.LogError ("Send Message form index = " + KeyIndex);
		//NeckDraw.Instance.SetCurrentKey (KeyIndex);
	}
}
