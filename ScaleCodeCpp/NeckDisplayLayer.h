
#include "cocos2d.h"
#include "cocos-ext.h"
#include "GenericMessage.h"

using namespace cocos2d;

namespace HandArcade
{
    class GuitarNeck;
    
    class NeckDisplayLayer : public Layer
    {
        
        enum
        {
            MarkerTag1 = 100,
            MarkerTag2 = 200,
            MarkerTag3 = 300,
            MarkerTag4 = 400,
            MarkerTag5 = 500,
            MarkerTag6 = 600,
        };
        enum
        {
            LabelTag1 = 150,
            LabelTag2 = 250,
            LabelTag3 = 350,
            LabelTag4 = 450,
            LabelTag5 = 550,
            LabelTag6 = 650,
        };
        
        enum
        {
            FretBackTag1 = 1000,
            FretNumberTag2 = 1100,
            FretInfoTag1 = 1200,
        };

        enum
        {
            DisplayStyleSharp = 0,
            DisplayStyleFlat,
            DisplayStyleInterval2,
            DisplayStyleInterval9,
        };
        
    public:
        NeckDisplayLayer();
        ~NeckDisplayLayer();
        
        bool init();
        static Layer* layer();
        CREATE_FUNC(NeckDisplayLayer);
        void purge();
        void update(float dt);

        void CreateLayout();
        void RefreshLayout();

        void ApplyForm(int key, int formIndex);
        
        void ChangeFretBacking(int type);


        GenericMessage* GetMessage();
        
        void SetOffsetXY(float x, float y){mOffsetX = x; mOffsetY = y;}
        void SetDisplayStyle(float style){mDisplayStyle = style;}
        void SetColorIdx(int idx){mColorIdx = idx;}

        void EnableTouch(bool state){mEnableTouch = state;}
        

        //touches
        void onTouchesBegan(const std::vector<Touch*>& touches, Event* event);
        void onTouchesMoved(const std::vector<Touch*>& touches, Event* event);
        void onTouchesEnded(const std::vector<Touch*>& touches, Event* event);
     
    private:
        
        GuitarNeck *mGuitarNeck;
        
        GenericMessageQueue _neckDisplayMessageQueue;
        void postMessageInt(int command, int int1, const int& priority);
        
        Rect getTargetRect(Sprite *pSprite);
        void processTouch(Point p);
        
        void LoadButtonPositions();

        int mKey, mForm;
        int mDisplayStyle;
        
        int mMarkerTags[8];
        int mLabelTags[8];
        
        int mColorIdx;
        int mColorIdxRed[4][3];
        int mColorIdxGreen[4][3];
        int mColorIdxBlue[4][3];
        int mColorIdxText[4][3];
        void LoadColors();

        float mNumStrings;
        
        bool mTouchGlide;
        bool mEnableTouch;
        bool TouchStateDown;
        Point mTouchLocationDown;
        Point mNeckLocation;
        float mTouchTimer;
        float mGlideY;
        float _coeff;
        
        float mNeckTopY;
        float mTopBoundary;
        float mBotBoundary;
        
        float mFretHeight, mFretWidth;
        float mStringGap, mFretGap, mfullStringGap;
        
        float mOffsetX, mOffsetY;
    };
}
