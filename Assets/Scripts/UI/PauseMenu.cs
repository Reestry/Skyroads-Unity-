// Copyright (c) 2012-2023 FuryLion Group. All Rights Reserved.

using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class PauseMenu : Window
    {
        [SerializeField]
        private Button _backButton;

        [SerializeField]
        private Button _continueButton;

        private void Awake()
        {
            _backButton.onClick.AddListener(OnBackButtonClicked);
            _continueButton.onClick.AddListener(OnContinueButtonClicked);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                WindowManager.Close(gameObject);

                GameLogic.IsPaused = false;
            }
        }

        /// <summary>
        /// рестарт игры и выход в главное меню
        /// </summary>
        private void OnBackButtonClicked()
        {
            GameLogic.ObstacleRestart = true;
            GameLogic.PlayerRestart = true;
            GameLogic.FloorRestart = true;

            WindowManager.OpenWindow<MainMenu>();
            WindowManager.Close(gameObject);

            GameLogic.IsGame = true;
            GameLogic.IsCollided = false;
        }

        private void OnContinueButtonClicked()
        {
            WindowManager.Close(gameObject);
            GameLogic.IsPaused = false;
        }

        private void OnEnable()
        {
            GameLogic.IsPaused = true;
        }
    }
}