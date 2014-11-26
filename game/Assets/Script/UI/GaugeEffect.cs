using UnityEngine;
using System.Collections;

public class GaugeEffect : MonoBehaviour
{

	// Use this for initialization
	void Start()
	{
		mTimer		= 0.0f;
		mStartPos	= transform.position;
		mNormDir	= (DirPosition - mStartPos).normalized;
		mNormDir	= Quaternion.Euler(Random.Range(0.0f, 360.0f), Random.Range(0.0f, 360.0f), 0.0f) * mNormDir;
	}
	
	// Update is called once per frame
	void Update()
	{
		float time1 = 0.3f * MoveTime;
		float time2 = 0.7f * MoveTime;

		mTimer += Time.deltaTime;

		if( mTimer < time1 ) {
			Vector3 pos = mStartPos 
						+ (DirPosition - mStartPos) * mTimer / MoveTime
						+ mNormDir * Amplitude * Mathf.Sin(Mathf.PI * 0.5f * mTimer / time1);
			transform.position = pos;
		}
		else if( mTimer < MoveTime ) {
			Vector3 pos = mStartPos 
					+ (DirPosition - mStartPos) * mTimer / MoveTime
					+ mNormDir * Amplitude * Mathf.Sin(Mathf.PI * (0.5f + 0.5f * (mTimer-time1) / time2));
			transform.position = pos;
		}
		else {
			Destroy( this.gameObject );
		}

		//transform.LookAt(Camera.main.transform);
	}

	public Vector3	DirPosition;
	public float	MoveTime;
	public float	Amplitude;

	float	mTimer = 0.0f;
	Vector3	mStartPos;
	Vector3 mNormDir;
}
