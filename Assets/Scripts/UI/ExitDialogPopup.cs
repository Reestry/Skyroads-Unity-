// Copyright (c) 2012-2023 FuryLion Group. All Rights Reserved.

using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class ExitDialogPopup : Window
    {
        [SerializeField]
        private Button _yesButton;

        [SerializeField]
        private Button _noButton;

        private void Awake()
        {
            _yesButton.onClick.AddListener(OnYesButtonClicked);
            _noButton.onClick.AddListener(OnNoButtonClicked);
        }

        private void OnYesButtonClicked()
        {
            Application.Quit();
            Debug.Log("Вы вышли из игры (^˵◕ω◕˵^)");
        }

        private void OnNoButtonClicked()
        {
            WindowManager.Close(gameObject);
        }
    }
}