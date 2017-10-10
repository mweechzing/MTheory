using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICanvas : MonoBehaviour 
{
	public GameObject MainPanel;
	public GameObject SoundOptions;
	public GameObject HeaderDisplayButton;
	public GameObject FormGridListDisplay;

	private bool OnOff = true;
	private bool FormListState = false;


	public static UICanvas Instance = null;

	void Awake () 
	{
		Instance = this;

	}

	void Start () 
	{
		FormGridListDisplay.SetActive(false);
		SoundOptions.SetActive(false);
	}
	
	public void ToggleMainPanel () 
	{
		if(FormListState == true)
			return;
		
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
		if(FormListState == true)
			return;
		
		if(AudioController.Instance != null)
			 AudioController.Instance.PlayButtonClick(1);

		if (OnOff == true) {
			MainPanel.gameObject.SetActive (false);
			HeaderDisplayButton.gameObject.SetActive(false);
			SoundOptions.gameObject.SetActive(true);
			TapPad.Instance.DragEnabled = false;
			OnOff = false;
		} else {
			MainPanel.gameObject.SetActive (true);
			HeaderDisplayButton.gameObject.SetActive(true);
			SoundOptions.gameObject.SetActive(false);
			TapPad.Instance.DragEnabled = true;
			OnOff = true;
		}
	}

	public void ShowingFormsList (bool state) 
	{
		FormListState = state;
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
