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
    /// Draws the custom inspector for the asset settings.
    /// </summary>
    [CustomEditor(typeof(SettingsAssetRuntime))]
    public sealed class SettingsAssetRuntimeEditor : UnityEditor.Editor
    {
        /* —————————————————————————————————————————————————————————————————————————————————————————————————————————————
        |   GUI
        ————————————————————————————————————————————————————————————————————————————————————————————————————————————— */
        
        public override void OnInspectorGUI()
        {
            GUILayout.Space(5f);
            UtilEditor.DrawSoScriptSection(target as SettingsAssetRuntime);
            
            GUILayout.Space(5f);
            
            SaveLocationProperty();
            
            GUILayout.Space(5f);
            
            LegacyPortingSettingsProperty();
            
            GUILayout.Space(2.5f);
        }

        /* —————————————————————————————————————————————————————————————————————————————————————————————————————————————
        |   Methods
        ————————————————————————————————————————————————————————————————————————————————————————————————————————————— */
        
        /// <summary>
        /// Draws the save location option.
        /// </summary>
        private void SaveLocationProperty()
        {
            EditorGUI.BeginDisabledGroup(true);
            EditorGUILayout.BeginVertical("HelpBox");
            EditorGUILayout.Space(1f);
            
            EditorGUILayout.PropertyField(UtilEditor.SettingsObject.Fp("saveLocation"));
            
            EditorGUILayout.Space(1.5f);
            EditorGUILayout.EndVertical();
            EditorGUI.EndDisabledGroup();
        }


        /// <summary>
        /// Draws the legacy porting option.
        /// </summary>
        private void LegacyPortingSettingsProperty()
        {
            EditorGUILayout.BeginVertical("HelpBox");
            EditorGUILayout.Space(1f);

            EditorGUI.BeginDisabledGroup(true);
            EditorGUI.indentLevel++;
            
            EditorGUILayout.PropertyField(UtilEditor.SettingsObject.Fp("legacyBoardPortTypes"));
            
            EditorGUI.indentLevel--;
            EditorGUI.EndDisabledGroup();
            
            EditorGUILayout.Space(1.5f);
            EditorGUILayout.EndVertical();
        }
    }
}