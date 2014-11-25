using UnityEngine;
using System.Collections;

public class ResultCamera : MonoBehaviour {
	// CameraState
	enum CameraState {
		NONE = 0,
		QUAKE,
		BACK,
	};

	// Private Member
	CameraState mCameraState;
	int mQuakeFrame;
	int mQuakeFrameEnd;
	float mPower;
	float mWaveLength;
	Vector3 mQuakeStartPos;

	float mSpeed;
	int mEndFrame;

	int mTimer;

	public void startQuake(int frame, float power, float waveLength){
		if(mCameraState != CameraState.NONE){
			return;
		}
		// Setup Param
		mCameraState = CameraState.QUAKE;
		mQuakeFrame = 0;
		mQuakeFrameEnd = frame;
		mPower = power;
		mWaveLength = waveLength;
		mQuakeStartPos = this.transform.position;
	}

	public void startBack(int frame, float speed){
		if(mCameraState != CameraState.NONE){
			return;
		}
		mCameraState = CameraState.BACK;
		mEndFrame = frame;
		mSpeed = speed;
		mTimer = 0;
	}

	// Use this for initialization
	void Start () {
		mCameraState = CameraState.NONE;
	}
	
	// Update is called once per frame
	void Update () {
		switch(mCameraState){
		case CameraState.NONE:
			break;
		case CameraState.QUAKE:
			{
				++mQuakeFrame;
				if(mQuakeFrame >= mQuakeFrameEnd){
					// Scene change
					mCameraState = CameraState.NONE;
					this.transform.position = mQuakeStartPos;
					return;
				}
				float amplitude;
				float theta = mQuakeFrame * (Mathf.PI * 2.0f) / mWaveLength;
				amplitude = Mathf.Sin(theta) * mPower;
				Vector3 pos = mQuakeStartPos + new Vector3(0.0f, amplitude, 0.0f);
				this.transform.position = pos;
				mPower *= 0.9f;
			}
			break;
		case CameraState.BACK:
			{
				++mTimer;
				if(mTimer > mEndFrame){
					mCameraState = CameraState.NONE;
					return;
				}
				Vector3 backVec = this.transform.forward * -1.0f * mSpeed;
				this.transform.position = this.transform.position + backVec;
			}
			break;
		}
	}
}
