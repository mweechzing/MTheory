using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuitarString : MonoBehaviour
{

	public int mStringNumber = 0;

	FretPos[] mFrets = new FretPos[Globals.MaxFrets];

	//----------------------------------------------------------------
	/*
                            Guitar String
    */
	//----------------------------------------------------------------
	public void InitGuitarString(int s, int openNote)
	{
		mStringNumber = s;
		int startNote = openNote;

		for(int f = 0; f < Globals.MaxFrets; f++)
		{
			mFrets [f] = new FretPos ();
			mFrets[f].Init(f, startNote);
			startNote++;

			if (startNote > (int)Globals._notes.NOTE_DS) {
				startNote = (int)Globals._notes.NOTE_E;
			}
		}

	}


	public void Clear()
	{
		for(int f = 0; f < Globals.MaxFrets; f++)
		{
			mFrets[f].SetNoteStatus(0, 0, 0);
		}
	}



	public void ApplyBucketData(int numInBucket, ref int[] bucketData, ref int[] intervalData, int level)
	{
		for(int f = 0; f < Globals.MaxFrets; f++)
		{
			int note = mFrets[f].GetNote();

			for(int b = 0; b < numInBucket; b++)
			{
				int bData = bucketData[b];
				if(note == bData)
				{
					mFrets[f].SetNoteStatus(level, 1, intervalData[b]);
					break;
				}
			}
		}
	}

	public int GetFretStatus(int fret)
	{
		return( mFrets[fret].GetNoteStatusOnOff(0) );
	}

	public int GetFretNote(int fret)
	{
		return( mFrets[fret].GetNote() );
	}

	public int GetFretInterval(int fret)
	{
		return( mFrets[fret].GetNoteStatusInterval(0) );
	}

	public void DebugPrintString()
	{
		for(int f = 0; f < Globals.MaxFrets; f++)
		{
			mFrets[f].DebugPrintFretNote(0);
		}
	}

}
