using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICanvas : MonoBehaviour 
{
	public GameObject MainPanel;
	public GameObject SoundOptions;
	public GameObject HeaderDisplayButton;

	private bool OnOff = true;
	private bool OptionsOnOff = false;

	void Start () 
	{
		
	}
	
	public void ToggleMainPanel () 
	{
		if(AudioController.Instance != null)
			AudioController.Instance.PlayButtonClick(1);

		if (OnOff == true) {
			MainPanel.gameObject.SetActive (false);
			OnOff = false;
		} else {
			MainPanel.gameObject.SetActive (true);
			OnOff = true;		
		}
	}

	public void ToggleOptionsPanel () 
	{
		if(AudioController.Instance != null)
			 AudioController.Instance.PlayButtonClick(1);

		if (OnOff == true) {
			MainPanel.gameObject.SetActive (false);
			HeaderDisplayButton.gameObject.SetActive(false);
			SoundOptions.gameObject.SetActive(true);
			OnOff = false;
		} else {
			MainPanel.gameObject.SetActive (true);
			HeaderDisplayButton.gameObject.SetActive(true);
			SoundOptions.gameObject.SetActive(false);
			OnOff = true;
		}
	}


	public void SwitchToFretboard () 
	{
		NeckDraw.Instance.RefreshDrawArea(NeckDraw.DrawMode.Guitar);
	}
		
	public void SwitchToKeyBoard () 
	{
		NeckDraw.Instance.RefreshDrawArea(NeckDraw.DrawMode.Piano);
	}

}
