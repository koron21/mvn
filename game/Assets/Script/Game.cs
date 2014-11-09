using UnityEngine;
using System.Collections;

public class Game : MonoBehaviour
{
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

    private float timer;

    // Use this for initialization
    void Start()
    {
        timer = stageTime;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0)
            {
                // result
                Application.LoadLevel(2);
            }
    }
}
