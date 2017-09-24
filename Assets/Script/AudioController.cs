using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour 
{
	public AudioSource[] sfxButtonClick;

	public static AudioController Instance;


	void Awake () 
	{
		Instance = this;

	}

	public void PlayButtonClick (int index) 
	{
		sfxButtonClick[index].Play();
	}
	
	void Update () 
	{
		
	}
}
