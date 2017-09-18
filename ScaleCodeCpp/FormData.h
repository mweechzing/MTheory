//
//  FormData.h
//  GuitarProto
//
//  Created by Dave Hards on 09/10/08.
//  Copyright 2008 Slippery Salmon Software. All rights reserved.
//
#include "cocos2d.h"
USING_NS_CC;

namespace HandArcade
{
    #define _maxFormData 16
    #define _maxForms 128
    #define FormQuantum 16
    #define KeyNoteBucket 16

    enum _notes
    {
        NOTE_E = 0,
        NOTE_F,         //1
        NOTE_FS,        //2
        NOTE_G,         //3
        NOTE_GS,        //4
        NOTE_A,         //5
        NOTE_AS,        //6
        NOTE_B,         //7
        NOTE_C,         //8
        NOTE_CS,        //9
        NOTE_D,         //10
        NOTE_DS,        //11
        NOTE_E2,        //12

    };

    enum _intervals
    {
        ROOT = 0,

    };



    typedef struct
    {
        int entryType;
        char *name;
        int data[_maxFormData];
        
    }FormEntry;

    typedef struct
    {
        int numFormsLoaded;
        FormEntry formList[_maxForms];
    }FormData;

    void InitForms();
    void InitFormData();

    int GetNumFormsLoaded();
    //CCString *GetFormNameAtIndex(int index);
    char *GetFormNameAtIndex(int index);
    void GetFormIntervalString(int index, char intervalString[256]);

    //CCString *GetKeyNameAtIndex(int index);

    void GetFormData(int *tableData, int formIndex);
    int GetKeyNoteBucket(int key, int formIndex, int *bucketData, int *intervalData);
    
    
    
    extern const char *gKeyNames1[];
    
    extern const char *gKeyNamesSharp[];
    extern const char *gKeyNamesFlat[];

    extern const char *gIntervalText[];
    extern const char *gIntervalExtendedText[];

    extern const char *gDisplayStyleText[];

}

