using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Gameplay;

namespace Interface
{
    public class DisplayController : MonoBehaviour
    {
        public Text textCurrentPlayer;

        public void Update()
        {
            var activePlayer = TeamManager.Instance.activeTeam.player;
            string message;
            if (!GameMaster.Instance.isGameOver)
            {
                message = activePlayer + " is active.";
            }
            else
            {
                message = activePlayer + " wins!";
            }
            textCurrentPlayer.text = message;
        }
    }
}