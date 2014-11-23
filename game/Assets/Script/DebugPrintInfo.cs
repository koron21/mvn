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
        if (!Debug.isDebugBuild)
            return;

        guiText.text = "Stage Time: " + game.Timer.ToString("F02") + "\n"
            + "Player1\n"
            + "==============\n"
            + "move left: a\n" 
            + "move right: d\n"
            + "boost: space\n"
            + "\n"
            + "Player2\n"
            + "==============\n"
            + "move left: left\n" 
            + "move right: right\n"
            + "boost: boost\n";
    }
}
