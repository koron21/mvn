using UnityEngine;
using System.Collections;

//==============================================================================
//! Drop Unit
//==============================================================================
public class DropUnit : MonoBehaviour
{
	public enum STATE : int
	{
		NONE,
		FALL,
		OUT,

		STATE_NUM,
	};

	//==========================================================================
	// Unity Functions
	//==========================================================================

	// Use this for initialization
	protected virtual void Start()
	{
		Vector3 initPos = new Vector3(transform.localPosition.x,
		                              DropSystem.Instance.GenerateHeight,
		                              DropSystem.Instance.GenerateDepth);
		transform.localPosition = initPos;

		//mVelocity = new Vector3(0.0f, -DropSystem.Instance.FallVelocity, 0.0f);
		mVelocity = new Vector3(0.0f, 0.0f, 0.0f);
		mState    = STATE.FALL;
	}
	
	// Update is called once per frame
	protected virtual void Update()
	{
		transform.localPosition += mVelocity * Time.deltaTime * SpeedRate;

		mVelocity.y -= DropSystem.Instance.FallVelocity * Time.deltaTime;

		if( mState == STATE.OUT ) {
			mOutDelayTime -= Time.deltaTime;

			if( mOutDelayTime <= 0.0f ) {
				// Shrink Drop Object
				transform.localScale += mOutScaleVel * Time.deltaTime;
				if( transform.localScale.magnitude < 0.3f ) {
					Destroy(this.gameObject);
				}
			}
		}
	}

	// Collision Event
	protected virtual void OnTriggerEnter(Collider other)
	{
		if( mState != STATE.FALL ) {
			return;
		}

		// If other is floor, bounce drop object then shrink it
		if( other.gameObject.name == "Ground" ) {
			mVelocity     = -(mVelocity * 0.3f);
			mState        = STATE.OUT;
			mOutDelayTime = 0.25f;
			mOutScaleVel  = -transform.localScale / 0.5f;

			// test score count
			ScoreSystem.Instance.addScore( ScoreLove, ScoreMoney );
		}
		else {
			// If ohter is Player Unit, add score
			// mijisou
		}
	}

	//==========================================================================
	// Private Functions
	//==========================================================================

	//! Add Rotation
	protected void addRotation(float rot_x, float rot_y, float rot_z)
	{
		rot_x = rot_x * Time.deltaTime;
		rot_y = rot_y * Time.deltaTime;
		rot_z = rot_z * Time.deltaTime;

		transform.Rotate(rot_x, rot_y, rot_z);
	}

	//==========================================================================
	// Public Member Variables
	//==========================================================================
	//! Reward Score
	public int ScoreMoney = 0;
	public int ScoreLove  = 0;
	public int ScoreOther = 0;

	//! Speed Adjustment
	public float SpeedRate = 1.0f;

	//==========================================================================
	// Private Member Variables
	//==========================================================================
	protected Vector3 mVelocity;
	protected STATE mState = STATE.NONE;
	protected float mOutDelayTime = 0.0f;
	protected Vector3 mOutScaleVel;
	
}

//==============================================================================
//! End of File
//==============================================================================
