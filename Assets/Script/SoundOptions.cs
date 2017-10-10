using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SoundOptions : MonoBehaviour 
{
	public GameObject TempoSlider1;
	public GameObject TempoSlider1text;
	public GameObject TempoSlider2;
	public GameObject TempoSlider2text;
	public GameObject ArpeggioStyleText;
	public GameObject PedalToneText;

	public GameObject RandomSlider1;
	public GameObject RandomSlider1text;

	public GameObject RandomSlider2;
	public GameObject RandomSlider2text;

	[HideInInspector]
	private float SliderValue1;

	private int scaleVoiceIndex = 0;
	private int pedalToneIndex = 0;

	TextMeshProUGUI proTextTempo1 = null;
	TextMeshProUGUI proTextTempo2 = null;
	TextMeshProUGUI proTextRandom1 = null;
	TextMeshProUGUI proTextRandom2 = null;

	public static SoundOptions Instance;

	void Awake () 
	{
		Instance = this;

	}

	void Start () 
	{
		proTextTempo1 = TempoSlider1text.GetComponent<TextMeshProUGUI> ();
		proTextTempo2 = TempoSlider2text.GetComponent<TextMeshProUGUI> ();
		proTextRandom1 = RandomSlider1text.GetComponent<TextMeshProUGUI> ();
		proTextRandom2 = RandomSlider2text.GetComponent<TextMeshProUGUI> ();

	}

	void Update () 
	{
		Slider slider1 = TempoSlider1.GetComponent<Slider> ();
		float value1 = slider1.value;
		proTextTempo1.SetText ("Tempo : " + value1);


		Slider slider2 = TempoSlider2.GetComponent<Slider> ();
		float value2 = slider2.value;
		proTextTempo2.SetText ("Tempo : " + value2);

		Slider slider3 = RandomSlider1.GetComponent<Slider> ();
		float value3 = slider3.value;
		proTextRandom1.SetText ("Random Variation : " + value3);

		Slider slider4 = RandomSlider2.GetComponent<Slider> ();
		float value4 = slider4.value;
		proTextRandom2.SetText ("Random Variation : " + value4);

	}

	public void ButtonGO()
	{
		gameObject.SetActive (false);
		TapPad.Instance.DragEnabled = true;


		Slider slider1 = TempoSlider1.GetComponent<Slider> ();
		float value1 = slider1.value;

		Slider slider2 = TempoSlider2.GetComponent<Slider> ();
		float value2 = slider2.value;

		Slider slider3 = RandomSlider1.GetComponent<Slider> ();
		float value3 = slider3.value;

		Slider slider4 = RandomSlider2.GetComponent<Slider> ();
		float value4 = slider4.value;

		AudioController.Instance.SetSoundOptions (scaleVoiceIndex, value1, (int)value3, pedalToneIndex, value2, (int)value4);

	}

	public void ButtonShowSelf()
	{
		TapPad.Instance.DragEnabled = false;
		gameObject.SetActive (true);
	}

	public void SetSampleOptions(int type, int index)
	{
		//type is scale voice or pedal tone voice

		if(type == 0) {
			
			scaleVoiceIndex = index;
			string text = FormData.Instance.gArpeggioStyleText [scaleVoiceIndex];
			TextMeshProUGUI proText = ArpeggioStyleText.GetComponent<TextMeshProUGUI> ();
			proText.SetText (text);

		} else {
			
			pedalToneIndex = index;
			string text = FormData.Instance.gPedalToneText [pedalToneIndex];
			TextMeshProUGUI proText = PedalToneText.GetComponent<TextMeshProUGUI> ();
			proText.SetText (text);
		}

	}
		

}
