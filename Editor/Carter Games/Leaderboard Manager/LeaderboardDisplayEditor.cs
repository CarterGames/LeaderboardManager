using UnityEngine;
using UnityEditor;

/****************************************************************************************************************************
 * 
 *  --{   Leaderboard Manager   }--
 *							  
 *	Leaderboard Display Editor Script
 *      Overrides the default leaderboard display script inspector with this.
 *			
 *  Written by:
 *      Jonathan Carter
 *      E: jonathan@carter.games
 *      W: https://jonathan.carter.games
 *		
 *  Version: 1.0.4
 *	Last Updated: 05/02/2021 (d/m/y)						
 * 
****************************************************************************************************************************/

namespace CarterGames.Assets.LeaderboardManager
{
    /// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    /// <summary>
    /// Editor Class (*Not Static*) | Edits the inspector for the leaderboard display script
    /// </summary>
    /// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    [CustomEditor(typeof(LeaderboardDisplay))]
    public class LeaderboardDisplayEditor : Editor
    {
        // Reference to LeaderboardDisaply Script
        private LeaderboardDisplay display;


        /// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Overrides the default inspector for the Leaderboard Display Script 
        /// Note: this is not required to use the asset, but make it look a little cleaner xD
        /// </summary>
        /// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        public override void OnInspectorGUI()
        {
            // References to the script
            display = (LeaderboardDisplay)target;

            GUILayout.Space(10);

            // Header display * Start *
            EditorGUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();

            // Shows either the Leaderboard Manager Logo or an alternative for if the icon is deleted when you import the package
            if (Resources.Load<Texture2D>("Carter Games/Leaderboard Manager/LogoLBM"))
            {
                if (GUILayout.Button(Resources.Load<Texture2D>("Carter Games/Leaderboard Manager/LogoLBM"), GUIStyle.none, GUILayout.Width(50), GUILayout.Height(50)))
                {
                    GUI.FocusControl(null);
                }
            }
            else
            {
                EditorGUILayout.BeginHorizontal();
                GUILayout.FlexibleSpace();
                EditorGUILayout.LabelField("Leaderboard Manager", EditorStyles.boldLabel, GUILayout.Width(150));
                GUILayout.FlexibleSpace();
                EditorGUILayout.EndHorizontal();
            }

            GUILayout.FlexibleSpace();
            EditorGUILayout.EndHorizontal();


            GUILayout.Space(5);
            EditorGUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            EditorGUILayout.LabelField("Leaderboard Display", EditorStyles.boldLabel, GUILayout.Width(140));
            GUILayout.FlexibleSpace();
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            EditorGUILayout.LabelField("Version: 1.0.4", GUILayout.Width(87.5f));
            GUILayout.FlexibleSpace();
            EditorGUILayout.EndHorizontal();

            GUILayout.Space(10);
            // Header display * End *



            EditorGUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            if (GUILayout.Button("Documentation", GUILayout.Width(110f)))
            {
                Application.OpenURL("https://carter.games/leaderboardmanager");
            }
            GUI.backgroundColor = Color.cyan;
            if (GUILayout.Button("Discord", GUILayout.Width(65f)))
            {
                Application.OpenURL("https://carter.games/discord");
            }
            GUI.backgroundColor = Color.red;
            if (GUILayout.Button("Report Issue", GUILayout.Width(100f)))
            {
                Application.OpenURL("https://carter.games/report");
            }
            GUI.backgroundColor = Color.white;
            GUI.color = Color.white;
            GUILayout.FlexibleSpace();
            EditorGUILayout.EndHorizontal();


            // Remaining inspector
            GUILayout.Space(10);

            EditorGUILayout.LabelField("References", EditorStyles.boldLabel);

            // reference needed to the display content gameobject.
            SerializedProperty displayProp = serializedObject.FindProperty("leaderboardDisplay");
            EditorGUILayout.PropertyField(displayProp, new GUIContent("Display Object:"));

            // reference needed to the row prefab for new rows.
            SerializedProperty rowProp = serializedObject.FindProperty("leaderboardRowPrefab");
            EditorGUILayout.PropertyField(rowProp, new GUIContent("Row Prefab:"));

            GUILayout.Space(10);

            EditorGUILayout.LabelField("Configuration", EditorStyles.boldLabel);

            EditorGUILayout.BeginHorizontal();

            // Controls how many entires should be shown
            SerializedProperty showAmountProp = serializedObject.FindProperty("showSpecificAmount");
            EditorGUILayout.LabelField("Show Specific Number Of Entries:", GUILayout.MinWidth(195));
            EditorGUILayout.PropertyField(showAmountProp, new GUIContent(""));
            serializedObject.ApplyModifiedProperties();
            EditorGUILayout.EndHorizontal();

            if (showAmountProp.boolValue)
            {
                SerializedProperty numberToShowProp = serializedObject.FindProperty("numberToShow");
                EditorGUILayout.PropertyField(numberToShowProp, new GUIContent("Number To Show:"));
            }

            GUILayout.Space(10);

            EditorGUILayout.LabelField("Layout Options", EditorStyles.boldLabel);

            // Controls the display settings
            SerializedProperty optionProp = serializedObject.FindProperty("displayChoice");
            EditorGUILayout.PropertyField(optionProp, new GUIContent("Order:"));
            serializedObject.ApplyModifiedProperties();
            serializedObject.Update();
        }
    }
}