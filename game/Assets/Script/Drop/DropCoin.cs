using UnityEngine;
using System.Collections;

public class DropCoin : DropUnit 
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

		mRotForward = (Random.Range(0,2) == 0) ? 1 : -1;
		mRotY = Random.Range(-100.0f, 100.0f);
	}
	
	// Update is called once per frame
	protected override void Update()
	{
		base.Update();

		if( mState == STATE.HOLD ) {
			return;
		}
		// rotation
		addRotation(RotateSpeed * mRotForward, mRotY, 0.0f);
		//addRotation(0.0f, RotateSpeed * mRotForward, 0.0f);
	}

	//==========================================================================
	// Public Member Variables
	//==========================================================================
	//! Rotation Speed
	public float RotateSpeed = 500.0f;

	//==========================================================================
	// Private Member Variables
	//==========================================================================
	//! Do Rotate Forward Direction
	private int mRotForward = 1;
	private float mRotY = 0.0f;
}
