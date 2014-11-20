using UnityEngine;
using System.Collections;

public class StartCtrl : MonoBehaviour {

	//==========================================================================
	// Unity Functions
	//==========================================================================
	// Use this for initialization
	void Start()
	{
		mBaseScale = transform.localScale;
		mInitScale = transform.localScale * MIN_RATE;
		mMaxScale = transform.localScale * MAX_RATE;
		mTimer = 0.0f;

		transform.localScale = mInitScale;
	}
	
	// Update is called once per frame
	void Update()
	{
		mTimer += Time.deltaTime;
		if( mTimer < ENLARGE_ANIM_TIME ) {
			Vector3 scale = mInitScale 
							+ (mMaxScale - mInitScale) * Mathf.Sin(Mathf.PI * 0.5f * mTimer / ENLARGE_ANIM_TIME);
			transform.localScale = scale;
		}
		else if( mTimer < (ENLARGE_ANIM_TIME + SHRINK_ANIM_TIME) ) {
			float obj_time = SHRINK_ANIM_TIME;
			float timer = mTimer - ENLARGE_ANIM_TIME;
			Vector3 scale = mBaseScale 
							+ (mMaxScale - mBaseScale) * Mathf.Sin((1.0f - timer / obj_time) * Mathf.PI * 0.5f);

			transform.localScale = scale;
		}
		else if( mTimer < (ENLARGE_ANIM_TIME + SHRINK_ANIM_TIME + WAIT_TIME) ) {
			transform.localScale = mBaseScale;
		}
		else if( mTimer < (ENLARGE_ANIM_TIME + SHRINK_ANIM_TIME + WAIT_TIME + FADE_TIME) ) {
			float difTime = mTimer - ENLARGE_ANIM_TIME - SHRINK_ANIM_TIME - WAIT_TIME;
			GetComponent<SpriteRenderer>().material.SetColor("_Color", new Color(1.0f, 1.0f, 1.0f, 1.0f - 1.0f * difTime / FADE_TIME));
		}
		else {
			Destroy(this.gameObject);
		}
	}

	//==========================================================================
	// Public Member Variables
	//==========================================================================
	public float MAX_RATE = 1.25f;
	public float MIN_RATE = 0.5f;
	public float ENLARGE_ANIM_TIME = 0.5f;
	public float SHRINK_ANIM_TIME = 0.15f;
	public float WAIT_TIME = 1.0f;
	public float FADE_TIME = 0.2f;
	
	//==========================================================================
	// Private Member Variables
	//==========================================================================
	private Vector3 mInitScale;
	private Vector3 mBaseScale;
	private Vector3 mMaxScale;
	private float mTimer = 0.0f;
}
