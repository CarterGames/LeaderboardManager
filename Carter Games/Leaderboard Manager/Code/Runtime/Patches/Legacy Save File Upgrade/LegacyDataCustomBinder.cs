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
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace CarterGames.Assets.LeaderboardManager.Editor
{
    /// <summary>
    /// Aids in converting older saves from 2.0.x into something readable to convert it to 2.1.x saves.
    /// </summary>
    public sealed class LegacyDataCustomBinder : SerializationBinder 
    {
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Methods
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        /// <summary>
        /// Helps to bind the legacy data structure. So old saves can be converted to something readable. 
        /// </summary>
        /// <param name="assemblyName">The assembly name ot use.</param>
        /// <param name="typeName">The type name to use.</param>
        /// <returns>The type to bind the data to.</returns>
        public override Type BindToType(string assemblyName, string typeName) {

            Type typeToDeserialize = null;

            // Old Leaderboard Data Stores.
            if (typeName.IndexOf("LeaderboardDataStore", StringComparison.Ordinal) != -1)
            {
                typeToDeserialize = typeof(LegacyBoardStore);
            }
            // Old Leaderboard Data (As List).
            else if (typeName.IndexOf(
                         "System.Collections.Generic.List`1[[CarterGames.Assets.LeaderboardManager.LeaderboardData, Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null]]", StringComparison.Ordinal) !=
                     -1)
            {
                typeToDeserialize = typeof(List<LegacyBoardDataClass>);
            }
            // Old Leaderboard Data.
            else if (typeName.IndexOf("LeaderboardData", StringComparison.Ordinal) != -1)
            {
                typeToDeserialize = typeof(LegacyBoardDataClass);
            }
            // Old Leaderboard Entry (As List).
            else if (typeName.IndexOf(
                         "System.Collections.Generic.List`1[[CarterGames.Assets.LeaderboardManager.LeaderboardEntry, Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null]]", StringComparison.Ordinal) !=
                     -1)
            {
                typeToDeserialize = typeof(List<LegacyEntryClass>);
            }
            // Old Leaderboard Entry.
            else if (typeName.IndexOf("LeaderboardEntry", StringComparison.Ordinal) != -1)
            {
                typeToDeserialize = typeof(LegacyEntryClass);
            }

            return typeToDeserialize;
        }
    }
}