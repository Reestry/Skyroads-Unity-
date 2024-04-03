// Copyright (c) 2012-2023 FuryLion Group. All Rights Reserved.

using UI.Leaderboard;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    /// <summary>
    /// События кнопок главного меню
    /// </summary>
    public class MainMenu : Window
    {
        [SerializeField]
        private Button _playButton;

        [SerializeField]
        private Button _optionsButton;

        [SerializeField]
        private Button _exitButton;

        [SerializeField]
        private Button _leaderboardButton;

        private void Awake()
        {
            _playButton.onClick.AddListener(OnStartButtonClicked);
            _optionsButton.onClick.AddListener(OnOptionsButtonClicked);
            _exitButton.onClick.AddListener(OnExitButtonClicked);
            _leaderboardButton.onClick.AddListener(OnLeaderboardButtonClicked);
        }

        private void Start()
        {
            GameLogic.IsPaused = true;
        }

        private void OnOptionsButtonClicked()
        {
            WindowManager.OpenWindow<OptionsWindow>();
        }

        private void OnExitButtonClicked()
        {
            WindowManager.OpenWindow<ExitDialogPopup>();
        }

        private void OnStartButtonClicked()
        {
            ScoreDataManager.Load();
            WindowManager.Close(gameObject);
            WindowManager.OpenWindow<GameUI>();
            RestartGame();
            GameLogic.IsPaused = false;
        }

        private void OnLeaderboardButtonClicked()
        {
            ScoreDataManager.Load();
            WindowManager.OpenWindow<LeaderboardMenu>();
        }

        public void RestartGame()
        {
            GameLogic.Score = 0;
            PersonController.CurrentSpeed = 0;
            GameLogic.Time = 0;

            GameLogic.IsGame = true;
            GameLogic.IsCollided = false;
        }
    }
}