using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CharacterController))]
public class Player : MonoBehaviour
{
    public float speed = 3.0F;
    public bool linkAnimationSpeedWithMotion;

    // reference
    private CharacterController controller;
    private GameInput gameInput;
    private GameObject bunbun;
    private GameObject aichan;
    private GameObject basket;

    // Use this for initialization
    void Start()
    {
        controller = GetComponent<CharacterController>();
        gameInput = FindObjectOfType<GameInput>();
        bunbun = transform.FindChild("bunbun").gameObject;
        aichan = transform.FindChild("aichan").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        // translation
        Vector3 forward = transform.TransformDirection(Vector3.right);
        float curSpeed = speed * gameInput.GetStick();
        controller.SimpleMove(forward * curSpeed);

        // animation
        if (linkAnimationSpeedWithMotion)
        {
            bunbun.SendMessage("SetAnimationSpeed", Mathf.Abs(gameInput.GetStick1()));
            aichan.SendMessage("SetAnimationSpeed", Mathf.Abs(gameInput.GetStick2()));
        }
        else 
        {   
            bunbun.SendMessage("SetAnimationSpeed", 1.0f);
            aichan.SendMessage("SetAnimationSpeed", 1.0f);
        }
    }
}
