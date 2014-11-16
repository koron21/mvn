using UnityEngine;
using System.Collections;

public class HeartParticle : MonoBehaviour
{
	//==========================================================================
	// Unity Functions
	//==========================================================================
	
	// Use this for initialization
	void Start()
	{
		mVelocity = new Vector3(0.0f, 0.0f, 0.0f);
		mVelocity.x = Random.Range(SpeedX_Max * 0.25f, SpeedX_Max);
		if( Random.Range(0, 2) == 0 ) {
			mVelocity.x = -mVelocity.x;
		}
		mAccelX = -mVelocity.x / WaitTime;

		mTimer = 0.0f;
		mInScaleVel  = transform.localScale / EnlargeTime;
		mOutScaleVel = -1.0f * transform.localScale / ShrinkTime;

		transform.localScale = new Vector3(0.0f, 0.0f, 0.0f);
	}
	
	// Update is called once per frame
	void Update()
	{
		mVelocity.y += AccelY * Time.deltaTime;

		float dVelX = mAccelX * Time.deltaTime;
		if( Mathf.Abs(mVelocity.x) > Mathf.Abs(dVelX) ) {
			mVelocity.x += dVelX;
		}
		else {
			mVelocity.x = 0.0f;
		}

		transform.localPosition += mVelocity * Time.deltaTime;

		mTimer += Time.deltaTime;
		if( mTimer < EnlargeTime ) {
			transform.localScale += mInScaleVel * Time.deltaTime;
		}
		else if( mTimer >= WaitTime ) {
			transform.localScale += mOutScaleVel * Time.deltaTime;
			if( transform.localScale.magnitude < 0.3f ) {
				Destroy(this.gameObject);
			}
		}
	}


	//==========================================================================
	// Public Member Variables
	//==========================================================================
	public float SpeedX_Max = 0.25f;
	public float AccelY = -0.1f;
	public float ShrinkTime = 0.5f;
	public float WaitTime = 0.5f;
	public float EnlargeTime = 0.2f;


	//==========================================================================
	// Private Member Variables
	//==========================================================================
	private Vector3 mVelocity;
	private float mAccelX = 0.0f;
	private float mTimer;
	private Vector3 mOutScaleVel;
	private Vector3 mInScaleVel;
}
