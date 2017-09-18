#include "MenuFunctionLayer.h"
#include "SceneGraph.h"
#include "AudioServices.h"
#include "GuitarNeck.h"
#include "FormData.h"
#include "ListBox.h"
#include "cell_progress_draw.h"
#include "MetaDataSave.h"


using namespace cocos2d;

namespace HandArcade
{
    
    
    MenuFunctionLayer::MenuFunctionLayer()
    {
    }
    
    MenuFunctionLayer::~MenuFunctionLayer()
    {
    }
    
    
    Layer* MenuFunctionLayer::layer()
    {
        MenuFunctionLayer *layer = MenuFunctionLayer::create();
        
        return layer;
    }
    
    
    
    
    bool MenuFunctionLayer::init()
    {
        if (!Layer::init())
        {
            return false;
        }
        
        
        
        mSlotIndex = 0;
        mStyleIndex = 0;
        mColorIndex = 0;
        mNeckIndex = 0;

        mEnableTouch = true;

        
        auto dispatcher = Director::getInstance()->getEventDispatcher();
        auto listener = EventListenerTouchAllAtOnce::create();
        listener->onTouchesBegan = CC_CALLBACK_2(MenuFunctionLayer::onTouchesBegan, this);
        listener->onTouchesMoved = CC_CALLBACK_2(MenuFunctionLayer::onTouchesMoved, this);
        listener->onTouchesEnded = CC_CALLBACK_2(MenuFunctionLayer::onTouchesEnded, this);
        dispatcher->addEventListenerWithSceneGraphPriority(listener, this);
        
        
        mMetaSaveData = new MetaSaveData();
        mMetaSaveData->LoadGameState();
        
        
        for(int i = 0; i < 5; i++)
        {
            int key = mMetaSaveData->GetSlotStateKey(i);
            int form = mMetaSaveData->GetSlotStateForm(i);
            SetForm(i, key, form);
        }


		scheduleUpdate();
        
        
        return true;
    }
    
    
    void MenuFunctionLayer::CreateLayout()
    {
        LoadButtonPositions();
        
        
        Sprite *pSprite = Sprite::createWithSpriteFrameName("load_button1.png");
        pSprite->setPosition(Point(LoadFormButtonX, LoadFormButtonY));
        this->addChild(pSprite, 10, ButtonLoadFormTag);

        Label *pLabel = Label::createWithBMFont("font_listbox_small.fnt", "Load Form");
        pLabel->setPosition(Point(LoadFormButtonX, LoadFormButtonY));
        pLabel->setColor(Color3B(0,0,0));
        this->addChild(pLabel, 20, ButtonLoadFormTextTag);
        
        
        //SLOT
        Sprite *slotSprite = Sprite::createWithSpriteFrameName("slot_button1.png");
        Size _ssize = slotSprite->getContentSize();
        
        //float swidth = _ssize.width;
        float sheight = _ssize.height;
        
        
        mSlotIndex = mMetaSaveData->GetSlotSelected();
        
        for(int i = 0; i < 5; i++)
        {
            Sprite *pSprite;
            
            if(i == mSlotIndex)
                pSprite = Sprite::createWithSpriteFrameName("slot_button2.png");
            else
                pSprite = Sprite::createWithSpriteFrameName("slot_button1.png");
            
            pSprite->setPosition(Point(SlotButtonX, SlotButtonY - sheight * i));
            
            this->addChild(pSprite, 10, ButtonSlotTag1 + i);
            
            char slotbuf[256];
            int keyIndex = mBasicForms[i].key;
            int formIndex = mBasicForms[i].form;
            char *formName = GetFormNameAtIndex(formIndex);
            sprintf(slotbuf, "%s %s", gKeyNames1[keyIndex], formName);
            
            
            Label *pLabel = Label::createWithBMFont("font_listbox_small.fnt", slotbuf);
            pLabel->setPosition(Point(SlotButtonX, SlotButtonY - sheight * i));
            if(i == mSlotIndex)
                pLabel->setColor(Color3B(0,0,0));
            else
                pLabel->setColor(Color3B(255,255,255));
            
            
            
            this->addChild(pLabel, 20, ButtonSlotTextTag1 + i);
        }
        
        
        //STYLE
        Sprite *roundSprite = Sprite::createWithSpriteFrameName("key_radio_button1.png");
        _ssize = roundSprite->getContentSize();
        
        //float swidth = _ssize.width;
        sheight = _ssize.height;
        
        mStyleIndex = mMetaSaveData->GetStyleSelected();

        for(int i = 0; i < 4; i++)
        {
            Sprite *pSprite;
            
            if(i == mStyleIndex)
                pSprite = Sprite::createWithSpriteFrameName("key_radio_button2.png");
            else
                pSprite = Sprite::createWithSpriteFrameName("key_radio_button1.png");
            
            pSprite->setPosition(Point(StyleButtonX, StyleButtonY - sheight * i));
            
            this->addChild(pSprite, 10, ButtonStyleTag1 + i);
            
            char slotbuf[256];
            sprintf(slotbuf, "%s", gDisplayStyleText[i]);
            
            
            Label *pLabel = Label::createWithBMFont("font_listbox_small.fnt", slotbuf);
            pLabel->setPosition(Point(StyleButtonX, StyleButtonY - sheight * i));
            if(i == mStyleIndex)
                pLabel->setColor(Color3B(0,0,0));
            else
                pLabel->setColor(Color3B(255,255,255));
            
            
            this->addChild(pLabel, 20, ButtonStyleTextTag1 + i);
        }
        
        
        //COLOR
        Sprite *colorSprite = Sprite::createWithSpriteFrameName("key_radio_button1.png");
        _ssize = colorSprite->getContentSize();
        
        //float swidth = _ssize.width;
        sheight = _ssize.height;
        
        mColorIndex = mMetaSaveData->GetColorSelected();
        
        for(int i = 0; i < 4; i++)
        {
            Sprite *pSprite;
            
            if(i == mColorIndex)
                pSprite = Sprite::createWithSpriteFrameName("key_radio_button2.png");
            else
                pSprite = Sprite::createWithSpriteFrameName("key_radio_button1.png");
            
            pSprite->setPosition(Point(ColorButtonX, ColorButtonY - sheight * i));
            
            this->addChild(pSprite, 10, ButtonColorTag1 + i);
            
            char slotbuf[256];
            sprintf(slotbuf, "%d", i+1);
            
            
            Label *pLabel = Label::createWithBMFont("font_listbox_small.fnt", slotbuf);
            pLabel->setPosition(Point(ColorButtonX, ColorButtonY - sheight * i));
            if(i == mColorIndex)
                pLabel->setColor(Color3B(0,0,0));
            else
                pLabel->setColor(Color3B(255,255,255));
            
            
            this->addChild(pLabel, 20, ButtonColorTextTag1 + i);
        }
        
        
        
        //NECK
        Sprite *neckSprite = Sprite::createWithSpriteFrameName("key_radio_button1.png");
        _ssize = neckSprite->getContentSize();
        
        //float swidth = _ssize.width;
        float swidth = _ssize.width;
        
        mNeckIndex = mMetaSaveData->GetNeckSelected();
        
        for(int i = 0; i < 2; i++)
        {
            Sprite *pSprite;
            
            if(i == mNeckIndex)
                pSprite = Sprite::createWithSpriteFrameName("key_radio_button2.png");
            else
                pSprite = Sprite::createWithSpriteFrameName("key_radio_button1.png");
            
            pSprite->setPosition(Point(NeckButtonX + swidth * i, NeckButtonY));
            
            this->addChild(pSprite, 10, ButtonNeckTag1 + i);
            
            char slotbuf[256];
            if(i == 0)
                sprintf(slotbuf, "Rose");
            else
                sprintf(slotbuf, "Maple");
            
            
            
            Label *pLabel = Label::createWithBMFont("font_listbox_small.fnt", slotbuf);
            pLabel->setPosition(Point(NeckButtonX + swidth * i, NeckButtonY));
            if(i == mNeckIndex)
                pLabel->setColor(Color3B(0,0,0));
            else
                pLabel->setColor(Color3B(255,255,255));
            
            
            this->addChild(pLabel, 20, ButtonNeckTextTag1 + i);
        }



        postMessageInt2(Event_LoadSlot, mBasicForms[mSlotIndex].key, mBasicForms[mSlotIndex].form, GenericMessage::PriorityHi);
        postMessageInt(Event_LoadStyle, mStyleIndex, GenericMessage::PriorityHi);
        postMessageInt(Event_LoadColor, mColorIndex, GenericMessage::PriorityHi);
        postMessageInt(Event_LoadNeck, mNeckIndex, GenericMessage::PriorityHi);

    }
    
    
    
    
    void MenuFunctionLayer::RefreshLayout()
    {
        LoadButtonPositions();
        
        //LOAD FORM
        Sprite *pSprite = (Sprite *)this->getChildByTag(ButtonLoadFormTag);
        pSprite->setPosition(Point(LoadFormButtonX, LoadFormButtonY));
        
        Label *pLabel = (Label *)this->getChildByTag(ButtonLoadFormTextTag );
        pLabel->setPosition(Point(LoadFormButtonX, LoadFormButtonY));
        
        
        //SLOTS
        Sprite *slotSprite = Sprite::createWithSpriteFrameName("slot_button1.png");
        Size _ssize = slotSprite->getContentSize();
        
        float sheight = _ssize.height;
        
        for(int i = 0; i < 5; i++)
        {
            Sprite *pSprite = (Sprite *)this->getChildByTag(ButtonSlotTag1 + i);
            pSprite->setPosition(Point(SlotButtonX, SlotButtonY - sheight * i));
            
            Label *pLabel = (Label *)this->getChildByTag(ButtonSlotTextTag1 + i );
            pLabel->setPosition(Point(SlotButtonX, SlotButtonY - sheight * i));
        }
        
        
        //STYLES
        Sprite *roundSprite = Sprite::createWithSpriteFrameName("key_radio_button1.png");
        _ssize = roundSprite->getContentSize();
        
        sheight = _ssize.height;
        
        for(int i = 0; i < 4; i++)
        {
            Sprite *pSprite = (Sprite *)this->getChildByTag(ButtonStyleTag1 + i);
            pSprite->setPosition(Point(StyleButtonX, StyleButtonY - sheight * i));
            
            Label *pLabel = (Label *)this->getChildByTag(ButtonStyleTextTag1 + i );
            pLabel->setPosition(Point(StyleButtonX, StyleButtonY - sheight * i));
        }
        
        
        //COLOR
        Sprite *colorSprite = Sprite::createWithSpriteFrameName("key_radio_button1.png");
        _ssize = colorSprite->getContentSize();
        
        sheight = _ssize.height;
        
        for(int i = 0; i < 4; i++)
        {
            Sprite *pSprite = (Sprite *)this->getChildByTag(ButtonColorTag1 + i);
            pSprite->setPosition(Point(ColorButtonX, ColorButtonY - sheight * i));
            
            Label *pLabel = (Label *)this->getChildByTag(ButtonColorTextTag1 + i );
            pLabel->setPosition(Point(ColorButtonX, ColorButtonY - sheight * i));
        }
        
        
        //NECK
        Sprite *neckSprite = Sprite::createWithSpriteFrameName("key_radio_button1.png");
        _ssize = neckSprite->getContentSize();
        
        float swidth = _ssize.width;
        
        for(int i = 0; i < 2; i++)
        {
            Sprite *pSprite = (Sprite *)this->getChildByTag(ButtonNeckTag1 + i);
            pSprite->setPosition(Point(NeckButtonX + swidth * i, NeckButtonY));
            
            Label *pLabel = (Label *)this->getChildByTag(ButtonNeckTextTag1 + i );
            pLabel->setPosition(Point(NeckButtonX + swidth * i, NeckButtonY));
        }
    }

    
    void MenuFunctionLayer::RefreshSlots()
    {
        for(int i = 0; i < 5; i++)
        {
            
            Label *pLabel = (Label *)this->getChildByTag(ButtonSlotTextTag1 + i);
            
            char slotbuf[256];
            int keyIndex = mBasicForms[i].key;
            int formIndex = mBasicForms[i].form;
            char *formName = GetFormNameAtIndex(formIndex);
            sprintf(slotbuf, "%s %s", gKeyNames1[keyIndex], formName);
            
            pLabel->setString(slotbuf);
            
            
            if(mSlotIndex == i)
            {
                Sprite *lastSprite = (Sprite *)this->getChildByTag(ButtonSlotTag1 + i);
                lastSprite->setSpriteFrame("slot_button2.png");
                
                Label *pLabel = (Label *)this->getChildByTag(ButtonSlotTextTag1 + i );
                pLabel->setColor(Color3B(0,0,0));
                
            }
            else
            {
                Sprite *lastSprite = (Sprite *)this->getChildByTag(ButtonSlotTag1 + i);
                lastSprite->setSpriteFrame("slot_button1.png");
                
                Label *pLabel = (Label *)this->getChildByTag(ButtonSlotTextTag1 + i );
                pLabel->setColor(Color3B(255,255,255));
                
            }

            mMetaSaveData->SaveSlotState(i, keyIndex, formIndex);
        }
        
        mMetaSaveData->SaveGameState();
    }
    
