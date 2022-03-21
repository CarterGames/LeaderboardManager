using UnityEditor;
using UnityEngine;

namespace CarterGames.Assets.LeaderboardManager.Editor
{
    [CustomEditor(typeof(LeaderboardDisplay))]
    public class LeaderboardDisplayEditor : UnityEditor.Editor
    {
        private SerializedProperty boardID;
        private SerializedProperty displayOption;
        private SerializedProperty boardParent;
        private SerializedProperty rowPrefab;

        private SerializedProperty entriesToShow;
        private SerializedProperty startAt;
        
        private SerializedProperty showOptions;
        private SerializedProperty showPosition;
        private SerializedProperty positionPrefix;
        private SerializedProperty positionSuffix;
        private SerializedProperty positionIndex;
        private SerializedProperty nameIndex;
        private SerializedProperty scoreIndex;
        
        private SerializedProperty formatScoreAsTime;
        private SerializedProperty timeFormat;


        protected virtual void OnEnable()
        {
            boardID = serializedObject.FindProperty("boardID");
            displayOption = serializedObject.FindProperty("displayOption");
            boardParent = serializedObject.FindProperty("boardParent");
            rowPrefab = serializedObject.FindProperty("rowPrefab");

            entriesToShow = serializedObject.FindProperty("entriesToShow");
            startAt = serializedObject.FindProperty("startAt");
            
            showOptions = serializedObject.FindProperty("showOptions");
            showPosition = serializedObject.FindProperty("showPosition");
            positionPrefix = serializedObject.FindProperty("positionPrefix");
            positionSuffix = serializedObject.FindProperty("positionSuffix");
            positionIndex = serializedObject.FindProperty("positionIndex");
            nameIndex = serializedObject.FindProperty("nameIndex");
            scoreIndex = serializedObject.FindProperty("scoreIndex");

            formatScoreAsTime = serializedObject.FindProperty("formatScoreAsTime");
            timeFormat = serializedObject.FindProperty("timeFormat");
        }


        public override void OnInspectorGUI()
        {
            HeaderDisplay("Leaderboard Display", 150);
            FirstSetupInspector();
            DisplaySetupInspector();
            CustomisationInspector();
            serializedObject.ApplyModifiedProperties();
            serializedObject.Update();
        }


        protected void HeaderDisplay(string headerTitle, int width)
        {
            GUILayout.Space(10);

            // Header display * Start *
            EditorGUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();

            // Shows either the Leaderboard Manager Logo or an alternative for if the icon is deleted when you import the package
            if (Resources.Load<Texture2D>("LBM Logo"))
            {
                if (GUILayout.Button(Resources.Load<Texture2D>("LBM Logo"), GUIStyle.none, GUILayout.Width(50), GUILayout.Height(50)))
                {
                    GUI.FocusControl(null);
                }
            }
            else
            {
                EditorGUILayout.BeginHorizontal();
                GUILayout.FlexibleSpace();
                EditorGUILayout.LabelField("Leaderboard Manager", EditorStyles.boldLabel, GUILayout.Width(width));
                GUILayout.FlexibleSpace();
                EditorGUILayout.EndHorizontal();
            }

            GUILayout.FlexibleSpace();
            EditorGUILayout.EndHorizontal();


            GUILayout.Space(5);
            EditorGUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            EditorGUILayout.LabelField(headerTitle, EditorStyles.boldLabel, GUILayout.Width(width));
            GUILayout.FlexibleSpace();
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            EditorGUILayout.LabelField("Version: 2.0.1", GUILayout.Width(87.5f));
            GUILayout.FlexibleSpace();
            EditorGUILayout.EndHorizontal();

            GUILayout.Space(10);
            
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
            
            GUILayout.Space(10);
        }
        
        
        protected virtual void FirstSetupInspector()
        {
            EditorGUILayout.LabelField("Leaderboard Setup", EditorStyles.boldLabel);
            
            EditorGUILayout.PropertyField(boardID);
            EditorGUILayout.PropertyField(displayOption);
        }


        protected virtual void DisplaySetupInspector()
        {
            EditorGUILayout.Space();
            
            if (displayOption.intValue <= 0) return;
            
            EditorGUILayout.LabelField("Display Setup", EditorStyles.boldLabel);
                
            EditorGUILayout.PropertyField(boardParent);
            EditorGUILayout.PropertyField(rowPrefab);

            if (displayOption.intValue != (int)DisplayOption.Top3Ascending && displayOption.intValue != (int)DisplayOption.Top3Descending)
                EditorGUILayout.PropertyField(entriesToShow);

            EditorGUILayout.PropertyField(startAt);
        }


        protected virtual void CustomisationInspector()
        {
            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Display Customisation", EditorStyles.boldLabel);
                
            showOptions.boolValue = EditorGUILayout.Foldout(showOptions.boolValue, "Customisation");

            if (showOptions.boolValue)
            {
                EditorGUILayout.PropertyField(showPosition);
                    
                if (showPosition.boolValue)
                {
                    EditorGUILayout.PropertyField(positionPrefix);
                    EditorGUILayout.PropertyField(positionSuffix);
                    EditorGUILayout.PropertyField(positionIndex);
                }
                    
                EditorGUILayout.PropertyField(nameIndex);
                EditorGUILayout.PropertyField(scoreIndex);

                EditorGUILayout.PropertyField(formatScoreAsTime);

                if (formatScoreAsTime.boolValue)
                {
                    EditorGUILayout.PropertyField(timeFormat);
                }
            }
        }
    }
}