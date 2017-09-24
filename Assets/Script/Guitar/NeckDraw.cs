using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeckDraw : MonoBehaviour 
{
	public int objectPoolSize = 6 * 24;

	public float FretGridWidth = 6f;
	public float FretGridHeight = 24f;
	public float FretGridDX = 1f;
	public float FretGridDY = 1f;

	public GameObject StoragePosition;
	public GameObject StartGridPosition;

	[HideInInspector]
	public List <GameObject> MarkerObjectList = null;
	private GameObject MarkerObjectContainer;


	[HideInInspector]
	public List <GameObject> FretPanelObjectList = null;
	private GameObject FretPanelObjectContainer;

	public ColorSet[] NeckNoteColors;
	public ColorSet[] NeckFretColors;



	private float targetScale = 1.0f;
	private float sizeScale = 0.5f;


	private const float TargetScreenWidth = 1536.0f;//standard retina

	private int NoteDisplayStyle = 0;
	private int CurrentFormIndex = 0;
	private int CurrentKey = 0;
	private int CurrentStyle = 0;

	private float gridStartX, gridStartY;

	private bool firstPass = true;

	public static NeckDraw Instance;


	void Awake () 
	{
		Instance = this;

		MarkerObjectList = new List<GameObject>();
		FretPanelObjectList = new List<GameObject>();
	}

	void Start () 
	{
		/*
		Resolution screenRes = Screen.currentResolution;
		float screenWidth = screenRes.width;

		targetScale = TargetScreenWidth;

		Debug.LogError(Screen.currentResolution);

		if (screenWidth < TargetScreenWidth) {
		
			targetScale *= (screenWidth / TargetScreenWidth); 
		}

		gameObject.transform.localScale = new Vector2 (targetScale, targetScale);
		#endif
		*/

		int key, form, style, display;
		SaveState.Instance.ReadFormState(out key, out form, out style, out display);
		//Debug.LogError(key.ToString() + " " + form.ToString());
		CurrentKey = key;
		CurrentFormIndex = form;
		CurrentStyle = style;
		NoteDisplayStyle = display;

		MarkerObjectContainer = GameObject.Find ("MarkerObjectContainer");
		FretPanelObjectContainer = GameObject.Find ("FretPanelObjectContainer");

		gridStartX = StartGridPosition.transform.position.x;
		gridStartY = StartGridPosition.transform.position.y;

		LoadFretPanelObjects ();
		QuerySetFretPanelObjectsLoaded ();

		LoadMarkerObjects ();
		QuerySetMarkerObjectsLoaded ();

		GuitarNeck.Instance.InitGuitarNeck ();
		GuitarNeck.Instance.Clear ();
		GuitarNeck.Instance.ApplyForm (CurrentKey, CurrentFormIndex, 0);


		QuerySetFretPanelObjectsPosition ();
		QuerySetMarkerObjectsPosition ();

		QuerySetObjectsResetColor ();
		ApplyForm ();

	}

	void Update () 
	{
		if(firstPass == true) {
			if(CurrentSelection.Instance != null) {
				CurrentSelection.Instance.RefreshSelectedForm ();
				firstPass = false;
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
				_sfObj.transform.localScale = new Vector2 (StoragePosition.transform.localScale.x, StoragePosition.transform.localScale.y);

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
		for (int t = 0; t < 24; t++) {

			GameObject _sfObj = Instantiate (Resources.Load ("Prefabs/FretPanelObj", typeof(GameObject))) as GameObject;

			if (_sfObj != null) {

				if (FretPanelObjectContainer != null) {
					_sfObj.transform.parent = FretPanelObjectContainer.transform;
				}
				_sfObj.name = "fretPanelObj" + t.ToString ();

				//default storage location
				_sfObj.transform.position = new Vector2 (StoragePosition.transform.position.x, StoragePosition.transform.position.y);

				FretPanelObj objectScript = _sfObj.GetComponent<FretPanelObj> ();
				objectScript.ID = t;

				FretPanelObjectList.Add (_sfObj);

			} else {

				Debug.Log ("Couldn't load marker object prefab");
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

	void QuerySetFretPanelObjectsPosition() 
	{
		float xOffset = 2.13f;
		float yOffset = 0f;
		int fretIndex = 0;
		int[] fretArray = new int[24] { 0,-1,-1,3,-1,5,-1,7,-1,9,-1,-1,12,-1,-1,15,-1,17,-1,19,-1,21,-1,23};

		foreach(GameObject tObj in FretPanelObjectList)
		{
			FretPanelObj objectScript = tObj.GetComponent<FretPanelObj> ();

			float x = gridStartX + xOffset;
			float y = gridStartY + yOffset;

			objectScript.SetGridPosition (new Vector3(x,y,-0.3f));

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

			yOffset += FretGridDY;

			fretIndex++;
		}
	}

	void QuerySetMarkerObjectsPosition() 
	{
		float xOffset = 0f;
		float yOffset = 0f;
		int colCount = 0;
		int rowCount = 0;
		int noteIndex = 0;
		int[] notes = new int[Globals.MaxStrings + 1] {(int)Globals._notes.NOTE_E, (int)Globals._notes.NOTE_A, (int)Globals._notes.NOTE_D, (int)Globals._notes.NOTE_G, (int)Globals._notes.NOTE_B, (int)Globals._notes.NOTE_E, 0}; 


		noteIndex = notes[colCount];
		foreach(GameObject tObj in MarkerObjectList)
		{
			MarkerObj objectScript = tObj.GetComponent<MarkerObj> ();

			float x = gridStartX + xOffset;
			float y = gridStartY + yOffset;

			objectScript.SetGridPosition (new Vector3(x,y,-0.5f));
			objectScript.NoteName = noteIndex;

			yOffset += FretGridDY;
			rowCount++;
			if (rowCount >= FretGridHeight) {
				rowCount = 0;
				yOffset = 0f;
				xOffset += FretGridDX;
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
		case 44:
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




}
