// Copyright (c) 2012-2023 FuryLion Group. All Rights Reserved.

using TMPro;
using UnityEngine;

namespace UI.Leaderboard
{
    public class ScrollDataView : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text _rankText;
    
        [SerializeField]
        private TMP_Text _scoreText;

        [SerializeField]
        private TMP_Text _timeText;

        [SerializeField]
        private TMP_Text _dateText;

        public void SetData(ScoreData scoreData, int rank)
        {
            _rankText.text = rank + ".";
            _scoreText.text = "Score: " + scoreData.Score;
            _timeText.text = "Time: " + scoreData.Time;
            _dateText.text = "Date: " + scoreData.Date;
        }
    }
}