    void MenuFunctionLayer::update(float dt)
    {
    }
    
    
    
    
	GenericMessage* MenuFunctionLayer::GetMessage()
	{
		GenericMessage *pMsg = _menuFunctionMessageQueue.Remove();
		return pMsg;
	}
	void MenuFunctionLayer::postMessageInt(int command, int int1, const int& priority)
	{
		GenericMessage *pMsg = new GenericMessage();
        
		pMsg->SetMessageCommand(command);
		pMsg->SetInt1(int1);
        
		_menuFunctionMessageQueue.Add(pMsg, priority);
	}
	void MenuFunctionLayer::postMessageInt2(int command, int int1, int int2, const int& priority)
	{
		GenericMessage *pMsg = new GenericMessage();
        
		pMsg->SetMessageCommand(command);
		pMsg->SetInt1(int1);
		pMsg->SetInt2(int2);
        
		_menuFunctionMessageQueue.Add(pMsg, priority);
	}
    
    
    
    
    
    
    
    void MenuFunctionLayer::onTouchesBegan(const std::vector<Touch*>& touches, Event* event)
    {
        if(mEnableTouch == false)
            return;

        Touch* touch = touches[0];
        Point location = touch->getLocation();
        
        
        
        processTouch(location);
    }
    void MenuFunctionLayer::onTouchesMoved(const std::vector<Touch*>& touches, Event* event)
    {
    }
    void MenuFunctionLayer::onTouchesEnded(const std::vector<Touch*>& touches, Event* event)
    {
    }
    
    
    Rect MenuFunctionLayer::getTargetRect(Sprite *pSprite)
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
   
