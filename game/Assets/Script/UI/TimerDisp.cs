using UnityEngine;
using System.Collections;

public class TimerDisp : MonoBehaviour
{
	// Use this for initialization
	void Start ()
	{
		mGameRef   = FindObjectOfType<Game>();
		mNumberRef = GetComponent<Number>();
		mNumberRef.DispNumber = (int)(mGameRef.Timer);
		mNumberRef.DispColor  = NormalColor;

		mDispNumOld = mNumberRef.DispNumber;
		mBaseScale  = transform.localScale;
		mbScaleAnim = false;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if( mGameRef.IsInGame == false ) {
			Color c = mNumberRef.DispColor;
			c.a = 0.0f;
			mNumberRef.DispColor = c;
		}
		else {
			int dispNum = (int)(mGameRef.Timer + 0.9f);
			mDispNumOld = mNumberRef.DispNumber;
			mNumberRef.DispNumber = dispNum;

			if( dispNum <= mGameRef.AlertTime && dispNum > 0 ) {
				mNumberRef.DispColor = AlertColor;

				if( mDispNumOld > mNumberRef.DispNumber ) {
					mPhase = 0;
					mTimer = 0.0f;
					mbScaleAnim = true;
				}
				
				if( mbScaleAnim == true ) {
					mTimer += Time.deltaTime;
					switch( mPhase ) {
					case 0:
						// scale up
						transform.localScale = mBaseScale + (ScaleUpValue - mBaseScale) * mTimer / ScaleUpTime;
						if( mTimer >= ScaleUpTime ) {
							transform.localScale = ScaleUpValue;
							mTimer = 0.0f;
							mPhase++;
						}
						break;
						
					case 1:
						// scale down
						transform.localScale = ScaleUpValue + (mBaseScale - ScaleUpValue) * mTimer / ScaleDownTime;
						if( mTimer >= ScaleDownTime ) {
							transform.localScale = mBaseScale;
							mTimer = 0.0f;
							mbScaleAnim = false;
						}
						break;
						
					default:
						break;
					}
				}
			}
			else if( dispNum == 0 ) {
				Color c = mNumberRef.DispColor;
				c.a = 0.0f;
				mNumberRef.DispColor = c;
			}
			else {
				mNumberRef.DispColor = NormalColor;
			}
		}
	}

	public Vector3 ScaleUpValue;
	public float ScaleUpTime;
	public float ScaleDownTime;
	public Color NormalColor;
	public Color AlertColor;

	private Game mGameRef;
	private Number mNumberRef;
	private float mTimer;
	private int mPhase;
	private int mDispNumOld;
	private bool mbScaleAnim;
	private Vector3 mBaseScale;
}
