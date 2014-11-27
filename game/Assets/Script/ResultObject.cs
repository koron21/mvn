using UnityEngine;
using System.Collections;

public class ResultObject : MonoBehaviour {

	enum OBJECT_STATE{
		OBS_NONE = 0,
		OBS_NONE_XZ,
		OBS_SETUP,
		OBS_ERASE,
		OBS_SETUP_XZ,
		OBS_PARABOLA,
	};

	Vector3 mInitScale;
	OBJECT_STATE mObjectState;

	// Setup
	float mSetupInitScale;
	float mSetupEndScale;
	int mSetupFrame;
	int mSetupEndFrame;

	// Erase
	float mEraseInitScale;
	int mEraseFrame;
	int mEraseEndFrame;

	// Parabola
	int mFrame;
	int mParabolaFrame;
	Vector3 mParabolaEndPos;
	Vector3 mParabolaMoveVecXZ;
	float mParabolaHeight;
	float mTheta;

	public bool isEndMove(){
		if(mObjectState == OBJECT_STATE.OBS_NONE){
			return true;
		}
		return false;
	}

	public void setInitScale(float scale){
		mInitScale = new Vector3(scale, scale, scale);
	}

	public void setSetup(float initScale, float endScale, int frame){
		mObjectState = OBJECT_STATE.OBS_SETUP;
		mSetupInitScale = initScale;
		mSetupEndScale = endScale;
		mSetupFrame = 0;
		mSetupEndFrame = frame;
	}

	public void setErase(float initScale, int frame){
		mObjectState = OBJECT_STATE.OBS_ERASE;
		mEraseInitScale = initScale;
		mEraseFrame = 0;
		mEraseEndFrame = frame;
	}

	public void setSetupXZ(float initScale, float endScale, int frame){
		mObjectState = OBJECT_STATE.OBS_SETUP_XZ;
		mSetupInitScale = initScale;
		mSetupEndScale = endScale;
		mSetupFrame = 0;
		mSetupEndFrame = frame;
	}

	public void setParabola(int frame, float height, Vector3 endPos){
		if(mObjectState != OBJECT_STATE.OBS_NONE){
			return;
		}
		mObjectState = OBJECT_STATE.OBS_PARABOLA;
		mParabolaFrame = frame;
		mParabolaEndPos = endPos;
		mTheta = 0.0f;
		mParabolaHeight = height;
		mFrame = 0;
		Vector3 vec = endPos - this.transform.position;
		mParabolaMoveVecXZ = new Vector3(vec.x, 0.0f, vec.z);
		mParabolaMoveVecXZ /= frame;
	}

	// Use this for initialization
	void Start () {
		mObjectState = OBJECT_STATE.OBS_NONE;
	}
	
	// Update is called once per frame
	void Update () {
		switch(mObjectState){
		case OBJECT_STATE.OBS_NONE:
			// UpdateInitScale
			this.transform.localScale = mInitScale;
			break;
		case OBJECT_STATE.OBS_NONE_XZ:
			// do nothing
			break;
		case OBJECT_STATE.OBS_SETUP:
			// setupState
			{
				int scaleUp = 3;
				int scaleChange = 4;
				int val = (mSetupEndFrame * scaleUp) / scaleChange;
				float valf = (float)scaleChange / (float)scaleUp;
				float maxScale = (float)mSetupEndScale * valf;
				float scale;
				if(mSetupFrame < val){
					// scaleUp
					scale = (maxScale - mSetupInitScale) / (float)val * (float)mSetupFrame + mSetupInitScale;
				}
				else{
					// scaleDown
					scale = ((mSetupEndScale - maxScale) / ((float)(mSetupEndFrame - val))) * (float)(mSetupFrame - val) + maxScale;
				}
				this.transform.localScale = new Vector3(scale, scale, scale);
				++mSetupFrame;
				if(mSetupFrame > mSetupEndFrame){
					mObjectState = OBJECT_STATE.OBS_NONE;
					setInitScale(mSetupEndScale);
				}
			}
			break;
		case OBJECT_STATE.OBS_ERASE:
			// setupState
			{
				float scale = (mEraseInitScale / mEraseEndFrame) * mEraseFrame * -1.0f + mEraseInitScale;
				this.transform.localScale = new Vector3(scale, scale, scale);
				++mEraseFrame;
				if(mEraseFrame > mEraseEndFrame){
					mObjectState = OBJECT_STATE.OBS_NONE;
					setInitScale(0.0f);
				}
			}
			break;
		case OBJECT_STATE.OBS_SETUP_XZ:
			{
				float scale = (mSetupEndScale - mSetupInitScale) / mSetupEndFrame * mSetupFrame + mSetupInitScale;
				this.transform.localScale = new Vector3(scale, scale, 0.0001f);
				++mSetupFrame;
				if(mSetupFrame > mSetupEndFrame){
					mObjectState = OBJECT_STATE.OBS_NONE_XZ;
					this.transform.localScale = new Vector3(mSetupEndScale, mSetupEndScale, 0.0001f);
				}
			}
			break;
		case OBJECT_STATE.OBS_PARABOLA:
			{
				++mFrame;
				Vector3 nowPos = this.transform.position;
				nowPos += mParabolaMoveVecXZ;
				mTheta = Mathf.PI * mFrame / mParabolaFrame;
				nowPos.y = Mathf.Sin(mTheta) * mParabolaHeight + mParabolaEndPos.y;
				if(mFrame > mParabolaFrame){
					nowPos = mParabolaEndPos;
					mObjectState = OBJECT_STATE.OBS_NONE;
				}
				this.transform.position = nowPos;
			}
			break;
		}
	}
}
