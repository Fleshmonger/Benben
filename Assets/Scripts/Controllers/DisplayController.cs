using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DisplayController : MonoBehaviour
{
    public TeamsManager teamsManager;
    public GameController gameController;

    public Text textCurrentPlayer;

    public void Update()
    {
        string message;
        if (!gameController.gameOver)
        {
            message = teamsManager.activeTeam.player + " is active.";
        }
        else
        {
            message = teamsManager.activeTeam.player + " wins!";
        }
        textCurrentPlayer.text = message;
    }
}
