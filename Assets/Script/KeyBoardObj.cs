using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class KeyBoardObj : MonoBehaviour
{
	public enum eState 
	{
		NoOp,
		Loaded,
		Empty,
		Full
	};
	public eState _State = eState.NoOp;

	public GameObject whiteKeySprite;
	public GameObject wkTextObj;
	public GameObject blackKeySprite;
	public GameObject bkTextObj;

	[HideInInspector]
	public bool isBlack = false;


	private int _id = 0;
	public int ID {
		get {return _id; } 
		set {_id = value; }
	}

	public void SetGridPosition(Vector3 vec)
	{
		transform.position = new Vector3(vec.x, vec.y, vec.z);
	}

	public Vector3 GetGridPosition()
	{
		return transform.position;
	}

	public void SetObjectColor(float red, float green, float blue, float alpha) 
	{
		if (whiteKeySprite != null) {
			whiteKeySprite.GetComponent<Renderer> ().material.color = new Color32 ((byte)red, (byte)green, (byte)blue, (byte)alpha);
		}
	}	

	public void SetWhiteBlack(int wb) 
	{
		if(wb == 0) {
			isBlack = false;
			whiteKeySprite.SetActive(true);
			blackKeySprite.SetActive(false);
		}else{
			isBlack = true;
			whiteKeySprite.SetActive(false);
			blackKeySprite.SetActive(true);
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


	public void SetWKeyLabel(string label, Color c) 
	{
		TextMeshPro tp = wkTextObj.GetComponent<TextMeshPro> ();

		tp.sortingLayerID = SortingLayer.NameToID ("MarkerText");
		tp.sortingOrder = 2;

		tp.SetText (label);

		tp.color = c;
	}	
	public void SetBKeyLabel(string label, Color c) 
	{
		TextMeshPro tp = bkTextObj.GetComponent<TextMeshPro> ();

		tp.sortingLayerID = SortingLayer.NameToID ("MarkerText");
		tp.sortingOrder = 2;

		tp.SetText (label);

		tp.color = c;
	}	


	public void SetVisibleStatus(bool status)
	{
		gameObject.SetActive (status);
	}

}

