using UnityEngine;
using System.Collections;

public class EndingCtrl : MonoBehaviour {

	public Fade mFade;
	public ResultObject mBun;
	public ResultObject mAi;
	public TitleCamera mTitleCamera;
	
	int mEndingState;
	int mTimer;

	// Use this for initialization
	void Start () {
		mEndingState = 0;
		mTimer = 0;
		mFade.setFadeColor(new Color(0.0f, 0.0f, 0.0f));
		mBun.setInitScale(3.0f);
		mAi.setInitScale(3.0f);
	}
	
	// Update is called once per frame
	void Update () {
		switch(mEndingState){
		case 0:
			mFade.startFadeOut(80);
			++mEndingState;
			mTitleCamera.startMoveBack(200, 0.05f, 0.015f, new Vector3(0.0f, 3.0f, 0.0f));
			SoundManager.Instance.requestStream2("se_kansei");
			break;
		case 1:
			++mTimer;
			if(mTimer % 120 == 60){
				mBun.setParabola(30, 0.7f, mBun.transform.position);
			}
			if(mTimer % 120 == 90){
				mAi.setParabola(30, 0.5f, mAi.transform.position);
			}


			break;
		}
	}
}
