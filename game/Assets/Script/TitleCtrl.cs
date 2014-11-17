using UnityEngine;
using System.Collections;

public class TitleCtrl : MonoBehaviour {

	public Fade mFade;

	public TitleCamera mTitleCamera; 
	public TitleObject mTakushima;
	public TitleObject mSphere;

	public GameObject mBooks;
	public GameObject mBook1;
	public GameObject mBook2;
	public GameObject mBook3;
	public GameObject mBook4;
	public ParticleSystem mBookKemuri;

	int mTitleState;
	
	int mTimer;

	void Init(){
		mFade.setFadeColor(new Color(1.0f, 1.0f, 1.0f));

		mTakushima.gameObject.transform.position = new Vector3(0.0f, 5.0f, 0.0f);
		mBook1.gameObject.SetActive(false);
		mBook2.gameObject.SetActive(false);
		mBook3.gameObject.SetActive(false);
		mBook4.gameObject.SetActive(false);
		mBookKemuri.renderer.enabled = false;
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
			++mTimer;
			if(mTimer > 60){
				++mTitleState;
				mTimer = 0;
			}
			break;
		case 1:
			// init
			mTakushima.setupFall(6.0f, 0.5f);
			++mTitleState;
			break;
		case 2:
			if(mTakushima.isEndMove() == true){
				mTitleCamera.startQuake(100, 2.0f, 10.0f);
				float speed = Mathf.PI / 60.0f;
				mSphere.setupUpDown(1.0f, speed);
				mBooks.SetActive(false);
				mBook1.gameObject.SetActive(true);
				mBook2.gameObject.SetActive(true);
				mBook3.gameObject.SetActive(true);
				mBook4.gameObject.SetActive(true);
				++mTitleState;
			}
			break;
		case 3:
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
			}
			break;
		case 4:
			++mTimer;
			if(mTimer == 120){
				Vector3 start = new Vector3(0.0f, 1.4f, 0.0f);
				Vector3 end = mSphere.gameObject.transform.position;
				mTitleCamera.startLookAt(start, end, 0.05f, 120);
				++mTitleState;
				mTimer = 0;
			}
			break;
		case 5:
			if(mTitleCamera.isEndMove() == true){
				mTitleCamera.startMoveForward(120, 0.025f);
				++mTitleState;
				mTimer = 0;
			}
			break;
		case 6:
			++mTimer;
			if(mTimer == 60){
				mFade.startFadeIn(60);
				++mTitleState;
				mTimer = 0;
			}
			break;
		}
	}
}
