#include "NeckDisplayLayer.h"
#include "SceneGraph.h"
#include "AudioServices.h"
#include "GuitarNeck.h"
#include "FormData.h"


using namespace cocos2d;

namespace HandArcade
{
    
    NeckDisplayLayer::NeckDisplayLayer()
    {
    }
    
    NeckDisplayLayer::~NeckDisplayLayer()
    {
    }
    
    
    Layer* NeckDisplayLayer::layer()
    {
        NeckDisplayLayer *layer = NeckDisplayLayer::create();
        
        return layer;
    }
    
    
    
    
    bool NeckDisplayLayer::init()
    {
        if (!Layer::init())
        {
            return false;
        }
        
    
        mGuitarNeck = new GuitarNeck();
        
        //default
        mGuitarNeck->ApplyForm(NOTE_E, 0, 0);
        
        
        mMarkerTags[0] = MarkerTag1;
        mMarkerTags[1] = MarkerTag2;
        mMarkerTags[2] = MarkerTag3;
        mMarkerTags[3] = MarkerTag4;
        mMarkerTags[4] = MarkerTag5;
        mMarkerTags[5] = MarkerTag6;

        mLabelTags[0] = LabelTag1;
        mLabelTags[1] = LabelTag2;
        mLabelTags[2] = LabelTag3;
        mLabelTags[3] = LabelTag4;
        mLabelTags[4] = LabelTag5;
        mLabelTags[5] = LabelTag6;

        LoadColors();
        
        auto dispatcher = Director::getInstance()->getEventDispatcher();
        auto listener = EventListenerTouchAllAtOnce::create();
        listener->onTouchesBegan = CC_CALLBACK_2(NeckDisplayLayer::onTouchesBegan, this);
        listener->onTouchesMoved = CC_CALLBACK_2(NeckDisplayLayer::onTouchesMoved, this);
        listener->onTouchesEnded = CC_CALLBACK_2(NeckDisplayLayer::onTouchesEnded, this);
        dispatcher->addEventListenerWithSceneGraphPriority(listener, this);
        
        mEnableTouch = true;
        TouchStateDown = false;
        mTouchGlide = false;
        mGlideY = 0.0;
        _coeff = 0.96;
        
        mKey = 0;
        mForm = 0;
        mDisplayStyle = DisplayStyleSharp;
        
        Size visibleSize = Director::getInstance()->getVisibleSize();

        mTopBoundary = 2000.0;
        mBotBoundary = 0.0;
        
        mOffsetX = 0;
        mOffsetY = 0;
        this->setPosition(mNeckLocation);
        
		scheduleUpdate();
        
        
        return true;
    }
    
    
    void NeckDisplayLayer::CreateLayout()
    {
        Size visibleSize = Director::getInstance()->getVisibleSize();
        float _centerX = visibleSize.width / 2.0;
        
        LoadButtonPositions();

        mNeckLocation = Point(mOffsetX, mOffsetY);
        this->setPosition(mNeckLocation);

        int fretBinary[MaxFrets] = {0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 0, 1, 0, 0, 1, 0, 1, 0, 1, 0, 1, 0, 0, 1};
        Sprite *pSprite = Sprite::createWithSpriteFrameName("fretbase.png");
        Size fretsize = pSprite->getContentSize();

        mNumStrings = 6;
        
        mFretHeight = fretsize.height;
        mFretWidth = fretsize.width - 20;
        
        CCLOG("mFretHeight = %f", mFretHeight);
        
        mStringGap = mFretWidth / mNumStrings;
        mFretGap = mFretHeight / 2;
        
        float fullFretWidth = fretsize.width;
        mfullStringGap = fullFretWidth / mNumStrings;
        
        float firstfretX = _centerX - mFretWidth/2 + mStringGap/2;
        
        
        float fretNumberOffsetX = mfullStringGap - mfullStringGap/5;
        
        for(int f = 0; f < MaxFrets; f++)
        {
            Sprite *pSprite;
            
            if(f < MaxFrets - 1)
                pSprite = Sprite::createWithSpriteFrameName("fretbase.png");
            else
                pSprite = Sprite::createWithSpriteFrameName("fretpickup.png");
            
            pSprite->setPosition(Point(_centerX, mNeckTopY - (mFretHeight * (float)f)));
            
            this->addChild(pSprite, 10, FretBackTag1 + f);
            
            if(fretBinary[f] == 1)
            {
                char buffer[128];
                sprintf(buffer, "%d", f);
                Label *pLabel = Label::createWithBMFont("font_listbox_large.fnt", buffer);
                pLabel->setPosition(Point(firstfretX - fretNumberOffsetX, mNeckTopY + mFretHeight - (mFretHeight * (float)f)));
                pLabel->setColor(Color3B(255,255,255));

                this->addChild(pLabel, 20, FretNumberTag2 + f);
            }
        }
        
        
        for(int s = 0; s < mNumStrings; s++)
        {
            for(int f = 0; f < MaxFrets; f++)
            {
                Sprite *pSprite = Sprite::createWithSpriteFrameName("marker_circle.png");
                
                pSprite->setPosition(Point(firstfretX + mStringGap * s, mNeckTopY + mFretHeight - (mFretHeight * (float)f)));
                
                this->addChild(pSprite, 100, mMarkerTags[s] + f);
                
                Label *pLabel = Label::createWithBMFont("font_listbox_med.fnt", "E");
                pLabel->setPosition(Point(firstfretX + mStringGap * s, mNeckTopY + mFretHeight - (mFretHeight * (float)f)));
                
                pLabel->setColor(Color3B(0,0,0));

                this->addChild(pLabel, 110, mLabelTags[s] + f);
            }
        }
        
        
        
        
        //Side text
        for(int i = 0; i < 6; i++)
        {
            char slotbuf[256];
            int keyIndex = mKey;
            int formIndex = mForm;
            char *formName = GetFormNameAtIndex(formIndex);
            sprintf(slotbuf, "%s %s", gKeyNames1[keyIndex], formName);
            
            Label *pLabel = Label::createWithBMFont("font_listbox_med.fnt", slotbuf);
            pLabel->setPosition(Point(firstfretX + mfullStringGap * 6 - (mfullStringGap/3), mNeckTopY - (mFretHeight*4) * i));
            pLabel->setColor(Color3B(255,255,255));
            pLabel->setRotation(270);
            
            
            this->addChild(pLabel, 20, FretInfoTag1 + i);
        }


    }
    
    
    
