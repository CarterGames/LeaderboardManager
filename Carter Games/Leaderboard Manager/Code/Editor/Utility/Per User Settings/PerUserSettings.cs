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

using System;
using UnityEditor;
using UnityEngine;

namespace CarterGames.Assets.LeaderboardManager.Editor
{
    /// <summary>
    /// Handles all the per user settings.
    /// </summary>
    public static class PerUserSettings
    {
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Fields
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        private const string UniqueIdId = "CarterGames_LeaderboardManager_Editor_UUID";
        
        private static readonly string AutoValidationAutoCheckId = $"{UniqueId}_CarterGames_LeaderboardManager_EditorSettings_AutoVersionCheck";
        
        private static readonly string EditorSettingsEditorTabId = $"{UniqueId}_CarterGames_LeaderboardManager_EditorSettings_EditorDropdown";
        private static readonly string RuntimeSettingsEditorTabId = $"{UniqueId}_CarterGames_LeaderboardManager_EditorSettings_RuntimeDropdown";
        
        private static readonly string EditorWindowTabPosId = $"{UniqueId}_CarterGames_LeaderboardManager_EditorWindow_MainTabPos";
        private static readonly string LegacyWindowTabPosId = $"{UniqueId}_CarterGames_LeaderboardManager_LegacyWindow_MainTabPos";
        
        private static readonly string EditorWindowScrollRectId = $"{UniqueId}_CarterGames_LeaderboardManager_EditorWindow_MainScrollRect";
        
        private static readonly string EditorLegacyShownId = $"{UniqueId}_CarterGames_LeaderboardManager_EditorSettings_LegacySettingsShown";
        
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Properties
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        /// <summary>
        /// The unique if for the assets settings to be per project...
        /// </summary>
        /// <remarks>
        /// Saved to player pref to allow settings to be different per project in the same editor version.
        /// </remarks>
        private static string UniqueId => (string)GetOrCreateValue<string>(UniqueIdId, SettingType.PlayerPref, Guid.NewGuid().ToString());
        
        
        /// <summary>
        /// Has the scanner been initialized.
        /// </summary>
        public static bool VersionValidationAutoCheckOnLoad
        {
            get => (bool) GetOrCreateValue<bool>(AutoValidationAutoCheckId, SettingType.EditorPref);
            set => SetValue<bool>(AutoValidationAutoCheckId, SettingType.EditorPref, value);
        }
        
        
        /// <summary>
        /// If the editor tab in the editor window is active or not.
        /// </summary>
        public static bool EditorSettingsEditorTab
        {
            get => (bool)GetOrCreateValue<bool>(EditorSettingsEditorTabId, SettingType.EditorPref);
            set => SetValue<bool>(EditorSettingsEditorTabId, SettingType.EditorPref, value);
        }
        
        
        /// <summary>
        /// If the runtime tab in the runtime window is active or not.
        /// </summary>
        public static bool RuntimeSettingsEditorTab
        {
            get => (bool)GetOrCreateValue<bool>(RuntimeSettingsEditorTabId, SettingType.EditorPref);
            set => SetValue<bool>(RuntimeSettingsEditorTabId, SettingType.EditorPref, value);
        }
        
        
        /// <summary>
        /// The tab position of the leaderboard editor window.
        /// </summary>
        public static int EditorWindowTabPos
        {
            get => (int)GetOrCreateValue<int>(EditorWindowTabPosId, SettingType.EditorPref);
            set => SetValue<int>(EditorWindowTabPosId, SettingType.EditorPref, value);
        }
        
        
        public static int LegacyWindowTabPos
        {
            get => (int)GetOrCreateValue<int>(LegacyWindowTabPosId, SettingType.EditorPref);
            set => SetValue<int>(LegacyWindowTabPosId, SettingType.EditorPref, value);
        }
        
        
        public static bool EditorLegacyShown
        {
            get => (bool)GetOrCreateValue<bool>(EditorLegacyShownId, SettingType.EditorPref);
            set => SetValue<bool>(EditorLegacyShownId, SettingType.EditorPref, value);
        }
        
        
        public static Vector2 EditorWindowScrollRect
        {
            get => (Vector2)GetOrCreateValue<Vector2>(EditorWindowScrollRectId, SettingType.EditorPref);
            set => SetValue<Vector2>(EditorWindowScrollRectId, SettingType.EditorPref, value);
        }


