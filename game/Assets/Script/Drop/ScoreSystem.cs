using UnityEngine;
using System.Collections;

//==============================================================================
//! Score Manage System
//==============================================================================
public class ScoreSystem : MonoBehaviour
{
	//==========================================================================
	// Accessor
	//==========================================================================
	//! Get System Ptr
	static public ScoreSystem Instance
	{
		get
		{
			return mpInstance;
		}
	}

	public void addScore(int loveScore, int moneyScore)
	{
		LoveScore += loveScore;
		MoneyScore += moneyScore;

		if( LoveScore > MaxScore ) {
			LoveScore = MaxScore;
		}
		if( MoneyScore > MaxScore ) {
			MoneyScore = MaxScore;
		}

		if( mpLoveGauge && loveScore > 0 ) {
			mpLoveGauge.addValue( (float)loveScore );
		}
		if( mpMoneyGauge && moneyScore > 0 ) {
			mpMoneyGauge.addValue( (float)moneyScore );
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
			// Assert!
			throw new UnityException();
		}
	}

	//! Called When Object is Destoryed
	void OnDestory()
	{
		if( mpInstance ) {
			mpInstance = null;
		}
	}


	// Use this for initialization
	void Start()
	{
		LoveScore = 0;
		MoneyScore = 0;

		mpLoveGauge = GameObject.Find("LoveGauge").GetComponent<GaugeCtrl>();
		mpMoneyGauge = GameObject.Find("MoneyGauge").GetComponent<GaugeCtrl>();
	}
	
	// Update is called once per frame
	void Update()
	{
	}


	//==========================================================================
	// Public Member Variables
	//==========================================================================
	public int LoveScore = 0;
	public int MoneyScore = 0;
	public int MaxScore = 1000;

	//==========================================================================
	// Private Member Variables
	//==========================================================================
	static private ScoreSystem mpInstance = null;

	private GaugeCtrl mpLoveGauge = null;
	private GaugeCtrl mpMoneyGauge = null;
}
