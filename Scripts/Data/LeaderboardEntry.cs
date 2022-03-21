using System;
using UnityEngine;

namespace CarterGames.Assets.LeaderboardManager
{
    /// <summary>
    /// A data class to define an entry to a leaderboard.
    /// </summary>
    [Serializable]
    public class LeaderboardEntry
    {
        [SerializeField, HideInInspector] private double entryID;
        [SerializeField, HideInInspector] private string name;
        [SerializeField, HideInInspector] private double score;

        /// <summary>
        /// The entry number for this entry, not the position, but the order of when it was added.
        /// </summary>
        public double EntryID
        {
            get => entryID;
            set => entryID = value;
        }
        
        
        /// <summary>
        /// The name for the entry to the board.
        /// </summary>
        public string Name
        {
            get => name;
            set => name = value;
        }


        /// <summary>
        /// The score for the entry to the board.
        /// </summary>
        public double Score
        {
            get => score;
            set => score = value;
        }
        

        /// <summary>
        /// Blank constructor with no setup.
        /// </summary>
        public LeaderboardEntry()
        { }

        
        /// <summary>
        /// Constructor with setup for a player name & score.
        /// </summary>
        /// <param name="name">The name for the entry.</param>
        /// <param name="score">The score for the entry.</param>
        public LeaderboardEntry(string name, double score)
        {
            this.name = name;
            this.score = score;
        }


        /// <summary>
        /// Converts the score to a time value in seconds. 
        /// </summary>
        /// <returns>The timespan in seconds</returns>
        public TimeSpan ConvertScoreToTime()
        {
            return TimeSpan.FromSeconds(score);
        }
    }
}