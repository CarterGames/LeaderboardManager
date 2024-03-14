/*
* Copyright (c) 2018-Present Carter Games
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
using UnityEditor;
using UnityEngine;

namespace CarterGames.Assets.LeaderboardManager.Editor
{
    /// <summary>
    /// Handles the settings window for the asset.
    /// </summary>
    public static class LeaderboardManagerSettingsProvider
    {
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Fields
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        private static SettingsProvider Provider;

        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Menu Items
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        /// <summary>
        /// The menu item for opening the settings window.
        /// </summary>
        [MenuItem("Tools/Carter Games/Leaderboard Manager/Edit Settings", priority = 0)]
        public static void OpenSettings()
        {
            SettingsService.OpenProjectSettings(UtilEditor.SettingsWindowPath);
        }

        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Methods
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        /// <summary>
        /// Handles the settings window in the engine.
        /// </summary>
        [SettingsProvider]
        public static SettingsProvider SettingsDrawer()
        {
            var provider = new SettingsProvider(UtilEditor.SettingsWindowPath, SettingsScope.Project)
            {
                guiHandler = (searchContext) =>
                {
                    if (UtilEditor.RuntimeSettings == null) return;

                    UtilEditor.DrawHeaderWithTexture(UtilEditor.AssetLogo);
                    
                    DrawInfo();
                    
                    EditorGUILayout.BeginVertical("HelpBox");
                    GUILayout.Space(1.5f);
            
                    EditorGUILayout.LabelField("Settings", EditorStyles.boldLabel);
                    
                    GUILayout.Space(1.5f);

                    EditorGUILayout.BeginVertical("HelpBox");
                    GUILayout.Space(1.5f);
                    DrawEditorOptions();
                    GUILayout.Space(1.5f);
                    EditorGUILayout.EndVertical();
                    
                    GUILayout.Space(1.5f);

                    EditorGUILayout.BeginVertical("HelpBox");
                    GUILayout.Space(1.5f);
                    DrawRuntimeOptions();
                    GUILayout.Space(1.5f);
                    EditorGUILayout.EndVertical();

                    GUILayout.Space(2.5f);
                    EditorGUILayout.EndVertical();
                    
                    GUILayout.Space(1.5f);
                    
                    DrawButtons();
                },
                
                keywords = new HashSet<string>(new[] { "Carter Games", "External Assets" })
            };
            
            return provider;
        }


        /// <summary>
        /// Draws the info section of the window.
        /// </summary>
        private static void DrawInfo()
        {
            EditorGUILayout.BeginVertical("HelpBox");
            GUILayout.Space(1.5f);
            
            EditorGUILayout.LabelField("Info", EditorStyles.boldLabel);

            EditorGUILayout.BeginHorizontal();
            
            EditorGUILayout.LabelField(new GUIContent("Version"),  new GUIContent(AssetVersionData.VersionNumber));
            VersionEditorGUI.DrawCheckForUpdatesButton();
            
            EditorGUILayout.EndHorizontal();
            
            EditorGUILayout.LabelField(new GUIContent("Release Date"), new GUIContent(AssetVersionData.ReleaseDate));

            GUILayout.Space(2.5f);
            EditorGUILayout.EndVertical();
        }


        private static void DrawEditorOptions()
        {
            PerUserSettings.EditorSettingsEditorTab = EditorGUILayout.Foldout(PerUserSettings.EditorSettingsEditorTab, "Editor");

            if (!PerUserSettings.EditorSettingsEditorTab) return;
            
            EditorGUILayout.BeginVertical("Box");
            GUILayout.Space(1.5f);
              
            // Auto Version Check...
            PerUserSettings.VersionValidationAutoCheckOnLoad = EditorGUILayout.Toggle(EditorMeta.Settings.VersionCheck, PerUserSettings.VersionValidationAutoCheckOnLoad);
            
            // Show Logs...
            PerUserSettingsRuntime.ShowDebugLogs = EditorGUILayout.Toggle(EditorMeta.Settings.ShowLogs, PerUserSettingsRuntime.ShowDebugLogs);
            
            GUILayout.Space(1.5f);
            EditorGUILayout.EndVertical();
        }
        
        
        
        private static void DrawRuntimeOptions()
        {
            PerUserSettings.RuntimeSettingsEditorTab = EditorGUILayout.Foldout(PerUserSettings.RuntimeSettingsEditorTab, "Options");

            if (!PerUserSettings.RuntimeSettingsEditorTab) return;
            
            EditorGUILayout.BeginVertical("Box");
            GUILayout.Space(1.5f);
              
            // Save Location
            EditorGUILayout.PropertyField(UtilEditor.SettingsObject.Fp("saveLocation"), EditorMeta.Settings.SaveLocation);

            DrawLegacySettings();
            
            GUILayout.Space(1.5f);
            EditorGUILayout.EndVertical();
        }


        private static void DrawLegacySettings()
        {
            PerUserSettings.EditorLegacyShown = EditorGUILayout.Foldout(PerUserSettings.EditorLegacyShown, "Legacy Convert Settings");

            if (!PerUserSettings.EditorLegacyShown) return;
            
            EditorGUILayout.BeginVertical();
            EditorGUI.BeginDisabledGroup(true);
            
            EditorGUILayout.HelpBox("Board id's and types to convert to if an older 2.0.x save is in the user's data", MessageType.None);
            
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField(EditorMeta.LegacyPortDictionary.KeyLabel);
            EditorGUILayout.LabelField(EditorMeta.LegacyPortDictionary.ValueLabel);
            EditorGUILayout.EndHorizontal();
            
            for (var i = 0; i < UtilEditor.SettingsObject.Fp("legacyBoardPortTypes").Fpr("list").arraySize; i++)
            {
                var element = UtilEditor.SettingsObject.Fp("legacyBoardPortTypes").Fpr("list").GetIndex(i);
             
                EditorGUILayout.BeginHorizontal();
                
                EditorGUILayout.PropertyField(element.Fpr("key"), GUIContent.none);
                
                GUI.backgroundColor = UtilEditor.Yellow;
                EditorGUILayout.PropertyField(element.Fpr("value"), GUIContent.none);
                GUI.backgroundColor = Color.white;
                
                EditorGUILayout.EndHorizontal();
            }
            
            EditorGUI.EndDisabledGroup();
            EditorGUILayout.EndVertical();
        }
        
        
        
        /// <summary>
        /// Draws the buttons section of the window.
        /// </summary>
        private static void DrawButtons()
        {
            EditorGUILayout.BeginHorizontal();
            
            
            if (GUILayout.Button("GitHub", GUILayout.Height(30), GUILayout.MinWidth(100)))
            {
                Application.OpenURL("https://github.com/CarterGames/LeaderboardManager");
            }

            
            if (GUILayout.Button("Documentation", GUILayout.Height(30), GUILayout.MinWidth(100)))
            {
                Application.OpenURL("https://carter.games/docs/leaderboardmanager");
            }

            
            if (GUILayout.Button("Support", GUILayout.Height(30), GUILayout.MinWidth(100)))
            {
                Application.OpenURL("https://carter.games/contact");
            }
            
            
            EditorGUILayout.EndHorizontal();

            if (UtilEditor.CarterGamesBanner != null)
            {
                var defaultTextColour = GUI.contentColor;
                GUI.contentColor = new Color(1, 1, 1, .75f);
            
                if (GUILayout.Button(UtilEditor.CarterGamesBanner, GUILayout.MaxHeight(40)))
                {
                    Application.OpenURL("https://carter.games");
                }
            
                GUI.contentColor = defaultTextColour;
            }
            else
            {
                if (GUILayout.Button("Carter Games", GUILayout.MaxHeight(40)))
                {
                    Application.OpenURL("https://carter.games");
                }
            }
        }
    }
}