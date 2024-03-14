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
using CarterGames.Assets.LeaderboardManager.Save;
using CarterGames.Common.Serializiation;
using UnityEngine;

namespace CarterGames.Assets.LeaderboardManager
{
    /// <summary>
    /// The data class that stores all the leaderboards. 
    /// </summary>
    [Serializable]
    public sealed class LeaderboardDataStore
    {
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Fields
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        [SerializeField] private SerializableDictionary<string, Leaderboard> leaderboards;
        
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Properties
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        /// <summary>
        /// Gets/Sets all the leaderboards in the store.
        /// </summary>
        public SerializableDictionary<string, Leaderboard> Leaderboards
        {
            get => leaderboards;
            set => leaderboards = value;
        }
        

        /// <summary>
        /// Gets the leaderboard data in a save-able state for JSOn saving.
        /// </summary>
        public SerializableDictionary<string, LeaderboardSaveData> SavableData
        {
            get
            {
                var items = new SerializableDictionary<string, LeaderboardSaveData>();
                
                foreach (var board in Leaderboards)
                {
                    items.Add(board.Key, new LeaderboardSaveData(board.Value));
                }

                return items;
            }
        }
        
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Constructor
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        /// <summary>
        /// Constructor that sets up a new list for the boards to be added to.
        /// </summary>
        public LeaderboardDataStore()
        {
            leaderboards = new SerializableDictionary<string, Leaderboard>();
        }

        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Methods
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        /// <summary>
        /// Updates the leaderboard store with the data that was saved.
        /// </summary>
        /// <param name="data">The data to load.</param>
        public void LoadFromSaveData(SerializableDictionary<string, LeaderboardSaveData> data)
        {
            leaderboards = new SerializableDictionary<string, Leaderboard>();

            if (data == null) return;
            if (data.Count <= 0) return;
            
            foreach (var board in data)
            {
                leaderboards.Add(board.Key, board.Value.ToLeaderboard());
            }
        }
    }
}