using UnityEngine;
using System.Collections;

public class EndingCtrl : MonoBehaviour {

	public Fade mFade;

	public TitleCamera mTitleCamera;
	
	int mEndingState;

	// Use this for initialization
	void Start () {
		mEndingState = 0;
		mFade.setFadeColor(new Color(0.0f, 0.0f, 0.0f));
	}
	
	// Update is called once per frame
	void Update () {
		switch(mEndingState){
		case 0:
			mFade.startFadeOut(80);
			++mEndingState;
			mTitleCamera.startMoveBack(200, 0.05f, 0.015f, new Vector3(0.0f, 3.0f, 0.0f));
			break;
		case 1:
			break;
		}
	}
}
