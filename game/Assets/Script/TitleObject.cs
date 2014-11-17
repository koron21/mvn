using UnityEngine;
using System.Collections;

public class TitleObject : MonoBehaviour {

	enum OBJECT_STATE {
		STATE_NONE,
		STATE_FALL,
		STATE_UP_DOWN,
	};

	float mFallSpeed;
	float mStartPosY;
	float mPower;
	float mSpeed;
	float mTheta;

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
		}
	}
}
