using UnityEngine;
using System.Collections;

public class TitleCtrl : MonoBehaviour {

	public Fade mFade;

	public TitleCamera mTitleCamera; 
	public TitleObject mTakushima;
	public TitleObject mSphere;
	public TitleObject mSphereBase;
	public TitleObject mLamp;
	public TitleObject mLampKemuri;

	public GameObject mBooks;
	public GameObject mBook1;
	public GameObject mBook2;
	public GameObject mBook3;
	public GameObject mBook4;
	public ParticleSystem mBookKemuri;

	public Text mTakushimaText;
	public Text mYoshinobuText;
	public Text mAiText;

	int mTitleState;
	
	int mTimer;

	bool isNext(){
		// Input Check
		return Input.GetKeyDown(KeyCode.DownArrow);
	}

	void Init(){
		mTakushima.gameObject.transform.position = new Vector3(0.0f, 5.0f, 0.0f);
		mBook1.gameObject.SetActive(false);
		mBook2.gameObject.SetActive(false);
		mBook3.gameObject.SetActive(false);
		mBook4.gameObject.SetActive(false);
		mBookKemuri.renderer.enabled = false;
		mLampKemuri.renderer.enabled = false;
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
			mFade.setFadeColor(new Color(0.0f, 0.0f, 0.0f));
			mFade.startFadeOut(60);
			++mTitleState;
			break;
		case 1:
			if(mFade.isEndMove() == true){
				string text = "肉くせぇ。何だここは？";
				mYoshinobuText.setText(text);
				mYoshinobuText.startTextOut(3);
				++mTitleState;
				mTimer = 0;
			}
			break;
		case 2:
			if(mYoshinobuText.isOutEnd() == true){
				if(isNext() == true){
					mYoshinobuText.setText("");
					mLampKemuri.renderer.enabled = true;
					mLampKemuri.setupKemuri(180, 0.02f, 0.05f, 1.0f, new Vector3(-1.0f, 2.0f, 0.0f));
					++mTitleState;
				}
			}
			break;
		case 3:
			if(mLampKemuri.isEndMove() == true){
				mLampKemuri.renderer.enabled = false;
				mTakushima.setupFall(6.0f, 0.5f);
				++mTitleState;
			}
			break;
		case 4:
			if(mTakushima.isEndMove() == true){
				mTitleCamera.startQuake(100, 2.0f, 10.0f);
				float speed = Mathf.PI / 60.0f;
				mSphere.setupUpDown(1.0f, speed);
				mSphereBase.setupUpDown(0.3f, speed);
				mLamp.setupUpDown(0.4f, speed);
				mBooks.SetActive(false);
				mBook1.gameObject.SetActive(true);
				mBook2.gameObject.SetActive(true);
				mBook3.gameObject.SetActive(true);
				mBook4.gameObject.SetActive(true);
				++mTitleState;
			}
			break;
		case 5:
			++mTimer;
			if(mTimer == 40){
				mBookKemuri.renderer.enabled = true;
			}
			else if(mTimer == 120){
				mBookKemuri.renderer.enabled = false;
			}
			if(mTitleCamera.isEndMove() == true){
				mTimer = 0;
				mBookKemuri.renderer.enabled = false;
				++mTitleState;
				string takushimaText = "じゃんじゃじゃーーーんっ\n多久島占いの館へようこそ！！";
				mTakushimaText.setText(takushimaText);
				mTakushimaText.startTextOut(3);
			}
			break;
		case 6:
			if(mTakushimaText.isOutEnd() == true){
				if(isNext() == true){
					string takushimaText = "今日は特別に、僕の占いを披露してあげるよ！";
					mTakushimaText.setText(takushimaText);
					mTakushimaText.startTextOut(3);
					++mTitleState;
				}
			}
			break;
		case 7:
			if(mTakushimaText.isOutEnd() == true){
				// Input Wait
				string takushimaText = "君たちは新婚さんかな？\nそれじゃあ、君たちの将来を占ってあげよう☆";
				mTakushimaText.setText(takushimaText);
				mTakushimaText.startTextOut(3);
				++mTitleState;
			}
			break;
		case 8:
			if(mTakushimaText.isOutEnd() == true){
				mTakushimaText.setText("");
				++mTitleState;
			}
			break;
		case 9:
			++mTimer;
			if(mTimer == 120){
				Vector3 end = mSphere.gameObject.transform.position;
				mTitleCamera.startLookAt(end, 0.05f, 120);
				++mTitleState;
				mTimer = 0;
			}
			break;
		case 10:
			if(mTitleCamera.isEndMove() == true){
				mTitleCamera.startMoveForward(120, 0.05f);
				++mTitleState;
				mTimer = 0;
			}
			break;
		case 11:
			++mTimer;
			if(mTimer == 60){
				mFade.setFadeColor(new Color(1.0f, 1.0f, 1.0f));
				mFade.startFadeIn(60);
				++mTitleState;
				mTimer = 0;
			}
			break;
		}
	}
}
