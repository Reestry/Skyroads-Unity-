// Copyright (c) 2012-2023 FuryLion Group. All Rights Reserved.

using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class LooseMenu : Window
    {
        [SerializeField]
        private Button _exitButton;

        private void Awake()
        {
            _exitButton.onClick.AddListener(OnExitButtonClicked);
        }

        private void OnEnable() 
        {
            GameLogic.IsPaused = true;
        }

        private void OnExitButtonClicked()
        {
            GameLogic.ObstacleRestart = true;
            GameLogic.PlayerRestart = true;
            GameLogic.FloorRestart = true;

            WindowManager.OpenWindow<MainMenu>();
            WindowManager.Close(gameObject);

            GameLogic.IsGame = true;
            GameLogic.IsCollided = false;
        }
    }
}