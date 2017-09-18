#include "ScaleSelectLayer.h"
#include "SceneGraph.h"
#include "AudioServices.h"
#include "GuitarNeck.h"
#include "FormData.h"
#include "ListBox.h"
#include "cell_progress_draw.h"


using namespace cocos2d;

namespace HandArcade
{
    
    ScaleSelectLayer::ScaleSelectLayer()
    {
    }
    
    ScaleSelectLayer::~ScaleSelectLayer()
    {
    }
    
    
    Layer* ScaleSelectLayer::layer()
    {
        ScaleSelectLayer *layer = ScaleSelectLayer::create();
        
        return layer;
    }
    
    
    
    
    bool ScaleSelectLayer::init()
    {
        if (!Layer::init())
        {
            return false;
        }
        
        mEvent = EventCancel;
        mSelectedKey = 0;
        mSelectedStoreButton = 0;
        
        auto dispatcher = Director::getInstance()->getEventDispatcher();
        auto listener = EventListenerTouchAllAtOnce::create();
        listener->onTouchesBegan = CC_CALLBACK_2(ScaleSelectLayer::onTouchesBegan, this);
        listener->onTouchesMoved = CC_CALLBACK_2(ScaleSelectLayer::onTouchesMoved, this);
        listener->onTouchesEnded = CC_CALLBACK_2(ScaleSelectLayer::onTouchesEnded, this);
        dispatcher->addEventListenerWithSceneGraphPriority(listener, this);
        
        
        _onScreen = false;
        
		scheduleUpdate();
        
        
        return true;
    }
    
    
    void ScaleSelectLayer::CreateLayout()
    {
        Size visibleSize = Director::getInstance()->getVisibleSize();
        //Point origin = Director::getInstance()->getVisibleOrigin();
        float _centerX = visibleSize.width / 2.0;
        float _centerY = visibleSize.height / 2.0;
        
        
        float maxWidth;
        if(visibleSize.width > visibleSize.height)
        {
            maxWidth = visibleSize.width;
        }
        else
        {
            maxWidth = visibleSize.height;
            
        }
        
        LoadButtonPositions();
        
        
        
        


        
        //KeyButtonX = _centerX - (maxWidth/2) * 58.0 / 100.0;
        //KeyButtonY = _centerY + (visibleSize.height/2) * 50.0 / 100.0;
        //StoreButtonX = _centerX + (maxWidth/2) * 58.0 / 100.0;
        //StoreButtonY = _centerY + (visibleSize.height/2) * 50.0 / 100.0;


        Sprite *dialogSprite = Sprite::create("scale_dialog_back.png");
        dialogSprite->setPosition(Point(_centerX, _centerY));
        this->addChild(dialogSprite, 0, DialogBackTag1);
        Size _csize = dialogSprite->getContentSize();
        float dialogHeight = _csize.height;

        
        
        Sprite *pCancelSprite = Sprite::createWithSpriteFrameName("cancel_button1.png");
        pCancelSprite->setPosition(Point(CancelButtonX, CancelButtonY));
        this->addChild(pCancelSprite, 12, CancelButtonTag1);

        
        Sprite *cellSprite = Sprite::createWithSpriteFrameName("select_listbox_back.png");
        _csize = cellSprite->getContentSize();

        float width = _csize.width;
        float height = _csize.height;
        
        float wadd = (width/2) * 18.0 / 100.0;
        
        //LIST BOX - IN PROGRESS
        mProgressListBox = (ListBox *)ListBox::layer();
        mProgressListBox->setListboxName("Achieve In Progress");
        mCellProgressDraw = new CellProgressDraw();
        mCellProgressDraw->SetCellSize(_csize);
        mProgressListBox->SetCellDrawBase(mCellProgressDraw);
        
        mProgressListBox->SetTableRect(0, 0, width, height * 6, width, height);
        mProgressListBox->SetRefreshType(0);
        
        mProgressListBox->LoadTable(0, 88, 4);
        mProgressListBox->SetRefreshType(0);
        this->addChild(mProgressListBox, 500, 500);
        
        mProgressListBox->setPosition(Point(ListBoxX, ListBoxY));
        
        
        //Create Key Buttons
        
        
        
        
        Sprite *buttonSprite = Sprite::createWithSpriteFrameName("key_radio_button1.png");
        Size _bsize = buttonSprite->getContentSize();
        
        float bwidth = _bsize.width;
        float bheight = _bsize.height;
        float ypos = 0;
        float xoff = 0;
        
        int binaryShift[] = {1,0,2,0,2,0,2,1,0,2,0,0,1,0,1,0};
        
        for(int i = 0; i < 12; i++)
        {
            Sprite *pSprite;
            
            if(i == 0)
            {
                pSprite = Sprite::createWithSpriteFrameName("key_radio_button2.png");
            }
            else
            {
                pSprite = Sprite::createWithSpriteFrameName("key_radio_button1.png");
            }
            
            pSprite->setPosition(Point(KeyButtonX + xoff, KeyButtonY - ypos));
            
            this->addChild(pSprite, 10, ButtonSprintTag1 + i);
            
            
            Label *pLabel = Label::createWithBMFont("font_listbox_med.fnt", gKeyNames1[i]);
            pLabel->setPosition(Point(KeyButtonX + xoff, KeyButtonY - ypos));
            if(i == 0)
                pLabel->setColor(Color3B(0,0,0));
            else
                pLabel->setColor(Color3B(255,255,255));

            
            this->addChild(pLabel, 20, ButtonLabelTag1 + i);
   
            int shift = binaryShift[i];
            if(shift == 2)
            {
                ypos += bheight/2;
                xoff = 0;
            }
            else if(shift == 1)
            {
                ypos += bheight;
                xoff = 0;
            }
            else
            {
                ypos += bheight/2;
                xoff = bwidth;
            }

        }
        
        
        
        Sprite *storeSprite = Sprite::createWithSpriteFrameName("store_button1.png");
        Size _ssize = storeSprite->getContentSize();
        
        //float swidth = _ssize.width;
        float sheight = _ssize.height;

        for(int i = 0; i < 5; i++)
        {
            
            Sprite *pSprite = Sprite::createWithSpriteFrameName("store_button1.png");
            
            pSprite->setPosition(Point(StoreButtonX, StoreButtonY - sheight * i));
            
            this->addChild(pSprite, 10, StoreButtonTag1 + i);
            
            char storebuf[256];
            sprintf(storebuf, "Add to\nSlot #%d", i+1);
            Label *pLabel = Label::createWithBMFont("font_listbox_small.fnt", storebuf);
            pLabel->setPosition(Point(StoreButtonX, StoreButtonY - sheight * i));
            pLabel->setColor(Color3B(0,0,0));
            
            
            this->addChild(pLabel, 20, StoreLabelTag1 + i);
        }

        
        
    }
    
    
    
