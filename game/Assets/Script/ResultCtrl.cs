using UnityEngine;
using System.Collections;

public class ResultCtrl : MonoBehaviour {

	public Fade mFade;

	// ResultObjects
	public ResultCamera mResultCamera;
	public ResultObject mHouse1;
	public ResultObject mHouse2;
	public GameObject mGO_House2;
	public ResultObject mChair1;
	public GameObject mGO_Chair1;
	public ResultObject mChair2;
	public GameObject mGO_Chair2;

	public ResultObject mTree1_1;
	public ResultObject mTree1_2;
	public ResultObject mTree2_1;
	public ResultObject mTree2_2;
	public ResultObject mTree3_1;

	public ResultObject mWeed1_1;
	public ResultObject mWeed1_2;

	public ResultObject mWeed2_1;
	public ResultObject mWeed2_2;
	
	public ResultObject mFlower2_1;
	public GameObject mGO_Flower2_1;
	public ResultObject mFlower2_2;
	public GameObject mGO_Flower2_2;
	public ResultObject mFlower2_3;
	public GameObject mGO_Flower2_3;
	
	public ResultObject mApple1;
	public ResultObject mApple2;

	public ResultObject mGround3;

	public GaugeCtrl mLoveEnergy;
	public GaugeCtrl mMoneyGauge;

	public ResultObject[] mHeart;
	public ResultObject[] mBaby;
	bool[] mFire;
	float[] mBabyScale;
	int mBabyIndex;
	int mFireIndex;

	public ResultObject mGrass1;
	public ResultObject mGrass2;
	public ResultObject mGrass3;
	public ResultObject mGrass4;
	public ResultObject mGrass5;
	public ResultObject mGrass6;

	public GameObject mResultText;

	int mResultState;

	int mTimer;

	static int WAIT_FRAME = 20;

	void Init () {
		// Init Object
		mHouse1.setInitScale(1.3f);
		mChair1.setInitScale(0.02f);
		mTree1_1.setInitScale(3.0f);
		mTree1_2.setInitScale(3.0f);
		mWeed1_1.setInitScale(2.0f);
		mWeed1_2.setInitScale(2.0f);

		mHouse2.setInitScale(0.0f);
		mGO_House2.SetActive(false);
		mChair2.setInitScale(0.001f);
		mGO_Chair2.SetActive(false);
		mTree2_1.renderer.enabled = false;
		mTree2_2.renderer.enabled = false;
		mTree3_1.renderer.enabled = false;
		mWeed2_1.renderer.enabled = false;
		mWeed2_2.renderer.enabled = false;

		mApple1.renderer.enabled = false;
		mApple2.renderer.enabled = false;

		mResultText.SetActive(false);

		mFlower2_1.setInitScale(0.001f);
		mFlower2_2.setInitScale(0.001f);
		mFlower2_3.setInitScale(0.001f);
		// How to Invisible
		//		mHouse1.renderer.enabled = true;

		// GaugeInit
		mLoveEnergy.addValue(1000.0f);
		mMoneyGauge.addValue(1000.0f);

		mFire = new bool[mBaby.Length];
		mBabyScale = new float[mBaby.Length];
		for(int i = 0; i < mBaby.Length; ++i){
//			mBaby[i].renderer.enabled = false;
//			mHeart[i].renderer.enabled = false;
			mHeart[i].setInitScale(0.0f);
			mBaby[i].setInitScale(0.0f);
			mFire[i] = false;
		}
		mBabyScale[0] = 1.8f;
		mBabyScale[1] = 1.8f;
		mBabyScale[2] = 1.5f;
		mBabyScale[3] = 1.6f;
		mBabyScale[4] = 1.5f;
		mBabyScale[5] = 1.8f;
		mBabyScale[6] = 1.6f;
		mBabyScale[7] = 1.6f;
		mBabyScale[8] = 1.7f;
		mBabyScale[9] = 1.8f;
		mBabyScale[10] = 1.8f;

		mGrass1.setInitScale(0.0f);
		mGrass2.setInitScale(0.0f);
		mGrass3.setInitScale(0.0f);
		mGrass4.setInitScale(0.0f);
		mGrass5.setInitScale(0.0f);
		mGrass6.setInitScale(0.0f);
	}

