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
using System.IO;
#if UNITY_WEBGL && !UNITY_EDITOR
using UnityEngine;
#endif

namespace CarterGames.Assets.LeaderboardManager.Save
{
    /// <summary>
    /// Handles saving the leaderboards to a file on the local system.
    /// </summary>
    public sealed class FileSaveLocation : ISaveLocation
    {
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Fields
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */ 
        
#pragma warning disable 
        private const string DefaultSavePath = "%Application.persistentDataPath%/leaderboards.lsf2";
        private const string DefaultSavePathWeb = "/idbfs/%productName%-%companyName%/leaderboards.lsf2";
#pragma warning restore
        
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Properties
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */ 
        
        /// <summary>
        /// Gets the save path for the game save data.
        /// </summary>
        private string SavePath
        {
            get
            {
#if UNITY_WEBGL && !UNITY_EDITOR
                return DefaultSavePathWeb.ParseSavePath();
#else
                return DefaultSavePath.ParseSavePath();
#endif
            }
        }
        
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   ISaveLocation Methods
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */ 
        
        /// <summary>
        /// Saves the data when called.
        /// </summary>
        /// <param name="json">The json to save.</param>
        public void Save(string json)
        {
            try
            {
                using (var stream = new FileStream(SavePath, FileMode.OpenOrCreate, FileAccess.Write, FileShare.Write))
                {
                    using (var writer = new StreamWriter(stream))
                    {
                        stream.SetLength(0);
                        writer.Write(json);
                        writer.Close();
                    }

                    stream.Close();
                }
            }
 #pragma warning disable
            catch (Exception e)
            {
#pragma warning restore
                // Nothing
            }

#if UNITY_WEBGL && !UNITY_EDITOR
            Application.ExternalEval("_JS_FileSystem_Sync();");
#endif
        }


        /// <summary>
        /// Loads the data when called.
        /// </summary>
        /// <returns>The loaded data.</returns>
        public string Load()
        {
#if UNITY_WEBGL && !UNITY_EDITOR
            if (!File.Exists(SavePath))
            {
                WebCreateFile(SavePath);
            }
#endif
            
            var jsonString = string.Empty;
            
            try
            {
                using (var stream = new FileStream(SavePath, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite))
                {
                    using (var reader = new StreamReader(stream))
                    {
                        jsonString = reader.ReadToEnd();
                        reader.Close();
                    }

                    stream.Close();
                }

                return jsonString;
            }
#pragma warning disable
            catch (Exception e)
            {
#pragma warning restore
                return jsonString;
            }
        }
        
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Methods
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        /// <summary>
        /// Creates the save file if it doesn't exist yet.
        /// </summary>
        /// <param name="path">The path to create the file in.</param>
        private void WebCreateFile(string path)
        {
            var split = path.Split('/');
            var pathBuilt = string.Empty;
                
            foreach (var s in split)
            {
                if (s.Equals("idbfs"))
                {
                    pathBuilt = s;
                    continue;
                }
                    
                if (s.Equals("leaderboards.lsf2")) continue;

                if (Directory.Exists(pathBuilt + "/" + s))
                {
                    pathBuilt += "/" + s;
                    continue;
                }

                Directory.CreateDirectory(pathBuilt + "/" + s);
                pathBuilt += "/" + s;
            }

            
            using (var stream = new FileStream(path, FileMode.Create))
            {
                stream.Flush();
                stream.Close();
            }
        }
    }
}