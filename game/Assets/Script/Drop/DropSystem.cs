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
			throw new UnityException();
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
		BasketRef = GameObject.Find("basket");
		if( BasketRef == null ) {
			throw new UnityException();
		}
		mGameRef = FindObjectOfType<Game>();
		if( mGameRef == null ) {
			throw new UnityException();
		}

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

		// manual event set
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
		// random event set
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

		Rank += mMaxRank / mGameRef.stageTime / 60.0f;
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
		obj.setDropType(objectType);
		obj.transform.position = generatePos;
	}

	public void generateDropInBasket(DROP_OBJECT objectType, int height)
	{
		const int BASKET_NUM = 8;

		if( objectType == DROP_OBJECT.COIN ) {
			return;
		}

		if( mBasketCount == 0 ) {
			mBasketRandPos = new int[BASKET_NUM];
			for(int i=0; i<BASKET_NUM; i++) {
				mBasketRandPos[i] = i;
			}

			// tekitou shuffle
			for(int i=0; i<BASKET_NUM; i++) {
				int j = Random.Range(0, BASKET_NUM);
				int temp = mBasketRandPos[j];
				mBasketRandPos[j] = mBasketRandPos[i];
				mBasketRandPos[i] = temp;
			}
		}

		DropUnit obj = Instantiate(DropUnitPrefabs[(int)objectType]) as DropUnit;
		obj.setHold();
		obj.transform.parent = BasketRef.transform;
		//obj.transform.localScale *= 0.5f;

		Vector3 pos = new Vector3(0.0f, 0.0f, 0.0f);
		float x_range = 0.9f;
		pos.x = -(x_range * (0.5f - 0.05f)) 
				+ (x_range * ((float)mBasketRandPos[mBasketCount] / (float)(BASKET_NUM-1) - 0.05f)) 
				+ Random.Range(-x_range * 0.05f, x_range * 0.05f);
		pos.y = 0.05f + 0.02f * ((float)mBasketHeight);
		pos.z = 0.0f;
	
		if( objectType == DROP_OBJECT.HEART ) {
			// heart dekai kara shrink
			obj.transform.localScale *= 0.8f;
		}
		if( objectType == DROP_OBJECT.COIN
		   || objectType == DROP_OBJECT.APPLE 
		   || objectType == DROP_OBJECT.RICE )
		{
			pos.y -= 0.05f;
		}

		obj.transform.localPosition = pos;
		// height ni oujite seigen kaketa houga iikamo
		obj.transform.Rotate(Random.Range(0.0f, 360.0f), Random.Range(0.0f, 360.0f), Random.Range(0.0f, 360.0f));

		// Count Drop Object In Basket
		if( objectType != DROP_OBJECT.COIN ) {
			mBasketCount++;
			if( mBasketCount >= BASKET_NUM ) {
				mBasketHeight++;
				mBasketCount = 0;

				if( mBasketHeight > 10 ) {
					mBasketHeight = 10;
				}
			}
		}
	}

	public void generateBonusDrop(DROP_OBJECT objectType)
	{
		if( DropUnitPrefabs[(int)objectType] == null ) {
			return;
		}

		Player pl = FindObjectOfType<Player>();
		float posX = pl.transform.position.x + Random.Range(-1.5f, 1.5f);

		Vector3 generatePos = new Vector3(posX, GenerateHeight, GenerateDepth);
		
		DropUnit obj = Instantiate(DropUnitPrefabs[(int)objectType]) as DropUnit;
		obj.setDropType(objectType);
		obj.transform.position = generatePos;
		obj.SpeedRate = 6.0f;
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

	//! Basket Ref
	public GameObject BasketRef = null;

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

	// basket control
	private int mBasketCount = 0;
	private int mBasketHeight = 0;
	private int[] mBasketRandPos;

	private Game mGameRef;
}

//==============================================================================
//! End of File
//==============================================================================

