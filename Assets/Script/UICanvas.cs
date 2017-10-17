using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UICanvas : MonoBehaviour 
{
	public GameObject MainPanel;
	public GameObject SoundOptions;
	public GameObject HeaderDisplayButton;
	public GameObject FormGridListDisplay;
	public GameObject DisplayScrollSlider;

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



		Slider slider1 = DisplayScrollSlider.GetComponent<Slider> ();
		//slider1.value = value1;

	}

	public void SetNeckSliderValue (float value) 
	{
		Slider slider1 = DisplayScrollSlider.GetComponent<Slider> ();
		slider1.value = value;
	}
		
	public void OnNeckSliderChanged () 
	{
		Slider slider1 = DisplayScrollSlider.GetComponent<Slider> ();
		float value1 = slider1.value;

		TapPad.Instance.SetNeckSliderAmount(value1);
	}

	void Update () 
	{



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
		if(AudioController.Instance != null)
			AudioController.Instance.PlayButtonClick(0);
		
		FormListState = state;
	}

	public void SwitchToFretboard () 
	{
		if(AudioController.Instance != null)
			AudioController.Instance.PlayButtonClick(0);
		
		NeckDraw.Instance.RefreshDrawArea(NeckDraw.DrawMode.Guitar);
	}
		
	public void SwitchToKeyBoard () 
	{
		if(AudioController.Instance != null)
			AudioController.Instance.PlayButtonClick(0);
		
		NeckDraw.Instance.RefreshDrawArea(NeckDraw.DrawMode.Piano);
	}

}
