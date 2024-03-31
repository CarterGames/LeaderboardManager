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

using System.Linq;
using CarterGames.Common;
using UnityEngine;

namespace CarterGames.Assets.LeaderboardManager
{
    [AddComponentMenu("Carter Games/Leaderboard Manager/Leaderboard Display")]
    public class LeaderboardDisplay : MonoBehaviour
    {
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Fields
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        [SerializeField] [Tooltip("The ID of the leaderboard to display.")] protected string boardId;
        [SerializeField] [Tooltip("The way the leaderboard should be displayed.")] protected DisplayOption displayOption;

        [SerializeField] [Tooltip("The transform that should be the parent for all the leaderboard rows when they spawn.")] protected Transform boardParent;
        [SerializeField] [Tooltip("The row prefab (UI) to spawn that shows the leaderboard entries.")] protected LeaderboardEntryDisplayBase rowPrefab;

        [SerializeField] [Tooltip("The number of entries to show on the display.")] protected int entriesToShow;

        // Custom Options...
        [SerializeField] [HideInInspector] private bool showOptions;
        [SerializeField] protected LeaderboardDisplayCustomisations customisations = new LeaderboardDisplayCustomisations();
        
        protected Leaderboard Base;
        protected ObjectPoolGeneric<LeaderboardEntryDisplayBase> pool;

        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Properties
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        /// <summary>
        /// The board ID of the board to show...
        /// </summary>
        public string BoardId
        {
            get => boardId;
            set => boardId = value;
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
        
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Methods
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        /// <summary>
        /// Updates the leaderboard display...
        /// </summary>
        public void UpdateDisplay()
        {
            if (customisations.StartingAt < 1)
            {
                LbmLogger.Error("[LeaderboardDisplay] Start at index must be greater than or equal to 1.");
                return;
            }
            
            Base = LeaderboardManager.GetLeaderboard(boardId);

            switch (DisplayOption)
            {
                case DisplayOption.Unassigned:
                    LbmLogger.Warning("[LeaderboardDisplay] Display option cannot be Unassigned");
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
            if (pool == null) return;
            if (!pool.IsInitialized) return;
            
            foreach (var inUse in pool.AllInUse.ToList())
            {
                inUse.ResetDisplay();
                inUse.gameObject.SetActive(false);
                pool.Return(inUse);
            }
        }


        /// <summary>
        /// Tries to initialize the object pool is it isn't already setup.
        /// </summary>
        private void TryInitializePool()
        {
            if (pool != null)
            {
                pool.Reset();
                return;
            }
            
            pool = new ObjectPoolGeneric<LeaderboardEntryDisplayBase>(rowPrefab.gameObject, boardParent)
            {
                ShouldExpand = true
            };
        }
        

        /// <summary>
        /// Spawns the rows based on the display option selected.
        /// </summary>
        /// <param name="numberToShow">The number to show on the display.</param>
        private void SpawnRows(int numberToShow)
        {
            TryInitializePool();

            LeaderboardEntry[] entries = null;
            
            if (Base != null)
            {
                switch (DisplayOption)
                {
                    case DisplayOption.Unassigned:
                        LbmLogger.Warning("[LeaderboardDisplay] Display option cannot be Unassigned.");
                        break;
                    case DisplayOption.AsWritten:
                        entries = Base.BoardData.ToArray();
                        break;
                    case DisplayOption.Ascending:
                        entries = Base.GetAllAscending();
                        break;
                    case DisplayOption.Descending:
                        entries = Base.GetAllDescending();
                        break;
                    case DisplayOption.Top3Ascending:
                        numberToShow = 3;
                        entries = Base.GetTop3Ascending();
                        break;
                    case DisplayOption.Top3Descending:
                        numberToShow = 3;
                        entries = Base.GetTop3Descending();
                        break;
                    case DisplayOption.TopXAscending:
                        entries = Base.GetTopXAscending(numberToShow);
                        break;
                    case DisplayOption.TopXDescending:
                        entries = Base.GetTopXDescending(numberToShow);
                        break;
                    default:
                        break;
                }
            }
            
            if (entries == null) return;
            
            UpdateDataOnRows(entries, numberToShow);
        }
        

        /// <summary>
        /// Updates the visuals with the data in the board.
        /// </summary>
        /// <param name="entries">The data to read</param>
        /// <param name="numberToShow">The number to show</param>
        protected virtual void UpdateDataOnRows(LeaderboardEntry[] entries, int numberToShow)
        {
            for (var i = customisations.StartingAt - 1; i < numberToShow + (customisations.StartingAt - 1); i++)
            {
                if (entries.Length + (customisations.StartingAt - 1) <= i) return;
                pool.Assign().UpdateDisplay(entries[i], i, customisations);
            }
        }
    }
}