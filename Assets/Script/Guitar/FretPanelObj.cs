using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FretPanelObj : MonoBehaviour 
{
	public enum eState 
	{
		NoOp,
		Loaded,
		Empty,
		Full
	};
	public eState _State = eState.NoOp;

	public GameObject backingSprite;
	public GameObject textObj;


	private int _id = 0;
	public int ID {
		get {return _id; } 
		set {_id = value; }
	}
		
	public void SetGridPosition(Vector3 vec)
	{
		transform.position = new Vector3(vec.x, vec.y, vec.z);
	}

	public void SetObjectColor(float red, float green, float blue, float alpha) 
	{
		if (backingSprite != null) {
			backingSprite.GetComponent<Renderer> ().material.color = new Color32 ((byte)red, (byte)green, (byte)blue, (byte)alpha);
		}
	}		

	public void SetFretLabel(string label, int size = 0) 
	{
		TextMeshPro tp = GetComponentInChildren<TextMeshPro> ();

		if(size > 0) {
			tp.fontSize = size;
		}
		tp.SetText (label);
	}	

	public void SetVisibleStatus(bool status)
	{
		gameObject.SetActive (status);
	}

}


