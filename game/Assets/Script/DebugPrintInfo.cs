using UnityEngine;
using System.Collections;

[RequireComponent(typeof(GUIText))]
public class DebugPrintInfo : MonoBehaviour
{
    private Game game;

    // Use this for initialization
    void Start()
    {
		game = FindObjectOfType<Game>();
    }

    // Update is called once per frame
    void Update()
    {
        guiText.text = "Stage Time: " + game.Timer.ToString("F02");
    }
}
