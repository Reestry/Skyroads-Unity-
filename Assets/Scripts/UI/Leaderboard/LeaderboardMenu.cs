// Copyright (c) 2012-2023 FuryLion Group. All Rights Reserved.

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Leaderboard
{
    public class LeaderboardMenu : Window
    {
        [SerializeField]
        private Button _backButton;

        [SerializeField]
        private ScrollDataView _scrollDataView;

        [SerializeField]
        private GameObject _content;

        private readonly List<ScrollDataView> _panelsList = new();

        private void Awake()
        {
            _backButton.onClick.AddListener(OnBackButtonClicked);
        }

        private void OnEnable()
        {
            ClearPanels();

            for (var index = 0; index < ScoreDataManager.ScoreData.Count; index++)
            {
                var newPanel = Instantiate(_scrollDataView, _content.transform, false);
                newPanel.SetData(ScoreDataManager.ScoreData[index], index + 1);
                _panelsList.Add(newPanel);
            }
        }

        private void OnBackButtonClicked()
        {
            ClearPanels();
            WindowManager.Close(gameObject);
        }

        private void ClearPanels()
        {
            foreach (var panel in _panelsList)
            {
                Destroy(panel.gameObject);
            }

            _panelsList.Clear();
        }
    }
}