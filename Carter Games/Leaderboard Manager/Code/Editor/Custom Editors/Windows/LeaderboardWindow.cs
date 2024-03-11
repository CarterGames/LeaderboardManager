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

using UnityEditor;
using UnityEngine;

namespace CarterGames.Assets.LeaderboardManager.Editor
{
    /// <summary>
    /// The editor window for the leaderboard manager.
    /// </summary>
    public sealed class LeaderboardWindow : EditorWindow
    {
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Fields
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        private static bool isInitialized;
        
        private EditorTab editorTab;
        private LegacyTab legacyTab;
        
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Window Access Method
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        /// <summary>
        /// Adds the menu item to open the window.
        /// </summary>
        [MenuItem("Tools/Carter Games/Leaderboard Manager/Leaderboard Editor", priority = 15)]
        private static void ShowWindow()
        {
            isInitialized = false;
            
            var window = GetWindow<LeaderboardWindow>("Leaderboard Editor");
            window.titleContent = new GUIContent("Leaderboard Editor", UtilEditor.EditorWindowIcon);
            window.Show();
        }

        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Unity Methods
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        private void OnEnable()
        {
            isInitialized = false;
        }

        
        private void OnGUI()
        {
            Initialize();

            PerUserSettings.EditorWindowTabPos = GUILayout.Toolbar(PerUserSettings.EditorWindowTabPos,
                new Texture[2] { UtilEditor.EditorTitle, UtilEditor.LegacyTitle }, GUILayout.Height(30f));
            
            EditorGUILayout.Space(2.5f);

            switch (PerUserSettings.EditorWindowTabPos)
            {
                case 0:
                    editorTab.DrawTab();
                    break;
                case 1:
                    legacyTab.DrawTab();
                    break;
            }
            
       
        }
        
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Utility Methods
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        /// <summary>
        /// Initializes the window when called if its not already.
        /// </summary>
        private void Initialize()
        {
            if (isInitialized) return;
            
            AssetIndexHandler.UpdateIndex();
            
            editorTab = new EditorTab();
            editorTab.Initialize();

            legacyTab = new LegacyTab();
            
            isInitialized = true;
        }
    }
}