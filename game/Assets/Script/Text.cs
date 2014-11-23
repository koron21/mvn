using UnityEngine;
using System.Collections;

public class Text : MonoBehaviour {

	enum TextState{
		NONE,
		OUT,
	};

	string mText;
	int mOutLength;
	int mTimer;
	int mFrame;
	TextState mTextState;

	public bool isOutEnd()
	{
		return (mTextState == TextState.NONE);
	}

	public void setText(string outText){
		if(mTextState != TextState.NONE){
			return;
		}
		// Init
		this.guiText.text = "";
		mOutLength = 0;
		mText = outText;
	}

	public void startTextOut(int frame){
		if(mTextState != TextState.NONE){
			return;
		}
		mTextState = TextState.OUT;
		this.guiText.text = "";
		mTimer = 0;
		mFrame = frame;
		mOutLength = 0;
	}

	// Use this for initialization
	void Start () {
		mText = this.guiText.text;
		this.guiText.text = "";
		mOutLength = 0;
		mTimer = 0;
		mFrame = 5;
		mTextState = TextState.NONE;
	}
	
	// Update is called once per frame
	void Update () {
		switch(mTextState){
		case TextState.NONE:
			break;
		case TextState.OUT:
			++mTimer;
			if(mTimer >= mFrame){
				++mOutLength;
				mTimer = 0;
				if(mText.Length >= mOutLength){
					this.guiText.text = mText.Substring(0, mOutLength);
				}
				if(mOutLength >= mText.Length){
					mTextState = TextState.NONE;
				}
			}
			break;
		}
	}
}