    void ScaleSelectLayer::RefreshLayout()
    {
        Size visibleSize = Director::getInstance()->getVisibleSize();
        //Point origin = Director::getInstance()->getVisibleOrigin();
        float _centerX = visibleSize.width / 2.0;
        float _centerY = visibleSize.height / 2.0;
        
        
        float maxWidth;
        if(visibleSize.width > visibleSize.height)
        {
            maxWidth = visibleSize.width;
        }
        else
        {
            maxWidth = visibleSize.height;
            
        }

        LoadButtonPositions();
        
        //KeyButtonX = _centerX - (maxWidth/2) * 58.0 / 100.0;
        //KeyButtonY = _centerY + (visibleSize.height/2) * 50.0 / 100.0;
        //StoreButtonX = _centerX + (maxWidth/2) * 58.0 / 100.0;
        //StoreButtonY = _centerY + (visibleSize.height/2) * 50.0 / 100.0;
        
        
        Sprite *dialogSprite = (Sprite *)this->getChildByTag(DialogBackTag1);
        dialogSprite->setPosition(Point(_centerX, _centerY));
        Size _csize = dialogSprite->getContentSize();
        float dialogHeight = _csize.height;
        
        
        Sprite *pCancelSprite = (Sprite *)this->getChildByTag(CancelButtonTag1);
        pCancelSprite->setPosition(Point(CancelButtonX, CancelButtonY));


        
        Sprite *cellSprite = Sprite::createWithSpriteFrameName("select_listbox_back.png");
        _csize = cellSprite->getContentSize();
        
        float width = _csize.width;
        float height = _csize.height;
        
        float wadd = (width/2) * 18.0 / 100.0;
        
        //LIST BOX - IN PROGRESS
        
        mProgressListBox->setPosition(Point(ListBoxX, ListBoxY));
        
        
        //Create Key Buttons
        
        
        
        
        Sprite *buttonSprite = Sprite::createWithSpriteFrameName("key_radio_button1.png");
        Size _bsize = buttonSprite->getContentSize();
        
        float bwidth = _bsize.width;
        float bheight = _bsize.height;
        float ypos = 0;
        float xoff = 0;
        
        int binaryShift[] = {1,0,2,0,2,0,2,1,0,2,0,0,1,0,1,0};
        
        for(int i = 0; i < 12; i++)
        {
            Sprite *pSprite = (Sprite *)this->getChildByTag(ButtonSprintTag1 + i);
            pSprite->setPosition(Point(KeyButtonX + xoff, KeyButtonY - ypos));
            
            
            Label *pLabel = (Label *)this->getChildByTag(ButtonLabelTag1 + i);
            pLabel->setPosition(Point(KeyButtonX + xoff, KeyButtonY - ypos));
            
            int shift = binaryShift[i];
            if(shift == 2)
            {
                ypos += bheight/2;
                xoff = 0;
            }
            else if(shift == 1)
            {
                ypos += bheight;
                xoff = 0;
            }
            else
            {
                ypos += bheight/2;
                xoff = bwidth;
            }
            
        }
        
        
        
        Sprite *storeSprite = Sprite::createWithSpriteFrameName("store_button1.png");
        Size _ssize = storeSprite->getContentSize();

        //float swidth = _ssize.width;
        float sheight = _ssize.height;
        
        for(int i = 0; i < 5; i++)
        {
            Sprite *pSprite = (Sprite *)this->getChildByTag(StoreButtonTag1 + i);
            pSprite->setPosition(Point(StoreButtonX, StoreButtonY - sheight * i));
            
            Label *pLabel = (Label *)this->getChildByTag(StoreLabelTag1 + i);
            pLabel->setPosition(Point(StoreButtonX, StoreButtonY - sheight * i));
        }

 
    }

    
    
