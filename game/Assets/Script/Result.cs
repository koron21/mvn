using UnityEngine;
using System.Collections;

public class Result : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // go to title
            Application.LoadLevel(0);
        }
    }
}
