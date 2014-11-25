using UnityEngine;
using System.Collections;

public class TitleObject : MonoBehaviour {

	enum OBJECT_STATE {
		STATE_NONE,
		STATE_FALL,
		STATE_UP_DOWN,
		STATE_KEMURI,
		STATE_VIBRATE,
	};

	float mFallSpeed;
	float mStartPosY;
	float mPower;
	float mSpeed;
	float mTheta;

	int mEndFrame;
	float mWaveLength;
	float mWaveSpeed;
	Vector3 mStartPos;
	Vector3 mKemuriVec;


	int mTimer;

	OBJECT_STATE mObjectState;

	public bool isEndMove(){
		if(mObjectState == OBJECT_STATE.STATE_NONE){
			return true;
		}
		return false;
	}

	public void setupFall(float fallPosY, float fallSpeed){
		if(mObjectState != OBJECT_STATE.STATE_NONE){
			return;
		}
		mFallSpeed = fallSpeed;
		Vector3 pos = this.gameObject.transform.position;
		pos.y = fallPosY;
		this.gameObject.transform.position = pos;
		mObjectState = OBJECT_STATE.STATE_FALL;
	}

	public void setupUpDown(float power, float speed){
		if(mObjectState != OBJECT_STATE.STATE_NONE){
			return;
		}
		mStartPosY = this.gameObject.transform.position.y;
		mPower = power;
		mSpeed = speed;
		mTheta = 0.0f;
		mObjectState = OBJECT_STATE.STATE_UP_DOWN;
	}

	public void setupKemuri(int frame, float speed, float waveSpeed, float wave, Vector3 vec){
		if(mObjectState != OBJECT_STATE.STATE_NONE){
			return;
		}
		mTheta = 0;
		mTimer = 0;
		mEndFrame = frame;
		mSpeed = speed;
		mWaveSpeed = waveSpeed;
		mWaveLength = wave;
		mStartPos = this.transform.position;
		mKemuriVec = vec.normalized;
		mObjectState = OBJECT_STATE.STATE_KEMURI;
	}

	public void setupVibrate(int frame){
		if(mObjectState != OBJECT_STATE.STATE_NONE){
			return;
		}
		mTimer = 0;
		mEndFrame = frame;
		mStartPos = this.transform.position;
		mObjectState = OBJECT_STATE.STATE_VIBRATE;
	}

	// Use this for initialization
	void Start () {
		mObjectState = OBJECT_STATE.STATE_NONE;
	}
	
	// Update is called once per frame
	void Update () {
		switch(mObjectState){
		case OBJECT_STATE.STATE_NONE:
			break;
		case OBJECT_STATE.STATE_FALL:
			{
				Vector3 pos = this.gameObject.transform.position;
				pos.y -= mFallSpeed;
				if(pos.y < 0.0f){
					pos.y = 0.0f;
					mObjectState = OBJECT_STATE.STATE_NONE;
				}
				this.gameObject.transform.position = pos;
			}
			break;
		case OBJECT_STATE.STATE_UP_DOWN:
			{
				mTheta += mSpeed;
				Vector3 pos = this.gameObject.transform.position;
				float posY = mStartPosY + Mathf.Sin(mTheta) * mPower;
				if(mTheta > Mathf.PI){
					posY = mStartPosY;
					mObjectState = OBJECT_STATE.STATE_NONE;
				}
				this.gameObject.transform.position = new Vector3(pos.x, posY, pos.z);
			}
			break;
		case OBJECT_STATE.STATE_KEMURI:
			{
				++mTimer;
				mTheta += mWaveSpeed;
				Vector3 nowPos = mStartPos + mTheta * mKemuriVec;
//				Vector3 wave = Vector3.Cross(mKemuriVec, new Vector3(0.0f, 0.0f, 1.0f));
//				nowPos = nowPos + wave * Mathf.Sin(mTheta) * mWaveLength;
				nowPos = nowPos + Mathf.Sin(mTheta) * mWaveLength * new Vector3(1.0f, 0.0f, 0.0f);
				this.gameObject.transform.position = nowPos;
				if(mTimer > mEndFrame){
					mObjectState = OBJECT_STATE.STATE_NONE;
				}
			}
			break;
		case OBJECT_STATE.STATE_VIBRATE:
			{
				++mTimer;
				const float len = 0.03f;
				float theta = Random.Range(0, 360) * (Mathf.PI * 2.0f / 360.0f);
				float PosX = Mathf.Cos(theta) * len;
				float PosZ = Mathf.Sin(theta) * len;
				Vector3 pos = mStartPos;
				this.transform.position = new Vector3(pos.x + PosX, pos.y, pos.z + PosZ);
				if(mTimer > mEndFrame){
					mObjectState = OBJECT_STATE.STATE_NONE;
					this.transform.position = mStartPos;
				}
			}
			break;
		}
	}
}