    void ScaleSelectLayer::update(float dt)
    {
    }
    
    
    
    
	GenericMessage* ScaleSelectLayer::GetMessage()
	{
		GenericMessage *pMsg = _scaleSelectMessageQueue.Remove();
		return pMsg;
	}
	void ScaleSelectLayer::postMessageInt(int command, int int1, const int& priority)
	{
		GenericMessage *pMsg = new GenericMessage();
        
		pMsg->SetMessageCommand(command);
		pMsg->SetInt1(int1);
        
		_scaleSelectMessageQueue.Add(pMsg, priority);
	}
	void ScaleSelectLayer::postMessageInt2(int command, int int1, int int2, const int& priority)
	{
		GenericMessage *pMsg = new GenericMessage();
        
		pMsg->SetMessageCommand(command);
		pMsg->SetInt1(int1);
		pMsg->SetInt2(int2);
        
		_scaleSelectMessageQueue.Add(pMsg, priority);
	}
	void ScaleSelectLayer::postMessageInt3(int command, int int1, int int2, int int3, const int& priority)
	{
		GenericMessage *pMsg = new GenericMessage();
        
		pMsg->SetMessageCommand(command);
		pMsg->SetInt1(int1);
		pMsg->SetInt2(int2);
		pMsg->SetInt3(int3);
        
		_scaleSelectMessageQueue.Add(pMsg, priority);
	}
    
    
    
    
    
    
    
    void ScaleSelectLayer::onTouchesBegan(const std::vector<Touch*>& touches, Event* event)
    {
        Touch* touch = touches[0];
        Point location = touch->getLocation();
        
        
        
        processTouch(location);
    }
    void ScaleSelectLayer::onTouchesMoved(const std::vector<Touch*>& touches, Event* event)
    {
    }
    void ScaleSelectLayer::onTouchesEnded(const std::vector<Touch*>& touches, Event* event)
    {
    }
    
    
    Rect ScaleSelectLayer::getTargetRect(Sprite *pSprite)
    {
        float px = pSprite->getPositionX();
        float py = pSprite->getPositionY();
        
        //adjust
        float gx = this->getPositionX();
        float gy = this->getPositionY();
        float _x = gx + px;
        float _y = gy + py;
        
        Size _size = pSprite->getContentSize();
        
        Rect targetRect
        (
            _x - _size.width/2,
            _y - _size.height/2,
            _size.width,
            _size.height
        );
        
        return targetRect;
    }
    