    //Reposition based on orientation change
    void NeckDisplayLayer::RefreshLayout()
    {
        Size visibleSize = Director::getInstance()->getVisibleSize();
        float _centerX = visibleSize.width / 2.0;
        
        LoadButtonPositions();

        
        mNeckLocation = Point(mOffsetX, mOffsetY);
        this->setPosition(mNeckLocation);
        
        int fretBinary[MaxFrets] = {0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 0, 1, 0, 0, 1, 0, 1, 0, 1, 0, 1, 0, 0, 1};
        float firstfretX = _centerX - mFretWidth/2 + mStringGap/2;
        float fretNumberOffsetX = mfullStringGap - mfullStringGap/5;

        
        for(int f = 0; f < MaxFrets; f++)
        {
            Sprite *pSprite = (Sprite *)this->getChildByTag(FretBackTag1 + f);
            pSprite->setPosition(Point(_centerX, mNeckTopY - (mFretHeight * (float)f)));
            
            if(fretBinary[f] == 1)
            {
                Label *pLabel = (Label *)this->getChildByTag(FretNumberTag2 + f);
                pLabel->setPosition(Point(firstfretX - fretNumberOffsetX, mNeckTopY + mFretHeight - (mFretHeight * (float)f)));
            }
        }
        
        
        for(int s = 0; s < mNumStrings; s++)
        {
            for(int f = 0; f < MaxFrets; f++)
            {
                Sprite *pSprite = (Sprite *)this->getChildByTag(mMarkerTags[s] + f);
                pSprite->setPosition(Point(firstfretX + mStringGap * s, mNeckTopY + mFretHeight - (mFretHeight * (float)f)));
                
                Label *pLabel = (Label *)this->getChildByTag(mLabelTags[s] + f);
                pLabel->setPosition(Point(firstfretX + mStringGap * s, mNeckTopY + mFretHeight - (mFretHeight * (float)f)));
            }
        }
        
        
        for(int i = 0; i < 6; i++)
        {
            char slotbuf[256];
            int keyIndex = mKey;
            int formIndex = mForm;
            char *formName = GetFormNameAtIndex(formIndex);
            sprintf(slotbuf, "%s %s", gKeyNames1[keyIndex], formName);
            
            Label *pLabel = (Label *)this->getChildByTag(FretInfoTag1 + i);
            pLabel->setPosition(Point(firstfretX + mfullStringGap * 6 - (mfullStringGap/3), mNeckTopY - (mFretHeight*4) * i));
        }

        
    }
    
    
    void NeckDisplayLayer::ChangeFretBacking(int type)
    {
        for(int f = 0; f < MaxFrets-1; f++)
        {
            Sprite *pSprite = (Sprite *)this->getChildByTag(FretBackTag1 + f);
            
            if(type == 0)
            {
                pSprite->setSpriteFrame("fretbase.png");
            }
            else
            {
                pSprite->setSpriteFrame("fretbase2.png");
            }
        }
    }


    
    void NeckDisplayLayer::ApplyForm(int key, int formIndex)
    {
        mKey = key;
        mForm = formIndex;
        
        mGuitarNeck->Clear();
        
        CCLOG("NeckDisplayLayer::ApplyForm %d : %d", key, formIndex);
        mGuitarNeck->ApplyForm(key, formIndex, 0);

        int c = mColorIdx;
        for(int s = 0; s < mNumStrings; s++)
        {
            for(int f = 0; f < MaxFrets; f++)
            {
                int note = mGuitarNeck->GetNote(s, f);
                int interval = mGuitarNeck->GetInterval(s, f);

                Sprite *pSprite = (Sprite *)this->getChildByTag(mMarkerTags[s] + f);
                Label *pLabel = (Label *)this->getChildByTag(mLabelTags[s] + f);
                
                
                
                if(mGuitarNeck->GetNoteStatus(s, f))
                {
                    pSprite->setVisible(true);
                    pLabel->setVisible(true);
                    
                    
                    if(interval == 0)
                    {
                        //Root
                        if(f == 0)
                        {
                            //open string
                            pSprite->setSpriteFrame("marker_hex_h.png");
                            pLabel->setColor(Color3B(255,255,255));
                        }
                        else
                        {
                            pSprite->setSpriteFrame("marker_hex.png");
                            pLabel->setColor(Color3B(mColorIdxText[c][0],mColorIdxText[c][0],mColorIdxText[c][0]));
                        }
                        
                        pSprite->setColor(Color3B(mColorIdxRed[c][0], mColorIdxGreen[c][0], mColorIdxBlue[c][0]));

                    }
                    else if(interval == 7)
                    {
                        //5th
                        if(f == 0)
                        {
                            //open string
                            pSprite->setSpriteFrame("marker_diamond_h.png");
                            pLabel->setColor(Color3B(255,255,255));
                        }
                        else
                        {
                            pSprite->setSpriteFrame("marker_diamond.png");
                            pLabel->setColor(Color3B(mColorIdxText[c][1],mColorIdxText[c][1],mColorIdxText[c][1]));
                        }
                        
                        pSprite->setColor(Color3B(mColorIdxRed[c][1], mColorIdxGreen[c][1], mColorIdxBlue[c][1]));
                    }
                    else
                    {
                        if(f == 0)
                        {
                            //open string
                            pSprite->setSpriteFrame("marker_circle_h.png");
                            pLabel->setColor(Color3B(255,255,255));
                        }
                        else
                        {
                            pSprite->setSpriteFrame("marker_circle.png");
                            pLabel->setColor(Color3B(mColorIdxText[c][2],mColorIdxText[c][2],mColorIdxText[c][2]));
                        }
                        
                        pSprite->setColor(Color3B(mColorIdxRed[c][2], mColorIdxGreen[c][2], mColorIdxBlue[c][2]));
                    }
                    
                    
                }
                else
                {
                    pSprite->setVisible(false);
                    pLabel->setVisible(false);
                }
                
                
                switch( mDisplayStyle )
                {
                    case DisplayStyleSharp:
                        pLabel->setString(gKeyNamesSharp[note]);
                        break;
                    case DisplayStyleFlat:
                        pLabel->setString(gKeyNamesFlat[note]);
                        break;
                    case DisplayStyleInterval2:
                        pLabel->setString(gIntervalText[interval]);
                        break;
                    case DisplayStyleInterval9:
                        pLabel->setString(gIntervalExtendedText[interval]);
                        break;
                }
                

            }
        }
        
        
        
        for(int i = 0; i < 6; i++)
        {
            char slotbuf[256];
            int keyIndex = mKey;
            int formIndex = mForm;
            char *formName = GetFormNameAtIndex(formIndex);
            sprintf(slotbuf, "%s %s", gKeyNames1[keyIndex], formName);
            
            Label *pLabel = (Label *)this->getChildByTag(FretInfoTag1 + i);
            
            pLabel->setString(slotbuf);
        }

        
    }
    
    
    void NeckDisplayLayer::update(float dt)
    {
        
        if(TouchStateDown == true)
        {
            mTouchTimer += dt;
        }
        
        if(mTouchGlide)
        {
            
            mGlideY *= _coeff;
            //CCLOG("mGlideY = %f", mGlideY);

            if(abs(mGlideY) < 0.1)
            {
                mTouchGlide = false;
            }
            
            
            mNeckLocation.y += mGlideY;
            
            if(mNeckLocation.y > mTopBoundary)
            {
                mNeckLocation.y = mTopBoundary;
                mTouchGlide = false;
            }
            else if(mNeckLocation.y < mBotBoundary)
            {
                mNeckLocation.y = mBotBoundary;
                mTouchGlide = false;
            }

            this->setPosition(mNeckLocation);

        }
    }
    
    
    
    
	GenericMessage* NeckDisplayLayer::GetMessage()
	{
		GenericMessage *pMsg = _neckDisplayMessageQueue.Remove();
		return pMsg;
	}
	void NeckDisplayLayer::postMessageInt(int command, int int1, const int& priority)
	{
		GenericMessage *pMsg = new GenericMessage();
        
		pMsg->SetMessageCommand(command);
		pMsg->SetInt1(int1);
        
		_neckDisplayMessageQueue.Add(pMsg, priority);
	}
    
    
    
    
    
    
    
