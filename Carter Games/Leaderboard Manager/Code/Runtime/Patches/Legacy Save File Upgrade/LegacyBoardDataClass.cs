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
    /// The data class structure of the legacy 2.0.x leaderboard data.
    /// </summary>
    [Serializable]
    public sealed class LegacyBoardDataClass
    {
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Fields
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */ 
        
        /// <summary>
        /// The id for this leaderboard.
        /// </summary>
        [SerializeField, HideInInspector] private string boardID;
        
        /// <summary>
        /// The entries for this leaderboard.
        /// </summary>
        [SerializeField, HideInInspector] private List<LegacyEntryClass> boardData;

        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Properties
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */ 
        
        /// <summary>
        /// The id of the board this store is for.
        /// </summary>
        public string BoardID
        {
            get => boardID;
            set => boardID = value;
        }

        /// <summary>
        /// The entries for the board that is held on this.
        /// </summary>
        public List<LegacyEntryClass> BoardData
        {
            get => boardData;
            set => boardData = value;
        }
        
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Constructors
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */ 
        
        /// <summary>
        /// Blank constructor which just makes a new data list when used...
        /// </summary>
        public LegacyBoardDataClass()
        {
            boardData = new List<LegacyEntryClass>();
        }

        /// <summary>
        /// Blank constructor which just makes a new data list when used...
        /// </summary>
        public LegacyBoardDataClass(string id)
        {
            boardID = id;
            boardData = new List<LegacyEntryClass>();
        }

        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Methods
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */ 
        
        /// <summary>
        /// Gets an entry based on its placement id.
        /// </summary>
        /// <param name="id">The id to find</param>
        /// <returns>The entry found or null is none were found</returns>
        public LegacyEntryClass GetEntry(double id) => boardData.FirstOrDefault(t => t.EntryID.Equals(id));


        /// <summary>
        /// Gets an entry based on the name & score entered.
        /// </summary>
        /// <param name="name">the name to match</param>
        /// <param name="score">the score to match</param>
        /// <returns>The entry found or null is none were found</returns>
        public LegacyEntryClass GetEntry(string name, double score) =>
            boardData.FirstOrDefault(t => t.Name.Equals(name) && t.Score.Equals(score));
        
        
        /// <summary>
        /// Gets an entry based on the entry data entered.
        /// </summary>
        /// <param name="entry">the entry to find</param>
        /// <returns>The entry found or null is none were found</returns>
        public LegacyEntryClass GetEntry(LegacyEntryClass entry) =>
            boardData.FirstOrDefault(t => t.Name.Equals(entry.Name) && t.Score.Equals(entry.Score));

       
        /// <summary>
        /// Gets the top 3 entries in the board with the lowest score...
        /// </summary>
        /// <returns>The top 3 entries found or null is none were found</returns>
        public LegacyEntryClass[] GetTop3Ascending => boardData.OrderBy(t => t.Score).Take(3).ToArray();
        
        
        /// <summary>
        /// Gets the top 3 entries in the board with the lowest score...
        /// </summary>
        /// <param name="x">The amount to return</param>
        /// <returns>The top x entries found or null is none were found</returns>
        public LegacyEntryClass[] GetTopXAscending(int x) => boardData.OrderBy(t => t.Score).Take(x).ToArray();
        
        
        /// <summary>
        /// Gets all the entries from the lowest score to the highest...
        /// </summary>
        /// <returns>The all entries from the lowest to the highest</returns>
        public LegacyEntryClass[] GetAllAscending => boardData.OrderBy(t => t.Score).ToArray();

        
        /// <summary>
        /// Gets the position of the entry entered...
        /// </summary>
        /// <param name="entry">The entry to find</param>
        /// <returns>The position the entry was found in.</returns>
        public int GetPositionAscending(LegacyEntryClass entry) => GetAllAscending.ToList().FindIndex(t => t.Equals(entry));
        

        /// <summary>
        /// Gets the position of the name & score entered...
        /// </summary>
        /// <param name="name">The name to match</param>
        /// <param name="score">The score to match</param>
        /// <returns>The position the entry was found in.</returns>
        public int GetPositionAscending(string name, double score) => GetAllAscending.ToList().FindIndex(t => t.Name.Equals(name) && t.Score.Equals(score));
        
        
        /// <summary>
        /// Gets the top 3 entries with the highest score...
        /// </summary>
        /// <returns>The top 3 entries found or null is none were found</returns>
        public LegacyEntryClass[] GetTop3Descending => boardData.OrderByDescending(t => t.Score).Take(3).ToArray();
        
        
        /// <summary>
        /// Gets the top x entries with the highest score...
        /// </summary>
        /// <param name="x">The amount to return</param>
        /// <returns>The top x entries found or null is none were found</returns>
        public LegacyEntryClass[] GetTopXDescending(int x) => boardData.OrderByDescending(t => t.Score).Take(x).ToArray();
        
        
        /// <summary>
        /// Gets all the entries from the highest score to the lowest...
        /// </summary>
        /// <returns>The all entries from the lowest to the highest</returns>
        public LegacyEntryClass[] GetAllDescending => boardData.OrderByDescending(t => t.Score).ToArray();
        
        
        /// <summary>
        /// Gets the position of the entry entered...
        /// </summary>
        /// <param name="entry">The entry to find</param>
        /// <returns>The position the entry was found in.</returns>
        public int GetPositionDescending(LegacyEntryClass entry) => GetAllDescending.ToList().FindIndex(t => t.Equals(entry));
        
        
        /// <summary>
        /// Gets the position of the name & score entered...
        /// </summary>
        /// <param name="name">The name to match</param>
        /// <param name="score">The score to match</param>
        /// <returns>The position the entry was found in.</returns>
        public int GetPositionDescending(string name, double score) => GetAllDescending.ToList().FindIndex(t => t.Name.Equals(name) && t.Score.Equals(score));


        /// <summary>
        /// Clears the data for this board...
        /// </summary>
        public void ClearBoard()
        {
            boardData.Clear();
        }

        /// <summary>
        /// Adds an entry to this board...
        /// </summary>
        /// <param name="name">The name to add</param>
        /// <param name="score">The score to add</param>
        public void AddEntry(string name, double score)
        {
            var _entry = new LegacyEntryClass(name, score);
            _entry.EntryID = boardData.Count;
            boardData.Add(_entry);
        }

        /// <summary>
        /// Adds an entry to this board...
        /// </summary>
        /// <param name="entry">The entry to add</param>
        public void AddEntry(LegacyEntryClass entry)
        {
            entry.EntryID = boardData.Count;
            boardData.Add(entry);
        }

        /// <summary>
        /// Deletes an entry from the board...
        /// </summary>
        /// <param name="name">The name to remove</param>
        /// <param name="score">The score to remove</param>
        public void DeleteEntry(string name, double score)
        {
            boardData.Remove(boardData.FirstOrDefault(t =>
                t.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase) && t.Score.Equals(score)));
        }

        /// <summary>
        /// Deletes an entry from the board...
        /// </summary>
        /// <param name="entry">The entry to remove</param>
        public void DeleteEntry(LegacyEntryClass entry)
        {
            boardData.Remove(entry);
        }
    }
}