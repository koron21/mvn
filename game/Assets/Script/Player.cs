using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CharacterController))]
public class Player : MonoBehaviour
{
    public Bounds bounds;
    public float speed = 3.0F;
    public float dustThreshold = 0.3f;

    // reference
    private CharacterController controller;
    private GameInput gameInput;
    private GameObject bunbun;
    private GameObject aichan;
    private GameObject basket;

    private AnimationCtrl bunbunAnimation;
    private AnimationCtrl aichanAnimation;

    private EffectCtrl bunbunEffect;
    private EffectCtrl aichanEffect;

	private Game game;
      
    // Use this for initialization
    void Start()
    {
        controller = GetComponent<CharacterController>();
        gameInput = FindObjectOfType<GameInput>();
        bunbun = transform.FindChild("bunbun").gameObject;
        aichan = transform.FindChild("aichan").gameObject;

        bunbunAnimation = bunbun.GetComponent<AnimationCtrl>();
        aichanAnimation = aichan.GetComponent<AnimationCtrl>();

        bunbunEffect = bunbun.transform.FindChild("effect").GetComponent<EffectCtrl>();
        aichanEffect = aichan.transform.FindChild("effect").GetComponent<EffectCtrl>();

		game = FindObjectOfType<Game>();

        StartCoroutine(StartRoutine());
    }

    IEnumerator StartRoutine()
    {
        yield return null;
        bunbunAnimation.Play(AnimationCtrl.AnimationNo.Leg);
        bunbunAnimation.Play(AnimationCtrl.AnimationNo.Blink);
        aichanAnimation.Play(AnimationCtrl.AnimationNo.Leg);
        aichanAnimation.Play(AnimationCtrl.AnimationNo.Blink);
    }

    // Update is called once per frame
    void Update()
    {
        // translation
        Vector3 forward = transform.TransformDirection(Vector3.right);
        float stick = gameInput.GetStick();
        float curSpeed = speed * stick;
        Vector3 end = transform.position + forward * curSpeed * Time.deltaTime;

		if (bunbun.GetComponent<PlayerCtrl>().IsZukkoke == false)
		{
			if (game.IsAfterGame == false) {
		        if (bounds.Contains(end))
		            controller.SimpleMove(forward * curSpeed);
			}
		}

        if (gameInput.GetStick1() * gameInput.GetStick2() < 0)
        {
			if( bunbun.GetComponent<PlayerCtrl>().IsZukkoke == false ) {
				bunbun.GetComponent<PlayerCtrl>().doZukkoke();

				SoundManager.Instance.requestSe("se_hit_01");

				if( Random.Range(0, 2) == 0 ) {
					SoundManager.Instance.requestSe("se_hit_02");
				}
				else {
					SoundManager.Instance.requestSe("se_hit_03");
				}
			}   
        }

		if( bunbun.GetComponent<PlayerCtrl>().IsZukkoke )
		{
			AnimationCtrl.AnimationNo[] bunbunList = 
			{
				AnimationCtrl.AnimationNo.Eye0,
				AnimationCtrl.AnimationNo.Eye1,
				AnimationCtrl.AnimationNo.Eye2
			};
			bunbunAnimation.PlayRandom(bunbunList);
			aichanAnimation.Play(AnimationCtrl.AnimationNo.Eye0);
		}
		else
		{
			bunbunAnimation.Play(AnimationCtrl.AnimationNo.Blink);
            aichanAnimation.Play(AnimationCtrl.AnimationNo.Blink);
        }

        float stickOld = gameInput.GetStickOld();
        if (Mathf.Abs(stick - stickOld) >= dustThreshold)
        {
            bunbunEffect.Play(EffectCtrl.Effect.Dust);
            aichanEffect.Play(EffectCtrl.Effect.Dust);
        }
    }
}
