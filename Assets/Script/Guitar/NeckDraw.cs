using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeckDraw : MonoBehaviour 
{
	[HideInInspector]
	public enum DrawMode
	{
		Guitar = 0,
		Piano,         
		Bass       
	};
	private DrawMode _drawMode = DrawMode.Guitar;


	public int objectPoolSize = 6 * 24;

	public float FretGridWidth = 6f;
	public float FretGridHeight = 26f;
	public float FretGridDX = 1f;
	public float FretGridDY = 1f;

	public GameObject StoragePosition;
	public GameObject StartGridPosition;
	public GameObject TapPad;

	[HideInInspector]
	public List <GameObject> MarkerObjectList = null;
	private GameObject MarkerObjectContainer;

	[HideInInspector]
	public List <GameObject> FretPanelObjectList = null;
	private GameObject FretPanelObjectContainer;

	[HideInInspector]
	public List <GameObject> KeyboardObjectList = null;
	private GameObject KeyboardObjectContainer;

	public ColorSet[] NeckNoteColors;
	public ColorSet[] NeckFretColors;



	private float targetScale = 1.0f;
	private float sizeScale = 0.5f;


	//private const float TargetScreenWidth = 1536.0f;//standard retina

	private int NoteDisplayStyle = 0;
	private int CurrentFormIndex = 0;
	private int CurrentKey = 0;
	private int CurrentStyle = 0;

	private float fretStartX, fretStartY;
	private float gridStartX, gridStartY;

	private bool firstPass = true;

	private float MarkerLabelRotation = 0f;

	//note animation
	private bool AnimateNeck = false;

	float elaspedTime = 0f;
	float waitTime = 2f;


	public static NeckDraw Instance;

	void Awake () 
	{
		Instance = this;

		MarkerObjectList = new List<GameObject>();
		FretPanelObjectList = new List<GameObject>();
		KeyboardObjectList = new List<GameObject>();



	}

	void Start () 
	{
		targetScale = ResolutionManager.Instance.GetTargetScale(); 

		float TapPadY = ResolutionManager.Instance.GetTapPadY();

		Vector3 vScale = TapPad.transform.localScale;
		TapPad.transform.localScale = new Vector3(vScale.x * targetScale, vScale.y * targetScale, 1f);

		Vector3 vPosition = TapPad.transform.position;
		vPosition.y = TapPadY;
		TapPad.transform.position = new Vector3(vPosition.x * targetScale, vPosition.y * targetScale, 1f);

		vPosition = TapPad.transform.position;

		gridStartX = TapPad.transform.position.x;
		gridStartY = TapPad.transform.position.y;

		fretStartX = gridStartX;
		fretStartY = gridStartY;


		vScale = TapPad.transform.localScale;

		gridStartX -= vScale.x * 0.32f;
		gridStartY += vScale.y * 0.32f;

		fretStartX = vPosition.x;
		fretStartY += vScale.y * 0.32f;



		_drawMode = (DrawMode)SaveState.Instance.ReadSaveStateInt("drawMode");


		int key, form, style, display;
		SaveState.Instance.ReadFormState(out key, out form, out style, out display);
		//Debug.LogError(key.ToString() + " " + form.ToString());
		CurrentKey = key;
		CurrentFormIndex = form;
		CurrentStyle = style;
		NoteDisplayStyle = display;

		MarkerObjectContainer = GameObject.Find ("MarkerObjectContainer");
		FretPanelObjectContainer = GameObject.Find ("FretPanelObjectContainer");
		KeyboardObjectContainer = GameObject.Find ("KeyboardObjectContainer");

		LoadFretPanelObjects ();
		QuerySetFretPanelObjectsLoaded ();

		LoadMarkerObjects ();
		QuerySetMarkerObjectsLoaded ();

		LoadKeyboardObjects ();
		QuerySetMarkerObjectsLoaded ();




		GuitarNeck.Instance.InitGuitarNeck ();
		GuitarNeck.Instance.Clear ();
		GuitarNeck.Instance.ApplyForm (CurrentKey, CurrentFormIndex, 0);

		if(AudioController.Instance != null)
			AudioController.Instance.ApplyFormAudio (CurrentKey, CurrentFormIndex);


		QuerySetFretPanelObjectsPosition ();
		QuerySetKeyboardlObjectsPosition();
		QuerySetMarkerObjectsPosition ();


		if(_drawMode == DrawMode.Piano) {

			FretPanelObjectContainer.SetActive(false);
			KeyboardObjectContainer.SetActive(true);;

		} else if(_drawMode == DrawMode.Guitar) {

			FretPanelObjectContainer.SetActive(true);
			KeyboardObjectContainer.SetActive(false);;


		} else if(_drawMode == DrawMode.Bass) {

		}

		QuerySetObjectsResetColor ();
		ApplyForm ();

		MarkerLabelRotation = (float)SaveState.Instance.ReadSaveStateInt("markerRotation");
		QuerySetMarkerObjectsRotation();


	}

	void Update () 
	{
		if(firstPass == true) {
			if(CurrentSelection.Instance != null) {
				CurrentSelection.Instance.RefreshSelectedForm ();
				firstPass = false;
			}
		}

		if (AnimateNeck == true) {

			float delta = Time.deltaTime;
			elaspedTime += delta;
			if (elaspedTime > waitTime) {

				ApplyNoteAnim ();

				AnimateNeck = false;
			}
		}
	}

	public void ToggleNeckScale () 
	{
		if (sizeScale == 0.5f) {
			sizeScale = 1f;
		} else {
			sizeScale = 0.5f;
		}

		float s = targetScale * sizeScale;
		Debug.Log ("ToggleNeckScale new scale = " + s);

		gameObject.transform.localScale = new Vector2 (s, s);
	}

	public void ReApplyForm(int formIndex)
	{
		CurrentFormIndex = formIndex;
		GuitarNeck.Instance.Clear ();
		GuitarNeck.Instance.ApplyForm (CurrentKey, formIndex, 0);

		if(AudioController.Instance != null)
			AudioController.Instance.ApplyFormAudio (CurrentKey, CurrentFormIndex);

		QuerySetObjectsResetColor ();
		ApplyForm ();

		CurrentSelection.Instance.RefreshSelectedForm ();
		SaveState.Instance.WriteFormState(CurrentKey, CurrentFormIndex, CurrentStyle, NoteDisplayStyle);
	}

	public void SetNoteDisplayStyle(int displayStyle)
	{
		NoteDisplayStyle = displayStyle;

		GuitarNeck.Instance.Clear ();
		GuitarNeck.Instance.ApplyForm (CurrentKey, CurrentFormIndex, 0);

		if(AudioController.Instance != null)
			AudioController.Instance.ApplyFormAudio (CurrentKey, CurrentFormIndex);

		QuerySetObjectsResetColor ();
		ApplyForm ();

		SaveState.Instance.WriteFormState(CurrentKey, CurrentFormIndex, CurrentStyle, NoteDisplayStyle);
	}


	//KEY
	public void SetCurrentKey(int key)
	{
		CurrentKey = key;

		GuitarNeck.Instance.Clear ();
		GuitarNeck.Instance.ApplyForm (CurrentKey, CurrentFormIndex, 0);

		if(AudioController.Instance != null)
			AudioController.Instance.ApplyFormAudio (CurrentKey, CurrentFormIndex);

		QuerySetObjectsResetColor ();
		ApplyForm ();

		CurrentSelection.Instance.RefreshSelectedForm ();
		SaveState.Instance.WriteFormState(CurrentKey, CurrentFormIndex, CurrentStyle, NoteDisplayStyle);
	}

	public int GetCurrentKey()
	{
		return CurrentKey;
	}

	//FORM
	public void SetCurrentFormIndex(int formIndex)
	{
		CurrentFormIndex = formIndex;

		GuitarNeck.Instance.Clear ();
		GuitarNeck.Instance.ApplyForm (CurrentKey, CurrentFormIndex, 0);

		if(AudioController.Instance != null)
			AudioController.Instance.ApplyFormAudio (CurrentKey, CurrentFormIndex);

		QuerySetObjectsResetColor ();
		ApplyForm ();


		CurrentSelection.Instance.RefreshSelectedForm ();
		SaveState.Instance.WriteFormState(CurrentKey, CurrentFormIndex, CurrentStyle, NoteDisplayStyle);

	}
	public int GetCurrentFormIndex()
	{
		return CurrentFormIndex;
	}


	//STYLE
	public void SetCurrentStyleIndex(int styleIndex)
	{
		CurrentStyle = styleIndex;

		GuitarNeck.Instance.Clear ();
		GuitarNeck.Instance.ApplyForm (CurrentKey, CurrentFormIndex, 0);

		if(AudioController.Instance != null)
			AudioController.Instance.ApplyFormAudio (CurrentKey, CurrentFormIndex);

		QuerySetObjectsResetColor ();
		ApplyForm ();


		CurrentSelection.Instance.RefreshSelectedForm ();
		SaveState.Instance.WriteFormState(CurrentKey, CurrentFormIndex, CurrentStyle, NoteDisplayStyle);

	}
	public int GetCurrenStyle()
	{
		return CurrentStyle;
	}


	private void LoadMarkerObjects()
	{
		for (int t = 0; t < objectPoolSize; t++) {

			GameObject _sfObj = Instantiate (Resources.Load ("Prefabs/MarkerObj", typeof(GameObject))) as GameObject;

			if (_sfObj != null) {

				if (MarkerObjectContainer != null) {
					_sfObj.transform.parent = MarkerObjectContainer.transform;
				}
				_sfObj.name = "squareObj" + t.ToString ();

				//default storage location
				_sfObj.transform.position = new Vector2 (StoragePosition.transform.position.x, StoragePosition.transform.position.y);
				_sfObj.transform.localScale = new Vector2 (targetScale, targetScale);

				MarkerObj objectScript = _sfObj.GetComponent<MarkerObj> ();
				objectScript.ID = t;

				MarkerObjectList.Add (_sfObj);

			} else {

				Debug.Log ("Couldn't load marker object prefab");
			}
		}
	}

	private void LoadFretPanelObjects()
	{
		for (int t = 0; t < 26; t++) {

			GameObject _sfObj = Instantiate (Resources.Load ("Prefabs/FretPanelObj", typeof(GameObject))) as GameObject;

			if (_sfObj != null) {

				if (FretPanelObjectContainer != null) {
					_sfObj.transform.parent = FretPanelObjectContainer.transform;
				}
				_sfObj.name = "fretPanelObj" + t.ToString ();

				//default storage location
				_sfObj.transform.position = new Vector2 (StoragePosition.transform.position.x, StoragePosition.transform.position.y);
				_sfObj.transform.localScale = new Vector2 (targetScale, targetScale);

				FretPanelObj objectScript = _sfObj.GetComponent<FretPanelObj> ();
				objectScript.ID = t;

				FretPanelObjectList.Add (_sfObj);

			} else {

				Debug.Log ("Couldn't load marker object prefab");
			}
		}
	}


	private void LoadKeyboardObjects()
	{
		for (int t = 0; t < 41; t++) {

			GameObject _sfObj = Instantiate (Resources.Load ("Prefabs/KeyBoardObj", typeof(GameObject))) as GameObject;

			if (_sfObj != null) {

				if (KeyboardObjectContainer != null) {
					_sfObj.transform.parent = KeyboardObjectContainer.transform;
				}
				_sfObj.name = "keyBoardObj" + t.ToString ();

				//default storage location
				_sfObj.transform.position = new Vector2 (StoragePosition.transform.position.x, StoragePosition.transform.position.y);
				_sfObj.transform.localScale = new Vector2 (targetScale, targetScale);

				KeyBoardObj objectScript = _sfObj.GetComponent<KeyBoardObj> ();
				objectScript.ID = t;

				KeyboardObjectList.Add (_sfObj);

			} else {

				Debug.Log ("Couldn't load keyboard object prefab");
			}
		}
	}

	 



	void QuerySetMarkerObjectsLoaded() 
	{
		foreach(GameObject tObj in MarkerObjectList)
		{
			MarkerObj objectScript = tObj.GetComponent<MarkerObj> ();
			objectScript._State = MarkerObj.eState.Loaded;
		}
	}

	void QuerySetFretPanelObjectsLoaded() 
	{
		foreach(GameObject tObj in MarkerObjectList)
		{
			FretPanelObj objectScript = tObj.GetComponent<FretPanelObj> ();
			objectScript._State = FretPanelObj.eState.Loaded;
		}
	}

	void QuerySetKeyboardObjectsLoaded() 
	{
		foreach(GameObject tObj in MarkerObjectList)
		{
			KeyBoardObj objectScript = tObj.GetComponent<KeyBoardObj> ();
			objectScript._State = KeyBoardObj.eState.Loaded;
		}
	}

	void QuerySetFretPanelObjectsPosition() 
	{
		float yOffset = 0f;
		int fretIndex = 0;
		int[] fretArray = new int[26] { 0,-1,-1,3,-1,5,-1,7,-1,9,-1,-1,12,-1,-1,15,-1,17,-1,19,-1,21,-1,-1,24,-1};


		foreach(GameObject tObj in FretPanelObjectList)
		{
			FretPanelObj objectScript = tObj.GetComponent<FretPanelObj> ();

			float x = fretStartX;
			float y = fretStartY + yOffset;
			objectScript.SetGridPosition (new Vector3(x, y, 1f));


			int f = fretArray [fretIndex];
			if (f > -1) {
				if(f == 0) {
					objectScript.SetFretLabel ("Open", 3);
				} else {
					objectScript.SetFretLabel (f.ToString ());
				}
			} else {
				objectScript.SetFretLabel (" ");			
			}

			if (fretIndex == 0) {
				objectScript.SetObjectColor (32, 64, 128, 255);
			}

			yOffset += FretGridDY * targetScale;

			fretIndex++;
		}
	}

	void QuerySetKeyboardlObjectsPosition() 
	{
		float yOffset = 0f;
		int keyIndex = 0;
		int[] keySeqArray = new int[41] { 0,1,0,1,0,0,1,0,1,0,1,0,0,1,0,1,0,0,1,0,1,0,1,0,0,1,0,1,0,0,1,0,1,0,1,0,0,1,0,1,0};
		int lastKey = 1;

		foreach(GameObject tObj in KeyboardObjectList)
		{
			KeyBoardObj objectScript = tObj.GetComponent<KeyBoardObj> ();

			int k = keySeqArray [keyIndex];
			if(k == 0) {
				
				objectScript.SetWhiteBlack (0);

				if(lastKey == 0) {
					yOffset += FretGridDY * targetScale;
				} else {
					yOffset += (FretGridDY / 2) * targetScale;				
				}

			} else if(k == 1){
				
				objectScript.SetWhiteBlack (1);

				yOffset += (FretGridDY / 2) * targetScale;
			}


			float x = fretStartX;
			float y = fretStartY + yOffset;
			objectScript.SetGridPosition (new Vector3(x, y, 1f));


			lastKey = k;
			keyIndex++;
		}
	}


	void QuerySetMarkerObjectsPosition() 
	{
		float xOffset = 0.42f * targetScale;
		float yOffset = 0f;
		int colCount = 0;
		int rowCount = 0;
		int noteIndex = 0;
		int[] notes = new int[Globals.MaxStrings + 1] {(int)Globals._notes.NOTE_E, (int)Globals._notes.NOTE_A, (int)Globals._notes.NOTE_D, (int)Globals._notes.NOTE_G, (int)Globals._notes.NOTE_B, (int)Globals._notes.NOTE_E, 0}; 
		int[] keySeqArray = new int[41] { 0,1,0,1,0,0,1,0,1,0,1,0,0,1,0,1,0,0,1,0,1,0,1,0,0,1,0,1,0,0,1,0,1,0,1,0,0,1,0,1,0};

		if(_drawMode == DrawMode.Guitar) {
			noteIndex = notes[colCount];
			foreach(GameObject tObj in MarkerObjectList)
			{
				MarkerObj objectScript = tObj.GetComponent<MarkerObj> ();

				float x = gridStartX + xOffset;
				float y = gridStartY + yOffset;

				objectScript.SetGridPosition (new Vector3(x,y,-0.5f));
				objectScript.NoteName = noteIndex;

				yOffset += FretGridDY * targetScale;
				rowCount++;
				if (rowCount >= FretGridHeight) {
					rowCount = 0;
					yOffset = 0f;
					xOffset += FretGridDX * targetScale;
					colCount++;

					noteIndex = notes [colCount];

				} else {
				
					noteIndex++;
					if (noteIndex >= 12) {
						noteIndex = 0;
					}
				}

				if(colCount >= FretGridWidth) {
					break;
				}
					
			}
		} else if(_drawMode == DrawMode.Piano) {

			int lastKey = 1;
			int keyIndex = 0;
			int count = KeyboardObjectList.Count;
			count--;
			noteIndex = (int)Globals._notes.NOTE_C;
			foreach(GameObject tObj in MarkerObjectList)
			{
				if(keyIndex < 41) {
					MarkerObj objectScript = tObj.GetComponent<MarkerObj> ();

					float x = gridStartX + xOffset + 4f;
					float y = gridStartY + yOffset;

					GameObject key = KeyboardObjectList[count];
					KeyBoardObj keyObjectScript = key.GetComponent<KeyBoardObj> ();
					Vector3 vpos = keyObjectScript.GetGridPosition();

					if(keyObjectScript.isBlack == true) {
						vpos.x = vpos.x - 1f;
					} else {
						vpos.x = vpos.x + 1f;

					}

					objectScript.SetGridPosition (new Vector3(vpos.x, vpos.y, -0.5f));
					objectScript.NoteName = noteIndex;

					int k = keySeqArray [keyIndex];
					if(k == 0) {

						if(lastKey == 0) {
							yOffset += FretGridDY * targetScale;
						} else {
							yOffset += (FretGridDY / 2) * targetScale;				
						}

					} else if(k == 1){


						yOffset += (FretGridDY / 2) * targetScale;
					}

					noteIndex++;
					if (noteIndex >= 12) {
						noteIndex = 0;
					}

					count--;
					if(count < 0)
						count = 0;

					lastKey = k;
					keyIndex++;
				} else {
					break;
				}

			}
		
		
		}
	}

	public void ToggleMarkerRotation()
	{
		if(MarkerLabelRotation == 0f) {
			MarkerLabelRotation = 90f;
		}else{
			MarkerLabelRotation = 0f;
		}

		SaveState.Instance.WriteSaveStateInt("markerRotation", (int)MarkerLabelRotation);

		QuerySetMarkerObjectsRotation();

		if(AudioController.Instance != null)
			AudioController.Instance.PlayButtonClick(0);
		
	}

	private void QuerySetMarkerObjectsRotation() 
	{
		foreach(GameObject tObj in MarkerObjectList)
		{
			MarkerObj objectScript = tObj.GetComponent<MarkerObj> ();
			objectScript.SetMarkerLabelRotation(MarkerLabelRotation);
		}
	}



	void ApplyForm() 
	{

		for (int f = 0; f < Globals.OctaveLen; f++) {
			
			int note = GuitarNeck.Instance.GetNote (0, f);

			//Debug.LogError ("ApplyForm note = " + note);

			int interval = GuitarNeck.Instance.GetInterval (0, f);
			if (GuitarNeck.Instance.GetNoteStatus (0, f) > 0) {
					
				QueryApplyNoteToOn (note, interval);
			}

		}
	}

	void QueryApplyNoteToOn(int note, int interval)
	{
		//Debug.LogError ("QueryApplyNoteToOn note = " + note);

		foreach (GameObject tObj in MarkerObjectList) {
			
			MarkerObj objectScript = tObj.GetComponent<MarkerObj> ();

			int noteIndex = objectScript.NoteName;

			if (noteIndex == note) {
			
				objectScript.SetVisibleStatus (true);

				//select color styles
				ColorSet cs = NeckNoteColors[CurrentStyle];
				Color c = GetColorFromSet(cs, interval);
				objectScript.SetObjectColor (c.r*255f, c.g*255f, c.b*255f, c.a*255f);
				objectScript.SetSpriteToUse(0);

				int colorCode = cs.GetColorCode(0);
				if(colorCode == 1) {
					if(interval == 0) {
						objectScript.SetSpriteToUse(1);
					} else if(interval == 7){
						objectScript.SetSpriteToUse(2);
					}else {
						objectScript.SetSpriteToUse(0);
					}
				}

				//select note display style
				string noteText = FormData.Instance.gKeyNamesSharp[noteIndex];
				if (NoteDisplayStyle == 1) {
					noteText = FormData.Instance.gKeyNamesFlat[noteIndex];
				}else if(NoteDisplayStyle == 2){
					noteText = FormData.Instance.gIntervalText[interval];
				}else if(NoteDisplayStyle == 3){
					noteText = FormData.Instance.gIntervalExtendedText[interval];
				}
					
				c = GetColorFromSet(cs, 12);
				objectScript.SetMarkerLabel (noteText, c);
			}
		}
	}


	void QuerySetObjectsResetColor() 
	{
		foreach(GameObject tObj in MarkerObjectList)
		{
			MarkerObj objectScript = tObj.GetComponent<MarkerObj> ();
			objectScript.SetVisibleStatus (false);
			objectScript.SetObjectColor (255, 255, 255, 0);
		}
	}

	void QuerySetObjectsResetPosition() 
	{
		foreach(GameObject tObj in MarkerObjectList)
		{
			MarkerObj objectScript = tObj.GetComponent<MarkerObj> ();
			objectScript.SetGridPosition( new Vector3 (StoragePosition.transform.position.x, StoragePosition.transform.position.y, -0.5f) );
		}
	}

	private Color GetColorFromSet(ColorSet cs, int index)
	{
		Color rc = cs.Color1;
		switch(index)
		{
		case 0:
			rc = cs.Color1;
			break;
		case 1:
			rc = cs.Color2;
			break;
		case 2:
			rc = cs.Color3;
			break;
		case 3:
			rc = cs.Color4;
			break;
		case 4:
			rc = cs.Color5;
			break;
		case 5:
			rc = cs.Color6;
			break;
		case 6:
			rc = cs.Color7;
			break;
		case 7:
			rc = cs.Color8;
			break;
		case 8:
			rc = cs.Color9;
			break;
		case 9:
			rc = cs.Color10;
			break;
		case 10:
			rc = cs.Color11;
			break;
		case 11:
			rc = cs.Color12;
			break;
		case 12:
			rc = cs.ColorText;
			break;
		}
			
		return rc;
	}




	void ApplyNoteAnim() 
	{
		for (int f = 0; f < Globals.OctaveLen; f++) {

			int note = GuitarNeck.Instance.GetNote (0, f);

			if (GuitarNeck.Instance.GetNoteStatus (0, f) > 0) {

				QueryApplyNoteAnimation (note);
			}
		}
	}

	void QueryApplyNoteAnimation(int note)
	{
		//Debug.LogError ("QueryApplyNoteToOn note = " + note);

		foreach (GameObject tObj in MarkerObjectList) {

			MarkerObj objectScript = tObj.GetComponent<MarkerObj> ();

			int noteIndex = objectScript.NoteName;

			if (noteIndex == note) {

				objectScript.StartColorAnim (Color.white, 0.25f);

			}
		}
	}



	public void RefreshDrawArea(DrawMode dm)
	{
		_drawMode = dm; 

		SaveState.Instance.WriteSaveStateInt("drawMode", (int)dm);

		if(_drawMode == DrawMode.Piano) {

			FretPanelObjectContainer.SetActive(false);
			KeyboardObjectContainer.SetActive(true);;

		} else if(_drawMode == DrawMode.Guitar) {
			
			FretPanelObjectContainer.SetActive(true);
			KeyboardObjectContainer.SetActive(false);;


		} else if(_drawMode == DrawMode.Bass) {

		}

		QuerySetObjectsResetPosition();
		QuerySetMarkerObjectsPosition ();

		QuerySetObjectsResetColor ();
		ApplyForm ();

	}

}
