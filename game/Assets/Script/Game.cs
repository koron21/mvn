using UnityEngine;
using System.Collections;

public class Game : MonoBehaviour
{
	enum GAME_STATE : int
	{
		PRE_START,	//!< kaishi mae
		START,		//!< kaishi ensyutsu
		IN_GAME,	//!< game tyuu
		FINISH,		//!< syuuryou ensyutsu
		END,		//!< syuuryou
	};

    //! @brief game time
    [Range(0, 300)]
    public float
        stageTime;

    //! @brief getter for timer
    public float Timer {
        get {
            return timer;
        }
    }

	//! @brief game state check
	public bool IsInGame {
		get {
			return (state == GAME_STATE.IN_GAME);
		}
	}

	public GameObject StartMesPrefab;
	public GameObject FinishMesPrefab;

    private float timer;
	private GAME_STATE state;
	private int phase;
	private GameObject startMes;
	private GameObject finishMes;
	private GameObject dropSystem;
	private Fade       fade;

	void Awake ()
	{
		fade       = FindObjectOfType<Fade>();
		dropSystem = GameObject.Find ("DropSystem");
		startMes   = null;
		finishMes  = null;

		// hajimaru made ugokasanai
		dropSystem.SetActive(false);
	}

    // Use this for initialization
    void Start ()
    {
		fade.setFadeColor(new Color(1.0f, 1.0f, 1.0f, 1.0f));
		fade.startFadeOut(60);

		timer = 0.0f;
		ChangeState(GAME_STATE.PRE_START);
    }

    // Update is called once per frame
    void Update ()
    {
		switch (state) {
		case GAME_STATE.PRE_START:
			UpdatePreStart();
			break;

		case GAME_STATE.START:
			UpdateStart();
			break;

		case GAME_STATE.IN_GAME:
			UpdateInGame();
			break;
			
		case GAME_STATE.FINISH:
			UpdateFinish();
			break;

		case GAME_STATE.END:
			UpdateEnd();
			break;
		}
    }

	private void ChangeState(GAME_STATE state)
	{
		print ("Change State: " + this.state + " to " + state);
		this.state = state;
		this.phase = 0;
	}

	private void UpdatePreStart()
	{
		// wait for input start button

		switch (phase) {
		case 0:
			if (fade.isEndMove()) {
				timer = 0.0f;
				phase++;
			}
			break;
		case 1:
			timer += Time.deltaTime;
			if (timer >= 1.0f) {
				ChangeState(GAME_STATE.START);
			}
			break;
		}
	}                     

	private void UpdateStart()
	{
		switch (phase) {
		case 0:
			if (StartMesPrefab) {
				startMes = Instantiate(StartMesPrefab) as GameObject;
			}
			phase++;
			break;

		case 1:
			if (startMes == null) {
				timer = stageTime;
				dropSystem.SetActive( true );
				SoundManager.Instance.requestStream("bgm");

				ChangeState(GAME_STATE.IN_GAME);
			}
			break;
		}
	}

	private void UpdateInGame()
	{
		timer -= Time.deltaTime;
		
		if (timer <= 0) {
			dropSystem.SetActive(false);
			ChangeState(GAME_STATE.FINISH);
		}
	}
	
	private void UpdateFinish()
	{
		switch (phase) {
		case 0:
			if (FinishMesPrefab) {
				finishMes = Instantiate(FinishMesPrefab) as GameObject;
			}
			SoundManager.Instance.stopStream();
			phase++;
			break;
			
		case 1:
			if (finishMes == null) {
				ChangeState(GAME_STATE.END);
			}
			break;
		}
	}

	private void UpdateEnd()
	{
		switch (phase) {
		case 0:
			fade.startFadeIn(60);
			phase++;
			break;

		case 1:
			if (fade.isEndMove()) {
				// result
				Application.LoadLevel (2);
			}
			break;
		}
	}
}
