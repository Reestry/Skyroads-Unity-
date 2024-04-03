// Copyright (c) 2012-2023 FuryLion Group. All Rights Reserved.

using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class ComicsUI : Window
    {
        [SerializeField]
        private string[] _messages;

        [SerializeField]
        private string[] _buttonMessages;

        [SerializeField]
        private TMP_Text _textField;

        [SerializeField]
        private Button _button;

        [SerializeField]
        private TMP_Text _buttonText;

        private int _currentIndex;

        private void Start()
        {
            _textField.text = _messages[0];
            _buttonText.text = _buttonMessages[0];
            _button.onClick.AddListener(OnButtonClick);
        }

        private void OnButtonClick()
        {
            if (_currentIndex < _messages.Length - 1)
            {
                _currentIndex++;
                _textField.text = _messages[_currentIndex];
                _buttonText.text = _buttonMessages[_currentIndex];
            }
            else
                WindowManager.Close(gameObject);
        }
    }
}