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

using System;
using UnityEngine;

namespace CarterGames.Assets.LeaderboardManager
{
    /// <summary>
    /// A leaderboard entry with a double as the value.
    /// </summary>
    [Serializable]
    public sealed class LeaderboardEntryScore : LeaderboardEntry
    {
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Fields
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */ 
        
        [SerializeField] private string entryUuid;
        [SerializeField] private double entryId;
        [SerializeField] private string entryName;
        [SerializeField] private double entryScore;
        
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Properties
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */ 
        
        /// <summary>
        /// The type the entry is.
        /// </summary>
        public override Type EntryType => typeof(LeaderboardEntryScore);
        
        
        /// <summary>
        /// The Uuid of the entry.
        /// </summary>
        public override string EntryUuid
        {
            get => entryUuid;
            set => entryUuid = value;
        }
        
        
        /// <summary>
        /// The number the entry is in the board.
        /// </summary>
        public override double EntryId 
        {
            get => entryId;
            set => entryId = value;
        }


        /// <summary>
        /// The name for the entry.
        /// </summary>
        public override string EntryName
        {
            get => entryName;
            set => entryName = value;
        }

        
        /// <summary>
        /// The value for the entry.
        /// </summary>
        public override object EntryValue
        {
            get => entryScore;
            set => entryScore = (double) value;
        }
        
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Constructor
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        /// <summary>
        /// Creates a new entry with the name and score value provided.
        /// </summary>
        /// <param name="name">The name to set.</param>
        /// <param name="score">The score value to set.</param>
        public LeaderboardEntryScore(string name, double score)
        {
            TryGenerateUuid();
            
            EntryName = name;
            EntryValue = score;
        }
    }
}