	static public bool isNext() {
		return Input.GetKeyDown(KeyCode.DownArrow);
	}

	// Use this for initialization
	void Start () {
		// State Init
		mResultState = 0;
		mTimer = 0;

		Init();
	}
	
	// Update is called once per frame
	void Update () {
		switch(mResultState){
		case 0:
			mFade.setFadeColor(new Color(1.0f, 1.0f, 1.0f));
			mFade.startFadeOut(120);
			SoundManager.Instance.requestStream("bgm_result");
			++mResultState;
			break;
		case 1:
			if(mFade.isEndMove() == true){
				++mResultState;
			}
			break;
		case 2:
			if(isNext() == true){
				++mResultState;
				// gauge Down
				mMoneyGauge.startDecrease(2.0f, mHouse1.transform.position);
				mTimer = 0;
			}
			break;
		case 3:
			++mTimer;
			if(mTimer > 50){
				++mResultState;
				mTimer = 0;

				mGO_House2.SetActive(true);
				// want to "Delay"
				mGO_Flower2_1.SetActive(false);
				mGO_Flower2_2.SetActive(false);
				mGO_Flower2_3.SetActive(false);
			}
			break;
		case 4:
			// input wait?
			mHouse2.setSetup(0.001f, 0.04f, 60);
			mHouse1.setErase(1.3f, 60);
			SoundManager.Instance.requestSe("se_build_a");
			++mResultState;
			break;
		case 5:
			if(mHouse2.isEndMove() == true){
				SoundManager.Instance.requestSe("se_build_b");
				mResultCamera.startQuake(80, 2.0f, 10.0f);
				mGO_Chair2.SetActive(true);
				++mResultState;
			}
			break;
		case 6:
			mChair2.setSetup(0.001f, 0.02f, WAIT_FRAME);
			mTree2_1.setSetup(0.01f, 3.0f, WAIT_FRAME);
			mTree2_2.setSetup(0.01f, 3.0f, WAIT_FRAME);
			mWeed2_1.setSetup(0.01f, 1.0f, WAIT_FRAME);
			mWeed2_2.setSetup(0.01f, 1.0f, WAIT_FRAME);
			mTree2_1.renderer.enabled = true;
			mTree2_2.renderer.enabled = true;
			mWeed2_1.renderer.enabled = true;
			mWeed2_2.renderer.enabled = true;

			mChair1.setErase(0.02f, WAIT_FRAME);
			mTree1_1.setErase(3.0f, WAIT_FRAME);
			mTree1_2.setErase(3.0f, WAIT_FRAME);
			mWeed1_1.setErase(2.0f, WAIT_FRAME);
			mWeed1_2.setErase(2.0f, WAIT_FRAME);
			++mResultState;
			break;
		case 7:
			if(mChair1.isEndMove() == true){
				// invisible
				mGO_Chair1.SetActive(false);
				mTree1_1.renderer.enabled = false;
				mTree1_2.renderer.enabled = false;
				mWeed1_1.renderer.enabled = false;
				mWeed1_2.renderer.enabled = false;

				mTimer = 0;
				++mResultState;
			}
			break;
		case 8:
			++mTimer;
			if(mTimer > 60){
				++mResultState;
				mGround3.setSetupXZ(1.0f, 60.0f, 120);
				mTimer = 0;
			}
			break;
		case 9:
			++mTimer;
			if(mTimer == 10){
				mGO_Flower2_1.gameObject.SetActive(true);
				mGrass1.setSetup(0.01f, 0.5f, WAIT_FRAME);
				SoundManager.Instance.requestSe("se_build_d");
			}
			if(mTimer == 11){
				mFlower2_1.setSetup(0.01f, 2.3f, WAIT_FRAME);
			}
			if(mTimer == 20){
				mGO_Flower2_2.gameObject.SetActive(true);
				mGrass2.setSetup(0.01f, 0.5f, WAIT_FRAME);
				mTree3_1.renderer.enabled = true;
				mTree3_1.setSetup(0.01f, 3.0f, WAIT_FRAME);
				SoundManager.Instance.requestSe("se_build_d");
			}
			if(mTimer == 21){
				mFlower2_2.setSetup(0.01f, 2.3f, WAIT_FRAME);
			}
			if(mTimer == 30){
				mGO_Flower2_3.gameObject.SetActive(true);
				mGrass3.setSetup(0.01f, 0.5f, WAIT_FRAME);
				SoundManager.Instance.requestSe("se_build_d");
			}
			if(mTimer == 31){
				mFlower2_3.setSetup(0.01f, 2.3f, WAIT_FRAME);
			}
			if(mTimer == 40){
				mApple1.renderer.enabled = true;
				mApple1.setSetup(0.01f, 1.5f, WAIT_FRAME);
				mGrass4.setSetup(0.01f, 0.5f, WAIT_FRAME);
				SoundManager.Instance.requestSe("se_build_d");
			}
			if(mTimer == 45){
				mApple2.renderer.enabled = true;
				mApple2.setSetup(0.01f, 1.5f, WAIT_FRAME);
			}
			if(mTimer == 50){
				mGrass5.setSetup(0.01f, 0.5f, WAIT_FRAME);
				SoundManager.Instance.requestSe("se_build_d");
			}
			if(mTimer == 60){
				mGrass6.setSetup(0.01f, 0.5f, WAIT_FRAME);
				SoundManager.Instance.requestSe("se_build_d");
				mTimer = 0;
				++mResultState;
			}
			break;
		case 10:
			if(isNext() == true){
				++mResultState;
			}
			break;
		case 11:
			// gauge Down
			mLoveEnergy.startDecrease(4.0f, mLoveEnergy.transform.position);
			mBabyIndex = 0;
			mFireIndex = 0;
			mTimer = 0;
			++mResultState;
			break;
		case 12:
			++mTimer;
			if(mFire[mFireIndex] == true){
				if(mHeart[mFireIndex].isEndMove() == true){
					mHeart[mFireIndex].setErase(1.0f, WAIT_FRAME);
					mBaby[mFireIndex].setInitScale(mBabyScale[mFireIndex]);
					mBaby[mFireIndex].setSetup(0.02f, mBabyScale[mFireIndex], WAIT_FRAME);
					SoundManager.Instance.requestSe("se_baby");
					++mFireIndex;
				}
			}
			if(mBabyIndex < mBaby.Length){
				if(mTimer % 20 == 17){
					mHeart[mBabyIndex].setInitScale(1.0f);
				}
				else if(mTimer % 20 == 19){
					mHeart[mBabyIndex].setInitScale(1.0f);
					mHeart[mBabyIndex].setParabola(WAIT_FRAME, 2.0f, mBaby[mBabyIndex].transform.position);
					mFire[mBabyIndex] = true;
					++mBabyIndex;
				}
			}

			if(mFireIndex >= mBaby.Length){
				++mResultState;
				mTimer = 0;
			}
			break;
		case 13:
			++mTimer;
			if(mTimer % 210 == 0){
				mBaby[mBaby.Length - 1].setParabola(60, 0.7f, mBaby[mBaby.Length - 1].transform.position);
			}
			if(isNext() == true){
				++mResultState;
			}
			break;
		case 14:
			mResultText.SetActive(true);
			++mResultState;
			break;
		case 15:
			if(mResultText.GetComponentInChildren<ResultText>().IsFinish == true){
				mResultCamera.startBack(180, 0.01f);
				mFade.setFadeColor(new Color(0.0f, 0.0f, 0.0f));
				mFade.startFadeIn(120);
				SoundManager.Instance.requestSe("se_return");
				++mResultState;
			}
			break;
		case 16:
			if(mFade.isEndMove() == true){
				Application.LoadLevel(3);
			}
			break;
		}
	}
}