        public static bool IsBoardExpanded(string id)
        {
            return (bool)GetOrCreateValue<bool>(id + "_IsExpanded", SettingType.EditorPref);
        }
        
        
        public static void SetIsBoardExpanded(string id, bool value)
        {
            SetValue<bool>(id + "_IsExpanded", SettingType.EditorPref, value);
        }
        
        
        public static Vector2 BoardPosition(string id)
        {
            return (Vector2)GetOrCreateValue<Vector2>(id + "_Position", SettingType.EditorPref);
        }
        
        
        public static void SetBoardPosition(string id, Vector2 value)
        {
            SetValue<Vector2>(id + "_Position", SettingType.EditorPref, value);
        }
        
        
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Methods
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */

        private static object GetOrCreateValue<T>(string key, SettingType type, object defaultValue = null)
        {
            switch (type)
            {
                case SettingType.EditorPref:

                    if (EditorPrefs.HasKey(key))
                    {
                        switch (typeof(T))
                        {
                            case var x when x == typeof(bool):
                                return EditorPrefs.GetBool(key);
                            case var x when x == typeof(int):
                                return EditorPrefs.GetInt(key);
                            case var x when x == typeof(float):
                                return EditorPrefs.GetFloat(key);
                            case var x when x == typeof(string):
                                return EditorPrefs.GetString(key);
                            case var x when x == typeof(Vector2):
                                return JsonUtility.FromJson<Vector2>(EditorPrefs.GetString(key));
                            default:
                                return null;
                        }
                    }

                    switch (typeof(T))
                    {
                        case var x when x == typeof(bool):
                            EditorPrefs.SetBool(key, defaultValue == null ? false : (bool)defaultValue);
                            return EditorPrefs.GetBool(key);
                        case var x when x == typeof(int):
                            EditorPrefs.SetInt(key, defaultValue == null ? 0 : (int)defaultValue);
                            return EditorPrefs.GetInt(key);
                        case var x when x == typeof(float):
                            EditorPrefs.SetFloat(key, defaultValue == null ? 0 : (float)defaultValue);
                            return EditorPrefs.GetFloat(key);
                        case var x when x == typeof(string):
                            EditorPrefs.SetString(key, (string)defaultValue);
                            return EditorPrefs.GetString(key);
                        case var x when x == typeof(Vector2):
                            EditorPrefs.SetString(key,
                                defaultValue == null
                                    ? JsonUtility.ToJson(Vector2.zero)
                                    : JsonUtility.ToJson(defaultValue));
                            return JsonUtility.FromJson<Vector2>(EditorPrefs.GetString(key));
                        default:
                            return null;
                    }
                    
                case SettingType.PlayerPref:
                    
                    if (PlayerPrefs.HasKey(key))
                    {
                        switch (typeof(T))
                        {
                            case var x when x == typeof(bool):
                                return PlayerPrefs.GetInt(key) == 1;
                            case var x when x == typeof(int):
                                return PlayerPrefs.GetInt(key);
                            case var x when x == typeof(float):
                                return PlayerPrefs.GetFloat(key);
                            case var x when x == typeof(string):
                                return PlayerPrefs.GetString(key);
                            case var x when x == typeof(Vector2):
                                return JsonUtility.FromJson<Vector2>(PlayerPrefs.GetString(key));
                            default:
                                return null;
                        }
                    }

                    switch (typeof(T))
                    {
                        case var x when x == typeof(bool):
                            PlayerPrefs.SetInt(key,
                                defaultValue == null ? 0 : defaultValue.ToString().ToLower() == "true" ? 1 : 0);
                            return PlayerPrefs.GetInt(key) == 1;
                        case var x when x == typeof(int):
                            PlayerPrefs.SetInt(key, defaultValue == null ? 0 : (int)defaultValue);
                            return PlayerPrefs.GetInt(key);
                        case var x when x == typeof(float):
                            PlayerPrefs.SetFloat(key, defaultValue == null ? 0 : (float)defaultValue);
                            return PlayerPrefs.GetFloat(key);
                        case var x when x == typeof(string):
                            PlayerPrefs.SetString(key, (string)defaultValue);
                            return PlayerPrefs.GetString(key);
                        case var x when x == typeof(Vector2):
                            PlayerPrefs.SetString(key,
                                defaultValue == null
                                    ? JsonUtility.ToJson(Vector2.zero)
                                    : JsonUtility.ToJson(defaultValue));
                            return JsonUtility.FromJson<Vector2>(PlayerPrefs.GetString(key));
                        default:
                            return null;
                    }
                    
                case SettingType.SessionState:

                    switch (typeof(T))
                    {
                        case var x when x == typeof(bool):
                            return SessionState.GetBool(key, defaultValue == null ? false : (bool)defaultValue);
                        case var x when x == typeof(int):
                            return SessionState.GetInt(key, defaultValue == null ? 0 : (int)defaultValue);
                        case var x when x == typeof(float):
                            return SessionState.GetFloat(key, defaultValue == null ? 0 : (float)defaultValue);
                        case var x when x == typeof(string):
                            return SessionState.GetString(key, (string)defaultValue);
                        case var x when x == typeof(Vector2):
                            return JsonUtility.FromJson<Vector2>(SessionState.GetString(key,
                                JsonUtility.ToJson(defaultValue)));
                        default:
                            return null;
                    }
                    
                default:
                    return null;
            }
        }


