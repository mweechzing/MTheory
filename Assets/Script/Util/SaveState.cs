using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveState : MonoBehaviour 
{
	public static SaveState Instance;


	void Awake () 
	{
		Instance = this;

	}
		

	public void WriteFormState (int key, int form, int style, int display) 
	{
		PlayerPrefs.SetInt ("saveKey", key);
		PlayerPrefs.SetInt ("saveForm", form);
		PlayerPrefs.SetInt ("saveStyle", style);
		PlayerPrefs.SetInt ("saveDisplay", display);
		PlayerPrefs.Save ();

	}

	public void ReadFormState (out int key, out int form, out int style, out int display) 
	{
		if(PlayerPrefs.HasKey("saveKey")) {
			key = PlayerPrefs.GetInt ("saveKey");
			form = PlayerPrefs.GetInt ("saveForm");
			style = PlayerPrefs.GetInt ("saveStyle");
			display = PlayerPrefs.GetInt ("saveDisplay");
		} else {
		
			key = 0;
			form = 0;
			style = 0;
			display = 0;
			WriteFormState (key, form, style, display);

		}

	}



}
