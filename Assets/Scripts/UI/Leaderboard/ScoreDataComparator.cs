// Copyright (c) 2012-2023 FuryLion Group. All Rights Reserved.

using System.Collections.Generic;

namespace UI.Leaderboard
{
    public class ScoreDataComparator : IComparer<ScoreData>
    {
        public int Compare(ScoreData x, ScoreData y)
        {
            // Сравниваем по значению Score
            if (x.Score > y.Score)
            {
                return -1;
            }

            return x.Score < y.Score ? 1 : 0;
        }
    }
}