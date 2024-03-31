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

using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using CarterGames.Assets.LeaderboardManager.Serialization;
using UnityEditor;
using UnityEngine;

namespace CarterGames.Assets.LeaderboardManager.Editor
{
    /// <summary>
    /// Handles the legacy tab of the leaderboard editor.
    /// </summary>
    public sealed class LegacyTab
    {
        /* —————————————————————————————————————————————————————————————————————————————————————————————————————————————
        |   Fields
        ————————————————————————————————————————————————————————————————————————————————————————————————————————————— */
        
        private static LegacyBoardStore legacyBoardStore;
        private static Dictionary<LegacyBoardDataClass, LeaderboardType> typesLookup;
        private static Dictionary<LegacyBoardDataClass, bool> showLookup;
        private static Dictionary<LegacyBoardDataClass, Vector2> scrollPos;
        
        /* —————————————————————————————————————————————————————————————————————————————————————————————————————————————
        |   Methods
        ————————————————————————————————————————————————————————————————————————————————————————————————————————————— */
        
        public void DrawTab()
        {
            EditorGUILayout.BeginVertical("Box");
            
            EditorGUILayout.Space(5f);
            
            PerUserSettings.LegacyWindowTabPos = GUILayout.Toolbar(PerUserSettings.LegacyWindowTabPos,
                new string[2] { "Convert Legacy File", "Setup Runtime Conversions" }, GUILayout.Height(30));

            EditorGUILayout.Space(5f);

            switch (PerUserSettings.LegacyWindowTabPos)
            {
                case 0:
                    OnEditorTab();
                    break;
                case 1:
                    OnRuntimeTab();
                    break;
            }
            
            EditorGUILayout.EndVertical();
        }
        
        
        /// <summary>
        /// Draws the settings for the editor convert tool for legacy files GUI
        /// </summary>
        private static void OnEditorTab()
        {
            // Intro prompt
            /* ────────────────────────────────────────────────────────────────────────────────────────────────────── */
            EditorGUILayout.HelpBox("Use the tool below to import a .lsf data into the project and port it to .lsf2", MessageType.Info);
            
            
            // Select button
            /* ────────────────────────────────────────────────────────────────────────────────────────────────────── */
            if (GUILayout.Button("Select File"))
            {
                var data = EditorUtility.OpenFilePanel("Select file", Application.persistentDataPath + "/Leaderboards", "lsf");
                if (string.IsNullOrEmpty(data)) return;
                legacyBoardStore = LoadData(data);
            }

            
            if (legacyBoardStore == null) return;

            // Assigns lookups if not already
            /* ────────────────────────────────────────────────────────────────────────────────────────────────────── */
            typesLookup ??= new Dictionary<LegacyBoardDataClass, LeaderboardType>();
            showLookup ??= new Dictionary<LegacyBoardDataClass, bool>();
            scrollPos ??= new Dictionary<LegacyBoardDataClass, Vector2>();
            
            
            EditorGUILayout.Space(5f);
        
            EditorGUILayout.BeginVertical("HelpBox");
            
            EditorGUILayout.LabelField("Global Settings", EditorStyles.boldLabel);
            UtilEditor.DrawHorizontalGUILine();

            GUI.backgroundColor = UtilEditor.Green;
            if (GUILayout.Button("Convert All", GUILayout.Height(25)))
            {
                if (EditorUtility.DisplayDialog("Convert All", "Are you sure you want to convert all the legacy leaderboards below with the current settings.", "Convert", "Cancel"))
                {
                    foreach (var board in legacyBoardStore.Leaderboards)
                    {
                        ConvertAndSave(board, typesLookup[board]);
                    }
                }
                    
            }
            GUI.backgroundColor = Color.white;
            
            EditorGUILayout.EndVertical();
            
            EditorGUILayout.Space(5f);
            
            EditorGUILayout.LabelField("Data Found:", EditorStyles.boldLabel);
            EditorGUILayout.BeginVertical("HelpBox");
            
  

            for (var i = -1; i < legacyBoardStore.Leaderboards.Count; i++)
            {
                EditorGUILayout.BeginHorizontal();
                
                if (i == -1)
                {
                    // Labels
                    /* ────────────────────────────────────────────────────────────────────────────────────────────── */
                    EditorGUILayout.LabelField("Id");
                    EditorGUILayout.LabelField("Number of entries");
                    EditorGUILayout.LabelField("Convert to");
                    EditorGUILayout.EndHorizontal();
                    continue;
                }
                else
                {
                    // Board summary
                    /* ────────────────────────────────────────────────────────────────────────────────────────────── */
                    var board = legacyBoardStore.Leaderboards[i];
                    
                    // If no type is assigned, it assumes its a score board.
                    if (!typesLookup.ContainsKey(board))
                    {
                        typesLookup.Add(board, LeaderboardType.Score);
                    }
                    
                    EditorGUILayout.LabelField(board.BoardID);
                    EditorGUILayout.LabelField(board.BoardData.Count.ToString());
                    typesLookup[board] = (LeaderboardType)EditorGUILayout.EnumPopup(GUIContent.none, typesLookup[board]);
                }
                
                EditorGUILayout.EndHorizontal();

                if (!showLookup.ContainsKey(legacyBoardStore.Leaderboards[i]))
                {
                    showLookup.Add(legacyBoardStore.Leaderboards[i], false);
                }
                
                showLookup[legacyBoardStore.Leaderboards[i]] =
                    EditorGUILayout.Foldout(showLookup[legacyBoardStore.Leaderboards[i]], "Show entries");
                
                if (!showLookup[legacyBoardStore.Leaderboards[i]]) continue;
                
                if (!scrollPos.ContainsKey(legacyBoardStore.Leaderboards[i]))
                {
                    scrollPos.Add(legacyBoardStore.Leaderboards[i], new Vector2());
                }
                

                
                
                // Board entries
                /* ────────────────────────────────────────────────────────────────────────────────────────────────── */
                for (var j = -1; j < legacyBoardStore.Leaderboards[i].BoardData.Count; j++)
                {
                    // Labels
                    /* ────────────────────────────────────────────────────────────────────────────────────────────── */
                    if (j == -1)
                    {
                        EditorGUILayout.BeginHorizontal();
                        
                        EditorGUILayout.LabelField("Id");
                        EditorGUILayout.LabelField("Name");
                        EditorGUILayout.LabelField("Score");
                        
                        EditorGUILayout.EndHorizontal();
                        continue;
                    }
                    
                    
                    // Scroll rect start
                    /* ────────────────────────────────────────────────────────────────────────────────────────────── */
                    if (j == 0)
                    {
                        scrollPos[legacyBoardStore.Leaderboards[i]] =
                            EditorGUILayout.BeginScrollView(scrollPos[legacyBoardStore.Leaderboards[i]]);
                    }

                    
                    // Entry ref
                    /* ────────────────────────────────────────────────────────────────────────────────────────────── */
                    var entry = legacyBoardStore.Leaderboards[i].BoardData[j];
                    
                    
                    // Entry display
                    /* ────────────────────────────────────────────────────────────────────────────────────────────── */
                    EditorGUILayout.BeginHorizontal(i % 2 == 1 ? "Box" : string.Empty);

                    EditorGUI.BeginDisabledGroup(true);
                    EditorGUILayout.TextField(entry.EntryID.ToString());
                    EditorGUILayout.TextField(entry.Name);
                    EditorGUILayout.TextField(entry.Score.ToString());
                    EditorGUI.EndDisabledGroup();
                    
                    EditorGUILayout.EndHorizontal();
                }
                
                EditorGUILayout.EndScrollView();
                
                
                // Convert button
                /* ────────────────────────────────────────────────────────────────────────────────────────────────── */
                GUI.backgroundColor = UtilEditor.Green;
                
                if (GUILayout.Button("Convert & Save"))
                {
                    ConvertAndSave(legacyBoardStore.Leaderboards[i], typesLookup[legacyBoardStore.Leaderboards[i]]);
                }
                
                GUI.backgroundColor = Color.white;
            }

            
            EditorGUILayout.EndVertical();
        }

        
        /// <summary>
        /// Draws the runtime settings for the legacy data conversion GUI.
        /// </summary>
        private static void OnRuntimeTab()
        {
            EditorGUILayout.HelpBox("Below are the settings the runtime will use to convert any legacy .lsf data found for users into the new .lsf2 format. This only applies if you have entered the board name and the type to convert to below. Otherwise the board will be ignored.", MessageType.Info);
            EditorGUILayout.BeginVertical("HelpBox");
            
            // Labels
            /* ────────────────────────────────────────────────────────────────────────────────────────────────────── */
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField(EditorMeta.LegacyPortDictionary.KeyLabel);
            EditorGUILayout.LabelField(EditorMeta.LegacyPortDictionary.ValueLabel);
            EditorGUILayout.LabelField(string.Empty, GUILayout.Width(25));
            EditorGUILayout.EndHorizontal();
            
            
            // Display entries
            /* ────────────────────────────────────────────────────────────────────────────────────────────────────── */
            for (var i = 0; i < UtilEditor.SettingsObject.Fp("legacyBoardPortTypes").Fpr("list").arraySize; i++)
            {
                // Element reference
                /* ────────────────────────────────────────────────────────────────────────────────────────────────── */
                var element = UtilEditor.SettingsObject.Fp("legacyBoardPortTypes").Fpr("list").GetIndex(i);
                
                
                // Draw element
                /* ────────────────────────────────────────────────────────────────────────────────────────────────── */
                EditorGUILayout.BeginHorizontal();
                EditorGUI.BeginChangeCheck();
                
                EditorGUILayout.PropertyField(element.Fpr("key"), GUIContent.none);
                
                GUI.backgroundColor = UtilEditor.Yellow;
                EditorGUILayout.PropertyField(element.Fpr("value"), GUIContent.none);
                GUI.backgroundColor = Color.white;

                if (EditorGUI.EndChangeCheck())
                {
                    UtilEditor.SettingsObject.ApplyModifiedProperties();
                    UtilEditor.SettingsObject.Update();
                }

                
                // Delete element button
                /* ────────────────────────────────────────────────────────────────────────────────────────────────── */
                GUI.backgroundColor = UtilEditor.Red;
                
                if (GUILayout.Button("-", GUILayout.Width(25)))
                {
                    UtilEditor.SettingsObject.Fp("legacyBoardPortTypes").Fpr("list").DeleteIndex(i);
                    
                    UtilEditor.SettingsObject.ApplyModifiedProperties();
                    UtilEditor.SettingsObject.Update();
                }
                
                GUI.backgroundColor = Color.white;
                
                EditorGUILayout.EndHorizontal();
            }

            // Spacing & divider
            /* ────────────────────────────────────────────────────────────────────────────────────────────────────── */
            EditorGUILayout.Space(2.5f);
            UtilEditor.DrawHorizontalGUILine();
            EditorGUILayout.Space(2.5f);

            
            // Add element button
            /* ────────────────────────────────────────────────────────────────────────────────────────────────────── */
            AddNewEntry();
            
            
            EditorGUILayout.EndVertical();
        }


