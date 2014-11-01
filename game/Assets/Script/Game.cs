using UnityEngine;
using System.Collections;

public class Game : MonoBehaviour
{
    //! @brief game time
    [Range(0, 300)]
    public float stageTime;

    //! @brief getter for timer
    public float Timer
    {
        get
        {
            return _timer;
        }
    }
    private float _timer;

    // Use this for initialization
    void Start()
    {
        _timer = stageTime;
    }

    // Update is called once per frame
    void Update()
    {
        _timer -= Time.deltaTime;

        if (_timer <= 0)
        {
            // result
            Application.LoadLevel(2);
        }
    }
}
