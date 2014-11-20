using UnityEngine;
using System.Collections;

public class DropHeavy : DropUnit
{
	//==========================================================================
	// Unity Functions
	//==========================================================================
	// Use this for initialization
	protected override void Start()
	{
		base.Start();

		if( mState == STATE.HOLD ) {
			return;
		}

		if( Random.Range(0,2) == 1 ) {
			// gyaku muki ni rakka saseru
			transform.localEulerAngles = -transform.localEulerAngles;
			RotationGoal = RotationGoalInv;
		}

		mRotVel  = new Vector3(0.0f, 0.0f, 0.0f);
		mRotInit = transform.localEulerAngles;
		mRotAcc  = (RotationGoal - mRotInit) * 2.0f / (RotationTime * RotationTime);
	}
	
	// Update is called once per frame
	protected override void Update()
	{
		base.Update();

		// rotation
		if( mState == STATE.FALL ) {
			if( mRotTimer < RotationTime ) {
				addRotation(mRotVel.x, mRotVel.y, mRotVel.z);

				mRotTimer += Time.deltaTime;
				mRotVel += mRotAcc * Time.deltaTime;
			}
		}
		else if( mState == STATE.OUT ) {
//			if( mRotTimer < RotationTime ) {
//				addRotation( mRotVel.x, mRotVel.y, mRotVel.z );
//
//				mRotTimer += Time.deltaTime;
//				//mRotVel += mRotAcc * Time.deltaTime;
//			}

			// sasaru kanji ni suru
			mVelocity.x = mVelocity.y = mVelocity.z = 0.0f;
		}
	}

	// Collision Event
	protected override void OnTriggerEnter(Collider other)
	{
		if( mState != STATE.FALL ) {
			return;
		}

		base.OnTriggerEnter(other);

		if( mState == STATE.OUT ) {
//			Vector3 oldRotGoal = RotationGoal;
//
//			RotationGoal = mRotInit;
//			mRotInit = oldRotGoal;
//			RotationTime = 0.3f;
//			mRotVel = (RotationGoal - mRotInit) / RotationTime;
//			//mRotVel  = new Vector3(0.0f, 0.0f, 0.0f);
//			//mRotAcc  = (RotationGoal - mRotInit) * 2.0f / (RotationTime * RotationTime);
//			mRotTimer = 0.0f;

			// sasaru kanji ni suru
			mVelocity.x = mVelocity.y = mVelocity.z = 0.0f;
			mOutDelayTime = 1.0f;
		}
	}

	//==========================================================================
	// Public Member Variables
	//==========================================================================
	//! Rotation Goal
	public Vector3 RotationGoal    = new Vector3(-90.0f, 0.0f, 0.0f);
	public Vector3 RotationGoalInv = new Vector3(90.0f, 0.0f, 0.0f);

	//! Rotation Time
	public float RotationTime = 0.2f;

	//==========================================================================
	// Private Member Variables
	//==========================================================================
	private Vector3 mRotVel;
	private Vector3 mRotInit;
	private Vector3 mRotAcc;
	private float mRotTimer = 0.0f;
}
