
#include "cocos2d.h"
USING_NS_CC;

namespace HandArcade
{

#define printf OutputDebugStringA


#define MaxFrets 25
#define MaxStrings 6
#define MaxLevels 8

struct NoteStatus
{
	int onOff;
	int interval;
};

struct NotePos
{
	int x;
	int y;
};


class FretPos
{
public:
	FretPos(int fret, int note);
	~FretPos();

	int GetNote();

	void SetNotePos(int x, int y);

	void SetNoteStatus(int levelIndex, int onOff, int interval);
    int GetNoteStatusOnOff(int levelIndex);
    int GetNoteStatusInterval(int levelIndex);

	void DebugPrintFret();
	void DebugPrintFretNote(int level);
private:
	int mFretNumber;
	int mNote;

	NotePos mNotePos;

	NoteStatus mNoteStatus[MaxLevels];
};


class GuitarString
{
public:
	GuitarString(int s, int openNote);
	~GuitarString();

	void ApplyBucketData(int numInBucket, int *bucketData, int *intervalData, int level);
    void Clear();

    int GetFretStatus(int fret);
    int GetFretNote(int fret);
    int GetFretInterval(int fret);


	void DebugPrintString();

private:
	int mStringNumber;

	FretPos *mFrets[MaxFrets];
};



class GuitarNeck
{
public:
	GuitarNeck();
	~GuitarNeck();

	void ApplyForm(int keyIndex, int formIndex, int level);
	void ApplyChord(int key, int chordIndex, int level);
    void Clear();

    int GetNoteStatus(int string, int fret);
    int GetNote(int string, int fret);
    int GetInterval(int string, int fret);

	void DebugPrintNeck();

private:

	GuitarString *mStrings[MaxStrings];

};

}