using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DisplayController : MonoBehaviour
{
    public TeamManager teamManager;
    public GameController gameController;

    public Text textCurrentPlayer;

    public void Update()
    {
        string message;
        if (!gameController.gameOver)
        {
            message = teamManager.activeTeam.player + " is active.";
        }
        else
        {
            message = teamManager.activeTeam.player + " wins!";
        }
        textCurrentPlayer.text = message;
    }
}