        private static void AddNewEntry()
        {
            GUI.backgroundColor = UtilEditor.Green;
            
            if (GUILayout.Button("Add Entry", GUILayout.Height(25)))
            {
                // Adds the entry
                /* ────────────────────────────────────────────────────────────────────────────────────────────────── */
                UtilEditor.SettingsObject.Fp("legacyBoardPortTypes").Fpr("list")
                    .InsertIndex(UtilEditor.SettingsObject.Fp("legacyBoardPortTypes").Fpr("list").arraySize);

                
                // Gets the new entry
                /* ────────────────────────────────────────────────────────────────────────────────────────────────── */                
                var element = UtilEditor.SettingsObject.Fp("legacyBoardPortTypes")
                    .Fpr("list")
                    .GetIndex(UtilEditor.SettingsObject.Fp("legacyBoardPortTypes").Fpr("list").arraySize - 1);

                
                // Resets the value to defaults
                /* ────────────────────────────────────────────────────────────────────────────────────────────────── */
                element.Fpr("key").stringValue = string.Empty;
                element.Fpr("value").intValue = 0;
                
                
                // Applies the changes
                /* ────────────────────────────────────────────────────────────────────────────────────────────────── */
                UtilEditor.SettingsObject.ApplyModifiedProperties();
                UtilEditor.SettingsObject.Update();
            }
            
            GUI.backgroundColor = Color.white;
        }


        /// <summary>
        /// Loads any legacy data into a usable structure.
        /// </summary>
        /// <param name="path">The path to load from.</param>
        /// <returns>The legacy data found.</returns>
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
        /// Converts and saves any legacy data entries that have a conversion setup.
        /// </summary>
        /// <param name="legacyBoardDataClass">The legacy board.</param>
        /// <param name="convertTo">The type to convert to.</param>
        private static void ConvertAndSave(LegacyBoardDataClass legacyBoardDataClass, LeaderboardType convertTo)
        {
            LeaderboardManager.Load();
            LeaderboardManager.CreateLeaderboard(legacyBoardDataClass.BoardID, convertTo);

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
    }
}