    void MenuFunctionLayer::processTouch(Point p)
    {
        
        for(int i = 0; i < 1; i++)
        {
            Sprite *pSprite = (Sprite *)this->getChildByTag(ButtonLoadFormTag + i);
            Rect targetRect = getTargetRect(pSprite);
            
            if (targetRect.containsPoint(p))
            {
                CCLOG("Key button %d", i);
                
                mEvent = Event_LoadForm;
                pSprite->setSpriteFrame("load_button2.png");
                animateButton(pSprite);
                //_PlaySFX(SFX_Bubble);
                break;
            }
        }
        
        
        for(int i = 0; i < 5; i++)
        {
            Sprite *pSprite = (Sprite *)this->getChildByTag(ButtonSlotTag1 + i);
            Rect targetRect = getTargetRect(pSprite);
            
            if (targetRect.containsPoint(p))
            {
                CCLOG("Slot button %d", i);
                
                //clear last slot index
                Sprite *lastSprite = (Sprite *)this->getChildByTag(ButtonSlotTag1 + mSlotIndex);
                lastSprite->setSpriteFrame("slot_button1.png");
                
                Label *pLabel = (Label *)this->getChildByTag(ButtonSlotTextTag1 + mSlotIndex );
                pLabel->setColor(Color3B(255,255,255));

  
                pLabel = (Label *)this->getChildByTag(ButtonSlotTextTag1 + i );
                pLabel->setColor(Color3B(0,0,0));

                mSlotIndex = i;
                mMetaSaveData->SaveSlotSelected(mSlotIndex);
                mMetaSaveData->SaveGameState();

                mEvent = Event_LoadSlot;
                pSprite->setSpriteFrame("slot_button2.png");
                animateButton(pSprite);
                //_PlaySFX(SFX_Bubble);
                break;
            }
        }

        //STYLE BUTTONS
        for(int i = 0; i < 4; i++)
        {
            Sprite *pSprite = (Sprite *)this->getChildByTag(ButtonStyleTag1 + i);
            Rect targetRect = getTargetRect(pSprite);
            
            if (targetRect.containsPoint(p))
            {
                //clear last style index
                Sprite *lastSprite = (Sprite *)this->getChildByTag(ButtonStyleTag1 + mStyleIndex);
                lastSprite->setSpriteFrame("key_radio_button1.png");
                
                
                Label *pLabel = (Label *)this->getChildByTag(ButtonStyleTextTag1 + mStyleIndex );
                pLabel->setColor(Color3B(255,255,255));
                
                
                pLabel = (Label *)this->getChildByTag(ButtonStyleTextTag1 + i );
                pLabel->setColor(Color3B(0,0,0));

                
                mStyleIndex = i;
                mMetaSaveData->SaveStyleSelected(mStyleIndex);
                mMetaSaveData->SaveGameState();

                
                mEvent = Event_LoadStyle;
                pSprite->setSpriteFrame("key_radio_button2.png");
                animateButton(pSprite);
                //_PlaySFX(SFX_Bubble);
                break;
            }

        }
        
        
        
        //COLOR BUTTONS
        for(int i = 0; i < 4; i++)
        {
            Sprite *pSprite = (Sprite *)this->getChildByTag(ButtonColorTag1 + i);
            Rect targetRect = getTargetRect(pSprite);
            
            if (targetRect.containsPoint(p))
            {
                //clear last style index
                Sprite *lastSprite = (Sprite *)this->getChildByTag(ButtonColorTag1 + mColorIndex);
                lastSprite->setSpriteFrame("key_radio_button1.png");
                
                
                Label *pLabel = (Label *)this->getChildByTag(ButtonColorTextTag1 + mColorIndex );
                pLabel->setColor(Color3B(255,255,255));
                
                
                pLabel = (Label *)this->getChildByTag(ButtonColorTextTag1 + i );
                pLabel->setColor(Color3B(0,0,0));
                
                
                mColorIndex = i;
                mMetaSaveData->SaveColorSelected(mColorIndex);
                mMetaSaveData->SaveGameState();
                
                
                mEvent = Event_LoadColor;
                pSprite->setSpriteFrame("key_radio_button2.png");
                animateButton(pSprite);
                //_PlaySFX(SFX_Bubble);
                break;
            }
            
        }

        //NECK BUTTONS
        for(int i = 0; i < 2; i++)
        {
            Sprite *pSprite = (Sprite *)this->getChildByTag(ButtonNeckTag1 + i);
            Rect targetRect = getTargetRect(pSprite);
            
            if (targetRect.containsPoint(p))
            {
                //clear last style index
                Sprite *lastSprite = (Sprite *)this->getChildByTag(ButtonNeckTag1 + mNeckIndex);
                lastSprite->setSpriteFrame("key_radio_button1.png");
                
                
                Label *pLabel = (Label *)this->getChildByTag(ButtonNeckTextTag1 + mNeckIndex );
                pLabel->setColor(Color3B(255,255,255));
                
                
                pLabel = (Label *)this->getChildByTag(ButtonNeckTextTag1 + i );
                pLabel->setColor(Color3B(0,0,0));
                
                
                mNeckIndex = i;
                mMetaSaveData->SaveNeckSelected(mNeckIndex);
                mMetaSaveData->SaveGameState();
                
                
                mEvent = Event_LoadNeck;
                pSprite->setSpriteFrame("key_radio_button2.png");
                animateButton(pSprite);
                //_PlaySFX(SFX_Bubble);
                break;
            }
            
        }

        
    }
    
