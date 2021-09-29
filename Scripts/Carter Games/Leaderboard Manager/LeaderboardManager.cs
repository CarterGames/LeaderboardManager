using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;
using System.Linq;

/****************************************************************************************************************************
 * 
 *  --{   Leaderboard Manager   }--
 *							  
 *	Leaderboard Manager Script
 *      Handles the backend of the manager script.
 *			
 *  Written by:
 *      Jonathan Carter
 *      E: jonathan@carter.games
 *      W: https://jonathan.carter.games
 *		
 *  Version: 1.0.4
 *	Last Updated: 05/02/2021 (d/m/y)						
 * 
****************************************************************************************************************************/

namespace CarterGames.Assets.LeaderboardManager
{
    /// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    /// <summary>
    /// Static Class | The main Leaderboard Manager Script, call a method in this script via LeaderboardManager.???, no reference needed.
    /// </summary>
    /// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    public class LeaderboardManager : MonoBehaviour
    {
        /// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Leaderboard Manager Static Method | Adds a new entry into the leaderboard and saves it.
        /// </summary>
        /// <param name="name">String | The player name to add with the new entry.</param>
        /// <param name="score">Float | The player score to add with the new entry.</param>
        /// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        public static void AddToLeaderboard(string name, float score)
        {
            string SavePath = Application.persistentDataPath + "/Leaderboard/leaderboardsavefile.lsf";

            if (File.Exists(SavePath))
            {
                LeaderboardStore _storedData = LoadLeaderboardStore();
                _storedData.leaderboardData.Add(new LeaderboardData(name, score));
                BinaryFormatter Formatter = new BinaryFormatter();
                FileStream Stream = new FileStream(SavePath, FileMode.OpenOrCreate);
                Formatter.Serialize(Stream, _storedData);
                Stream.Close();
            }
            else
            {
                SaveLeaderboardStore(new LeaderboardStore());
                LeaderboardStore _storedData = new LeaderboardStore();
                _storedData.leaderboardData.Add(new LeaderboardData(name, score));
                BinaryFormatter Formatter = new BinaryFormatter();
                FileStream Stream = new FileStream(SavePath, FileMode.OpenOrCreate);
                Formatter.Serialize(Stream, _storedData);
                Stream.Close();
                Debug.LogWarning("* Leaderboard Manager * | Warning Code 1 | No file, creating one.");
            }
        }


        /// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Leaderboard Manager Static Method | Removes an entry from the leaderboard.
        /// </summary>
        /// <param name="playerName">String | The players name to check for.</param>
        /// <param name="playerScore">Float | The player score to check for.</param>
        /// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        public static void RemoveEntryFromLeaderboard(string playerName, float playerScore)
        {
            LeaderboardStore _store = LoadLeaderboardStore();

            for (int i = 0; i < _store.leaderboardData.Count; i++)
            {
                if (_store.leaderboardData[i].playerName.Equals(playerName) && _store.leaderboardData[i].playerScore.Equals(playerScore))
                {
                    _store.leaderboardData.RemoveAt(i);
                    break;
                }
            }

            SaveLeaderboardStore(_store);
        }


        /// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Leaderboard Manager Static Method | Saves the leaderboard store into the save file.
        ///   Also checks to see if the leaderboard directory exsists in your games default save location.
        /// </summary>
        /// <param name="_storedData">LeaderboardStore | To save in the file.</param>
        /// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        public static void SaveLeaderboardStore(LeaderboardStore _storedData)
        {
            string SavePath = Application.persistentDataPath + "/Leaderboard/leaderboardsavefile.lsf";

            if (File.Exists(SavePath))
            {
                BinaryFormatter Formatter = new BinaryFormatter();
                FileStream Stream = new FileStream(SavePath, FileMode.OpenOrCreate);
                Formatter.Serialize(Stream, _storedData);
                Stream.Close();
            }
            else
            {
                if (!Directory.Exists(Application.persistentDataPath + "/Leaderboard"))
                {
                    Directory.CreateDirectory(Application.persistentDataPath + "/Leaderboard");
                    BinaryFormatter Formatter = new BinaryFormatter();
                    FileStream Stream = new FileStream(SavePath, FileMode.OpenOrCreate);
                    Formatter.Serialize(Stream, _storedData);
                    Stream.Close();
                }
                else
                {
                    BinaryFormatter Formatter = new BinaryFormatter();
                    FileStream Stream = new FileStream(SavePath, FileMode.OpenOrCreate);
                    Formatter.Serialize(Stream, _storedData);
                    Stream.Close();
                }
            }
        }


        /// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Leaderboard Manager Static Method | Loads the saved LeaderboardStore from the save file used for the leaderboard.
        /// </summary>
        /// <returns>LeaderboardStore | The store of leaderboard entries to return.</returns>
        /// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        public static LeaderboardStore LoadLeaderboardStore()
        {
            string SavePath = Application.persistentDataPath + "/Leaderboard/leaderboardsavefile.lsf";

            if (File.Exists(SavePath))
            {
                BinaryFormatter Formatter = new BinaryFormatter();
                FileStream Stream = new FileStream(SavePath, FileMode.Open);

                LeaderboardStore _data = Formatter.Deserialize(Stream) as LeaderboardStore;

                Stream.Close();

                return _data;
            }
            else
            {
                Debug.LogError("* Leaderboard Manager * | Error Code 1 | Leaderboard save file not found!");
                return null;
            }
        }


        /// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Leaderboard Manager Static Method | Loads the saved LeaderboardData from the save file used for the leaderboard.
        /// </summary>
        /// <returns>LeaderboardData Array | The loaded data in a leaderboard data array.</returns>
        /// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        public static LeaderboardData[] LoadLeaderboardData()
        {
            string SavePath = Application.persistentDataPath + "/Leaderboard/leaderboardsavefile.lsf";

            if (File.Exists(SavePath))
            {
                BinaryFormatter Formatter = new BinaryFormatter();
                FileStream Stream = new FileStream(SavePath, FileMode.Open);

                LeaderboardStore _data = Formatter.Deserialize(Stream) as LeaderboardStore;

                Stream.Close();

                return _data.leaderboardData.ToArray();
            }
            else
            {
                Debug.LogError("* Leaderboard Manager * | Error Code 1 | Leaderboard save file not found!");
                return null;
            }
        }


        /// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Leaderboard Manager Static Method | Loads the leaderboard values in a decesnding order based on the players scores.
        /// </summary>
        /// <returns>LeaderboardData Array | Entries in the save file ordered by score.</returns>
        /// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        public static LeaderboardData[] LoadLeaderboardDataDescending()
        {
            string SavePath = Application.persistentDataPath + "/Leaderboard/leaderboardsavefile.lsf";

            if (File.Exists(SavePath))
            {
                BinaryFormatter Formatter = new BinaryFormatter();
                FileStream Stream = new FileStream(SavePath, FileMode.Open);
                LeaderboardStore _store = Formatter.Deserialize(Stream) as LeaderboardStore;
                Stream.Close();

                List<LeaderboardData> _array = new List<LeaderboardData>();
                _array = _store.SortedLBD.ToList();
                _array.Reverse();

                return _array.ToArray();
            }
            else
            {
                Debug.LogError("* Leaderboard Manager * | Error Code 1 | Leaderboard save file not found!");
                return null;
            }
        }


        /// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Leaderboard Manager Static Method | Loads the leaderboard values in a ascending order based on the players scores.
        /// </summary>
        /// <returns>LeaderboardData Array | Entries in the save file ordered by score.</returns>
        /// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        public static LeaderboardData[] LoadLeaderboardDataAscending()
        {
            string SavePath = Application.persistentDataPath + "/Leaderboard/leaderboardsavefile.lsf";

            if (File.Exists(SavePath))
            {
                BinaryFormatter Formatter = new BinaryFormatter();
                FileStream Stream = new FileStream(SavePath, FileMode.Open);
                LeaderboardStore _store = Formatter.Deserialize(Stream) as LeaderboardStore;
                Stream.Close();

                List<LeaderboardData> _array = new List<LeaderboardData>();
                _array = _store.SortedLBD.ToList();

                return _array.ToArray();
            }
            else
            {
                Debug.LogError("* Leaderboard Manager * | Error Code 1 | Leaderboard save file not found!");
                return null;
            }
        }


        /// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Leaderboard Manager Static Method | Clears the leaderboard data at the file level.
        /// </summary>
        /// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        public static void ClearLeaderboardData()
        {
            string SavePath = Application.persistentDataPath + "/Leaderboard/leaderboardsavefile.lsf";
            LeaderboardStore _storedData = new LeaderboardStore();

            if (File.Exists(SavePath))
            {
                BinaryFormatter Formatter = new BinaryFormatter();
                FileStream Stream = new FileStream(SavePath, FileMode.OpenOrCreate);
                Formatter.Serialize(Stream, _storedData);
                Stream.Close();
            }
            else
            {
                if (!Directory.Exists(Application.persistentDataPath + "/Leaderboard"))
                {
                    Directory.CreateDirectory(Application.persistentDataPath + "/Leaderboard");
                    BinaryFormatter Formatter = new BinaryFormatter();
                    FileStream Stream = new FileStream(SavePath, FileMode.OpenOrCreate);
                    Formatter.Serialize(Stream, _storedData);
                    Stream.Close();
                }
                else
                {
                    BinaryFormatter Formatter = new BinaryFormatter();
                    FileStream Stream = new FileStream(SavePath, FileMode.OpenOrCreate);
                    Formatter.Serialize(Stream, _storedData);
                    Stream.Close();
                }
            }
        }


        /// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Leaderboard Manager Static Method | Gets the position of an entry in the leaderboard. 
        /// </summary>
        /// <param name="_score">Int | The score they got to match to.</param>
        /// <param name="option">LeaderboardDisplay.DisplayOptions | The ordering you wish to search by.</param>
        /// <returns>Int | The position the values match up to.</returns>
        /// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        public static int GetPosition(int _score, LeaderboardDisplay.DisplayOptions option)
        {
            LeaderboardData[] _data;
            int _matchingScore = 0;
            int _endingPos = 0;


            switch (option)
            {
                case LeaderboardDisplay.DisplayOptions.Unordered:
                    _data = LoadLeaderboardData();
                    break;
                case LeaderboardDisplay.DisplayOptions.Descending:
                    _data = LoadLeaderboardDataDescending();
                    break;
                case LeaderboardDisplay.DisplayOptions.Ascending:
                    _data = LoadLeaderboardDataAscending();
                    break;
                default:
                    _data = new LeaderboardData[0];
                    break;
            }


            // checks to see if there is only 1 entry with the entered score.
            for (int i = 0; i < _data.Length; i++)
            {
                if (_data[i].playerScore.Equals(_score))
                {
                    _matchingScore++;
                }
            }

            // if only 1 entry it will find the entry and return the position, else it will find the lowest entry with that score and return it (as when you tie with people you both have the lower position value).
            if (_matchingScore.Equals(1))
            {
                for (int i = 0; i < _data.Length; i++)
                {
                    if (_data[i].playerScore.Equals(_score))
                    {
                        return i + 1;
                    }
                }
            }
            else
            {
                for (int i = 0; i < _data.Length; i++)
                {
                    if (_data[i].playerScore.Equals(_score))
                    {
                        _endingPos = i;
                    }
                }

                return (_endingPos + 1);
            }


            // shouldn't happen.... but needs to have a default value if it doesn't work xD.
            return 0;
        }
    }
}