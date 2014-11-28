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

	public class SoundStatus
	{
		public float LeftLength;
		public bool IsPlaying;
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
			Destroy(this.gameObject);
			print ("Already Instance is Created!");
			return;
			//throw new UnityException();
		}

		 DontDestroyOnLoad(this);

		mAudioSourceSe = GetComponents<AudioSource>()[0];
		mAudioSourceStream = GetComponents<AudioSource>()[1];
		mAudioSourceStream2 = GetComponents<AudioSource>()[2];

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

		for(int i=0; i<mSoundStatusList.Count; i++) {
			SoundStatus status = mSoundStatusList[i];

			status.LeftLength -= Time.deltaTime;
			if( status.LeftLength <= 0.0f ) {
				status.IsPlaying = false;
				mSoundStatusList.Remove( status );
				i--;
			}
		}
	}
	
	//==========================================================================
	// Public Member Functions
	//==========================================================================
	public SoundStatus requestSe(string name)
	{
		if( mSoundMap.ContainsKey(name) == false ) {
			return null;
		}
		
		AudioClip clip = mSoundMap[name].Resource;
		if(clip == null) {
			return null;
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
			return null;
		}
		
		mAudioSourceSe.PlayOneShot( clip, volume );

		// add list
		SoundStatus status = new SoundStatus();
		status.LeftLength = clip.length;
		status.IsPlaying = true;
		mSoundStatusList.Add( status );

		return status;
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

		mAudioSourceStream.clip = clip;
		mAudioSourceStream.Play();
	}

	public void requestStream2(string name)
	{
		if( mSoundMap.ContainsKey(name) == false ) {
			return;
		}
		
		AudioClip clip = mSoundMap[name].Resource;
		if(clip == null) {
			return;
		}
		
		mAudioSourceStream2.clip = clip;
		mAudioSourceStream2.Play();
	}

	public void stopStream()
	{
		mAudioSourceStream.Stop();
	}

	public void stopStream2()
	{
		mAudioSourceStream2.Stop();
	}
	
	
	//==========================================================================
	// Public Member Variables
	//==========================================================================
	public SoundInfo[] SoundList;
	
	
	//==========================================================================
	// Private Member Variables
	//==========================================================================
	private static SoundManager mpInstance;
	private AudioSource mAudioSourceSe;
	private AudioSource mAudioSourceStream;
	private AudioSource mAudioSourceStream2;
	private Dictionary<string, SoundInfo> mSoundMap = new Dictionary<string, SoundInfo>();
	private Dictionary<string, List<SoundPlay> > mPlayingSoundMap = new Dictionary<string, List<SoundPlay> >();
	private List<SoundStatus> mSoundStatusList = new List<SoundStatus>();
}
