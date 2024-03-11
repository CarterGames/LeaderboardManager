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

namespace CarterGames.Assets.LeaderboardManager.Editor
{
    /// <summary>
    /// Handles the base inspector logic for the LeaderboardDisplay class.
    /// </summary>
    [CustomEditor(typeof(LeaderboardDisplay))]
    public class LeaderboardDisplayEditor : UnityEditor.Editor
    {
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   GUI
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        public override void OnInspectorGUI()
        {
            UtilEditor.DrawMonoScriptSection((LeaderboardDisplay) target);
            EditorGUILayout.Space(5f);
            FirstSetupInspector();
            EditorGUILayout.Space(5f);
            DisplaySetupInspector();
            EditorGUILayout.Space(5f);
            CustomisationInspector();
            serializedObject.ApplyModifiedProperties();
            serializedObject.Update();
        }
        
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Methods
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        private void FirstSetupInspector()
        {
            EditorGUILayout.BeginVertical("HelpBox");
            EditorGUILayout.Space(1.5f);
            
            EditorGUILayout.LabelField("Leaderboard", EditorStyles.boldLabel);
            
            UtilEditor.DrawHorizontalGUILine();
            
            EditorGUILayout.PropertyField(serializedObject.Fp("boardId"));
            EditorGUILayout.PropertyField(serializedObject.Fp("displayOption"));
            
            EditorGUILayout.Space(1.5f);
            EditorGUILayout.EndVertical();
        }


        private void DisplaySetupInspector()
        {
            EditorGUILayout.BeginVertical("HelpBox");
            EditorGUILayout.Space(1.5f);
            

            if (serializedObject.Fp("displayOption").intValue <= 0)
            {
                EditorGUILayout.HelpBox("Assign a display option to enable these options", MessageType.Info);
            }
            
            EditorGUI.BeginDisabledGroup(serializedObject.Fp("displayOption").intValue <= 0);
            
            EditorGUILayout.LabelField("Display Setup", EditorStyles.boldLabel);
            
            UtilEditor.DrawHorizontalGUILine();
                
            EditorGUILayout.PropertyField(serializedObject.Fp("boardParent"));
            EditorGUILayout.PropertyField(serializedObject.Fp("rowPrefab"));

            EditorGUI.BeginDisabledGroup(serializedObject.Fp("displayOption").intValue == (int)DisplayOption.Unassigned || 
                                         serializedObject.Fp("displayOption").intValue == (int)DisplayOption.Top3Ascending ||
                                         serializedObject.Fp("displayOption").intValue == (int)DisplayOption.Top3Descending);
            
            EditorGUILayout.PropertyField(serializedObject.Fp("entriesToShow"));

            EditorGUI.EndDisabledGroup();

            EditorGUILayout.PropertyField(serializedObject.Fp("customisations").Fpr("startAt"));
            
            EditorGUILayout.Space(1.5f);
            EditorGUILayout.EndVertical();
            
            EditorGUI.EndDisabledGroup();
        }


        private void CustomisationInspector()
        {
            EditorGUILayout.BeginVertical("HelpBox");
            EditorGUILayout.Space(1.5f);
            
            if (serializedObject.Fp("displayOption").intValue <= 0)
            {
                EditorGUILayout.HelpBox("Assign a display option to enable these options", MessageType.Info);
            }
            
            EditorGUI.BeginDisabledGroup(serializedObject.Fp("displayOption").intValue <= 0);
            
            EditorGUILayout.LabelField("Display Customisation", EditorStyles.boldLabel);
            
            UtilEditor.DrawHorizontalGUILine();

            CustomisationElements();
            
            EditorGUILayout.Space(1.5f);
            EditorGUILayout.EndVertical();
            
            EditorGUI.EndDisabledGroup();
        }


        protected virtual void CustomisationElements()
        {
            EditorGUILayout.PropertyField(serializedObject.Fp("customisations").Fpr("showPosition"));

            EditorGUI.BeginDisabledGroup(!serializedObject.Fp("customisations").Fpr("showPosition").boolValue);
            EditorGUILayout.PropertyField(serializedObject.Fp("customisations").Fpr("positionPrefix"));
            EditorGUI.EndDisabledGroup();
        }
    }
}