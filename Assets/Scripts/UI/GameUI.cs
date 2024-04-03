// Copyright (c) 2012-2023 FuryLion Group. All Rights Reserved.

using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class GameUI : Window
    {
        [SerializeField]
        private Button _pauseButton;

        [SerializeField]
        private TMP_Text _scoreText;

        [SerializeField]
        private TMP_Text _timeText;

        private void Awake()
        {
            _pauseButton.onClick.AddListener(OnPauseButtonClicked);
        }

        private void OnEnable()
        {
            GameLogic.IsPaused = false;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
                OnPauseButtonClicked();

            if (!GameLogic.IsGame)
                WindowManager.Close(gameObject);

            _scoreText.text = $"Score: {GameLogic.Score.ToString("0")}";
            _timeText.text = $"Time: {GameLogic.Time.ToString("0")}";
        }

        private void OnPauseButtonClicked()
        {
            WindowManager.OpenWindow<PauseMenu>();
        }

        private void OnDestroy()
        {
            GameLogic.IsPaused = true;
        }
    }
}