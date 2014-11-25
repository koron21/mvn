using UnityEngine;
using System.Collections;

public class PlayerCtrl : MonoBehaviour
{
	public bool IsZukkoke {
		get {
			return mbZukkoke;
		}
	}

	// Use this for initialization
	void Start()
	{
		mBasePos = transform.localPosition;
		TargetPos = new Vector3(mBasePos.x, mBasePos.y - 0.6f, mBasePos.z);
		mBaseRot = transform.localRotation.eulerAngles;
		mTimer   = 0.0f;
	}
	
	// Update is called once per frame
	void Update()
	{
		if( mbZukkoke ) {
			const float rate1 = 0.1f;
			const float rate2 = 0.65f;
			const float rate3 = 0.15f;

			float time1 = AnimTime * rate1;
			float time2 = AnimTime * (rate1 + rate2);
			float time3 = AnimTime * (rate1 + rate2 + rate3);

			mTimer += Time.deltaTime;

			if( mTimer < time1 ) {
				transform.localPosition = mBasePos + (TargetPos - mBasePos) * mTimer / time1;
				transform.localRotation = Quaternion.Euler( mBaseRot + (TargetRot - mBaseRot) * mTimer / time1 );
			}
			else if( mTimer < time2 ) {
				transform.localPosition = TargetPos;
				transform.localRotation = Quaternion.Euler( TargetRot );
			}
			else if( mTimer < time3 ) {
				transform.localPosition = TargetPos + (mBasePos - TargetPos) * (mTimer - time2) / (time3 - time2);
				transform.localRotation = Quaternion.Euler( TargetRot + (mBaseRot - TargetRot) * (mTimer - time2) / (time3 - time2) );
			}
			else {
				transform.localPosition = mBasePos;
				transform.localRotation = Quaternion.Euler( mBaseRot );
				mbZukkoke = false;
			}
		}	
	}

	public void doZukkoke()
	{
		mTimer = 0.0f;
		mbZukkoke = true;
	}

	public Vector3 TargetPos;
	public Vector3 TargetRot;
	public float AnimTime;

	private Vector3 mBasePos;
	private Vector3 mBaseRot;
	private float mTimer;
	private bool mbZukkoke;
}
