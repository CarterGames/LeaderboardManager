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

using CarterGames.Assets.LeaderboardManager.Save;
using CarterGames.Common.Serializiation;
using UnityEngine;

namespace CarterGames.Assets.LeaderboardManager
{
    /// <summary>
    /// The main manager class for the leaderboard manager asset.
    /// </summary>
    public static class LeaderboardManager
    {
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Fields
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        private static LeaderboardDataStore data;
        private static ISaveLocation saveLocation;
        
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Properties
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */

        /// <summary>
        /// Gets if the manager is initialized.
        /// </summary>
        public static bool IsInitialized { get; private set; }

        
        /// <summary>
        /// Gets the number of boards saved in the manager.
        /// </summary>
        public static int NumberOfBoards
        {
            get
            {
                if (!IsInitialized)
                {
                    Initialize();
                }

                return data.Leaderboards.Count;
            }
        }


        /// <summary>
        /// Gets all the leaderboards currently stored.
        /// </summary>
        public static SerializableDictionary<string, Leaderboard> AllLeaderboards => data.Leaderboards;


        /// <summary>
        /// Gets the save location to use with the manager.
        /// </summary>
        private static ISaveLocation SaveLocation
        {
            get
            {
                switch (UtilRuntime.Settings.SaveLocation)
                {
                    case LeaderboardSaveLocation.LocalFile:
                        saveLocation = new FileSaveLocation();
                        return saveLocation;
                    default:
                        break;
                }

                return saveLocation;
            }
        }
        
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Methods
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */

        /// <summary>
        /// Initializes the manager by loading the data currently saved ready for use.
        /// </summary>
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void Initialize()
        {
            if (IsInitialized) return;
            Load();
            IsInitialized = true;
        }
        
        
        /// <summary>
        /// Checks to see if the board of the id entered exists in the system.
        /// </summary>
        /// <param name="boardId">The id the board to check for.</param>
        /// <returns>Bool</returns>
        public static bool BoardExists(string boardId)
        {
            return data.Leaderboards.ContainsKey(boardId);
        }
        

        /// <summary>
        /// Creates a new leaderboard with the id entered...
        /// </summary>
        /// <param name="boardId">The id the board should be defined as</param>
        /// <param name="boardType">The type of board to make.</param>
        public static void CreateLeaderboard(string boardId, LeaderboardType boardType)
        {
            if (data.Leaderboards.Count <= 0)
            {
                var firstBoard = new Leaderboard(boardId, boardType);
            
                data.Leaderboards.Add(firstBoard.Id, firstBoard);
                
                Save();
                return;
            }
            
            if (BoardExists(boardId))
            {
                LbmLogger.Normal($"[Leaderboard Manager] Unable to create leaderboard with the ID {boardId} as a board of this ID already exists.");
                return;
            }

            var newBoard = new Leaderboard(boardId, boardType);
            
            data.Leaderboards.Add(newBoard.Id, newBoard);
            
            Save();
        }


        /// <summary>
        /// Gets the leaderboard of the entered id or creates it if it doesn't exist.
        /// </summary>
        /// <param name="boardId">The id the board should be defined as or gotten</param>
        /// <param name="boardType">The type of board to make.</param>
        /// <returns>The board either found or just created.</returns>
        public static Leaderboard CreateOrGetLeaderboard(string boardId, LeaderboardType boardType)
        {
            if (BoardExists(boardId))
            {
                return GetLeaderboard(boardId);
            }

            CreateLeaderboard(boardId, boardType);
            return GetLeaderboard(boardId);
        }


        /// <summary>
        /// Deletes the leaderboard of the id entered...
        /// </summary>
        /// <param name="boardId">The board id to find and delete</param>
        public static void DeleteLeaderboard(string boardId)
        {
            if (data.Leaderboards.Count <= 0) return;

            if (!BoardExists(boardId))
            {
                LbmLogger.Normal($"[Leaderboard Manager] Unable to delete leaderboard with the ID {boardId} as there are no board with this ID saved.");
                return;
            }
            
            data.Leaderboards.Remove(boardId);
            
            Save();
        }
        
        
        /// <summary>
        /// Clears the leaderboard of the id entered...
        /// </summary>
        /// <param name="boardId">The board id to find and delete</param>
        public static void ClearLeaderboard(string boardId)
        {
            if (!BoardExists(boardId))
            {
                LbmLogger.Normal($"[Leaderboard Manager] Unable to clear leaderboard with the ID {boardId} as there are no board with this ID saved.");
                return;
            }
            
            data.Leaderboards[boardId]?.ClearBoard();
            
            Save();
        }


        /// <summary>
        /// Clears all the leaderboards stored in the asset when called.
        /// </summary>
        public static void ClearAllLeaderboards()
        {
            foreach (var board in data.Leaderboards)
            {
                board.Value.ClearBoard();
            }
            
            Save();
        }


        /// <summary>
        /// Calls to save all leaderboards in the game...
        /// </summary>
        public static void Save()
        {
            SaveLocation.Save(JsonUtility.ToJson(data.SavableData));
        }
        

        /// <summary>
        /// Loads the data locally for the system to use...
        /// </summary>
        public static void Load()
        {
            if (data == null)
            {
                data = new LeaderboardDataStore();
            }
            
            data.LoadFromSaveData(JsonUtility.FromJson<SerializableDictionary<string, LeaderboardSaveData>>(SaveLocation.Load()));
        }
        
        
        /// <summary>
        /// Gets the leaderboard of the ID entered...
        /// </summary>
        /// <param name="boardId">The board ID to find</param>
        /// <returns>The data of the board or null if none were found</returns>
        public static Leaderboard GetLeaderboard(string boardId)
        {
            if (BoardExists(boardId))
            {
                return data.Leaderboards[boardId];
            }
    
            LbmLogger.Normal($"[Leaderboard Manager] Unable to find leaderboard with the ID {boardId} as a board of this ID doesn't exists.");
            return null;
        }
        

        /// <summary>
        /// Adds an entry to the board defined...
        /// </summary>
        /// <param name="boardId">The board to add to</param>
        /// <param name="entry">the data to add</param>
        public static void AddEntryToBoard(string boardId, LeaderboardType type, LeaderboardEntry entry)
        {
            if (!BoardExists(boardId))
            {
                CreateLeaderboard(boardId, type);
                GetLeaderboard(boardId).AddEntry(entry);
                Save();
                return;
            }
            
            GetLeaderboard(boardId).AddEntry(entry);
            Save();
        }
        
        
        /// <summary>
        /// Deletes an entry from the board defined...
        /// </summary>
        /// <param name="boardId">The board to remove from</param>
        /// <param name="entry">the data to remove</param>
        public static void DeleteEntryFromBoard(string boardId, LeaderboardEntry entry)
        {
            if (!BoardExists(boardId)) return;
            
            data.Leaderboards[boardId]?.DeleteEntry(entry);
            Save();
        }
        
        
        /// <summary>
        /// Deletes an entry from the board defined...
        /// </summary>
        /// <param name="boardId">The board to remove from</param>
        /// <param name="name">The name of the entry</param>
        /// <param name="score">The score of the entry</param>
        public static void DeleteEntryFromBoard(string boardId, string name, double score)
        {
            if (!BoardExists(boardId)) return;

            data.Leaderboards[boardId]?.DeleteEntry(name, score);

            Save();
        }
    }
}