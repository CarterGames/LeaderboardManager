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

using UnityEngine;

namespace CarterGames.Assets.LeaderboardManager.Editor
{
    /// <summary>
    /// A helper class to hold meta data for GUIContents throughout the assets editors.
    /// </summary>
    public static class EditorMeta
    {
        /* —————————————————————————————————————————————————————————————————————————————————————————————————————————————
        |   Settings
        ————————————————————————————————————————————————————————————————————————————————————————————————————————————— */
        
        public struct Settings
        {
            public static readonly GUIContent VersionCheck =
                new GUIContent(
                    "Update Check On Load", 
                    "Checks for any updates to the asset from the GitHub page when you load the project.");

            
            public static readonly GUIContent ShowLogs =
                new GUIContent(
                    "Show Asset Logs?",
                    "If TRUE, the asset will throw log messages, if off none will be fired intentionally.");


            public static readonly GUIContent SaveLocation =
                new GUIContent(
                    "Save Location",
                    "The method of saving the asset uses for the leaderboards.");
        }
        
        /* —————————————————————————————————————————————————————————————————————————————————————————————————————————————
        |   Legacy Dictionary
        ————————————————————————————————————————————————————————————————————————————————————————————————————————————— */
        
        public struct LegacyPortDictionary
        {
            public static readonly GUIContent KeyLabel =
                new GUIContent(
                    "Id", 
                    "The id of the board to convert.");

            
            public static readonly GUIContent ValueLabel =
                new GUIContent(
                    "Convert Type", 
                    "The entry type to convert the leaderboard to.");
        }
    }
}