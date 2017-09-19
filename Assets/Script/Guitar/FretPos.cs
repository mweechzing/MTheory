using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FretPos 
{
	public struct NoteStatus
	{
		public int onOff;
		public int interval;
	};

	public struct NotePos
	{
		public int x;
		public int y;
	};

	//int mFretNumber = 0;
	int mNote = 0;

	NotePos mNotePos;
	NoteStatus mNoteStatus;

	public void Init(int fret, int note)
	{
		//mFretNumber = fret;
		mNote = note;
		mNoteStatus.onOff = 0;
		mNoteStatus.interval = 0;
	}


	public int GetNote()
	{
		return mNote;
	}

	public void SetNotePos(int x, int y)
	{
		mNotePos.x = x;
		mNotePos.y = y;
	}

	public void SetNoteStatus(int levelIndex, int onOff, int interval)
	{
		mNoteStatus.onOff = onOff;
		mNoteStatus.interval = interval;
	}

	public int GetNoteStatusOnOff(int levelIndex)
	{
		return( mNoteStatus.onOff);
	}
	public int GetNoteStatusInterval(int levelIndex)
	{
		return( mNoteStatus.interval);
	}


	public void DebugPrintFret()
	{
		//char buf[256];
		//sprintf(buf, "%d ", mNote);
		//CCLOG("%s",buf);
	}
	public void DebugPrintFretNote(int level)
	{
		//char buf[256];
		//if(mNoteStatus[level].onOff)
		//    sprintf(buf, "X ", mNote);
		//else
		//    sprintf(buf, "0 ", mNote);

		//CCLOG("%s",buf);
	}

}
