/*
 * Copyright (c) 2024 Carter Games
 *
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 *
 * The above copyright notice and this permission notice shall be included in
 * all copies or substantial portions of the Software.
 *
 *
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NON-INFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
 * THE SOFTWARE.
 */

using UnityEngine;
using UnityEngine.UI;

namespace CarterGames.Assets.LeaderboardManager.Demo
{
    /// <summary>
    /// Handles the example scene logic for the leaderboard manager asset.
    /// </summary>
    public sealed class ExampleManager : MonoBehaviour
    {
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Fields
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        [SerializeField] private InputField boardId;
        [SerializeField] private InputField playerName;
        [SerializeField] private InputField playerScore;

        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Methods
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        /// <summary>
        /// Called by example scene button to add an entry to the example board.
        /// </summary>
        public void AddToBoard()
        {
            if (string.IsNullOrEmpty(playerName.text) || string.IsNullOrEmpty(playerScore.text))
            {
                LbmLogs.Normal("[DEMO]: Either the name or score fields were blank, please ensure the fields are filled before using this option.");
                return;
            }
            
            LeaderboardManager.AddEntryToBoard(boardId.text, new LeaderboardEntryScore(playerName.text, double.Parse(playerScore.text)));
            playerName.text = string.Empty;
            playerScore.text = string.Empty;
        }
        
        
        /// <summary>
        /// Called by example scene button to remove an entry to the example board.
        /// </summary>
        public void RemoveFromBoard()
        {
            if (string.IsNullOrEmpty(playerName.text) || string.IsNullOrEmpty(playerScore.text))
            {
                LbmLogs.Normal("[DEMO]: Either the name or score fields were blank, please ensure the fields are filled before using this option.");
                return;
            }
                
            LeaderboardManager.DeleteEntryFromBoard(boardId.text, playerName.text, double.Parse(playerScore.text));
            playerName.text = string.Empty;
            playerScore.text = string.Empty;
        }

        
        /// <summary>
        /// Called by example scene button to clear the example board of entries.
        /// </summary>
        public void ClearBoard()
        {
            LeaderboardManager.ClearLeaderboard(boardId.text);
        }
    }
}