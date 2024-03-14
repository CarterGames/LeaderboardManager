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
    /// The entry class structure of the legacy 2.0.x leaderboard data.
    /// </summary>
    [Serializable]
    public sealed class LegacyEntryClass
    {
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Fields
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        [SerializeField, HideInInspector] private double entryID;
        [SerializeField, HideInInspector] private string name;
        [SerializeField, HideInInspector] private double score;
        
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Properties
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        /// <summary>
        /// The entry number for this entry, not the position, but the order of when it was added.
        /// </summary>
        public double EntryID
        {
            get => entryID;
            set => entryID = value;
        }
        
        
        /// <summary>
        /// The name for the entry to the board.
        /// </summary>
        public string Name
        {
            get => name;
            set => name = value;
        }


        /// <summary>
        /// The score for the entry to the board.
        /// </summary>
        public double Score
        {
            get => score;
            set => score = value;
        }
        
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Constructors
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        /// <summary>
        /// Blank constructor with no setup.
        /// </summary>
        public LegacyEntryClass()
        { }

        
        /// <summary>
        /// Constructor with setup for a player name & score.
        /// </summary>
        /// <param name="name">The name for the entry.</param>
        /// <param name="score">The score for the entry.</param>
        public LegacyEntryClass(string name, double score)
        {
            this.name = name;
            this.score = score;
        }
    }
}