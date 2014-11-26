using UnityEngine;
using System.Collections;

public class GaugeCtrl : MonoBehaviour
{
	public enum TYPE
	{
		GAME,
		RESULT,
	};

	public bool IsDecraeseEnd
	{
		get {
			return mbDecreaseEnd;
		}
	}

	//==========================================================================
	// Unity Functions
	//==========================================================================
	// Use this for initialization
	void Start()
	{
		if( GaugeType == TYPE.GAME ) {
			Value = 0.0f;
			mObjectValue = 0.0f;
			mAddValue = 0.0f;
		}
		else {
			Value = MaxValue;
			mObjectValue = MaxValue;
			mAddValue = 0.0f;
			mbDecrease = false;
		}
	}
	
	// Update is called once per frame
	void Update()
	{
		if( GaugeType == TYPE.GAME ) {
			mTimer += Time.deltaTime;
			if( mObjectValue > Value ) {
				Value += mAddValue * Time.deltaTime;
			}
			else {
				Value = mObjectValue;
			}

			// flash animation
			float alpha = 0;
			float maxAlpha = 0.5f;
			if( mTimer < (AddTime * 0.5f) ) {
				alpha = 1.0f * mTimer / (AddTime * 0.5f);
			}
			else if( mTimer < AddTime ) {
				alpha = 1.0f - 1.0f * (mTimer - AddTime * 0.5f) / (AddTime - AddTime * 0.5f);
			}
			else {
				alpha = 0;
			}
			GaugeBarFlash.GetComponent<SpriteRenderer>().material.SetColor("_TintColor", new Color(1.0f, 1.0f, 1.0f, maxAlpha * alpha));
		}
		else {
			if( mbDecrease == true ) {
				mTimer += Time.deltaTime;

				Value = MaxValue * (1.0f - mTimer / mDecreaseTime);
				if( mDecreaseTime <= mTimer ) {
					mbDecrease = false;
					mbDecreaseEnd = true;
				}

				// generate effect
				if( EffectPrefab ) {
					mGenerateFrame++;

					if( (mGenerateFrame % 3) == 0 ) {
						GaugeEffect effect = Instantiate( EffectPrefab ) as GaugeEffect;
						effect.DirPosition = mObjPosition;
					}
				}
			}
		}

		// scale adjustment
		Vector3 scale = GaugeBar.transform.localScale;
		scale.x = 1.0f * Value / MaxValue;
		GaugeBar.transform.localScale = scale;
		GaugeBarFlash.transform.localScale = scale;
	}

	//==========================================================================
	// Public Functions
	//==========================================================================
	public void addValue(float value)
	{
		mObjectValue += value;
		if( mObjectValue > MaxValue ) {
			mObjectValue = MaxValue;
		}
		mTimer = 0.0f;
		mAddValue = (mObjectValue - Value) / AddTime;
	}

	public void startDecrease(float time, Vector3 obj_pos)
	{
		mTimer = 0.0f;
		mDecreaseTime = time;
		mObjPosition = obj_pos;
		mbDecrease = true;
	}

	public void changeObjPos(Vector3 obj_pos)
	{
		mObjPosition = obj_pos;
	}

	public void stopDecrease()
	{
		mbDecrease = false;
	}

	public void resumeDecrease()
	{
		mbDecrease = true;
	}


	//==========================================================================
	// Public Member Variables
	//==========================================================================
	public GameObject GaugeBar = null;
	public GameObject GaugeBarFlash = null;
	public float Value = 0.0f;
	public float MaxValue = 1000.0f;
	public float AddTime = 0.5f;
	public GaugeEffect EffectPrefab = null;
	public GaugeCtrl.TYPE GaugeType = GaugeCtrl.TYPE.GAME;

	//==========================================================================
	// Private Member Variables
	//==========================================================================
	private float mObjectValue = 0.0f;
	private float mAddValue = 0.0f;
	private float mTimer = 0.0f;
	
	private bool mbDecrease = false;
	private bool mbDecreaseEnd = false;
	private float mDecreaseTime = 0.0f;
	private Vector3 mObjPosition;
	private int mGenerateFrame = 0;
}