        private static void SetValue<T>(string key, SettingType type, object value)
        {
            switch (type)
            {
                case SettingType.EditorPref:
                    
                    switch (typeof(T))
                    {
                        case var x when x == typeof(bool):
                            EditorPrefs.SetBool(key, (bool)value);
                            break;
                        case var x when x == typeof(int):
                            EditorPrefs.SetInt(key, (int)value);
                            break;
                        case var x when x == typeof(float):
                            EditorPrefs.SetFloat(key, (float)value);
                            break;
                        case var x when x == typeof(string):
                            EditorPrefs.SetString(key, (string)value);
                            break;
                        case var x when x == typeof(Vector2):
                            EditorPrefs.SetString(key, JsonUtility.ToJson(value));
                            break;
                    }
                    
                    break;
                case SettingType.PlayerPref:
                    
                    switch (typeof(T))
                    {
                        case var x when x == typeof(bool):
                            PlayerPrefs.SetInt(key, ((bool)value) ? 1 : 0);
                            break;
                        case var x when x == typeof(int):
                            PlayerPrefs.SetInt(key, (int)value);
                            break;
                        case var x when x == typeof(float):
                            PlayerPrefs.SetFloat(key, (float)value);
                            break;
                        case var x when x == typeof(string):
                            PlayerPrefs.SetString(key, (string)value);
                            break;
                        case var x when x == typeof(Vector2):
                            PlayerPrefs.SetString(key, JsonUtility.ToJson(value));
                            break;
                    }
                    
                    PlayerPrefs.Save();
                    
                    break;
                case SettingType.SessionState:
                    
                    switch (typeof(T))
                    {
                        case var x when x == typeof(bool):
                            SessionState.SetBool(key, (bool)value);
                            break;
                        case var x when x == typeof(int):
                            SessionState.SetInt(key, (int)value);
                            break;
                        case var x when x == typeof(float):
                            SessionState.SetFloat(key, (float)value);
                            break;
                        case var x when x == typeof(string):
                            SessionState.SetString(key, (string)value);
                            break;
                        case var x when x == typeof(Vector2):
                            SessionState.SetString(key, JsonUtility.ToJson(value));
                            break;
                    }
                    
                    break;
            }
        }

        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Menu Items
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        [MenuItem("Tools/Carter Games/Leaderboard Manager/Reset User Editor Settings")]
        public static void ResetPrefs()
        {
            // Reset per user settings....
            PerUserSettingsRuntime.ResetPrefs();
        }
    }
}