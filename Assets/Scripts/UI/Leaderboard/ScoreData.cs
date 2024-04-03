// Copyright (c) 2012-2023 FuryLion Group. All Rights Reserved.

using System;

namespace UI.Leaderboard
{
    [Serializable]
    public class ScoreData
    {
        public float Time;

        public float Score;

        public string Date;

        public void SetData(float time, float score, string date)
        {
            Time = time;
            Score = score;
            Date = date;
        }
    }
}