    void NeckDisplayLayer::onTouchesBegan(const std::vector<Touch*>& touches, Event* event)
    {
        if(mEnableTouch == false)
            return;
        
        Touch* touch = touches[0];
        Point location = touch->getLocation();
        
        
        //float _height = SceneGraph::getInstance()->GetDesignHeight();
        //location.y = _height - location.y;
        
        mTouchLocationDown = location;
        TouchStateDown = true;
        mTouchTimer = 0.0;
        mTouchGlide = false;
        
        
        processTouch(location);
    }
    void NeckDisplayLayer::onTouchesMoved(const std::vector<Touch*>& touches, Event* event)
    {
        if(mEnableTouch == false)
            return;
        
        Touch* touch = touches[0];
        Point location = touch->getLocation();
        
        float diffY = location.y - mTouchLocationDown.y;
        Point offset = mNeckLocation;
        offset.y += diffY;
        
        if(offset.y > mTopBoundary)
        {
            offset.y = mTopBoundary;
        }
        else if(offset.y < mBotBoundary)
        {
            offset.y = mBotBoundary;
        }
        
        this->setPosition(offset);
        
    }
    void NeckDisplayLayer::onTouchesEnded(const std::vector<Touch*>& touches, Event* event)
    {
        if(mEnableTouch == false)
            return;
        
        Touch* touch = touches[0];
        Point location = touch->getLocation();
        
        float diffY = location.y - mTouchLocationDown.y;
        Point offset = mNeckLocation;
        offset.y += diffY;
        
        if(offset.y > mTopBoundary)
        {
            offset.y = mTopBoundary;
        }
        else if(offset.y < mBotBoundary)
        {
            offset.y = mBotBoundary;
        }

        if(mTouchTimer < 0.5)
        {
            mGlideY = diffY / 6.0;
            
            CCLOG("diffY = %f", diffY);
            
            mTouchGlide = true;
        }
        
        mNeckLocation = offset;
        this->setPosition(mNeckLocation);

        TouchStateDown = false;
        
    }
    
    
    Rect NeckDisplayLayer::getTargetRect(Sprite *pSprite)
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
    
