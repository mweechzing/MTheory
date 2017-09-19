using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuitarNeck : MonoBehaviour 
{

	GuitarString[] mStrings = new GuitarString[Globals.MaxStrings];


	public void InitGuitarNeck()
	{
		int[] notes = new int[Globals.MaxStrings] {(int)Globals._notes.NOTE_E, (int)Globals._notes.NOTE_A, (int)Globals._notes.NOTE_D, (int)Globals._notes.NOTE_G, (int)Globals._notes.NOTE_B, (int)Globals._notes.NOTE_E}; 

		for(int s = 0; s < Globals.MaxStrings; s++)
		{
			mStrings [s] = new GuitarString ();
			mStrings[s].InitGuitarString(s, notes[s]);
		}
	}


	public void ApplyForm(int key, int formIndex, int level)
	{
		int[] intervalData = new int[16] {0,0,0,0,0,0,0,0, 0,0,0,0,0,0,0,0}; 
		int[] bucketData = new int[16] {-1,-1,-1,-1,-1,-1,-1,-1, -1,-1,-1,-1,-1,-1,-1,-1}; 

		int numInBucket =  FormData.Instance.GetKeyNoteBucket(key, formIndex, ref bucketData, ref intervalData);

		for(int s = 0; s < Globals.MaxStrings; s++)
		{
			mStrings[s].ApplyBucketData(numInBucket, ref bucketData, ref intervalData, level);
		}
	}

	public void Clear()
	{
		for(int s = 0; s < Globals.MaxStrings; s++)
		{
			mStrings[s].Clear();
		}
	}

	public void ApplyChord(int key, int chordIndex, int level)
	{

	}

	public int GetNoteStatus(int stringIndex, int fret)
	{
		int status = 0;

		status = mStrings[stringIndex].GetFretStatus(fret);

		//Debug.LogError ("GetNoteStatus status = " + status);

		return status;
	}


	public int GetNote(int stringIndex, int fret)
	{
		int note = 0;

		note = mStrings[stringIndex].GetFretNote(fret);

		return note;
	}

	public int GetInterval(int stringIndex, int fret)
	{
		int interval = 0;

		interval = mStrings[stringIndex].GetFretInterval(fret);

		return interval;
	}


	public void DebugPrintNeck()
	{
		for(int s = 0; s < Globals.MaxStrings; s++)
		{
			mStrings[s].DebugPrintString();
		}

	}
		
	public static GuitarNeck Instance;

	void Awake () 
	{
		Instance = this;

	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}


