using UnityEngine;
using System.Collections;

public class ResultCamera : MonoBehaviour {
	// CameraState
	enum CameraState {
		NONE = 0,
		QUAKE,
	};

	// Private Member
	CameraState mCameraState;
	int mQuakeFrame;
	int mQuakeFrameEnd;
	float mPower;
	float mWaveLength;
	Vector3 mQuakeStartPos;

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
		}
	}
}
