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
		

	public void WriteFormState (int key, int form) 
	{
		PlayerPrefs.SetInt ("saveKey", key);
		PlayerPrefs.SetInt ("saveForm", form);
		PlayerPrefs.Save ();

	}

	public void ReadFormState (out int key, out int form) 
	{
		if(PlayerPrefs.HasKey("saveKey")) {
			key = PlayerPrefs.GetInt ("saveKey");
			form = PlayerPrefs.GetInt ("saveForm");
		} else {
		
			key = 0;
			form = 0;
			WriteFormState (key, form);

		}

	}



}