    void MenuFunctionLayer::animateButton(Sprite *_sprite)
    {
        ActionInterval *actionScaleDown = ScaleTo::create(0.1, 0.94f, 0.94f);
        CallFunc *actionCallback = CallFunc::create( CC_CALLBACK_0(MenuFunctionLayer::_animButtonPress, this, _sprite) );
        FiniteTimeAction* _action = Sequence::create(actionScaleDown, actionCallback, NULL);
        
        _sprite->runAction( _action );
    }
    
	void MenuFunctionLayer::_animButtonPress(Node* sender)
	{
        Sprite *_sprite = (Sprite *)sender;
        
        ActionInterval *actionScaleUp = ScaleTo::create(0.1, 1.0f, 1.0f);
        
        CallFunc *actionCallback = CallFunc::create(CC_CALLBACK_0(MenuFunctionLayer::_animButtonRelease, this, _sprite));
        
        FiniteTimeAction* _action = Sequence::create(actionScaleUp, actionCallback, NULL);
        ActionInterval* _actionBounce = EaseBounceOut::create((ActionInterval*) _action );
        
        _sprite->runAction( _actionBounce );
	}
    
	void MenuFunctionLayer::_animButtonRelease(Node* sender)
	{
        Sprite *pSprite = (Sprite *)sender;
        
        if(mEvent == Event_LoadForm)
        {
            pSprite->setSpriteFrame("load_button1.png");
            postMessageInt(Event_LoadForm, 0, GenericMessage::PriorityHi);
        }
        else if(mEvent == Event_LoadSlot)
        {
            postMessageInt2(Event_LoadSlot, mBasicForms[mSlotIndex].key, mBasicForms[mSlotIndex].form, GenericMessage::PriorityHi);
        }
        else if(mEvent == Event_LoadStyle)
        {
            postMessageInt(Event_LoadStyle, mStyleIndex, GenericMessage::PriorityHi);
        }
        else if(mEvent == Event_LoadColor)
        {
            postMessageInt(Event_LoadColor, mColorIndex, GenericMessage::PriorityHi);
        }
        else if(mEvent == Event_LoadNeck)
        {
            postMessageInt(Event_LoadNeck, mNeckIndex, GenericMessage::PriorityHi);
        }
    }
    
    
    
