using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CharacterController))]
public class Player : MonoBehaviour
{
    public float speed = 3.0F;

    // reference
    private CharacterController controller;
    private GameInput gameInput;
    private GameObject bunbun;
    private GameObject aichan;
    private GameObject basket;

    private AnimationCtrl bunbunAnimation;
    private AnimationCtrl aichanAnimation;

    
    // Use this for initialization
    void Start()
    {
        controller = GetComponent<CharacterController>();
        gameInput = FindObjectOfType<GameInput>();
        bunbun = transform.FindChild("bunbun").gameObject;
        aichan = transform.FindChild("aichan").gameObject;

        bunbunAnimation = bunbun.GetComponent<AnimationCtrl>();
        aichanAnimation = aichan.GetComponent<AnimationCtrl>();

        bunbunAnimation.Play(AnimationCtrl.AnimationNo.Leg);
        bunbunAnimation.Play(AnimationCtrl.AnimationNo.Blink);
        aichanAnimation.Play(AnimationCtrl.AnimationNo.Leg);
    }

    // Update is called once per frame
    void Update()
    {
        // translation
        Vector3 forward = transform.TransformDirection(Vector3.right);
        float curSpeed = speed * gameInput.GetStick();
        controller.SimpleMove(forward * curSpeed);

        if (gameInput.GetStick1() * gameInput.GetStick2() < 0)
            bunbunAnimation.Play(AnimationCtrl.AnimationNo.Eye0);
    }
}
