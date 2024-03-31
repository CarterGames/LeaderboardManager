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
using System.Linq;
using CarterGames.Assets.LeaderboardManager.Serialization;
using UnityEditor;
using UnityEngine;

namespace CarterGames.Assets.LeaderboardManager.Editor
{
    /// <summary>
    /// Handles the editor tab of the leaderboard editor.
    /// </summary>
    public class EditorTab
    {
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Fields
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        private List<Leaderboard> boards;
        
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Methods
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        /// <summary>
        /// Initializes the tab when called.
        /// </summary>
        public void Initialize()
        {
            if (!LeaderboardManager.IsInitialized)
            {
                LeaderboardManager.Load();
            }

            boards = LeaderboardManager.AllLeaderboards.Values.ToList();
        }


        /// <summary>
        /// Draws the GUI for the tab when called.
        /// </summary>
        public void DrawTab()
        {
            Initialize();
            
            EditorGUILayout.Space(7.5f);
            
            EditorGUILayout.LabelField("Leaderboards", EditorStyles.boldLabel);
            UtilEditor.DrawHorizontalGUILine();

            if (LeaderboardManager.AllLeaderboards.Count <= 0)
            {
                EditorGUILayout.HelpBox("No leaderboards detected", MessageType.Info);
                return;
            }

            PerUserSettings.EditorWindowScrollRect =
                EditorGUILayout.BeginScrollView(PerUserSettings.EditorWindowScrollRect);

            for (var i = 0; i < LeaderboardManager.NumberOfBoards; i++)
            {
                EditorGUILayout.BeginVertical("HelpBox");
                EditorGUI.BeginChangeCheck();

                EditorGUILayout.BeginVertical();
                EditorGUILayout.BeginHorizontal();
                PerUserSettings.SetIsBoardExpanded(boards[i].Id, EditorGUILayout.Foldout(PerUserSettings.IsBoardExpanded(boards[i].Id), boards[i].Id));
                
                GUI.backgroundColor = UtilEditor.Red;
                
                if (GUILayout.Button("Delete", GUILayout.Width(85)))
                {
                    if (EditorUtility.DisplayDialog("Delete Leaderboard", "Are you sure you want to delete the board?",
                            "Delete", "Cancel"))
                    {
                        LeaderboardManager.AllLeaderboards.Remove(boards[i].Id);
                        LeaderboardManager.Save();
                        break;
                    }
                }
                
                GUI.backgroundColor = Color.white;
                
                EditorGUILayout.EndHorizontal();
                GUILayout.Space(3f);
                EditorGUILayout.EndVertical();
                
                EditorGUILayout.Space(1.5f);

                if (PerUserSettings.IsBoardExpanded(boards[i].Id))
                {
                    EditorGUILayout.LabelField("Summary", EditorStyles.boldLabel);
                    UtilEditor.DrawHorizontalGUILine();
                    EditorGUILayout.TextField(new GUIContent("Total Entries:"), boards[i].BoardData.Count.ToString());

                    UtilEditor.DrawHorizontalGUILine();

                    PerUserSettings.SetBoardPosition(boards[i].Id,
                        EditorGUILayout.BeginScrollView(PerUserSettings.BoardPosition(boards[i].Id), "Box", GUILayout.ExpandHeight(false)));
                    EditorGUI.BeginDisabledGroup(true);

                    for (var j = 0; j < boards[i].BoardData.Count; j++)
                    {
                        EditorGUILayout.BeginHorizontal();
                        EditorGUILayout.TextField(boards[i].BoardData[j].EntryUuid);
                        EditorGUILayout.TextField(boards[i].BoardData[j].EntryName);
                        
                        if (boards[i].Type.Equals(LeaderboardType.Time))
                        {
                            EditorGUILayout.TextField(((SerializableTime) boards[i].BoardData[j].EntryValue).ToString("dd:hh:mm:ss.fff"));
                        }
                        else
                        {
                            EditorGUILayout.TextField(boards[i].BoardData[j].EntryValue.ToString());
                        }
                        
                        EditorGUILayout.EndHorizontal();
                    }

                    EditorGUI.EndDisabledGroup();
                    EditorGUILayout.EndScrollView();
                }
                
                EditorGUILayout.EndVertical();
            }
            
            EditorGUILayout.EndScrollView();
        }
    }
}