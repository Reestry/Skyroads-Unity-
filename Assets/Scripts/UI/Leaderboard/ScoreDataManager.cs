// Copyright (c) 2012-2023 FuryLion Group. All Rights Reserved.

using System;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

namespace UI.Leaderboard
{
    public static class ScoreDataManager
    {
        [Serializable]
        private class ScoreDataList
        {
            public List<ScoreData> SavedData;

            public ScoreDataList(List<ScoreData> scores)
            {
                SavedData = scores;
            }
        }

        private static List<ScoreData> _scores = new();

        private static string _json;
        private const string JsonFileName = "Save.json";

        public static readonly List<ScoreData> ScoreData = new();

        public static void Save()
        {
            var scoreData = new ScoreData();
            var comparator = new ScoreDataComparator();

            scoreData.SetData((float) Convert.ToDouble(GameLogic.Time), (float) Convert.ToDouble(GameLogic.Score),
                DateTime.Now.ToString());

            _scores.Add(scoreData);
            _scores.Sort(comparator);

            // Конвертация списка в формат JSON
            _json = JsonUtility.ToJson(new ScoreDataList(_scores));

            // Запись данных в JSON 
            File.WriteAllText(JsonFileName, _json);
        }

        public static void Load()
        {
            if (!File.Exists(JsonFileName))
            {
                GameLogic.BestScore = 0;
                return;
            }

            _scores.Clear();
            ScoreData.Clear();

            _json = File.ReadAllText(JsonFileName);
            _scores = JsonUtility.FromJson<ScoreDataList>(_json).SavedData;

            foreach (var element in _scores)
            {
                ScoreData.Add(new ScoreData()
                {
                    Score = element.Score,
                    Time = element.Time,
                    Date = element.Date
                });
            }

            GameLogic.BestScore = ScoreData[0].Score;
        }
    }
}