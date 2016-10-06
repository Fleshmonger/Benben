using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIMaster : MonoBehaviour
{
    public Text textCurrentPlayer;
    public GameMaster gameMaster;

    public string GetPlayerString()
    {
        return "(Player " + gameMaster.GetCurrentPlayer() + ")";
    }

    public void Update()
    {
        string message;
        if (!gameMaster.IsGameOver())
        {
            message = gameMaster.GetCurrentTeam().owner + "'s turn " + GetPlayerString();
        }
        else
        {
            message = gameMaster.GetCurrentTeam().owner + " " + GetPlayerString() + " wins the game!";
        }
        textCurrentPlayer.text = message;
    }
}
