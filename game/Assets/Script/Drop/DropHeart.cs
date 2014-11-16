using UnityEngine;
using System.Collections;

public class DropHeart : DropUnit
{
	enum HEART_STATE
	{
		DOKUN,
		WAIT,
	}

	//==========================================================================
	// Unity Functions
	//==========================================================================
	// Use this for initialization
	protected override void Start()
	{
		base.Start();

		mTimer = 0.0f;
		mBaseScale = transform.localScale.x;
		mCurrentScale = mBaseScale;
		mHeartState = HEART_STATE.WAIT;
		mParticleCount = 0;
		mDelayCount = DelayCount;
		mTimer = Random.Range(0.0f, IntervalTime);
	}
	
	// Update is called once per frame
	protected override void Update()
	{
		base.Update();
		if( this.gameObject == null ) {
			return;
		}

		if( mState == STATE.FALL ) {
			if( mHeartState == HEART_STATE.DOKUN ) {
				mCurrentScale = mBaseScale + (mBaseScale * ScaleRate - mBaseScale) * Mathf.Sin(mTimer / ScaleTime * Mathf.PI);
				mTimer += Time.deltaTime;

				mDelayCount--;
				if( mDelayCount <= 0 && mParticleCount < ParticleNum ) {
					mParticleCount++;
					if( mParticleCount % 6 == 1 ) {
						Instantiate( ParticlePrefab, this.transform.position + new Vector3(0.0f, -0.5f, -0.5f), this.transform.rotation );
					}
				}

				if( mTimer >= ScaleTime ) {
					mCurrentScale = mBaseScale;
					mTimer = 0.0f;
					mHeartState = HEART_STATE.WAIT;
				}
			}
			else if( mHeartState == HEART_STATE.WAIT ) {
				// wait a interval time
				mTimer += Time.deltaTime;

				if( mTimer >= IntervalTime ) {
					mTimer = 0.0f;
					mParticleCount = 0;
					mDelayCount = DelayCount;
					mHeartState = HEART_STATE.DOKUN;
				}
			}

			transform.localScale = new Vector3( mCurrentScale, mCurrentScale, mCurrentScale );
		}
		else if( mState == STATE.OUT ) {
			if( transform.localRotation.eulerAngles.z < 90.0f ) {
				transform.Rotate(0.0f, 0.0f, 90.0f * Time.deltaTime * 2.0f);
				transform.localPosition += new Vector3(0.0f, -0.25f * Time.deltaTime * 2.0f, 0.0f);
			}

			mVelocity.x = mVelocity.y = mVelocity.z = 0.0f;
		}

	}

	// Collision Event
	protected override void OnTriggerEnter(Collider other)
	{
		base.OnTriggerEnter(other);

		if( mState == STATE.OUT ) {
			mVelocity = new Vector3(0.0f, 0.0f, 0.0f);
			mOutDelayTime += 2.0f;
			return;
		}
	}

	//==========================================================================
	// Public Member Variables
	//==========================================================================
	public float ScaleRate = 1.3f;
	public float ScaleTime = 0.5f;
	public float IntervalTime = 0.5f;
	public HeartParticle ParticlePrefab = null;
	public int ParticleNum = 10;
	public int DelayCount = 30;

	//==========================================================================
	// Private Member Variables
	//==========================================================================
	private float mTimer = 0.0f;
	private float mBaseScale = 1.0f;
	private float mCurrentScale = 1.0f;
	private HEART_STATE mHeartState;
	private int mParticleCount = 0;
	private int mDelayCount = 0;
	private float mOutRotZ = 0.0f;
}

 