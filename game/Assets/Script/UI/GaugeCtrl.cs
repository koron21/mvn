using UnityEngine;
using System.Collections;

public class GaugeCtrl : MonoBehaviour
{
	//==========================================================================
	// Unity Functions
	//==========================================================================
	// Use this for initialization
	void Start()
	{
		Value = 0.0f;
		mObjectValue = 0.0f;
		mAddValue = 0.0f;
	}
	
	// Update is called once per frame
	void Update()
	{
		mTimer += Time.deltaTime;
		if( mObjectValue > Value ) {
			Value += mAddValue * Time.deltaTime;
		}
		else {
			Value = mObjectValue;
		}

		// scale adjustment
		Vector3 scale = GaugeBar.transform.localScale;
		scale.x = 1.0f * Value / MaxValue;
		GaugeBar.transform.localScale = scale;
		GaugeBarFlash.transform.localScale = scale;

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


	//==========================================================================
	// Public Member Variables
	//==========================================================================
	public GameObject GaugeBar = null;
	public GameObject GaugeBarFlash = null;
	public float Value = 0.0f;
	public float MaxValue = 1000.0f;
	public float AddTime = 0.5f;

	//==========================================================================
	// Private Member Variables
	//==========================================================================
	private float mObjectValue = 0.0f;
	private float mAddValue = 0.0f;
	private float mTimer = 0.0f;

}
