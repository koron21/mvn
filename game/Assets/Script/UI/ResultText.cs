using UnityEngine;
using System.Collections;

public class ResultText : MonoBehaviour {

	public string HomeText;
	public string WorkText;
	public string HealthText;
	public GameObject ResultDisp;
	public SpriteRenderer SheetTex;
	public SpriteRenderer KekkaHappyoTex;
	public SpriteRenderer HomeLuckTex;
	public SpriteRenderer WorkLuckTex;
	public SpriteRenderer HealthLuckTex;

	public float FrameInTime1   = 0.8f;
	public float FrameInTime2   = 0.2f;
	public float FadeOutTime    = 0.25f;
	public float FadeInTime     = 0.25f;
	public float InitOffsetPosY = 4.0f;
	public float OverOffsetPosY = -1.0f;

	public bool IsFinish {
		get {
			return mbFinish;
		}
	}

	// Use this for initialization
	void Start ()
	{
		mGUIText = GetComponent<GUIText>();
		mGUIText.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
		mGUIText.fontSize = (int)(60.0f * (float)Screen.width / 1280.0f);

		KekkaHappyoTex.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
		HomeLuckTex.color    = new Color(1.0f, 1.0f, 1.0f, 0.0f);
		WorkLuckTex.color    = new Color(1.0f, 1.0f, 1.0f, 0.0f);
		HealthLuckTex.color  = new Color(1.0f, 1.0f, 1.0f, 0.0f);

		Vector3 pos = ResultDisp.transform.localPosition;
		mBasePosY = pos.y;
		mInitPosY = mBasePosY + InitOffsetPosY;
		mOverPosY = mBasePosY + OverOffsetPosY;

		pos.y += InitOffsetPosY;
		ResultDisp.transform.localPosition = pos;

		mState = 0;
		mRno   = 0;
		mTimer = 0.0f;
		mbFinish = false;
		mbSeCall = false;
		//setNextText();
	}
	
	// Update is called once per frame
	void Update ()
	{
		if( mState == 0 ) {
			// kekka happyo ! uekara in

			Vector3 pos = ResultDisp.transform.localPosition;
			switch( mRno ) {
			case 0:
				mTimer += Time.deltaTime;
				if( mTimer >= (FrameInTime1 + FrameInTime2) ) {

					pos.y = mBasePosY;
					mRno++;
				}
				else if( mTimer >= FrameInTime1 ) {
					if( mbSeCall == false ) {
						SoundManager.Instance.requestSe("se_hit");
						mbSeCall = true;
					}
					pos.y = mInitPosY + (mBasePosY - mInitPosY) * Mathf.Sin(Mathf.PI * 0.5f * mTimer / (FrameInTime1 + FrameInTime2) );
				}
				else {
					pos.y = mInitPosY + (mBasePosY - mInitPosY) * Mathf.Sin(Mathf.PI * 0.5f * mTimer / (FrameInTime1 + FrameInTime2) );
				}
				ResultDisp.transform.localPosition = pos;
				break;

			case 1:
				// wait for input
				if( ResultCtrl.isNext() == true ) {
					mRno++;
					mAlpha = 1.0f;
				}
				break;

			case 2:
				// fade out
				mAlpha -= 1.0f * Time.deltaTime / FadeOutTime;
				if( mAlpha <= 0.0f ) {
					mAlpha = 0.0f;
					mState++;
					setNextText();
				}
				KekkaHappyoTex.color = new Color(1.0f, 1.0f, 1.0f, mAlpha);
				break;
			}
			
		}
		else if( mState == 1 ) {
			switch( mRno ) {
			case 0:
				mAlpha -= 1.0f * Time.deltaTime / FadeOutTime;
				if( mAlpha <= 0.0f ) {
					mAlpha = 0.0f;
					mRno++;
				}
				mCurrentTex.color = new Color(1.0f, 1.0f, 1.0f, mAlpha);
				mGUIText.color = new Color(1.0f, 1.0f, 1.0f, mAlpha);
				break;

			case 1:
				mCurrentTex = mNextTex;
				mGUIText.text = mText;
				mAlpha += 1.0f * Time.deltaTime / FadeInTime;
				if( mAlpha >= 1.0f ) {
					mAlpha = 1.0f;
					mRno++;
				}
				else if( mAlpha >= 0.5f ) {
					if( mbSeCall == false ) {
						SoundManager.Instance.requestSe("se_hit");
						mbSeCall = true;
					}
				}
				mCurrentTex.color = new Color(1.0f, 1.0f, 1.0f, mAlpha);
				mGUIText.color = new Color(1.0f, 1.0f, 1.0f, mAlpha);
				break;

			case 2:
				// wait for input
				if( ResultCtrl.isNext() == true ) {
					if( setNextText() == false ) {
						mState++;
						mRno = 0;
						mAlpha = 1.0f;
					}
				}
				break;
			}
		}
		else if( mState == 2 ) {
			switch( mRno ) {
			case 0:
				// fade out
				mAlpha -= 1.0f * Time.deltaTime / FadeOutTime;
				if( mAlpha <= 0.0f ) {
					mAlpha = 0.0f;
					mRno++;
					mbFinish = true;
				}
				mCurrentTex.color = new Color(1.0f, 1.0f, 1.0f, mAlpha);
				mGUIText.color = new Color(1.0f, 1.0f, 1.0f, mAlpha);
				SheetTex.color = new Color(1.0f, 1.0f, 1.0f, mAlpha);
				break;

			case 1:
				break;
			}
		}
	}

	public bool setNextText()
	{
		if( mTextId > 2 ) {
			return false;
		}

		switch( mTextId ) {
		case 0:
			mText = HomeText;
			mNextTex = HomeLuckTex;
			break;

		case 1:
			mText = WorkText;
			mNextTex = WorkLuckTex;
			break;

		case 2:
			mText = HealthText;
			mNextTex = HealthLuckTex;
			break;
		}

		mRno = 0;
		mAlpha = 1.0f;
		if( mTextId == 0 ) {
			// start fade in
			mRno = 1;
			mAlpha = 0.0f;
		}

		mTextId++;
		mbSeCall = false;

		return true;
	}
	
	private GUIText mGUIText;
	private int mState = 0;
	private int mRno = 0;
	private string mText;
	private int mTextId = 0;
	private float mAlpha = 0.0f;
	private float mTimer = 0.0f;
	private float mInitPosY = 0.0f;
	private float mBasePosY = 0.0f;
	private float mOverPosY = 0.0f;
	private SpriteRenderer mCurrentTex;
	private SpriteRenderer mNextTex;
	private bool mbFinish = false;
	private bool mbSeCall = false;
}
