using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MarkerObj : MonoBehaviour 
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





	private int _noteName = 0;
	public int NoteName {
		get {return _noteName; } 
		set {_noteName = value; }
	}

	private int _markedColor = 0;
	public int MarkedColor {
		get {return _markedColor; } 
		set {_markedColor = value; }
	}

	void Start () 
	{}

	public void SetGridPosition(Vector3 vec)
	{
		transform.position = new Vector3(vec.x, vec.y, vec.z);
	}

	void Update () 
	{
	}
		
	public void SetObjectColor(float red, float green, float blue, float alpha) 
	{
		if (backingSprite != null) {
			backingSprite.GetComponent<Renderer> ().material.color = new Color32 ((byte)red, (byte)green, (byte)blue, (byte)alpha);
		}
	}		

}

