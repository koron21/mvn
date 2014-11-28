using UnityEngine;
using System.Collections;

//==============================================================================
//! Manage Multi Drop Unit Timing
//==============================================================================
public class DropEvent : MonoBehaviour
{
	// koredato property ni dasenakatta node sorezore dokuritsu no tbl ni shimasu
	/*
	public struct DropEventParam
	{
		DropSystem.DROP_OBJECT objectType;
		float dropOffsetTime; // [sec]
		float centerPosX; // [-1.0f, 1.0f]
		float radiusPosX; // [0.0f, 2.0f]

		// centerPosX kara radiusPosX no hanni no dokoka ni drop suru
		// radiusPosX wo 0 ni sureba centerPosX ni drop suru
	};
	*/

	//==========================================================================
	// Unity Functions
	//==========================================================================

	// Use this for initialization
	void Start()
	{
		mObjectNum = DropObjectTypes.Length;

		if( DropOffsetTimes.Length != mObjectNum
		   || DropCenterPosX.Length != mObjectNum
		   || DropRadiusPosX.Length != mObjectNum )
		{
			// Assert Program
			throw new UnityException();
		}

		// create generate flag
		mbGeneratedObject = new bool[mObjectNum];
		for(int i=0; i<mObjectNum; i++) {
			mbGeneratedObject[i] = false;
		}

		mTimer = 0.0f;

		mOffsetPosX = EventOffsetPosX + Random.Range(-EventRandRadius, EventRandRadius);

		for(int i=1; i<mObjectNum; i++) {
			DropOffsetTimes[i] += Random.Range(0.0f, DropOffsetTimes[i] * 0.5f);
		}
	}
	
	// Update is called once per frame
	void Update()
	{
		mTimer += Time.deltaTime;

		bool bAllGenerated = true;

		for(int i=0; i<mObjectNum; i++) {
			if( mbGeneratedObject[i] == true ) {
				continue;
			}

			// Check Offset Time
			if( mTimer >= DropOffsetTimes[i] ) {
				float genPosX = mOffsetPosX + DropCenterPosX[i] + Random.Range(-DropRadiusPosX[i], DropRadiusPosX[i]);

				DropSystem.DROP_OBJECT dropObj = DropObjectTypes[i];
				if( dropObj == DropSystem.DROP_OBJECT.NUM ) {
					if( RareDrop == false ) {
						dropObj = (DropSystem.DROP_OBJECT)Random.Range(2, 6);
					}
					else {
						if( DropSystem.Instance.RareDropNum % 2 == 0 ) {
							dropObj = DropSystem.DROP_OBJECT.HEART;
						}
						else {
							dropObj = DropSystem.DROP_OBJECT.MILLION;
						}
						DropSystem.Instance.RareDropNum++;
					}
				}

				DropSystem.Instance.generateDrop( dropObj, genPosX );
				mbGeneratedObject[i] = true;
			}

			bAllGenerated = false;
		}

		if( bAllGenerated == true ) {
			Destroy(this.gameObject);
		}
	}

	//==========================================================================
	// Public Member Variables
	//==========================================================================
	// koitsu ha system de kanri suru
	// system gawa no timer de invoke time keika shitara prefab kara instantiate sareru
	//public float EventInvokeTime = 0.0f; // [sec]

	public bool  RareDrop = false;
	public float EventOffsetPosX = 0.0f;
	public float EventRandRadius = 0.0f;
	
	public DropSystem.DROP_OBJECT[] DropObjectTypes = new DropSystem.DROP_OBJECT[1];
	public float[] DropOffsetTimes = new float[1];
	public float[] DropCenterPosX = new float[1];
	public float[] DropRadiusPosX = new float[1];

	//==========================================================================
	// Private Member Variables
	//==========================================================================
	private int mObjectNum = 0;
	private bool[] mbGeneratedObject;
	private float mTimer = 0.0f;
	private float mOffsetPosX = 0.0f;
}

//==============================================================================
//! End of File
//============================================================================
