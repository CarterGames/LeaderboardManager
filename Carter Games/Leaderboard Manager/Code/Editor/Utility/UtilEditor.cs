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

using UnityEditor;
using UnityEngine;

namespace CarterGames.Assets.LeaderboardManager.Editor
{
    /// <summary>
    /// A utility class for editor specific bits.
    /// </summary>
    public static class UtilEditor
    {
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Fields
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */

        // Paths
        /* ────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        public const string SettingsWindowPath = "Project/Carter Games/Leaderboard Manager";
        
        
        // Filters
        /* ────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        private const string CarterGamesBannerFilter = "T_CarterGamesBanner";
        private const string AssetLogoFilter = "T_LeaderboardManager_Logo";
        private const string EditorWindowIconFilter = "T_LeaderboardManager_EditorWindowIcon";
        
        
        // Titles
        /* ────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        private const string EditorTitleFilter = "T_LeaderboardManager_EditorWindow_EditorTabTitle";
        private const string LegacyTitleFilter = "T_LeaderboardManager_EditorWindow_LegacyTabTitle";
        
        
        // Asset Caches
        /* ────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        private static Texture2D carterGamesBannerCache;
        private static Texture2D assetLogoCache;
        private static Texture2D editorWindowIcon;
        private static Texture2D editorTitle;
        private static Texture2D legacyTitle;
        
        
        // Colours
        /* ────────────────────────────────────────────────────────────────────────────────────────────────────────── */

        /// <summary>
        /// Green Apple (Green) Color
        /// </summary>
        /// <see cref="https://www.computerhope.com/cgi-bin/htmlcolor.pl?c=4CC417"/>
        public static readonly Color Green = new Color32(76, 196, 23, 255);

        
        /// <summary>
        /// Rubber Ducky Yellow (Yellow) Color
        /// </summary>
        /// <see cref="https://www.computerhope.com/cgi-bin/htmlcolor.pl?c=FFD801"/>
        public static readonly Color Yellow = new Color32(255, 216, 1, 255);
        

        /// <summary>
        /// Scarlet Red (Red) Color
        /// </summary>
        /// <see cref="https://www.computerhope.com/cgi-bin/htmlcolor.pl?c=FF2400"/>
        public static readonly Color Red = new Color32(255, 36, 23, 255);
        
        
        /// <summary>
        /// Carbon Grey (Grey) Color
        /// </summary>
        /// <see cref="https://www.computerhope.com/cgi-bin/htmlcolor.pl?c=625D5D"/>
        public static readonly Color Grey = new Color32(98, 93, 93, 255);
        

        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Properties
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        // Graphics
        /* ────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        /// <summary>
        /// Gets the carter games banner.
        /// </summary>
        public static Texture2D CarterGamesBanner => FileEditorUtil.GetOrAssignCache(ref carterGamesBannerCache, CarterGamesBannerFilter);
        
        
        /// <summary>
        /// Gets the asset logo
        /// </summary>
        public static Texture2D AssetLogo => FileEditorUtil.GetOrAssignCache(ref assetLogoCache, AssetLogoFilter);
        
        
        
        /// <summary>
        /// The icon for the leaderboard editor window
        /// </summary>
        public static Texture2D EditorWindowIcon => FileEditorUtil.GetOrAssignCache(ref editorWindowIcon, EditorWindowIconFilter);
        
        // Titles
        /* ────────────────────────────────────────────────────────────────────────────────────────────────────────── */

        /// <summary>
        /// The editor tab title for the editor window.
        /// </summary>
        public static Texture2D EditorTitle => FileEditorUtil.GetOrAssignCache(ref editorTitle, EditorTitleFilter);
        
        
        /// <summary>
        /// The legacy tab title for the editor window.
        /// </summary>
        public static Texture2D LegacyTitle => FileEditorUtil.GetOrAssignCache(ref legacyTitle, LegacyTitleFilter);
        

        // Assets
        /* ────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        /// <summary>
        /// Gets/Sets the Leaderboard Manager settings asset.
        /// </summary>
        public static AssetIndex AssetIndex => ScriptableRef.AssetIndex;
        
        
        /// <summary>
        /// Gets the settings asset for use.
        /// </summary>
        public static SettingsAssetRuntime RuntimeSettings => ScriptableRef.RuntimeSettings;
        
        
        /// <summary>
        /// Gets the settings asset for use.
        /// </summary>
        public static SerializedObject SettingsObject => ScriptableRef.RuntimeSettingsObject;
        
        
        /// <summary>
        /// Gets if there is a settings asset in the project.
        /// </summary>
        public static bool HasInitialized
        {
            get
            {
                AssetIndexHandler.UpdateIndex();
                return ScriptableRef.HasAllAssets;
            }
        }
        
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Methods
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */

