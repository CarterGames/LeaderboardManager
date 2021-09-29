using UnityEngine;
using UnityEngine.UI;

/****************************************************************************************************************************
 * 
 *  --{   Leaderboard Manager   }--
 *							  
 *	Leaderboard Display
 *      The script that handles the displaying of the leaderboard.
 *      
 *      Note:
 *        - "leaderboardDisplay" is the parent to the row elements and should have some type of UI layout group on it.
 *        - "leaderboardRowPrefab" should be a UI parent with 3 children of type "text" in the order, position on board, 
 *          name and score respectively. See provided prefab for example.
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
    /// Class (*Not Static*) | Sets up a display for the leaderboard entries.
    /// </summary>
    /// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    public class LeaderboardDisplay : MonoBehaviour
    {
        /// <summary>
        /// The parent that all row UI elements to be attached to.
        /// </summary>
        [Tooltip("The 'Content' that is in the scroll view for the leaderboard.")]
        [SerializeField] private GameObject leaderboardDisplay;

        /// <summary>
        /// The row prefab the holds enough text components for all elements saved in the leaderboard data that you wish to show.
        /// </summary>
        [Tooltip("A Prefab that holds 3 text elements for a leaderboard entry.")]
        [SerializeField] private GameObject leaderboardRowPrefab;

        /// <summary>
        /// Boolean that controls whether or not to only show a specific number of entries.
        /// </summary>
        [Tooltip("Should the display only show 'x' amount of entries?")]
        [SerializeField] private bool showSpecificAmount;

        /// <summary>
        /// The number of entries to show (only used if the the 'showSpecificAmount' bool is true).
        /// </summary>
        [Tooltip("The amount of entries the display will show. This is ignored if the value is null.")]
        [SerializeField] private int numberToShow;

        /// <summary>
        /// The local reference for a leaderboard data array, used for the entries in the leaderboard
        /// </summary>
        private LeaderboardData[] _data;

        /// <summary>
        /// Boolean that controls whether or not the leaderboard should update, is updated via the UpdateLeaderboardDisplay() method.
        /// </summary>
        private bool shouldUpdateBoard;

        /// <summary>
        /// Enum of options for the ordering of the leaderboard entries.
        /// </summary>
        public enum DisplayOptions { Unordered, Descending, Ascending };

        /// <summary>
        /// The choice for how the leaderboard should be displayed
        /// </summary>
        [Tooltip("How should the entries be displayed.")]
        public DisplayOptions displayChoice;


        /// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Unity Update Method
        /// </summary>
        /// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        private void Update()
        {
            if (shouldUpdateBoard)
            {
                RefreshLBDisplay();
                shouldUpdateBoard = false;
            }
        }


        /// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Leaderboard Display Method | Updates the Leaderboard Display with the entries in the leaderboard.
        /// </summary>
        /// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        private void RefreshLBDisplay()
        {
            // Goes through your choice from the inspector to get the right leaderbaord data to use in the display
            switch (displayChoice)
            {
                case DisplayOptions.Unordered:
                    _data = LeaderboardManager.LoadLeaderboardData();
                    break;
                case DisplayOptions.Descending:
                    _data = LeaderboardManager.LoadLeaderboardDataDescending();
                    break;
                case DisplayOptions.Ascending:
                    _data = LeaderboardManager.LoadLeaderboardDataAscending();
                    break;
                default:
                    break;
            }

            // Checks to see if there has been a request for a specific amount of entries...
            // If Yes, it uses the amount put in the inspector...
            // Else it uses the length of the data...
            if (!showSpecificAmount)
            {
                numberToShow = _data.Length;
            }


            // Adds a display row prefab for each row, if they exsist already for the row it will enable it, otherwise it will create a new row. 
            // Meaning it only uses instantiate when needed.... 
            for (int i = 0; i < numberToShow; i++)
            {
                if (i >= leaderboardDisplay.transform.childCount - 1 && _data.Length >= i+1)
                {
                    GameObject _go = Instantiate(leaderboardRowPrefab, leaderboardDisplay.transform);
                    _go.GetComponentsInChildren<Text>()[0].text = "#" + (i + 1).ToString();
                    _go.GetComponentsInChildren<Text>()[1].text = _data[i].playerName;
                    _go.GetComponentsInChildren<Text>()[2].text = _data[i].playerScore.ToString();
                }

                if (i <= leaderboardDisplay.transform.childCount - 1 && _data.Length >= i + 1)
                {
                    if (!leaderboardDisplay.transform.GetChild(i + 1).gameObject.activeInHierarchy)
                    {
                        GameObject _go = leaderboardDisplay.transform.GetChild(i + 1).gameObject;
                        _go.SetActive(true);
                        _go.GetComponentsInChildren<Text>()[0].text = "#" + (i + 1).ToString();
                        _go.GetComponentsInChildren<Text>()[1].text = _data[i].playerName;
                        _go.GetComponentsInChildren<Text>()[2].text = _data[i].playerScore.ToString();
                    }
                }
            }
        }


        /// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Leaderboard Display Method | Calls for the leaderbaord to be updated
        /// (Runs ClearLeaderboard() Method & Sets shouldUpdateBoard to true)
        /// </summary>
        /// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        public void UpdateLeaderboardDisplay()
        {
            ClearLeaderboard();
            shouldUpdateBoard = true;
        }


        /// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Leaderboard Display Method | Clears the leaderboard via disabeling the old objects...
        /// </summary>
        /// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        public void ClearLeaderboard()
        {
            if (!showSpecificAmount)
            {
                numberToShow = leaderboardDisplay.transform.childCount;
            }

            for (int i = 1; i <= leaderboardDisplay.transform.childCount - 1; i++)
            {
                if (leaderboardDisplay.transform.childCount > i)
                    leaderboardDisplay.transform.GetChild(i).gameObject.SetActive(false);
            }
        }
    }
}