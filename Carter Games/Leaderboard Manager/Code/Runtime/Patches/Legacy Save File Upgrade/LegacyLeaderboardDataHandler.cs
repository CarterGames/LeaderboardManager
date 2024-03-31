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

using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using CarterGames.Assets.LeaderboardManager.Editor;
using CarterGames.Assets.LeaderboardManager.Serialization;
using UnityEngine;

namespace CarterGames.Assets.LeaderboardManager.Patches
{
    /// <summary>
    /// Handles converting the legacy save data to the latest.
    /// </summary>
    public static class LegacyLeaderboardDataHandler
    {
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Methods
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        /// <summary>
        /// Runs the save porting logic if there is a save to port.
        /// </summary>
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void RunPorting()
        {
            if (!Directory.Exists(UtilRuntime.LegacyLeaderboardSaveFolderPath)) return;
            if (!File.Exists(UtilRuntime.LegacyLeaderboardSaveFilePath)) return;
            var file = LoadData(UtilRuntime.LegacyLeaderboardSaveFilePath);
            
            // Backup of legacy, just in case...
            using (var stream = new FileStream(UtilRuntime.BackupLegacyLeaderboardSaveFilePath, FileMode.OpenOrCreate, FileAccess.Write, FileShare.Write))
            {
                using (var writer = new StreamWriter(stream))
                {
                    stream.SetLength(0);
                    writer.Write(JsonUtility.ToJson(file));
                    writer.Close();
                }

                stream.Close();
            }

            foreach (var board in file.Leaderboards)
            {
                if (!UtilRuntime.Settings.LegacyBoardPortTypes.ContainsKey(board.BoardID)) continue;
                ConvertAndSave(board, UtilRuntime.Settings.LegacyBoardPortTypes[board.BoardID]);
            }

            DeleteDirectoryAndContents(UtilRuntime.LegacyLeaderboardSaveFolderPath);
        }
        
        
        /// <summary>
        /// Loads the data from the legacy setup for use.
        /// </summary>
        /// <param name="path">The path to search.</param>
        /// <returns>The legacy store populated with the old data.</returns>
        private static LegacyBoardStore LoadData(string path)
        {
            var binaryFormatter = new BinaryFormatter();
            var fileStream = new FileStream(path, FileMode.Open);

            binaryFormatter.Binder = new LegacyDataCustomBinder();
            
            var data = binaryFormatter.Deserialize(fileStream) as LegacyBoardStore;
            
            fileStream.Close();
            return data;
        }
        
        
        /// <summary>
        /// Converts the legacy data into the current and saves it.
        /// </summary>
        /// <param name="legacyBoardDataClass">The board to convert.</param>
        /// <param name="convertTo">The type of board to convert to.</param>
        private static void ConvertAndSave(LegacyBoardDataClass legacyBoardDataClass, LeaderboardType convertTo)
        {
            LeaderboardManager.Load();

            if (LeaderboardManager.BoardExists(legacyBoardDataClass.BoardID)) return;
            
            LeaderboardManager.CreateLeaderboard(legacyBoardDataClass.BoardID, LeaderboardType.Score);

            foreach (var entry in legacyBoardDataClass.BoardData)
            {
                var portedEntry = new LeaderboardEntry();
                
                switch (convertTo)
                {
                    case LeaderboardType.Score:
                        portedEntry = new LeaderboardEntryScore(entry.Name, entry.Score);
                        break;
                    case LeaderboardType.Time:
                        portedEntry = new LeaderboardEntryTime(entry.Name, SerializableTime.FromSeconds(entry.Score));
                        break;
                }
                
                LeaderboardManager.GetLeaderboard(legacyBoardDataClass.BoardID).AddEntry(portedEntry);
            }
            
            LeaderboardManager.Save();
        }
        
        
        /// <summary>
        /// Deletes the entered directory and its contents.
        /// </summary>
        /// <param name="path">The path to clear.</param>
        private static void DeleteDirectoryAndContents(string path)
        {
            foreach (var file in Directory.GetFiles(path).ToList())
            {
                File.Delete(file);
            }
            
            Directory.Delete(path);
        }
    }
}