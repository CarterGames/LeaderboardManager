using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace CarterGames.Assets.LeaderboardManager
{
    public static class LeaderboardManager
    {
        private static readonly string LeaderboardDirectory = Application.persistentDataPath + "/Leaderboards";
        private static readonly string LeaderboardSaveFilePath = Application.persistentDataPath + "/Leaderboards/data.lsf";
        private static LeaderboardDataStore Data;
        private static bool shouldLoad = true;


        /// <summary>
        /// Checks to see if the board of the id entered exists in the system.
        /// </summary>
        /// <param name="boardID">The id the board to check for.</param>
        /// <returns>Bool</returns>
        public static bool BoardExists(string boardID)
        {
            Load(); 
            return Data.Leaderboards.Any(t => t.BoardID.Equals(boardID));
        }
        

        /// <summary>
        /// Creates a new leaderboard with the id entered...
        /// </summary>
        /// <param name="boardID">The id the board should be defined as</param>
        public static void CreateLeaderboard(string boardID)
        {
            Load();

            if (Data.Leaderboards.Count <= 0)
            {
                Data.Leaderboards.Add(new LeaderboardData(boardID));
                Save();
                return;
            }
                
            if (Data.Leaderboards.Any(t => t.BoardID.Equals(boardID)))
            {
                Debug.LogError($"<color=D6BA64><b>Leaderboard Manager</b> | Unable to create leaderboard with the ID {boardID} as a board of this ID already exists.</color>");
                return;
            }

            Data.Leaderboards.Add(new LeaderboardData(boardID));
            Save();
        }


        /// <summary>
        /// Deletes the leaderboard of the id entered...
        /// </summary>
        /// <param name="boardID">The board id to find and delete</param>
        public static void DeleteLeaderboard(string boardID)
        {
            Load();
            
            if (Data.Leaderboards.Count <= 0)
                return;

            if (Data.Leaderboards.All(t => !t.BoardID.Equals(boardID)))
            {
                Debug.LogError($"<color=D6BA64><b>Leaderboard Manager</b> | Unable to delete leaderboard with the ID {boardID} as there are no board with this ID saved.</color>");
                return;
            }
            
            Data.Leaderboards.Remove(Data.Leaderboards.FirstOrDefault(t => t.BoardID.Equals(boardID)));
            Save();
        }
        
        
        /// <summary>
        /// Clears the leaderboard of the id entered...
        /// </summary>
        /// <param name="boardID">The board id to find and delete</param>
        public static void ClearLeaderboard(string boardID)
        {
            Load();
            
            if (Data.Leaderboards.All(t => !t.BoardID.Equals(boardID)))
            {
                Debug.LogError($"<color=D6BA64><b>Leaderboard Manager</b>Leaderboard Manager | Unable to clear leaderboard with the ID {boardID} as there are no board with this ID saved.</color>");
                return;
            }
            
            Data.Leaderboards.FirstOrDefault(t => t.BoardID.Equals(boardID))?.ClearBoard();
            Save();
        }


        /// <summary>
        /// Calls to save all leaderboards in the game...
        /// </summary>
        public static void Save()
        {
            SaveLeaderboardData();
        }


        /// <summary>
        /// Saves all the leaderboard data in the game to the save file...
        /// </summary>
        private static void SaveLeaderboardData()
        {
            if (!Directory.Exists(LeaderboardDirectory))
                Directory.CreateDirectory(LeaderboardDirectory);
            
            var _binaryFormatter = new BinaryFormatter();
            var _fileStream = new FileStream(LeaderboardSaveFilePath, FileMode.OpenOrCreate);
            
            _binaryFormatter.Serialize(_fileStream, Data);
            _fileStream.Close();
        }


        /// <summary>
        /// Loads the data locally for the system to use...
        /// </summary>
        public static void Load()
        {
            if (!shouldLoad) return;
            Data = LoadLeaderboardData();
        }

        
        /// <summary>
        /// Loads the leaderboard data from the save file to the system for use...
        /// </summary>
        /// <returns>A list of leaderboard data stores</returns>
        private static LeaderboardDataStore LoadLeaderboardData()
        {
            if (!Directory.Exists(LeaderboardDirectory))
                Directory.CreateDirectory(LeaderboardDirectory);
            
            if (File.Exists(LeaderboardSaveFilePath))
            {
                var _binaryFormatter = new BinaryFormatter();
                var _fileStream = new FileStream(LeaderboardSaveFilePath, FileMode.Open);
                var _data = _binaryFormatter.Deserialize(_fileStream) as LeaderboardDataStore;
                _fileStream.Close();
                shouldLoad = false;
                return _data;
            }
            else
            {
                Data = new LeaderboardDataStore();
                var _binaryFormatter = new BinaryFormatter();
                var _fileStream = new FileStream(LeaderboardSaveFilePath, FileMode.Create);
                _binaryFormatter.Serialize(_fileStream, Data);
                _fileStream.Close();
                shouldLoad = false;
                return new LeaderboardDataStore();
            }
        }
        
        

        /// <summary>
        /// Gets the leaderboard of the ID entered...
        /// </summary>
        /// <param name="boardID">The board ID to find</param>
        /// <returns>The data of the board or null if none were found</returns>
        public static LeaderboardData GetLeaderboard(string boardID)
        {
            Load();
            if (Data.Leaderboards.Any(t => t.BoardID.Equals(boardID)))
                return Data.Leaderboards.FirstOrDefault(t => t.BoardID.Equals(boardID));
    
            Debug.LogError($"<color=D6BA64><b>Leaderboard Manager</b>Leaderboard Manager | Unable to find leaderboard with the ID {boardID} as a board of this ID doesn't exists.</color>");
            return null;
        }
        


        /// <summary>
        /// Adds an entry to the board defined...
        /// </summary>
        /// <param name="boardID">The board to add to</param>
        /// <param name="entry">the data to add</param>
        public static void AddEntryToBoard(string boardID, LeaderboardEntry entry)
        {
            Load();
            
            if (!BoardExists(boardID))
            {
                CreateLeaderboard(boardID);
                GetLeaderboard(boardID).AddEntry(entry);
                Save();
                return;
            }
            
            GetLeaderboard(boardID).AddEntry(entry);
            Save();
            shouldLoad = true;
        }
        
        
        /// <summary>
        /// Adds an entry to the board defined...
        /// </summary>
        /// <param name="boardID">The board to add to</param>
        /// <param name="name">The name for the entry</param>
        /// <param name="score">The score for the entry</param>
        public static void AddEntryToBoard(string boardID, string name, double score)
        {
            Load();

            if (!BoardExists(boardID))
            {
                CreateLeaderboard(boardID);
                GetLeaderboard(boardID).AddEntry(new LeaderboardEntry(name, score));
                Save();
                return;
            }

            GetLeaderboard(boardID).AddEntry(new LeaderboardEntry(name, score));
            Save();
        }
        
        
        /// <summary>
        /// Deletes an entry from the board defined...
        /// </summary>
        /// <param name="boardID">The board to remove from</param>
        /// <param name="entry">the data to remove</param>
        public static void DeleteEntryFromBoard(string boardID, LeaderboardEntry entry)
        {
            Load();

            if (!BoardExists(boardID))
                return;
            
            Data.Leaderboards.FirstOrDefault(t => t.BoardID.Equals(boardID))?.DeleteEntry(entry);
            Save();
        }
        
        
        /// <summary>
        /// Deletes an entry from the board defined...
        /// </summary>
        /// <param name="boardID">The board to remove from</param>
        /// <param name="name">The name of the entry</param>
        /// <param name="score">The score of the entry</param>
        public static void DeleteEntryFromBoard(string boardID, string name, double score)
        {
            Load();

            if (!BoardExists(boardID))
                return;

            Data.Leaderboards.FirstOrDefault(t => t.BoardID.Equals(boardID))?.DeleteEntry(name, score);
            Save();
        }
    }
}