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

namespace CarterGames.Assets.LeaderboardManager
{
    /// <summary>
    /// Handles the leaderboard entry display for the Unity text component.
    /// </summary>
    [AddComponentMenu("Carter Games/Leaderboard Manager/Leaderboard Entry Display (Legacy Text)")]
    public sealed class LeaderboardEntryDisplayText : LeaderboardEntryDisplayBase
    {
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Fields
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        [SerializeField] private Text positionLabel;
        [SerializeField] private Text nameLabel;
        [SerializeField] private Text scoreLabel;

        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Properties
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        /// <summary>
        /// Gets if a position reference has been made.
        /// </summary>
        private bool HasPosition => positionLabel != null;
        
        
        /// <summary>
        /// Gets if a name reference has been made.
        /// </summary>
        private bool HasName => nameLabel != null;
        
        
        /// <summary>
        /// Gets if a score reference has been made.
        /// </summary>
        private bool HasScore => scoreLabel != null;
        
        
        /// <summary>
        /// Gets if the display has an entry to display.
        /// </summary>
        public bool IsDisplayingEntry => DisplayingEntryBase != null;
        
        
        /// <summary>
        /// Gets the entry shown on this display.
        /// </summary>
        public LeaderboardEntry DisplayingEntryBase { get; private set; }

        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Methods
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        /// <summary>
        /// Updates the display when called.
        /// </summary>
        /// <param name="entry">The entry to display.</param>
        /// <param name="entryPosition">The position of the entry.</param>
        /// <param name="customisations">The customisations to apply to the display.</param>
        public override void UpdateDisplay(LeaderboardEntry entry, int entryPosition, LeaderboardDisplayCustomisations customisations)
        {
            DisplayingEntryBase = entry;

            if (HasPosition && customisations.UsePosition)
            {
                positionLabel.text = $"{customisations.PositionPrefix}{entryPosition + 1}";
            }
            
            if (HasName)
            {
                nameLabel.text = entry.EntryName;
            }

            if (HasScore)
            {
                switch (entry.EntryType.FullName)
                {
                    case nameof(LeaderboardEntryTime):
                        scoreLabel.text = ((LeaderboardEntryTime) entry).ValueFormatted(customisations.TimeFormat);
                        break;
                    default:
                        scoreLabel.text = entry.EntryValue.ToString();
                        break;
                }
         
            }
            
            gameObject.SetActive(true);
        }


        /// <summary>
        /// Resets the display when called.
        /// </summary>
        public override void ResetDisplay()
        {
            DisplayingEntryBase = null;
            
            if (HasPosition)
            {
                positionLabel.text = string.Empty;
            }
            
            if (HasName)
            {
                nameLabel.text = string.Empty;
            }
            
            if (HasScore)
            {
                scoreLabel.text = string.Empty;
            }
        }
    }
}