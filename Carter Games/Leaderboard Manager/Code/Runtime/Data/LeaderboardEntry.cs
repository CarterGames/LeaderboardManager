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
    /// Holds the data for a leaderboard entry.
    /// </summary>
    [Serializable]
    public class LeaderboardEntry
    {
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Properties
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        /// <summary>
        /// The type the entry is.
        /// </summary>
        public virtual Type EntryType { get; }
        
        
        /// <summary>
        /// The Uuid of the entry.
        /// </summary>
        public virtual string EntryUuid { get; set; }
        
        
        /// <summary>
        /// The number the entry is in the board.
        /// </summary>
        public virtual double EntryId { get; set; }
        
        
        /// <summary>
        /// The name for the entry.
        /// </summary>
        public virtual string EntryName { get; set; }
        
        
        /// <summary>
        /// The value for the entry.
        /// </summary>
        public virtual object EntryValue { get; set; }

        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Methods
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        /// <summary>
        /// Tries to generate a uuid for the entry if one doesn't exist (only used for legacy entries).
        /// </summary>
        public void TryGenerateUuid()
        {
            if (!string.IsNullOrEmpty(EntryUuid)) return;
            EntryUuid = Guid.NewGuid().ToString();
        }


        /// <summary>
        /// Resets the entry to default values.
        /// </summary>
        public void Reset()
        {
            EntryName = string.Empty;
            EntryValue = null;
        }
        

        /// <summary>
        /// Gets the data of the entry in a save-able state.
        /// </summary>
        /// <returns>The data in a save-able state.</returns>
        public string GetSaveData()
        {
            return JsonUtility.ToJson(this);
        }
    }
}