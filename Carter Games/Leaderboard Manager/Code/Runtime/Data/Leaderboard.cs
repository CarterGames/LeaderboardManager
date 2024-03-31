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
using System.Linq;
using UnityEngine;

namespace CarterGames.Assets.LeaderboardManager
{
    /// <summary>
    /// A leaderboard of entries with an id.
    /// </summary>
    [Serializable]
    public sealed class Leaderboard
    {
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Fields
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        [SerializeField] private string boardID;
        [SerializeField] private LeaderboardType type;
        [SerializeField] private List<LeaderboardEntry> boardData;
        
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Properties
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        /// <summary>
        /// The id of the board this store is for.
        /// </summary>
        public string Id
        {
            get => boardID;
            set => boardID = value;
        }
        
        
        /// <summary>
        /// The type the leaderboard is for.
        /// </summary>
        public LeaderboardType Type
        {
            get => type;
            set => type = value;
        }

        
        /// <summary>
        /// The entries for the board that is held on this.
        /// </summary>
        public List<LeaderboardEntry> BoardData
        {
            get => boardData;
            set => boardData = value;
        }

        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Constructors
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        /// <summary>
        /// Makes a new leaderboard based on the entered data.
        /// </summary>
        /// <param name="id">The id to assign.</param>
        /// <param name="type">The type of leaderboard to make.</param>
        public Leaderboard(string id, LeaderboardType type)
        {
            boardID = id;
            this.type = type;
            boardData = new List<LeaderboardEntry>();
        }

        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Methods
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */

        /// <summary>
        /// Gets an entry based on its uuid.
        /// </summary>
        /// <param name="uuid">The uuid to find</param>
        /// <returns>The entry found or null is none were found</returns>
        public LeaderboardEntry GetEntry(string uuid)
        {
            return boardData.FirstOrDefault(t => t.EntryUuid.Equals(uuid));
        } 
        
        
        /// <summary>
        /// Gets an entry based on its placement id.
        /// </summary>
        /// <param name="id">The id to find</param>
        /// <returns>The entry found or null is none were found</returns>
        public LeaderboardEntry GetEntry(double id)
        {
            return boardData.FirstOrDefault(t => t.EntryId.Equals(id));
        } 


        /// <summary>
        /// Gets an entry based on the name & score entered.
        /// </summary>
        /// <param name="name">the name to match</param>
        /// <param name="score">the score to match</param>
        /// <returns>The entry found or null is none were found</returns>
        public LeaderboardEntry GetEntry(string name, object score)
        {
            return boardData.FirstOrDefault(t => t.EntryName.Equals(name) && t.EntryValue.Equals(score));
        }
        
        
        /// <summary>
        /// Gets an entry based on the entry data entered.
        /// </summary>
        /// <param name="entry">the entry to find</param>
        /// <returns>The entry found or null is none were found</returns>
        public LeaderboardEntry GetEntry(LeaderboardEntry entry) 
        {
            return boardData.FirstOrDefault(t => t.EntryName.Equals(entry.EntryName) && t.Equals(entry.EntryValue));
        }

       
        /// <summary>
        /// Gets the top 3 entries in the board with the lowest score...
        /// </summary>
        /// <returns>The top 3 entries found or null is none were found</returns>
        public LeaderboardEntry[] GetTop3Ascending()
        {
            return boardData.OrderBy(t => t.EntryValue).Take(3).ToArray();
        }
        
        
        /// <summary>
        /// Gets the top 3 entries in the board with the lowest score...
        /// </summary>
        /// <param name="x">The amount to return</param>
        /// <returns>The top x entries found or null is none were found</returns>
        public LeaderboardEntry[] GetTopXAscending(int x) 
        {
            return boardData.OrderBy(t => t.EntryValue).Take(x).ToArray();
        }
        
        
        /// <summary>
        /// Gets all the entries from the lowest score to the highest...
        /// </summary>
        /// <returns>The all entries from the lowest to the highest</returns>
        public LeaderboardEntry[] GetAllAscending()
        {
            return boardData.OrderBy(t => t.EntryValue).ToArray();
        }
        
        
        /// <summary>
        /// Gets the top 3 entries with the highest score...
        /// </summary>
        /// <returns>The top 3 entries found or null is none were found</returns>
        public LeaderboardEntry[] GetTop3Descending() 
        {
            return boardData.OrderByDescending(t => t.EntryValue).Take(3).ToArray();
        }
        
        
        /// <summary>
        /// Gets the top x entries with the highest score...
        /// </summary>
        /// <param name="x">The amount to return</param>
        /// <returns>The top x entries found or null is none were found</returns>
        public LeaderboardEntry[] GetTopXDescending(int x) 
        {
            return boardData.OrderByDescending(t => t.EntryValue).Take(x).ToArray();
        }
        
        
        /// <summary>
        /// Gets all the entries from the highest score to the lowest...
        /// </summary>
        /// <returns>The all entries from the lowest to the highest</returns>
        public LeaderboardEntry[] GetAllDescending() 
        {
            return boardData.OrderByDescending(t => t.EntryValue).ToArray();
        }

        
        /// <summary>
        /// Adds an entry to this board...
        /// </summary>
        /// <param name="entry">The entry to add</param>
        public void AddEntry(LeaderboardEntry entry)
        {
            entry.EntryId = boardData.Count;
            boardData.Add(entry);
        }

        
        /// <summary>
        /// Deletes an entry from the board...
        /// </summary>
        /// <param name="name">The name to remove</param>
        /// <param name="score">The score to remove</param>
        public void DeleteEntry(string name, object score)
        {
            boardData.Remove(boardData.FirstOrDefault(t => t.EntryName.Equals(name, StringComparison.InvariantCultureIgnoreCase) && t.EntryValue.Equals(score)));
        }

        
        /// <summary>
        /// Deletes an entry from the board...
        /// </summary>
        /// <param name="entry">The entry to remove</param>
        public void DeleteEntry(LeaderboardEntry entry)
        {
            boardData.Remove(entry);
        }
        
        
        /// <summary>
        /// Clears the data for this board...
        /// </summary>
        public void ClearBoard()
        {
            boardData.Clear();
        }
    }
}