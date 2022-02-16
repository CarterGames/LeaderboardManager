using System;
using System.Collections.Generic;
using UnityEngine;

namespace CarterGames.Assets.LeaderboardManager
{
    /// <summary>
    /// The data class that stores all the leaderboards. 
    /// </summary>
    /// <remarks>
    /// This is used by the system to access & save the boards for your game.
    /// </remarks>
    [Serializable]
    public class LeaderboardDataStore
    {
        /// <summary>
        /// All the leaderboards in the store.
        /// </summary>
        [SerializeField, HideInInspector] private List<LeaderboardData> leaderboards;

        /// <summary>
        /// Gets/Sets all the leaderboards in the store.
        /// </summary>
        public List<LeaderboardData> Leaderboards
        {
            get => leaderboards;
            set => leaderboards = value;
        }

        /// <summary>
        /// Constructor that sets up a new list for the boards to be added to.
        /// </summary>
        public LeaderboardDataStore()
        {
            leaderboards = new List<LeaderboardData>();
        }
    }
}