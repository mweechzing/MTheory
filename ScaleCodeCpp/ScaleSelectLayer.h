
#include "cocos2d.h"
#include "cocos-ext.h"
#include "GenericMessage.h"

using namespace cocos2d;

namespace HandArcade
{
    class ListBox;
    class CellProgressDraw;
    
    class ScaleSelectLayer : public Layer
    {
        
        enum
        {
            DialogBackTag1 = 1,
            ButtonSprintTag1 = 500,
            ButtonLabelTag1 = 600,
            StoreButtonTag1 = 700,
            StoreLabelTag1 = 800,
            CancelButtonTag1 = 900
        };
        
    public:
        enum
        {
            EventCancel = 0,
            EventSet
        };
        
    public:
        ScaleSelectLayer();
        ~ScaleSelectLayer();
        
        bool init();
        static Layer* layer();
        CREATE_FUNC(ScaleSelectLayer);
        void purge();
        void update(float dt);
        
        
        void CreateLayout();
        void RefreshLayout();

        void SetGuiPosition(float px, float py);
        void SetGuiOffscreen(float dx, float dy);
        void Activate();
        void Deactivate();

        
        GenericMessage* GetMessage();
        
        //touches
        void onTouchesBegan(const std::vector<Touch*>& touches, Event* event);
        void onTouchesMoved(const std::vector<Touch*>& touches, Event* event);
        void onTouchesEnded(const std::vector<Touch*>& touches, Event* event);
        
    private:
        
        ListBox *mProgressListBox;
        CellProgressDraw *mCellProgressDraw;

        
        void LoadButtonPositions();

        Rect getTargetRect(Sprite *pSprite);
        void processTouch(Point p);

        GenericMessageQueue _scaleSelectMessageQueue;
        void postMessageInt(int command, int int1, const int& priority);
        void postMessageInt2(int command, int int1, int int2, const int& priority);
        void postMessageInt3(int command, int int1, int int2, int int3, const int& priority);

        void animateButton(Sprite *_sprite);
        void _animButtonPress(Node* sender);
        void _animButtonRelease(Node* sender);
        
        void MoveIn();
        void MoveOut();
        void _moveComplete(Node* sender);

        int mEvent;
        int mSelectedKey;
        int mSelectedStoreButton;
        
        bool _onScreen;
        bool _buttonBeingProcessed;
        bool _moveInProgress;
        float _destX, _destY;
        float _offscreenX, _offscreenY;
        
        
        float CancelButtonX, CancelButtonY;
        float KeyButtonX, KeyButtonY;
        float StoreButtonX, StoreButtonY;
        float ListBoxX, ListBoxY;

        
    };
}
