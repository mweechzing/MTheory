using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Core.PathCore;
using DG.Tweening.Plugins.Options;

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
	public GameObject secondarySprite;
	public GameObject tertiarySprite;
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

	private int _useSecondary = 0;
	public int UseSecondary {
		get {return _useSecondary; } 
		set {_useSecondary = value; }
	}

	private Sequence AnimSequence = null;

	void Start () 
	{}

	public void SetGridPosition(Vector3 vec)
	{
		transform.position = new Vector3(vec.x, vec.y, vec.z);
	}

	public void SetSpriteToUse(int s)
	{
		if(s == 0) {
			backingSprite.SetActive(true);
			secondarySprite.SetActive(false);
			tertiarySprite.SetActive(false);
		}else if(s == 1) {
			backingSprite.SetActive(false);
			secondarySprite.SetActive(true);
			tertiarySprite.SetActive(false);
		}else{
			backingSprite.SetActive(false);
			secondarySprite.SetActive(false);
			tertiarySprite.SetActive(true);
		}
	}


	void Update () 
	{
	}
		
	public void SetObjectColor(float red, float green, float blue, float alpha) 
	{
		if (backingSprite != null) {
			backingSprite.GetComponent<Renderer> ().material.color = new Color32 ((byte)red, (byte)green, (byte)blue, (byte)alpha);
			secondarySprite.GetComponent<Renderer> ().material.color = new Color32 ((byte)red, (byte)green, (byte)blue, (byte)alpha);
			tertiarySprite.GetComponent<Renderer> ().material.color = new Color32 ((byte)red, (byte)green, (byte)blue, (byte)alpha);
		}
	}		

	public void SetMarkerLabel(string label, Color c) 
	{
		TextMeshPro tp = GetComponentInChildren<TextMeshPro> ();

		tp.sortingLayerID = SortingLayer.NameToID ("MarkerText");
		tp.sortingOrder = 0;
	
		tp.SetText (label);

		tp.color = c;

	}	

	public void SetMarkerLabelRotation(float angle) 
	{
		TextMeshPro tp = GetComponentInChildren<TextMeshPro> ();
		tp.transform.rotation =  Quaternion.Euler(new Vector3(0, 0, angle));

	}
		

	public void SetVisibleStatus(bool status)
	{
		gameObject.SetActive (status);
	}



	public void StartColorAnim (Color newColor, float speed) 
	{
		//slowest----- each of these has an Out as well.
		//InSine
		//InQuad
		//InCubic
		//InQuint
		//InExpo
		//fastest-----

		Debug.LogError ("StartColorAnim note = " + NoteName);


		Color orgColor = backingSprite.GetComponent<Renderer> ().material.color;

		AnimSequence = DOTween.Sequence ().SetEase (Ease.Linear);

		Material m = backingSprite.GetComponent<Renderer> ().material;

		AnimSequence.Append(m.DOColor (newColor, speed));
		//AnimSequence.Join(transform.DOScale(new Vector3(2f, 2f, 1f), speed));

		AnimSequence.Append(m.DOColor (orgColor, speed).OnComplete(ColorSequenceComplete));
		//AnimSequence.Join(transform.DOScale(new Vector3(2f, 2f, 1f), speed));
	}

	void ColorSequenceComplete () 
	{
		//wait = 0;
		//transform.position = new Vector3 (sx, sy, sz);
		//StartAnim (sx, sy - 7f, sz, 0.25f);
		//AnimState = eAnimState.Ready;
	}
		

}

