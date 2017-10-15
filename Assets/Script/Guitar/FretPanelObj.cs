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
	public GameObject GString6;
	public GameObject GString5;
	public GameObject GString4;
	public GameObject GString3;
	public GameObject GString2;
	public GameObject GString1;


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

	public void SetObjectColor(Color c) 
	{
		if (backingSprite != null) {
			backingSprite.GetComponent<Renderer> ().material.color = c;
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

	public void SetStringColors(Color colorAux1, Color colorAux2) 
	{
		if (GString6 != null) {
			GString6.GetComponent<Renderer> ().material.color = colorAux1;
		}
		if (GString5 != null) {
			GString5.GetComponent<Renderer> ().material.color = colorAux1;
		}
		if (GString4 != null) {
			GString4.GetComponent<Renderer> ().material.color = colorAux1;
		}
		if (GString3 != null) {
			GString3.GetComponent<Renderer> ().material.color = colorAux2;
		}
		if (GString2 != null) {
			GString2.GetComponent<Renderer> ().material.color = colorAux2;
		}
		if (GString1 != null) {
			GString1.GetComponent<Renderer> ().material.color = colorAux2;
		}
	}		


}


