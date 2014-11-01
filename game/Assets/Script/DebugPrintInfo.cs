using UnityEngine;
using System.Collections;

[RequireComponent(typeof(GUIText))]
public class DebugPrintInfo : MonoBehaviour
{
    public Game game;

    private GUIText _guiText;

    // Use this for initialization
    void Start()
    {
        _guiText = GetComponent<GUIText>();
    }

    // Update is called once per frame
    void Update()
    {
        _guiText.text = "Stage Time: " + game.Timer.ToString("F02");
    }
}
