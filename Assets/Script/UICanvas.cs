using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICanvas : MonoBehaviour 
{
	public GameObject MainPanel;

	private bool OnOff = true;

	void Start () 
	{
		
	}
	
	public void ToggleMainPanel () 
	{
		AudioController.Instance.PlayButtonClick(1);

		if (OnOff == true) {
			MainPanel.gameObject.SetActive (false);
			OnOff = false;
		} else {
			MainPanel.gameObject.SetActive (true);
			OnOff = true;		
		}
	}
}