    void ScaleSelectLayer::processTouch(Point p)
    {
        for(int i = 0; i < 1; i++)
        {
            Sprite *pSprite = (Sprite *)this->getChildByTag(CancelButtonTag1 + i);
            Rect targetRect = getTargetRect(pSprite);
            
            if (targetRect.containsPoint(p))
            {
                mEvent = EventCancel;
                
                pSprite->setSpriteFrame("cancel_button2.png");
                animateButton(pSprite);
                //_PlaySFX(SFX_Bubble);
                break;
            }
        }
        
        for(int i = 0; i < 12; i++)
        {
            Sprite *pSprite = (Sprite *)this->getChildByTag(ButtonSprintTag1 + i);
            Rect targetRect = getTargetRect(pSprite);
            
            if (targetRect.containsPoint(p))
            {
                Sprite *pLastSprite = (Sprite *)this->getChildByTag(ButtonSprintTag1 + mSelectedKey);
                pLastSprite->setSpriteFrame("key_radio_button1.png");
                
                Label *pLabel = (Label *)this->getChildByTag(ButtonLabelTag1 + mSelectedKey );
                pLabel->setColor(Color3B(255,255,255));

                pLabel = (Label *)this->getChildByTag(ButtonLabelTag1 + i );
                pLabel->setColor(Color3B(0,0,0));

                mSelectedKey = i;
                pSprite->setSpriteFrame("key_radio_button2.png");
                
                //_PlaySFX(SFX_Bubble);
            }
        }
        
        for(int i = 0; i < 5; i++)
        {
            Sprite *pSprite = (Sprite *)this->getChildByTag(StoreButtonTag1 + i);
            Rect targetRect = getTargetRect(pSprite);
            
            if (targetRect.containsPoint(p))
            {
                mEvent = EventSet;

                mSelectedStoreButton = i;
                pSprite->setSpriteFrame("store_button2.png");
                animateButton(pSprite);
                
                //_PlaySFX(SFX_Bubble);
                
                break;
            }
        }

        
    }
    
    void ScaleSelectLayer::animateButton(Sprite *_sprite)
    {
        ActionInterval *actionScaleDown = ScaleTo::create(0.1, 0.94f, 0.94f);
        CallFunc *actionCallback = CallFunc::create( CC_CALLBACK_0(ScaleSelectLayer::_animButtonPress, this, _sprite) );
        FiniteTimeAction* _action = Sequence::create(actionScaleDown, actionCallback, NULL);
        
        _sprite->runAction( _action );
    }
    
	void ScaleSelectLayer::_animButtonPress(Node* sender)
	{
        Sprite *_sprite = (Sprite *)sender;
        
        ActionInterval *actionScaleUp = ScaleTo::create(0.1, 1.0f, 1.0f);
        
        CallFunc *actionCallback = CallFunc::create(CC_CALLBACK_0(ScaleSelectLayer::_animButtonRelease, this, _sprite));
        
        FiniteTimeAction* _action = Sequence::create(actionScaleUp, actionCallback, NULL);
        ActionInterval* _actionBounce = EaseBounceOut::create((ActionInterval*) _action );
        
        _sprite->runAction( _actionBounce );
	}
    
	void ScaleSelectLayer::_animButtonRelease(Node* sender)
	{
        if(mEvent == EventSet)
        {
            Sprite *pSprite = (Sprite *)sender;
            pSprite->setSpriteFrame("store_button1.png");

            int _formIndex = mCellProgressDraw->GetHiliteIdx();
            postMessageInt3(EventSet, mSelectedStoreButton, mSelectedKey, _formIndex, GenericMessage::PriorityHi);
        }
        else if(mEvent == EventCancel)
        {
            Sprite *pSprite = (Sprite *)sender;
            pSprite->setSpriteFrame("cancel_button1.png");
            
            postMessageInt3(EventCancel, 0, 0, 0, GenericMessage::PriorityHi);
        }
    }
    
    
    
    
    
