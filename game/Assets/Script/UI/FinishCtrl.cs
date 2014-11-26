using UnityEngine;
using System.Collections;

public class FinishCtrl : MonoBehaviour
{
	//==========================================================================
	// Unity Functions
	//==========================================================================
	// Use this for initialization
	void Start()
	{
		mTimer = 0.0f;
		mInitPos = transform.localPosition;
		mInitPos.x = INIT_POS_X;

		mOverPos = transform.localPosition;
		mOverPos.x += OVER_POS_X;

		mBasePos = transform.localPosition;

		mBaseScaleX = transform.localScale.x;
		mShrinkScaleX = transform.localScale.x * 0.5f;

		transform.localPosition = mInitPos;
	}
	
	// Update is called once per frame
	void Update()
	{
		mTimer += Time.deltaTime;
		if( mTimer <= MOVE_TIME ) {
			Vector3 pos = transform.localPosition;
			pos.x = mInitPos.x + (mOverPos.x - mInitPos.x) * Mathf.Sin(Mathf.PI * 0.5f * mTimer / MOVE_TIME);
			transform.localPosition = pos;

			float shrinkStartTime = MOVE_TIME - SHRINK_TIME;
			if( mTimer >= shrinkStartTime ) {
				float difTime = mTimer - shrinkStartTime;
				Vector3 scale = transform.localScale;
				scale.x = mBaseScaleX + (mShrinkScaleX - mBaseScaleX) * difTime / SHRINK_TIME;
				transform.localScale = scale;
			}
		}
		else if( mTimer <= (MOVE_TIME + SHRINK_TIME) ) {
			float difTime = mTimer - MOVE_TIME;
			Vector3 scale = transform.localScale;
			scale.x = mShrinkScaleX + (mBaseScaleX - mShrinkScaleX) * difTime / SHRINK_TIME;
			transform.localScale = scale;

			Vector3 pos = transform.localPosition;
			pos.x = mBasePos.x + (mOverPos.x - mBasePos.x) * Mathf.Sin(Mathf.PI * 0.5f * (1.0f - difTime / SHRINK_TIME));
			transform.localPosition = pos;
		}
		else if( mTimer <= (MOVE_TIME + SHRINK_TIME + WAIT_TIME ) ){
			Vector3 scale = transform.localScale;
			scale.x = mBaseScaleX;
			transform.localScale = scale;
			transform.localPosition = mBasePos;
		}
		else if( mTimer <= (MOVE_TIME + SHRINK_TIME + WAIT_TIME + FADE_TIME) ) {
			float difTime = mTimer - MOVE_TIME - SHRINK_TIME - WAIT_TIME;
			GetComponent<SpriteRenderer>().material.SetColor("_Color", new Color(1.0f, 1.0f, 1.0f, 1.0f - 1.0f * difTime / FADE_TIME));
		}
		else {
			Destroy( this.gameObject );
		}
	}

	//==========================================================================
	// Public Member Variables
	//==========================================================================
	public float INIT_POS_X = -2.0f;
	public float OVER_POS_X = 0.1f;
	public float MOVE_TIME = 0.25f;
	public float SHRINK_TIME = 0.05f;
	public float WAIT_TIME = 0.8f;
	public float FADE_TIME = 0.1f;

	//==========================================================================
	// Private Member Variables
	//==========================================================================
	private Vector3 mInitPos;
	private Vector3 mBasePos;
	private Vector3 mOverPos;
	private float mBaseScaleX;
	private float mShrinkScaleX;
	private float mTimer = 0.0f;
}
