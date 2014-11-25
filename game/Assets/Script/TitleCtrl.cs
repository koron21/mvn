using UnityEngine;
using System.Collections;

public class TitleCtrl : MonoBehaviour {

	public Fade mFade;

	public GameObject mTitleUI;
	public TitleCamera mTitleCamera;
	public GameObject mSphere;
	public TitleObject mSphereBase;
	public TitleObject mLamp;
	public TitleObject mLampKemuri;

	public Text mExplanation;

	int mTitleState;
	
	int mTimer;

	bool isNext(){
		// Input Check
		return Input.GetKeyDown(KeyCode.DownArrow);
	}

	void Init(){
		mLampKemuri.renderer.enabled = false;
		mExplanation.setText("");
	}

	// Use this for initialization
	void Start () {
		mTimer = 0;
		mTitleState = 0;

		Init();
	}

	// Update is called once per frame
	void Update () {
		switch(mTitleState){
		case 0:
			mTitleUI.renderer.enabled = true;
			++mTitleState;
			break;
		case 1:
			if(isNext() == true){
				++mTitleState;
				mFade.setFadeColor(new Color(0.0f, 0.0f, 0.0f));
				mFade.startFadeIn(120);
				SoundManager.Instance.requestStream("title_bgm");
			}
			break;
		case 2:
			if(mFade.isEndMove() == true){
				++mTitleState;
				string txt = "新婚旅行でハワイに来た良宣、愛夫妻。\nショッピングを楽しんでいたら、\n怪しいお店に迷い込んでしまった様子。";
				mExplanation.setText(txt);
				mExplanation.startTextOut(4);
			}
			break;
		case 3:
			if(mExplanation.isOutEnd() == true){
				if(isNext() == true){
					++mTitleState;
					string txt = "はてさて何が起こるやら…。";
					mExplanation.setText(txt);
					mExplanation.startTextOut(4);
				}
			}
			break;
		case 4:
			if(mExplanation.isOutEnd() == true){
				if(isNext() == true){
					++mTitleState;
					mExplanation.setText("");
					mFade.startFadeOut(120);
				}
			}
			break;
		case 5:
			if(mFade.isEndMove() == true){
				if(isNext() == true){
					++mTitleState;
					mLamp.setupVibrate(150);
					SoundManager.Instance.requestSe("oyakata_01");
				}
			}
			break;
		case 6:
			if(mLamp.isEndMove() == true){
				++mTitleState;
				mLampKemuri.renderer.enabled = true;
				mLampKemuri.setupKemuri(180, 0.02f, 0.05f, 1.0f, new Vector3(-1.0f, 2.0f, 0.0f));
			}
			break;
		case 7:
			if(mLampKemuri.isEndMove() == true){
				mLampKemuri.renderer.enabled = false;
				++mTitleState;
			}
			break;
		case 8:
			if(isNext() == true){
				++mTitleState;
				SoundManager.Instance.stopStream();
				SoundManager.Instance.requestSe("oyakata_02");
			}
			break;
		case 9:
			if(isNext() == true){
				Vector3 end = mSphere.gameObject.transform.position;
				mTitleCamera.startLookAt(end, 0.05f, 120);
				++mTitleState;
				mTimer = 0;
			}
			break;
		case 10:
			if(mTitleCamera.isEndMove() == true){
				mTitleCamera.startMoveForward(120, 0.04f);
				++mTitleState;
				mTimer = 0;
			}
			break;
		case 11:
			++mTimer;
			if(mTimer == 40){
				mFade.setFadeColor(new Color(1.0f, 1.0f, 1.0f));
				mFade.startFadeIn(60);
				++mTitleState;
				mTimer = 0;
			}
			break;
		case 12:
			if(mFade.isEndMove() == true){
				Application.LoadLevel(1);
			}
			break;
		}
	}
}
