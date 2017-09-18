
#include "cocos2d.h"
#include "cocos-ext.h"
#include "GenericMessage.h"

using namespace cocos2d;

namespace HandArcade
{
    class MetaSaveData;

    class MenuFunctionLayer : public Layer
    {
    public:
        enum
        {
            Event_LoadForm = 0,
            Event_LoadSlot,
            Event_LoadStyle,
            Event_LoadColor,
            Event_LoadNeck,
        };
        
        enum
        {
            CurrentFormTag = 10,
            
            ButtonLoadFormTag = 100,
            ButtonLoadFormTextTag = 101,
            
            ButtonSlotTag1 = 200,
            ButtonSlotTextTag1 = 250,
            
            ButtonStyleTag1 = 300,
            ButtonStyleTextTag1 = 350,

            ButtonColorTag1 = 400,
            ButtonColorTextTag1 = 450,
            
            ButtonNeckTag1 = 500,
            ButtonNeckTextTag1 = 550,
        };
        
        
        struct BasicForm
        {
            int key;
            int form;
        };

        
    public:
        MenuFunctionLayer();
        ~MenuFunctionLayer();
        
        bool init();
        static Layer* layer();
        CREATE_FUNC(MenuFunctionLayer);
        void purge();
        void update(float dt);
        
        void EnableTouch(bool state){mEnableTouch = state;}

        void CreateLayout();
        void RefreshLayout();
        void RefreshSlots();

        
        GenericMessage* GetMessage();
        
        void SetForm(int index, int key, int form)
        {
            mSlotIndex = index;
            mBasicForms[index].key = key;
            mBasicForms[index].form = form;
        }
    
        int GetCurrentSlotKey(){return( mBasicForms[mSlotIndex].key );}
        int GetCurrentSlotForm(){return( mBasicForms[mSlotIndex].form );}
        
        //touches
        void onTouchesBegan(const std::vector<Touch*>& touches, Event* event);
        void onTouchesMoved(const std::vector<Touch*>& touches, Event* event);
        void onTouchesEnded(const std::vector<Touch*>& touches, Event* event);
        
    private:
        
        MetaSaveData *mMetaSaveData;

        void processTouch(Point p);
        
        GenericMessageQueue _menuFunctionMessageQueue;
        void postMessageInt(int command, int int1, const int& priority);
        void postMessageInt2(int command, int int1, int int2, const int& priority);

        void animateButton(Sprite *_sprite);
        void _animButtonPress(Node* sender);
        void _animButtonRelease(Node* sender);
        
        Rect getTargetRect(Sprite *pSprite);
        void LoadButtonPositions();

        int mEvent;
        
        bool mEnableTouch;
        
        bool _buttonBeingProcessed;
        
        int mSlotIndex;
        int mStyleIndex;
        int mColorIndex;
        int mNeckIndex;
        BasicForm mBasicForms[5];
        
        float LoadFormButtonX, LoadFormButtonY;
        float SlotButtonX, SlotButtonY;
        float StyleButtonX, StyleButtonY;
        float ColorButtonX, ColorButtonY;
        float NeckButtonX, NeckButtonY;
        

        
        
    };
}
