// Copyright (c) 2012-2023 FuryLion Group. All Rights Reserved.

using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class OptionsWindow : Window
    {
        [SerializeField]
        private Button _backButton;

        private void Awake()
        {
            _backButton.onClick.AddListener(OnBackButtonClicked);
        }

        private void OnBackButtonClicked()
        {
            WindowManager.Close(gameObject);
        }
    }
}