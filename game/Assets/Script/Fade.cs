using UnityEngine;
using System.Collections;

public class Fade : MonoBehaviour {

	enum FadeState{
		NONE,
		FADE_IN,
		FADE_OUT,
	}

	FadeState mFadeState;
	int mTimer;
	int mLimit;
	Color mFadeColor;

	public bool isEndMove(){
		if(mFadeState == FadeState.NONE){
			return true;
		}
		return false;
	}

	public void setFadeColor(Color col){
		mFadeColor = col;
	}

	public void startFadeIn(int fadeTime){
		if(mFadeState != FadeState.NONE){
			return;
		}
		mTimer = 0;
		mLimit = fadeTime;
		mFadeState = FadeState.FADE_IN;
		this.guiTexture.color = new Color(mFadeColor.r, mFadeColor.g, mFadeColor.b, 0.0f);
	}

	public void startFadeOut(int fadeTime){
		if(mFadeState != FadeState.NONE){
			return;
		}
		mTimer = 0;
		mLimit = fadeTime;
		mFadeState = FadeState.FADE_OUT;
		this.guiTexture.color = new Color(mFadeColor.r, mFadeColor.g, mFadeColor.b, 1.0f);
	}


	// Use this for initialization
	void Start () {
		mFadeState = FadeState.NONE;
		mFadeColor = new Color(0.0f, 0.0f, 0.0f);
		this.guiTexture.color = new Color(mFadeColor.r, mFadeColor.g, mFadeColor.b, 0.0f);

	}
	
	// Update is called once per frame
	void Update () {
		float alpha;
		switch(mFadeState){
		case FadeState.NONE:
			break;
		case FadeState.FADE_IN:
			++mTimer;
			alpha = (float)mTimer * (1.0f / (float)mLimit);
			if(mTimer > mLimit){
				mFadeState = FadeState.NONE;
				alpha = 1.0f;
			}
			this.guiTexture.color = new Color(mFadeColor.r, mFadeColor.g, mFadeColor.b, alpha);
			break;
		case FadeState.FADE_OUT:
			++mTimer;
			alpha = 1.0f - (float)mTimer * (1.0f / (float)mLimit);
			if(mTimer > mLimit){
				mFadeState = FadeState.NONE;
				alpha = 1.0f;
			}
			this.guiTexture.color = new Color(mFadeColor.r, mFadeColor.g, mFadeColor.b, alpha);
			break;
		}
	}
}
