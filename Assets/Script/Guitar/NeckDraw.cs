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

	private float gridStartX, gridStartY;

	public static NeckDraw Instance;


	void Awake () 
	{
		Instance = this;

		MarkerObjectList = new List<GameObject>();


		Debug.LogError(Screen.currentResolution);
	}

	void Start () 
	{
		MarkerObjectContainer = GameObject.Find ("MarkerObjectContainer");

		gridStartX = StartGridPosition.transform.position.x;
		gridStartY = StartGridPosition.transform.position.y;

		LoadMarkerObjects ();
		QuerySetObjectsLoaded ();

		GuitarNeck.Instance.InitGuitarNeck ();
		GuitarNeck.Instance.Clear ();
		GuitarNeck.Instance.ApplyForm (0, 0, 0);


		QuerySetObjectsPosition ();

		QuerySetObjectsResetColor ();
		ApplyForm ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		
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

				MarkerObj objectScript = _sfObj.GetComponent<MarkerObj> ();
				objectScript.ID = t;

				MarkerObjectList.Add (_sfObj);

			} else {

				Debug.Log ("Couldn't load marker object prefab");
			}
		}


	}

	void QuerySetObjectsLoaded() 
	{
		foreach(GameObject tObj in MarkerObjectList)
		{
			MarkerObj objectScript = tObj.GetComponent<MarkerObj> ();
			objectScript._State = MarkerObj.eState.Loaded;
		}
	}


	void QuerySetObjectsPosition() 
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

			objectScript.SetGridPosition (new Vector3(x,y,1));
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

			//int interval = GuitarNeck.Instance.GetInterval (0, f);

			if (GuitarNeck.Instance.GetNoteStatus (0, f) > 0) {
					
				QueryApplyNoteToOn (note);
			}

		}
	}

	void QueryApplyNoteToOn(int note)
	{
		Debug.LogError ("QueryApplyNoteToOn note = " + note);

		foreach (GameObject tObj in MarkerObjectList) {
			
			MarkerObj objectScript = tObj.GetComponent<MarkerObj> ();

			int noteIndex = objectScript.NoteName;

			if (noteIndex == note) {
			
				objectScript.SetObjectColor (0, 128, 255, 255);

			}

		}
	}


	void QuerySetObjectsResetColor() 
	{
		foreach(GameObject tObj in MarkerObjectList)
		{
			MarkerObj objectScript = tObj.GetComponent<MarkerObj> ();
			objectScript.SetObjectColor (255, 255, 255, 0);
		}
	}




}
