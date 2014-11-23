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

	class SoundPlay
	{
		public float LeftLength;
		public float Volume;
		public bool isUsed;
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
			mPlayingSoundMap.Add( SoundList[i].Name, new List<SoundPlay>() );

			List<SoundPlay> playList = mPlayingSoundMap[ SoundList[i].Name ];
			for(int j=0; j<16; j++) {
				SoundPlay play = new SoundPlay();
				play.LeftLength = 0.0f;
				play.isUsed = false;
				play.Volume = 1.0f * Mathf.Pow (0.5f, j);
				playList.Add( play );
			}
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
		foreach( KeyValuePair<string, List<SoundPlay> > pair in mPlayingSoundMap ) {
			foreach( SoundPlay play in pair.Value ) {
				if( play.isUsed == false ) {
					continue;
				}
				play.LeftLength -= Time.deltaTime;
				if( play.LeftLength <= 0.0f ) {
					play.isUsed = false;
				}
			}
		}
	}
	
	//==========================================================================
	// Public Member Functions
	//==========================================================================
	public void requestSe(string name)
	{
		if( mSoundMap.ContainsKey(name) == false ) {
			return;
		}
		
		AudioClip clip = mSoundMap[name].Resource;
		if(clip == null) {
			return;
		}

		List<SoundPlay> playingList = mPlayingSoundMap[name];
		float volume = 1.0f;
		bool bFound = false;
		foreach(SoundPlay play in playingList) {
			if( play.isUsed == false ) {
				play.isUsed = true;
				play.LeftLength = 0.1f;
				volume = play.Volume;
				bFound = true;
				break;
			}
		}
		if( bFound == false ) {
			return;
		}
		
		mAudioSource.PlayOneShot( clip, volume );
	}

	public void requestStream(string name)
	{
		if( mSoundMap.ContainsKey(name) == false ) {
			return;
		}
		
		AudioClip clip = mSoundMap[name].Resource;
		if(clip == null) {
			return;
		}

		mAudioSource.clip = clip;
		mAudioSource.Play();
	}

	public void stopStream()
	{
		mAudioSource.Stop();
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
	private Dictionary<string, List<SoundPlay> > mPlayingSoundMap = new Dictionary<string, List<SoundPlay> >();
}
