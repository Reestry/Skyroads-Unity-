// Copyright (c) 2012-2023 FuryLion Group. All Rights Reserved.

using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class WinWindow : Window
    {
        [SerializeField]
        private Button _exitButton;

        [SerializeField]
        private Button _newRacedButton;

        [SerializeField]
        private MainMenu _mainMenu;

        private void Awake()
        {
            _exitButton.onClick.AddListener(OnExitButtonClicked);
            _newRacedButton.onClick.AddListener(OnNewRaceButtonClicked);
        }

        private void OnExitButtonClicked()
        {
            WindowManager.OpenWindow<MainMenu>();
            GameLogic.IsGame = true;
            GameLogic.IsCollided = false;
            GameLogic.ObstacleRestart = true;
            GameLogic.PlayerRestart = true;
            GameLogic.FloorRestart = true;
            GameLogic.IsPaused = true;
            WindowManager.Close(gameObject);
        }

        private void OnNewRaceButtonClicked()
        {
            GameLogic.ObstacleRestart = true;
            GameLogic.PlayerRestart = true;
            GameLogic.FloorRestart = true;
            _mainMenu.RestartGame();
            WindowManager.OpenWindow<GameUI>();
            WindowManager.Close(gameObject);
        }
    }
}