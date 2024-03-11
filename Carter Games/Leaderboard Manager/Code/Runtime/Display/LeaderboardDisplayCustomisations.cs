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
using UnityEngine;

namespace CarterGames.Assets.LeaderboardManager
{
    /// <summary>
    /// A container class for leaderboard display edits that may not always be needed.
    /// </summary>
    [Serializable]
    public class LeaderboardDisplayCustomisations
    {
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Fields
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        [SerializeField] [Tooltip("The index the display should start at.")] protected int startAt = 0;
        [SerializeField] [Tooltip("Should the board display show the position of the entries.")] protected bool showPosition = true;
        [SerializeField] [Tooltip("The prefix for the position number in the board.")] protected string positionPrefix = "#";
        
        [SerializeField] [Tooltip("The formatting type for the score value as time.")] protected DisplayTimeFormat timeFormat;
        
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Properties
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        /// <summary>
        /// Gets the starting index of the display.
        /// </summary>
        public int StartingAt => startAt;
        
        
        /// <summary>
        /// Gets if the position should be used.
        /// </summary>
        public bool UsePosition => showPosition;
        
        
        /// <summary>
        /// Gets the prefix for the position to show.
        /// </summary>
        public string PositionPrefix => positionPrefix;
        
        
        /// <summary>
        /// Gets the time formatting for the display.
        /// </summary>
        public DisplayTimeFormat TimeFormat => timeFormat;
    }
}