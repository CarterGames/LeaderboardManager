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

using System;
using System.Globalization;
using CarterGames.Assets.LeaderboardManager.Serialization;
using UnityEngine;

namespace CarterGames.Assets.LeaderboardManager
{
    /// <summary>
    /// A leaderboard entry with time as the value.
    /// </summary>
    [Serializable]
    public sealed class LeaderboardEntryTime : LeaderboardEntry
    {
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Fields
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */ 
        
        [SerializeField] private string entryUuid;
        [SerializeField] private double entryId;
        [SerializeField] private string entryName;
        [SerializeField] private SerializableTime entryTime;
        
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Properties
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */

        /// <summary>
        /// The type the entry is.
        /// </summary>
        public override Type EntryType => typeof(LeaderboardEntryTime);
        
        
        /// <summary>
        /// The Uuid of the entry.
        /// </summary>
        public override string EntryUuid
        {
            get => entryUuid;
            set => entryUuid = value;
        }
        
        
        /// <summary>
        /// The number the entry is in the board.
        /// </summary>
        public override double EntryId 
        {
            get => entryId;
            set => entryId = value;
        }


        /// <summary>
        /// The name for the entry.
        /// </summary>
        public override string EntryName
        {
            get => entryName;
            set => entryName = value;
        }

        
        /// <summary>
        /// The value for the entry.
        /// </summary>
        public override object EntryValue
        {
            get => entryTime;
            set => entryTime = (SerializableTime) value;
        }


        /// <summary>
        /// Gets the entry value casted to serialized time for local use.
        /// </summary>
        private SerializableTime EntryValueAsTime => (SerializableTime)EntryValue;
        
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Constructors
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */ 
        
        /// <summary>
        /// Creates a new entry with the name and time value provided.
        /// </summary>
        /// <param name="name">The name to set.</param>
        /// <param name="time">The time value to set.</param>
        public LeaderboardEntryTime(string name, SerializableTime time)
        {
            TryGenerateUuid();
            
            EntryName = name;
            EntryValue = time;
        }

        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Methods
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */

        /// <summary>
        /// Returns the entry value in a string format.
        /// </summary>
        /// <param name="format">The format to display in.</param>
        /// <returns>The formatted string.</returns>
        public string ValueFormatted(DisplayTimeFormat format)
        {
            switch (format)
            {
                case DisplayTimeFormat.SecondsOnly:
                    return EntryValueAsTime.TotalSeconds.ToString(CultureInfo.InvariantCulture);
                case DisplayTimeFormat.MinutesOnly:
                    return EntryValueAsTime.TotalMinutes.ToString(CultureInfo.InvariantCulture);
                case DisplayTimeFormat.MinutesSeconds:
                    return EntryValueAsTime.ToString($"{EntryValueAsTime.TotalMinutes}':'ss");
                case DisplayTimeFormat.HoursOnly:
                    return EntryValueAsTime.TotalHours.ToString(CultureInfo.InvariantCulture);
                case DisplayTimeFormat.HoursMinutes:
                    return EntryValueAsTime.ToString($"{EntryValueAsTime.TotalHours}':'mm");
                case DisplayTimeFormat.HoursMinutesSeconds:
                    return EntryValueAsTime.ToString($"{EntryValueAsTime.TotalHours}':'mm':'ss");
                case DisplayTimeFormat.MillisecondsOnly:
                    return EntryValueAsTime.TotalMilliSeconds.ToString(CultureInfo.InvariantCulture);
                case DisplayTimeFormat.SecondsMilliseconds:
                    return EntryValueAsTime.ToString($"{EntryValueAsTime.TotalSeconds}'.'fff");
                case DisplayTimeFormat.MinutesSecondsMilliseconds:
                    return EntryValueAsTime.ToString($"{EntryValueAsTime.TotalMinutes}':'ss'.'fff");
                case DisplayTimeFormat.HoursMinutesSecondsMilliseconds:
                    return EntryValueAsTime.ToString($"{EntryValueAsTime.TotalHours}':'mm':'ss'.'fff");
                case DisplayTimeFormat.Unassigned:
                default:
                    LbmLogger.Normal("[LeaderboardEntryTime] No valid time format selected.");
                    return EntryValueAsTime.ToString($"{EntryValueAsTime.TotalHours}':'mm':'ss");
            }
        }
    }
}