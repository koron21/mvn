using UnityEngine;
using System.Collections;

public class TitleCamera : MonoBehaviour {
	// CameraState
	enum CameraState {
		NONE = 0,
		MOVE_FORWARD,
		QUAKE,
		LOOKAT,
	};
	
	// Private Member
	float mMoveForwardSpeed;
	int mMoveTime;

	int mQuakeFrame;
	int mQuakeFrameEnd;
	float mPower;
	float mWaveLength;
	Vector3 mQuakeStartPos;

	Vector3 mLookAt;
	Vector3 mEndLookAt;
	float mLookAtSpeed;
	int mLookAtWait;

	CameraState mCameraState;
	int mTimer;

	public bool isEndMove(){
		if(mCameraState == CameraState.NONE){
			return true;
		}
		return false;
	}

	public void startMoveForward(int frame, float speed){
		if(mCameraState != CameraState.NONE){
			return;
		}
		mCameraState = CameraState.MOVE_FORWARD;
		mMoveTime = frame;
		mMoveForwardSpeed = speed;
		mTimer = 0;
	}

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

	public void startLookAt(Vector3 startLookAt, Vector3 endLookAt, float lookAtSpeed, int waitTime){
		if(mCameraState != CameraState.NONE){
			return;
		}
		mCameraState = CameraState.LOOKAT;
		mLookAt = startLookAt;
		mEndLookAt = endLookAt;
		mLookAtSpeed = lookAtSpeed;
		mLookAtWait = waitTime;
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
		case CameraState.MOVE_FORWARD:
			{
				++mTimer;
				Vector3 pos = this.transform.position;
				pos += this.transform.forward * mMoveForwardSpeed;
				this.transform.position = pos;
				if(mTimer > mMoveTime){
					mCameraState = CameraState.NONE;
				}
			}
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
		case CameraState.LOOKAT:
			{
				++mTimer;
				Vector3 pos = (mEndLookAt - mLookAt) * mLookAtSpeed + mLookAt;
				mLookAt = pos;
				this.transform.LookAt(mLookAt);
				if(mTimer > mLookAtWait){
					mCameraState = CameraState.NONE;
				}
			}
			break;
		}
	}
}
