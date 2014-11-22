using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SoundManager : MonoBehaviour
{
	[System.Serializable]
	public class SoundInfo
	{
		public string Name;
		public AudioClip Resource;
	};

	//==========================================================================
	// Accessor
	//==========================================================================
	//! Get System Ptr
	static public SoundManager Instance
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
		
		mAudioSource = GetComponent<AudioSource>();
		for(int i=0; i<SoundList.Length; i++) {
			mSoundMap.Add( SoundList[i].Name, SoundList[i] );
			mPlayingSoundMap.Add( SoundList[i].Name, new List<float>() );
		}
	}
	
	//! Called When Object is Destoryed
	void OnDestory()
	{
		if( mpInstance == this ) {
			mpInstance = null;
		}
	}
	
	// Use this for initialization
	void Start()
	{
		
	}
	
	// Update is called once per frame
	void Update()
	{
		foreach( KeyValuePair<string, List<float> > pair in mPlayingSoundMap ) {
			for(int i=0; i<pair.Value.Count; i++) {
				pair.Value[i] -= Time.deltaTime;
				if( pair.Value[i] <= 0.0f ) {
					pair.Value.RemoveAt( i );
					i--;
				}
			}
		}
	}
	
	//==========================================================================
	// Public Member Functions
	//==========================================================================
	public void request(string name)
	{
		if( mSoundMap.ContainsKey(name) == false ) {
			return;
		}
		
		AudioClip clip = mSoundMap[name].Resource;
		if(clip == null) {
			return;
		}

		List<float> playingList = mPlayingSoundMap[name];
		float volume = 1.0f;
		int count = 0;
		while( count < playingList.Count ) {
			volume *= 0.5f;
			count++;
		}
		
		mAudioSource.PlayOneShot( clip, volume );

		playingList.Add(0.1f);
	}
	
	
	//==========================================================================
	// Public Member Variables
	//==========================================================================
	public SoundInfo[] SoundList;
	
	
	//==========================================================================
	// Private Member Variables
	//==========================================================================
	private static SoundManager mpInstance;
	private AudioSource mAudioSource;
	private Dictionary<string, SoundInfo> mSoundMap = new Dictionary<string, SoundInfo>();
	private Dictionary<string, List<float> > mPlayingSoundMap = new Dictionary<string, List<float> >();
}
