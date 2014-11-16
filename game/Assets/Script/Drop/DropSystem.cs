using UnityEngine;
using System.Collections;

//==============================================================================
//! Drop Manage System
//==============================================================================
public class DropSystem : MonoBehaviour
{
	public enum DROP_OBJECT : int
	{
		COIN = 0,
		MILLION,
		MEAT,
		FISH,
		RICE,
		APPLE,
		HEART,

		NUM,
	};

	[System.Serializable]
	public class Event
	{
		public DropEvent EventPrefab;
		public float InvokeTime;
	};

	//==========================================================================
	// Accessor
	//==========================================================================
	//! Get System Ptr
	static public DropSystem Instance
	{
		get
		{
			return mpInstance;
		}
	}

	//==========================================================================
	// Unity Functions
	//==========================================================================

	//! First Initialization
	void Awake()
	{
		if( mpInstance == null ) {
			mpInstance = this;
		}
		else {
			// Assert kaketai
		}
	}

	//! Called When Object is Destoryed
	//! @note korede atterunoka ha yoku wakaran
	//!       kuwasii hito oshiete-
	void OnDestory()
	{
		if( mpInstance ) {
			mpInstance = null;
		}
	}
	
	// Use this for initialization
	void Start()
	{
		// create generate flag
		mEventNum = DropEventList.Length;
		mbGeneratedEvent = new bool[ mEventNum ];
		for(int i=0; i<mEventNum; i++) {
			mbGeneratedEvent[i] = false;
		}

		mTimer = 0.0f;
		mGenTime = 0.0f;
		Rank = 0.0f;
		mDropCount = 0;
	}
	
	// Update is called once per frame
	void Update()
	{
		mTimer += Time.deltaTime;

#if false
		for(int i=0; i<mEventNum; i++) {
			if( mbGeneratedEvent[i] == true ) {
				continue;
			}

			if( DropEventList[i].EventPrefab == null ) {
				continue;
			}

			if( mTimer >= DropEventList[i].InvokeTime ) {
				mbGeneratedEvent[i] = true;
				Instantiate( DropEventList[i].EventPrefab );
			}
		}
#endif

		if( mTimer >= mGenTime ) {
			if( (mDropCount % 10) == 9 ) {
				Instantiate( DropEventList[1].EventPrefab );
				mGenTime += 2.0f * calcNextDropTime();
			}
			else {
				Instantiate( DropEventList[0].EventPrefab );
				mGenTime += calcNextDropTime();
			}
			mDropCount++;
		}

		Rank += Time.deltaTime;
		if( Rank >= mMaxRank ) {
			Rank = mMaxRank;
		}
	}

	//==========================================================================
	// Public Functions
	//==========================================================================
	public void generateDrop(DROP_OBJECT objectType, float posX_rate)
	{
		if( DropUnitPrefabs[(int)objectType] == null ) {
			return;
		}

		// offset mo kouryo shita houga yoi?
		// z fight boushi no tame z houkou no offset mo iru
		Vector3 generatePos = new Vector3(GenerateWidth * posX_rate, GenerateHeight, GenerateDepth);

		DropUnit obj = Instantiate(DropUnitPrefabs[(int)objectType]) as DropUnit;
		obj.transform.position = generatePos;
	}


	//==========================================================================
	// Private Functions
	//==========================================================================
	private float calcNextDropTime()
	{
		float minTime = 0.5f;
		float maxTime = 3.0f;
		float rangeTime = 0.5f;
		float time = maxTime - (maxTime - minTime) * Rank * Rank / (mMaxRank * mMaxRank);

		return Random.Range (time, time+rangeTime);
	}

	
	//==========================================================================
	// Public Member Variables
	//==========================================================================
	public float FallVelocity = 0.2f;
	//public float GravityAccel = 0.01f;

	public float GenerateWidth  = 2.0f;
	public float GenerateHeight = 2.4f;
	public float GenerateDepth  = -8.0f;

	//! Origin DropUnit Prefab List
	public DropUnit[] DropUnitPrefabs = new DropUnit[ (int)DROP_OBJECT.NUM ];
	//! Drop Generate Event Data
	public Event[] DropEventList = new Event[2];

	//==========================================================================
	// Private Member Variables
	//==========================================================================
	static private DropSystem mpInstance = null;

	private float mTimer = 0.0f;
	private int mEventNum = 0;
	private bool[] mbGeneratedEvent;

	public float Rank = 0.0f;
	private float mGenTime = 0.0f;
	private float mMaxRank = 100.0f;
	private int mDropCount = 0;
}

//==============================================================================
//! End of File
//==============================================================================

