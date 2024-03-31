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
using System.Collections.Generic;
using UnityEngine;

namespace CarterGames.Assets.LeaderboardManager.Save
{
    [Serializable]
    public class LeaderboardSaveData
    {
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Fields
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */ 
        
        [SerializeField] private string id;
        [SerializeField] private LeaderboardType type;
        [SerializeField] private List<string> entriesJson;

        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Constructors
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */ 
        
        /// <summary>
        /// Makes a new save data with the entered leaderboard.
        /// </summary>
        /// <param name="leaderboard">The board to store.</param>
        public LeaderboardSaveData(Leaderboard leaderboard)
        {
            id = leaderboard.Id;
            type = leaderboard.Type;
            entriesJson = new List<string>();
            
            foreach (var v in leaderboard.BoardData)
            {
                entriesJson.Add(JsonUtility.ToJson(v));
            }
        }

        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Methods
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */ 
        
        /// <summary>
        /// Converts the save data of a leaderboard into a usable leaderboard.
        /// </summary>
        /// <returns>A leaderboard from the save dat in this class instance.</returns>
        public Leaderboard ToLeaderboard()
        {
            var board = new Leaderboard(id, type);
            board.BoardData = new List<LeaderboardEntry>();

            foreach (var entry in entriesJson)
            {
                LeaderboardEntry parsedEntry = null;
                
                switch (type)
                {
                    case LeaderboardType.Score:
                        parsedEntry = JsonUtility.FromJson<LeaderboardEntryScore>(entry);
                        break;
                    case LeaderboardType.Time:
                        parsedEntry = JsonUtility.FromJson<LeaderboardEntryTime>(entry);
                        break;
                }
                
                board.BoardData.Add(parsedEntry);
            }

            return board;
        }
    }
}