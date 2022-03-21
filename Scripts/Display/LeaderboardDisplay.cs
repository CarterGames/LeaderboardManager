using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace CarterGames.Assets.LeaderboardManager
{
    [HelpURL("https://carter.games/leaderboardmanager/leaderboarddisplay")]
    [AddComponentMenu("Carter Games/Leaderboard Manager/Leaderboard Display")]
    public class LeaderboardDisplay : MonoBehaviour
    {
        [SerializeField] [Tooltip("The ID of the leaderboard to display.")] protected string boardID;
        [SerializeField] [Tooltip("The way the leaderboard should be displayed.")] protected DisplayOption displayOption;

        [SerializeField] [Tooltip("The transform that should be the parent for all the leaderboard rows when they spawn.")] protected Transform boardParent;
        [SerializeField] [Tooltip("The row prefab (UI) to spawn that shows the leaderboard entries.")] protected GameObject rowPrefab;

        [SerializeField] [Tooltip("The number of entries to show on the display.")] protected int entriesToShow;

        // Custom Options...
        [SerializeField] [HideInInspector] private bool showOptions;

        [SerializeField] [Tooltip("The index the display should start at.")] protected int startAt = 0;
        [SerializeField] [Tooltip("Should the board display show the position of the entries.")] protected bool showPosition = true;
        [SerializeField] [Tooltip("The prefix for the position number in the board.")] protected string positionPrefix = "#";
        [SerializeField] [Tooltip("The suffix for the position number in the board.")] protected string positionSuffix = string.Empty;
        [SerializeField] [Tooltip("The index in the row prefab that the position field should display in.")] protected int positionIndex = 0;
        [SerializeField] [Tooltip("The index in the row prefab that the name field should display in.")] protected int nameIndex = 1;
        [SerializeField] [Tooltip("The index in the row prefab that the score field should display in.")] protected int scoreIndex = 2;

        [SerializeField] [Tooltip("Should the score be displayed in a time format?")] protected bool formatScoreAsTime;
        [SerializeField] [Tooltip("The formatting type for the score value as time.")] protected DisplayTimeFormat timeFormat;
        
        
        protected LeaderboardData data;
        protected List<GameObject> cachedRows;


        /// <summary>
        /// The board ID of the board to show...
        /// </summary>
        public string BoardID
        {
            get => boardID;
            set => boardID = value;
        }
        
        /// <summary>
        /// The option to use when displaying the board...
        /// </summary>
        public DisplayOption DisplayOption
        {
            get => displayOption;
            set => displayOption = value;
        }
        
        /// <summary>
        /// The number of entries to show...
        /// </summary>
        public int EntriesToShow
        {
            get => entriesToShow;
            set => entriesToShow = value;
        }
        
        /// <summary>
        /// Updates the leaderboard display...
        /// </summary>
        public void UpdateDisplay()
        {
            if (startAt < 0)
            {
                Debug.LogError("<color=D6BA64><b>Leaderboard Manager</b> | Start at index must be greater than or equal to 0.</color>");
                return;
            }
            
            data = LeaderboardManager.GetLeaderboard(boardID);

            switch (DisplayOption)
            {
                case DisplayOption.Unassigned:
                    Debug.LogWarning("<color=D6BA64><b>Leaderboard Manager</b> | Display option cannot be Unassigned.</color>");
                    break;
                case DisplayOption.Top3Ascending:
                case DisplayOption.Top3Descending:
                    SpawnRows(3);
                    break;
                default:
                    SpawnRows(entriesToShow);
                    break;
            }
        }


        /// <summary>
        /// Clears the display of the leaderboard...
        /// </summary>
        public void ClearDisplay()
        {
            if (cachedRows == null) return;
            foreach (var row in cachedRows.Where(row => row.activeInHierarchy))
                row.SetActive(false);
        }
        

        /// <summary>
        /// Spawns the rows based on the display option selected.
        /// </summary>
        /// <param name="numberToShow">The number to show on the display.</param>
        private void SpawnRows(int numberToShow)
        {
            if (cachedRows == null)
                cachedRows = new List<GameObject>();

            var _data = Array.Empty<LeaderboardEntry>();
            
            switch (DisplayOption)
            {
                case DisplayOption.Unassigned:
                    Debug.LogWarning("<color=D6BA64><b>Leaderboard Manager</b> | Display option cannot be Unassigned.</color>");
                    break;
                case DisplayOption.AsWritten:
                    _data = data.BoardData.ToArray();
                    break;
                case DisplayOption.Ascending:
                    _data = data.GetAllAscending;
                    break;
                case DisplayOption.Descending:
                    _data = data.GetAllDescending;
                    break;
                case DisplayOption.Top3Ascending:
                    numberToShow = 3;
                    _data = data.GetTop3Ascending;
                    break;
                case DisplayOption.Top3Descending:
                    numberToShow = 3;
                    _data = data.GetTop3Descending;
                    break;
                case DisplayOption.TopXAscending:
                    _data = data.GetTopXAscending(numberToShow);
                    break;
                case DisplayOption.TopXDescending:
                    _data = data.GetTopXDescending(numberToShow);
                    break;
            }
            
            UpdateDataOnRows(_data, numberToShow);
        }
        

        /// <summary>
        /// Updates the visuals with the data in the board.
        /// </summary>
        /// <param name="_data">The data to read</param>
        /// <param name="numberToShow">The number to show</param>
        protected virtual void UpdateDataOnRows(LeaderboardEntry[] _data, int numberToShow)
        {
            var _rowCount = boardParent.transform.childCount;

            foreach (var row in cachedRows)
                row.SetActive(false);
            
            cachedRows.Clear();

            for (var i = startAt; i < numberToShow + startAt; i++)
            {
                if (_data.Length + startAt <= i) return;

                GameObject _obj;
                Text[] _text;

                if (i + startAt >= _rowCount + startAt)
                {
                    _obj = Instantiate(rowPrefab, boardParent);
                    _obj.name = "Leaderboard Display Row";
                    cachedRows.Add(_obj);
                    _text = _obj.GetComponentsInChildren<Text>(true);
                }
                else
                {
                    _obj = boardParent.transform.GetChild(i).gameObject;
                    _obj.name = "Leaderboard Display Row";
                    cachedRows.Add(_obj);
                    _text = _obj.GetComponentsInChildren<Text>(true);
                }

                if (!_obj.activeInHierarchy)
                    _obj.SetActive(true);

                _text[positionIndex].text = $"{positionPrefix}{i}{positionSuffix}";
                _text[nameIndex].text = _data[i - startAt].Name;

                _text[scoreIndex].text = formatScoreAsTime
                    ? GetScoreTimeFormatted(_data[i - startAt].Score)
                    : _data[i - startAt].Score.ToString(CultureInfo.InvariantCulture);
            }
        }
        
        
        /// <summary>
        /// Formats the score value as if it were time...
        /// </summary>
        /// <param name="score">The score to read</param>
        /// <returns>The formatted string.</returns>
        protected string GetScoreTimeFormatted(double score)
        {
            switch (timeFormat)
            {
                case DisplayTimeFormat.SecondsOnly:
                    return TimeSpan.FromSeconds(score).ToString("ss");
                case DisplayTimeFormat.MinutesOnly:
                    return TimeSpan.FromSeconds(score).ToString("mm");
                case DisplayTimeFormat.MinutesSeconds:
                    return TimeSpan.FromSeconds(score).ToString("mm':'ss");
                case DisplayTimeFormat.HoursOnly:
                    return TimeSpan.FromSeconds(score).ToString("hh");
                case DisplayTimeFormat.HoursMinutes:
                    return TimeSpan.FromSeconds(score).ToString("hh':'mm");
                case DisplayTimeFormat.HoursMinutesSeconds:
                    return TimeSpan.FromSeconds(score).ToString("hh':'mm':'ss");
                case DisplayTimeFormat.MillisecondsOnly:
                    return TimeSpan.FromMilliseconds(score).ToString("fff");
                case DisplayTimeFormat.SecondsMilliseconds:
                    return TimeSpan.FromMilliseconds(score).ToString("ss':'fff");
                case DisplayTimeFormat.MinutesSecondsMilliseconds:
                    return TimeSpan.FromMilliseconds(score).ToString("mm':'ss':'fff");
                case DisplayTimeFormat.HoursMinutesSecondsMilliseconds:
                    return TimeSpan.FromMilliseconds(score).ToString("hh':'mm':'ss':'fff");
                default:
                    Debug.Log($"<color=D6BA64><b>Leaderboard Manager</b> | No valid time format selected.</color>");
                    return score.ToString(CultureInfo.InvariantCulture);
            }
        }
    }
}