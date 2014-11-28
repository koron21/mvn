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
		BONUS,		//!< bonus time
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
	public bool IsBonus {
		get {
			return (state == GAME_STATE.BONUS);
		}
	}
	public bool IsAfterGame {
		get {
			return (state > GAME_STATE.IN_GAME);
		}
	}

	public float AlertTime;
	public GameObject StartMesPrefab;
	public GameObject FinishMesPrefab;
	public GameObject BonusMesPrefab;

    private float timer;
	private GAME_STATE state;
	private int phase;
	private GameObject startMes;
	private GameObject finishMes;
	private GameObject bonusMes;
	private GameObject dropSystem;
	private GameObject controllerInfo;
	private GUIText    controllerInfoText;
	private Fade       fade;

	private int			bonusNum = 0;
	private int			bonusMoneyNum = 0;
	private int			bonusLoveNum = 0;
	private float		bonusWait = 0.0f;

	private SoundManager.SoundStatus hurrySeStatus;

	void Awake ()
	{
		fade       = FindObjectOfType<Fade>();
		dropSystem = GameObject.Find ("DropSystem");
		startMes   = null;
		finishMes  = null;

		// controller info setting
		controllerInfo = GameObject.Find ("ControllerInfo");
		controllerInfoText = controllerInfo.GetComponent<GUIText>();
		controllerInfoText.fontSize = (int)(70.0f * (float)Screen.width / 1280.0f);
		controllerInfoText.color    = new Color(1.0f, 1.0f, 1.0f, 0.0f);
		controllerInfo.SetActive(false);

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

		case GAME_STATE.BONUS:
			UpdateBonus();
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
		// wait for input space key
		switch (phase) {
		case 0:
			if (fade.isEndMove()) {
				controllerInfo.SetActive(true);
				phase++;
			}
			break;

		case 1:
		{
			Color temp = new Color(controllerInfoText.color.r, 
			                       controllerInfoText.color.g, 
			                       controllerInfoText.color.b, 
			                       controllerInfoText.color.a);
			temp.a += 1.0f * Time.deltaTime / 0.25f;
			if (temp.a >= 1.0f) {
				temp.a = 1.0f;
				//SoundManager.Instance.requestStream("bgm_desc");
				phase++;
			}
			controllerInfoText.color = temp;

			break;
		}

		case 2:
			if (Input.GetKeyDown(KeyCode.Space)) {
				SoundManager.Instance.stopStream();
				phase++;
			}
			break;
		
		case 3:
		{
			Color temp = new Color(controllerInfoText.color.r, 
			                       controllerInfoText.color.g, 
			                       controllerInfoText.color.b, 
			                       controllerInfoText.color.a);
			temp.a -= 1.0f * Time.deltaTime / 0.5f;
			if (temp.a <= 0.0f) {
				temp.a = 0.0f;
				
				controllerInfo.SetActive(false);
				ChangeState(GAME_STATE.START);
			}
			controllerInfoText.color = temp;

			break;
		}

		}
	}                     

	private void UpdateStart()
	{ 
		switch (phase) {
		case 0:
			if (StartMesPrefab) {
				startMes = Instantiate(StartMesPrefab) as GameObject;
				SoundManager.Instance.requestSe("telop_start_01");
				SoundManager.Instance.requestStream("bgm");
			}
			phase++;
			break;

		case 1:
			if (startMes == null) {
				timer = 0.25f; // tyotto mattekara game start suru
				phase++;
			}
			break;

		case 2:
			timer -= Time.deltaTime;
			if (timer <= 0.0f) {
				timer = stageTime;
				dropSystem.SetActive( true );
				//SoundManager.Instance.requestStream("bgm");
				
				ChangeState(GAME_STATE.IN_GAME);
			}
			break;
		}
	}

	private void UpdateInGame()
	{
		timer -= Time.deltaTime;
	
		switch(phase) {
		case 0:
			if (timer <= AlertTime) {
				SoundManager.Instance.stopStream();
				hurrySeStatus = SoundManager.Instance.requestSe("telop_hurry_01");
				phase++;
			}
			break;
		case 1:
			if( hurrySeStatus == null || hurrySeStatus.IsPlaying == false ) {
				hurrySeStatus = null;
				SoundManager.Instance.requestStream("bgm_hurry");
				phase++;
			}
			break;
		}

		if (timer <= 0) {
			dropSystem.SetActive(false);
			ChangeState(GAME_STATE.FINISH);
		}
	}
	
	private void UpdateFinish()
	{
		switch (phase) {
		case 0:
			SoundManager.Instance.stopStream();

			if (FinishMesPrefab) {
				finishMes = Instantiate(FinishMesPrefab) as GameObject;
				SoundManager.Instance.requestSe("telop_finish_01");
			}
			phase++;
			break;
			
		case 1:
			if (finishMes == null) {
				if (ScoreSystem.Instance.LoveScore < ScoreSystem.Instance.MaxScore ||
				    ScoreSystem.Instance.MoneyScore < ScoreSystem.Instance.MaxScore) {

					bonusMoneyNum = (ScoreSystem.Instance.MaxScore - ScoreSystem.Instance.MoneyScore + 99) / 100;
					bonusLoveNum  = (ScoreSystem.Instance.MaxScore - ScoreSystem.Instance.LoveScore + 99) / 100;
					bonusNum      = 0;

					ChangeState(GAME_STATE.BONUS);
				}
				else {
					ChangeState(GAME_STATE.END);
				}
			}
			break;
		}
	}

	private void UpdateBonus()
	{
		switch (phase) {
		case 0:
			SoundManager.Instance.requestSe("se_bonus_01");
			SoundManager.Instance.requestStream("bgm_bonus");
			bonusMes = Instantiate(BonusMesPrefab) as GameObject;
			phase++;
			break;

		case 1:
			if (bonusLoveNum == 0 && bonusMoneyNum == 0) {
				timer = 4.0f;
				phase++;
			}
			else {
				if( bonusWait <= 0.0f ) {
					bonusWait = 0.1f;
					bonusNum = Random.Range(0, 2);

					if ( bonusLoveNum > 0 && (bonusMoneyNum == 0 || (bonusNum % 2) == 0) ) {
						DropSystem.Instance.generateBonusDrop(DropSystem.DROP_OBJECT.HEART);
						bonusLoveNum--;
						break;
					}
					if ( bonusMoneyNum > 0 && (bonusLoveNum == 0 || (bonusNum % 2) == 1) ) {
						DropSystem.Instance.generateBonusDrop(DropSystem.DROP_OBJECT.MILLION);
						bonusMoneyNum--;
						break;
					}
				}
				else {
					bonusWait -= Time.deltaTime;
				}
			}
			break;

		case 2:
			timer -= Time.deltaTime;
			if (timer <= 0.0f) {
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