    void ScaleSelectLayer::MoveIn()
    {
        ActionInterval *actionMoveTo = MoveTo::create(0.45, Point(_destX, _destY));
        
        CallFunc *actionCallback = CallFunc::create(CC_CALLBACK_0(ScaleSelectLayer::_moveComplete, this, this));
        
        FiniteTimeAction* _action = Sequence::create(actionMoveTo, actionCallback, NULL);
        ActionInterval* _actionBounce = EaseBounceOut::create((ActionInterval*) _action );
        
        this->runAction( _actionBounce );
        
        _moveInProgress = true;
        _onScreen = true;

        
    }
    
    void ScaleSelectLayer::MoveOut()
    {
        
        ActionInterval *actionMoveTo = MoveTo::create(0.25, Point(_offscreenX, _offscreenY));
        
        CallFunc *actionCallback = CallFunc::create(CC_CALLBACK_0(ScaleSelectLayer::_moveComplete, this, this));
        
        FiniteTimeAction* _action = Sequence::create(actionMoveTo, actionCallback, NULL);
        ActionInterval* _actionBounce = EaseBounceOut::create((ActionInterval*) _action );
        
        this->runAction( _actionBounce );
        
        _moveInProgress = true;
        
        _onScreen = false;
    }
    
	void ScaleSelectLayer::_moveComplete(Node* sender)
	{
        _moveInProgress = false;
    }
    
    void ScaleSelectLayer::SetGuiPosition(float px, float py)
    {
        _destX = px;
        _destY = py;
        
        this->setPosition(Point(_destX, _destY));
    }
    
    void ScaleSelectLayer::SetGuiOffscreen(float dx, float dy)
    {
        _offscreenX = dx;
        _offscreenY = dy;
        
        this->setPosition(Point(_offscreenX, _offscreenY));
    }
    
    void ScaleSelectLayer::Activate()
    {
        MoveIn();
    }
    
    void ScaleSelectLayer::Deactivate()
    {
        MoveOut();
    }
    
    
    
    
    void ScaleSelectLayer::LoadButtonPositions()
    {
        Size visibleSize = Director::getInstance()->getVisibleSize();
        
        int gw = SceneGraph::getInstance()->GetGraphWidth(visibleSize.width);
        
        if(gw == SceneGraph::GraphWidth_HD_Wide)
        {
            CancelButtonX = 200;
            CancelButtonY = 620;
            
            
            KeyButtonX = 220;
            KeyButtonY = 540;
            StoreButtonX = 804;
            StoreButtonY = 580;
            
            ListBoxX = 356;
            ListBoxY = 644;

        }
        else if(gw == SceneGraph::GraphWidth_HD_Narrow)
        {
            CancelButtonX = 80;
            CancelButtonY = 740;
            
            KeyButtonX = 90;
            KeyButtonY = 640;
            StoreButtonX = 680;
            StoreButtonY = 700;
            
            ListBoxX = 226;
            ListBoxY = 774;
            
        }
        else if(gw == SceneGraph::GraphWidth_Wide)
        {
            int gs = SceneGraph::getInstance()->GetGraphResolution();
            if(gs == SceneGraph::GraphRes_960x640)
            {
                CancelButtonX = 200;
                CancelButtonY = 534;
                
                
                KeyButtonX = 216;
                KeyButtonY = 460;
                StoreButtonX = 746;
                StoreButtonY = 490;
                
                ListBoxX = 344;
                ListBoxY = 550;
            }
            else
            {
                CancelButtonX = 288;
                CancelButtonY = 534;
                
                
                KeyButtonX = 304;
                KeyButtonY = 460;
                StoreButtonX = 832;
                StoreButtonY = 490;
                
                ListBoxX = 426;
                ListBoxY = 550;
                
            }
            
        }
        else if(gw == SceneGraph::GraphWidth_Narrow)
        {
            
            int gs = SceneGraph::getInstance()->GetGraphResolution();
            if(gs == SceneGraph::GraphRes_960x640)
            {
                CancelButtonX = 40;
                CancelButtonY = 680;
                
                KeyButtonX = 60;
                KeyButtonY = 600;
                StoreButtonX = 586;
                StoreButtonY = 630;
                
                ListBoxX = 180;
                ListBoxY = 710;
            }
            else
            {
                CancelButtonX = 40;
                CancelButtonY = 780;
                
                KeyButtonX = 60;
                KeyButtonY = 700;
                StoreButtonX = 586;
                StoreButtonY = 730;
                
                ListBoxX = 180;
                ListBoxY = 800;
            }
            
        }
        else
        {
            assert(0);
        }
    }

    
    
}
