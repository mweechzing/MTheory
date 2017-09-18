
#include "GuitarNeck.h"
#include "FormData.h"

namespace HandArcade
{

    //----------------------------------------------------------------
    /*
                                Fret Position
    */
    //----------------------------------------------------------------
    FretPos::FretPos(int fret, int note)
    : mFretNumber(fret)
    , mNote(note)
    {
        for(int i = 0; i < MaxLevels; i++)
        {
            mNoteStatus[i].onOff = 0;
            mNoteStatus[i].interval = 0;
        }
    }

    FretPos::~FretPos()
    {}

    int FretPos::GetNote()
    {
        return mNote;
    }

    void FretPos::SetNotePos(int x, int y)
    {
        mNotePos.x = x;
        mNotePos.y = y;
    }

    void FretPos::SetNoteStatus(int levelIndex, int onOff, int interval)
    {
        mNoteStatus[levelIndex].onOff = onOff;
        mNoteStatus[levelIndex].interval = interval;
    }
        
    int FretPos::GetNoteStatusOnOff(int levelIndex)
    {
        return( mNoteStatus[levelIndex].onOff);
    }
    int FretPos::GetNoteStatusInterval(int levelIndex)
    {
        return( mNoteStatus[levelIndex].interval);
    }


    void FretPos::DebugPrintFret()
    {
        char buf[256];
        sprintf(buf, "%d ", mNote);
        CCLOG("%s",buf);
    }
    void FretPos::DebugPrintFretNote(int level)
    {
        //char buf[256];
        //if(mNoteStatus[level].onOff)
        //    sprintf(buf, "X ", mNote);
        //else
        //    sprintf(buf, "0 ", mNote);
        
        //CCLOG("%s",buf);
    }




    //----------------------------------------------------------------
    /*
                            Guitar String
    */
    //----------------------------------------------------------------
    GuitarString::GuitarString(int s, int openNote)
    : mStringNumber(s)
    {
        //mFrets = new CCArray();

        int startNote = openNote;

        for(int f = 0; f < MaxFrets; f++)
        {
            //mFrets->addObject(new FretPos(f, startNote));
            
            mFrets[f] = new FretPos(f, startNote);
            startNote++;

            if(startNote > NOTE_DS)
                startNote = NOTE_E;
        }

    }

    GuitarString::~GuitarString()
    {
    }

    void GuitarString::Clear()
    {
        for(int f = 0; f < MaxFrets; f++)
        {
            FretPos *_fretPos = mFrets[f];
            _fretPos->SetNoteStatus(0, 0, 0);
        }
    }

    void GuitarString::ApplyBucketData(int numInBucket, int *bucketData, int *intervalData, int level)
    {
        for(int f = 0; f < MaxFrets; f++)
        {
            FretPos *_fretPos = mFrets[f];

            int note = _fretPos->GetNote();

            for(int b = 0; b < numInBucket; b++)
            {
                int bData = bucketData[b];
                if(note == bData)
                {
                    _fretPos->SetNoteStatus(level, 1, intervalData[b]);
                    break;
                }
            }
        }
    }

    int GuitarString::GetFretStatus(int fret)
    {
        FretPos *_fretPos = mFrets[fret];
        
        return( _fretPos->GetNoteStatusOnOff(0) );
    }
    
    int GuitarString::GetFretNote(int fret)
    {
        FretPos *_fretPos = mFrets[fret];
        
        return( _fretPos->GetNote() );
    }
    
    int GuitarString::GetFretInterval(int fret)
    {
        FretPos *_fretPos = mFrets[fret];
        
        return( _fretPos->GetNoteStatusInterval(0) );
    }

    void GuitarString::DebugPrintString()
    {
        for(int f = 0; f < MaxFrets; f++)
        {
            FretPos *_fretPos = mFrets[f];

            //_fretPos->DebugPrintFret();
            _fretPos->DebugPrintFretNote(0);

            
        }
    }




    //----------------------------------------------------------------
    /*
                            Guitar Neck
    */
    //----------------------------------------------------------------
    GuitarNeck::GuitarNeck()
    {
        //mStrings = new CCArray();

        int notes[] = {NOTE_E, NOTE_A, NOTE_D, NOTE_G, NOTE_B, NOTE_E};

        for(int s = 0; s < MaxStrings; s++)
        {
            mStrings[s] = new GuitarString(s, notes[s]);
        }
    }

    GuitarNeck::~GuitarNeck()
    {
    }


    void GuitarNeck::ApplyForm(int key, int formIndex, int level)
    {
        int intervalData[16] = {0,0,0,0,0,0,0,0, 0,0,0,0,0,0,0,0};
        int bucketData[16] = {-1,-1,-1,-1,-1,-1,-1,-1, -1,-1,-1,-1,-1,-1,-1,-1};
        int numInBucket =  GetKeyNoteBucket(key, formIndex, bucketData, intervalData);

        for(int s = 0; s < MaxStrings; s++)
        {
            GuitarString *_string =  mStrings[s];

            _string->ApplyBucketData(numInBucket, bucketData, intervalData, level);
        }
    }
    
    void GuitarNeck::Clear()
    {
        for(int s = 0; s < MaxStrings; s++)
        {
            GuitarString *_string =  mStrings[s];
            
            _string->Clear();
        }
    }

    void GuitarNeck::ApplyChord(int key, int chordIndex, int level)
    {

    }

    int GuitarNeck::GetNoteStatus(int string, int fret)
    {
        int status = 0;
        
        GuitarString *_string =  mStrings[string];
        
        status = _string->GetFretStatus(fret);
     
        
        return status;
    }
    
    
    int GuitarNeck::GetNote(int string, int fret)
    {
        int note = 0;
        
        GuitarString *_string =  mStrings[string];
        note = _string->GetFretNote(fret);
        
        return note;
    }

    int GuitarNeck::GetInterval(int string, int fret)
    {
        int interval = 0;
        
        GuitarString *_string =  mStrings[string];
        interval = _string->GetFretInterval(fret);
        
        return interval;
    }


    void GuitarNeck::DebugPrintNeck()
    {

        for(int s = 0; s < MaxStrings; s++)
        {
            GuitarString *_string =  mStrings[s];

            _string->DebugPrintString();
        }

    }
    
}
