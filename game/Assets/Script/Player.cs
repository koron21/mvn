using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CharacterController))]
public class Player : MonoBehaviour
{
    public float speed = 3.0F;

    private CharacterController controller;

    // Use this for initialization
    void Start ()
    {
        controller = GetComponent<CharacterController>();
    }

	// Update is called once per frame
    void Update ()
    {
        Vector3 forward = transform.TransformDirection(Vector3.right);
        float curSpeed = speed * Input.GetAxis("Horizontal");
        controller.SimpleMove(forward * curSpeed);
    }
}
