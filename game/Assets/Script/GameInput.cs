using UnityEngine;
using System.Collections;

[System.Serializable]
public class PlayerInputSettings
{
    public PlayerInputSettings(string jan, string kan, KeyCode bb, KeyCode bk)
    {
        Apply(jan, kan, bb, bk);
    }

    public void Apply(string jan, string kan, KeyCode bb, KeyCode bk)
    {
        this.joystickAxisName = jan;
        this.keyboardAxisName = kan;
        this.boostButton = bb;
        this.boostKey = bk;
    }

    public string joystickAxisName;
    public string keyboardAxisName;
    public KeyCode boostButton;
    public KeyCode boostKey;
}

public class GameInput : MonoBehaviour
{
    // Info - Read Only
    public string[] joystickNames;
    public float stick;
    public float stickOld;

    public float stick1;
    [Range(1.0f, 2.0f)]
    public float boost1;
    public float stick2;
    [Range(1.0f, 2.0f)]
    public float boost2;

    [Range(-1.0f, 1.0f)]
    public float balance;
    [Range(0.0f, 0.5f)]
    public float boostAmount = 0.1f;
    [Range(0.0f, 0.5f)]
    public float boostTime = 0.5f;

    public PlayerInputSettings p1Settings;
    public PlayerInputSettings p2Settings;
    [Range(0.0f, 1.0f)]
    public float deadZone;

    void DefaultSettings()
    {
        // for player 1
        p1Settings.joystickAxisName = "Player1-Joystick";
        p1Settings.keyboardAxisName = "Player1-Keyboard";
        p1Settings.boostButton = KeyCode.Joystick1Button0;
        p1Settings.boostKey = KeyCode.Space;

        // for player 2
        p1Settings.joystickAxisName = "Player2-Joystick";
        p1Settings.keyboardAxisName = "Player2-Keyboard";
        p1Settings.boostButton = KeyCode.Joystick2Button0;
        p1Settings.boostKey = KeyCode.Return;
    }

    void Start()
    {
        joystickNames = Input.GetJoystickNames();
        boost1 = 1.0f;
        boost2 = 1.0f;
    }

    void Update()
    {
        stickOld = stick;
        stick = GetStick();
        stick1 = GetStick1();
        stick2 = GetStick2();

        // boost for player 1
        if (Input.GetKeyDown(p1Settings.boostKey) 
            || Input.GetKeyDown(p1Settings.boostButton))
        {
            StartCoroutine(BoostPlayer1());
        }

        // boost for player 2
        if (Input.GetKeyDown(p2Settings.boostKey) 
            || Input.GetKeyDown(p2Settings.boostButton))
        {
            StartCoroutine(BoostPlayer2());
        }
    }

    private IEnumerator BoostPlayer1()
    {
        boost1 += boostAmount;
        yield return new WaitForSeconds(boostTime);
        boost1 -= boostAmount;
    }
    private IEnumerator BoostPlayer2()
    {
        boost2 += boostAmount;
        yield return new WaitForSeconds(boostTime);
        boost2 -= boostAmount;
    }

    public float GetStick()
    {
        return (balance + 1.0f) * stick1 * boost1 
            + (1.0f - balance) * stick2 * boost2;
    }

    public float GetStick1()
    {
        return GetStick(p1Settings.joystickAxisName,
                        p1Settings.keyboardAxisName);
    }

    public float GetStickOld()
    {
        return stickOld;
    }

    public float GetStick2()
    {
        return GetStick(p2Settings.joystickAxisName,
                        p2Settings.keyboardAxisName);
    }

    private float GetStick(string joystick, string keyboard)
    {
        /*
        if (Input.GetJoystickNames().Length != 0)
            return Input.GetAxis(joystick);
        else
            return Input.GetAxis(keyboard);
        */
        float ret = Input.GetAxis(joystick) + Input.GetAxis(keyboard);
        if (Mathf.Abs(ret) < deadZone)
            ret = 0;
        return ret;
    }
}