        /// <summary>
        /// Initializes the class when called.
        /// </summary>
        public static void Initialize()
        {
            if (HasInitialized) return;

            var index = AssetIndex;
            var rSettings = RuntimeSettings; ;

            AssetIndexHandler.UpdateIndex();
            
            EditorUtility.SetDirty(AssetIndex);
            EditorUtility.SetDirty(RuntimeSettings);

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
        
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Editor Drawer Methods
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        /// <summary>
        /// Draws the header logo for the asset.
        /// </summary>
        public static void DrawHeaderWithTexture(Texture texture)
        {
            EditorGUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            
            if (texture != null)
            {
                if (GUILayout.Button(texture, GUIStyle.none, GUILayout.MaxHeight(110)))
                {
                    GUI.FocusControl(null);
                }
            }
            
            GUILayout.FlexibleSpace();
            EditorGUILayout.EndHorizontal();
        }
        
        
        /// <summary>
        /// Draws the script fields in the custom inspector...
        /// </summary>
        public static void DrawMonoScriptSection<T>(T target) where T : MonoBehaviour
        {
            EditorGUILayout.BeginVertical("HelpBox");
            GUILayout.Space(1.5f);
            
            EditorGUI.BeginDisabledGroup(true);
            EditorGUILayout.ObjectField("Script:", MonoScript.FromMonoBehaviour(target), typeof(T), false);
            EditorGUI.EndDisabledGroup();
            
            GUILayout.Space(1.5f);
            EditorGUILayout.EndVertical();
        }
        
        
        /// <summary>
        /// Draws the script fields in the custom inspector...
        /// </summary>
        public static void DrawSoScriptSection<T>(T target) where T : ScriptableObject
        {
            EditorGUILayout.BeginVertical("HelpBox");
            GUILayout.Space(1.5f);
            
            EditorGUI.BeginDisabledGroup(true);
            EditorGUILayout.ObjectField("Script:", MonoScript.FromScriptableObject(target), typeof(T), false);
            EditorGUI.EndDisabledGroup();
            
            GUILayout.Space(1.5f);
            EditorGUILayout.EndVertical();
        }
        
        
        
        /// <summary>
        /// Creates a deselect zone to let users click outside of any editor window to unfocus from their last selected field.
        /// </summary>
        /// <param name="rect">The rect to draw on.</param>
        public static void CreateDeselectZone(ref Rect rect)
        {
            if (rect.width <= 0)
            {
                rect = new Rect(0, 0, Screen.width, Screen.height);
            }

            if (GUI.Button(rect, string.Empty, GUIStyle.none))
            {
                GUI.FocusControl(null);
            }
        }
        
        
        /// <summary>
        /// Draws a horizontal line
        /// </summary>
        public static void DrawHorizontalGUILine()
        {
            GUIStyle boxStyle = new GUIStyle(GUI.skin.box);
            boxStyle.normal.background = new Texture2D(1, 1);
            boxStyle.normal.background.SetPixel(0, 0, Color.gray);
            boxStyle.normal.background.Apply();

            var one = EditorGUILayout.BeginHorizontal();
            GUILayout.Box("", boxStyle, GUILayout.ExpandWidth(true), GUILayout.Height(2));
            EditorGUILayout.EndHorizontal();
        }
        
        
        /// <summary>
        /// Draws a horizontal line
        /// </summary>
        public static void DrawHorizontalGUILine(Color lineCol)
        {
            GUIStyle boxStyle = new GUIStyle(GUI.skin.box);
            boxStyle.normal.background = new Texture2D(1, 1);
            boxStyle.normal.background.SetPixel(0, 0, lineCol);
            boxStyle.normal.background.Apply();

            var one = EditorGUILayout.BeginHorizontal();
            GUILayout.Box("", boxStyle, GUILayout.ExpandWidth(true), GUILayout.Height(2));
            EditorGUILayout.EndHorizontal();
        }
        
        
        /// <summary>
        /// Draws a vertical line
        /// </summary>
        public static Rect DrawVerticalGUILine()
        {
            GUIStyle boxStyle = new GUIStyle(GUI.skin.box);
            boxStyle.normal.background = new Texture2D(1, 1);
            boxStyle.normal.background.SetPixel(0, 0, Color.grey);
            boxStyle.normal.background.Apply();

            var one = EditorGUILayout.BeginVertical();
            GUILayout.Box("", boxStyle, GUILayout.ExpandHeight(true), GUILayout.Width(2));
            EditorGUILayout.EndVertical();

            return one;
        }
        
        
        /// <summary>
        /// Draws a vertical line
        /// </summary>
        public static void DrawVerticalGUILine(Color lineCol)
        {
            GUIStyle boxStyle = new GUIStyle(GUI.skin.box);
            boxStyle.normal.background = new Texture2D(1, 1);
            boxStyle.normal.background.SetPixel(0, 0, lineCol);
            boxStyle.normal.background.Apply();

            var one = EditorGUILayout.BeginVertical();
            GUILayout.Box("", boxStyle, GUILayout.ExpandHeight(true), GUILayout.Width(2));
            EditorGUILayout.EndVertical();
        }

        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Editor Extension Methods
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        /// <summary>
        /// Gets the width of a string's GUI.
        /// </summary>
        /// <param name="text">The text to size.</param>
        /// <returns>The resulting size.</returns>
        public static float Width(this string text)
        {
            return GUI.skin.label.CalcSize(new GUIContent(text)).x;
        }
    }
}