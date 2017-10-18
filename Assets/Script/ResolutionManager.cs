using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//iOS resolutions
//iPhone 4 		: 640 x 960
//iPhone 5 		: 640 x 1136
//iPhone 8 		: 750 x 1334
//iPhone 8+ 	: 1242 x 2208
//iPhone X 		: 1125 x 2436
//iPhone 4/Mini : 1536 x 2048
//iPhone pro10 	: 1668 x 2224
//iPhone pro12 	: 2048 x 2732

public class ResolutionManager : MonoBehaviour 
{
	public ResScaler[] resScaler;

	private int resIndex;
	private float targetScale;

	private bool scaledMode = false;
	private float scaledAmount = 0.75f;

	public static ResolutionManager Instance;

	void Awake () 
	{
		Instance = this;

		Resolution screenRes = Screen.currentResolution;

		float w = screenRes.width;
		float h = screenRes.height;

		#if UNITY_EDITOR
		//debug
		w = 1242;
		h = 2208;
		#endif


		float targetaspect = 1536.0f / 2048.0f;
		float windowaspect = w / h;

		float scaleheight = windowaspect / targetaspect;
		targetScale = scaleheight; 

		SetResIndex(w, h);
	}

	void Start () 
	{

	}

	public void SetScaledMode (bool state) 
	{
		scaledMode = state;
	}

	
	public void SetResIndex (float sw, float sh) 
	{
		resIndex = 0;
		foreach(ResScaler rs in resScaler) {
	
			if(rs.width == sw && rs.height == sh) {
				break;
			} else {
				resIndex++;
			}
		}
		
	}

	public float GetTargetScale () 
	{
		float rs = targetScale;
		if (scaledMode == true) {
			rs = rs * scaledAmount;
		}
		return rs;
	}

	public float GetTapPadY () 
	{
		float rs = resScaler[resIndex].TapPadY;
		if (scaledMode == true) {
			rs = rs * scaledAmount;
		}

		return resScaler[resIndex].TapPadY;
	}
		
	public float GetCamLimY () 
	{
		float rs = resScaler[resIndex].ScrollCamLimY;
		if (scaledMode == true) {
			rs = rs * scaledAmount;
		}

		return rs;
	}
}