    void NeckDisplayLayer::processTouch(Point p)
    {
        
        
        for(int s = 0; s < mNumStrings; s++)
        {
            for(int f = 0; f < MaxFrets-1; f++)
            {
                if(mGuitarNeck->GetNoteStatus(s, f))
                {
                    Sprite *pSprite = (Sprite *)this->getChildByTag(mMarkerTags[s] + f);
                    Rect targetRect = getTargetRect(pSprite);

                    if (targetRect.containsPoint(p))
                    {
                        CCLOG("%d, %d", s, f);
                        
                        
                        //_PlaySFX(SFX_Bubble);
                    }

                }
            }
        }
    }
    
    
    
    
    
    void NeckDisplayLayer::LoadButtonPositions()
    {
        Size visibleSize = Director::getInstance()->getVisibleSize();
        
        int gw = SceneGraph::getInstance()->GetGraphWidth(visibleSize.width);
        
        if(gw == SceneGraph::GraphWidth_HD_Wide)
        {
            mNeckTopY = 600.0;
            
            mTopBoundary = 2150.0;
            mBotBoundary = 0.0;

        }
        else if(gw == SceneGraph::GraphWidth_HD_Narrow)
        {
            mNeckTopY = 860.0;
            
            mTopBoundary = 1900.0;
            mBotBoundary = 0.0;

        }
        else if(gw == SceneGraph::GraphWidth_Wide)
        {
            mNeckTopY = 490.0;
            
            mTopBoundary = 1800.0;
            mBotBoundary = 0.0;

        }
        else if(gw == SceneGraph::GraphWidth_Narrow)
        {
            
            int gs = SceneGraph::getInstance()->GetGraphResolution();
            if(gs == SceneGraph::GraphRes_960x640)
            {
                mNeckTopY = 820.0;
                
                mTopBoundary = 1460.0;
                
            }
            else
            {
                mNeckTopY = 990.0;
                
                mTopBoundary = 1300.0;
                
            }

            mBotBoundary = 0.0;

        }
        else
        {
            assert(0);
        }
    }
    
    
    void NeckDisplayLayer::LoadColors()
    {
        mColorIdx = 3;
        
        
        
        //color #0
        mColorIdxText[0][0] = 255;
        
        mColorIdxRed[0][0] = 255;
        mColorIdxGreen[0][0] = 0;
        mColorIdxBlue[0][0] = 0;
        
        mColorIdxText[0][1] = 0;
        
        mColorIdxRed[0][1] = 255;
        mColorIdxGreen[0][1] = 128;
        mColorIdxBlue[0][1] = 0;
        
        mColorIdxText[0][2] = 0;
        
        mColorIdxRed[0][2] = 255;
        mColorIdxGreen[0][2] = 255;
        mColorIdxBlue[0][2] = 128;
        
        
        //color #1
        mColorIdxText[1][0] = 255;
        
        mColorIdxRed[1][0] = 0;
        mColorIdxGreen[1][0] = 160;
        mColorIdxBlue[1][0] = 0;
        
        mColorIdxText[1][1] = 0;
        
        mColorIdxRed[1][1] = 64;
        mColorIdxGreen[1][1] = 255;
        mColorIdxBlue[1][1] = 64;
        
        mColorIdxText[1][2] = 0;
        
        mColorIdxRed[1][2] = 128;
        mColorIdxGreen[1][2] = 255;
        mColorIdxBlue[1][2] = 128;
        
        
        //color #2
        mColorIdxText[2][0] = 255;
        
        mColorIdxRed[2][0] = 0;
        mColorIdxGreen[2][0] = 0;
        mColorIdxBlue[2][0] = 255;
        
        mColorIdxText[2][1] = 0;
        
        mColorIdxRed[2][1] = 0;
        mColorIdxGreen[2][1] = 128;
        mColorIdxBlue[2][1] = 255;
        
        mColorIdxText[2][2] = 0;
        
        mColorIdxRed[2][2] = 128;
        mColorIdxGreen[2][2] = 160;
        mColorIdxBlue[2][2] = 255;

        
        //color #3
        mColorIdxText[3][0] = 0;
        
        mColorIdxRed[3][0] = 255;
        mColorIdxGreen[3][0] = 255;
        mColorIdxBlue[3][0] = 255;
        
        mColorIdxText[3][1] = 0;
        
        mColorIdxRed[3][1] = 200;
        mColorIdxGreen[3][1] = 200;
        mColorIdxBlue[3][1] = 200;
        
        mColorIdxText[3][2] = 255;
        
        mColorIdxRed[3][2] = 64;
        mColorIdxGreen[3][2] = 64;
        mColorIdxBlue[3][2] = 64;
    }


 
}