    void MenuFunctionLayer::LoadButtonPositions()
    {
        Size visibleSize = Director::getInstance()->getVisibleSize();
        
        int gw = SceneGraph::getInstance()->GetGraphWidth(visibleSize.width);
        
        if(gw == SceneGraph::GraphWidth_HD_Wide)
        {
            LoadFormButtonX = 180;
            LoadFormButtonY = 650;
            
            SlotButtonX = 180;
            SlotButtonY = 580;
            
            StyleButtonX = 132;
            StyleButtonY = 288;
            
            
            ColorButtonX = 220;
            ColorButtonY = 288;
            
            NeckButtonX = 138;
            NeckButtonY = 76;

            
        }
        else if(gw == SceneGraph::GraphWidth_HD_Narrow)
        {
            LoadFormButtonX = 100;
            LoadFormButtonY = 750;
            
            SlotButtonX = 100;
            SlotButtonY = 680;
            
            StyleButtonX = 56;
            StyleButtonY = 380;
            
            ColorButtonX = 140;
            ColorButtonY = 380;
            
            NeckButtonX = 60;
            NeckButtonY = 160;
        }
        else if(gw == SceneGraph::GraphWidth_Wide)
        {
            LoadFormButtonX = 210;
            LoadFormButtonY = 580;
            
            SlotButtonX = 210;
            SlotButtonY = 500;
            
            StyleButtonX = 170;
            StyleButtonY = 232;
            
            ColorButtonX = 250;
            ColorButtonY = 232;
            
            NeckButtonX = 174;
            NeckButtonY = 38;
        }
        else if(gw == SceneGraph::GraphWidth_Narrow)
        {
            LoadFormButtonX = 80;
            LoadFormButtonY = 780;
            
            SlotButtonX = 80;
            SlotButtonY = 680;
            
            StyleButtonX = 40;
            StyleButtonY = 410;
            
            ColorButtonX = 120;
            ColorButtonY = 410;
            
            NeckButtonX = 44;
            NeckButtonY = 210;
        }
        else
        {
            assert(0);
        }
    }
    
    
}
