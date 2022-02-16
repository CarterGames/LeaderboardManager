using UnityEngine;
using UnityEngine.UI;

namespace CarterGames.Assets.LeaderboardManager.Demo
{
    public class ExampleManager : MonoBehaviour
    {
        [SerializeField] private Text boardID;
        [SerializeField] private Text playerName;
        [SerializeField] private Text playerScore;
        
        
        public void AddToBoard()
        {
            if (playerName.text.Length <= 0 || playerScore.text.Length <= 0)
            {
                Debug.Log($"<color=D6BA64><b>Leaderboard Manager</b> | Either the name or score fields were blank, please ensure the fields are filled before using this option.</color>");
                return;
            }
            
            LeaderboardManager.AddEntryToBoard(boardID.text, playerName.text, double.Parse(playerScore.text));
        }
        
        public void RemoveFromBoard()
        {
            if (playerName.text.Length <= 0 || playerScore.text.Length <= 0)
            {
                Debug.Log($"<color=D6BA64><b>Leaderboard Manager</b> | Either the name or score fields were blank, please ensure the fields are filled before using this option.</color>");
                return;
            }
                
            LeaderboardManager.DeleteEntryFromBoard(boardID.text, playerName.text, double.Parse(playerScore.text));
        }

        public void ClearBoard()
        {
            LeaderboardManager.ClearLeaderboard(boardID.text);
        }
    }
}