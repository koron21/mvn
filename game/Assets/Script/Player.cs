using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CharacterController))]
public class Player : MonoBehaviour
{
    public float speed = 3.0F;

    private CharacterController controller;
    private GameInput gameInput;

    // Use this for initialization
    void Start()
    {
        controller = GetComponent<CharacterController>();
        gameInput = FindObjectOfType<GameInput>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 forward = transform.TransformDirection(Vector3.right);
        float curSpeed = speed * gameInput.GetStick();
        controller.SimpleMove(forward * curSpeed);
    }
}
