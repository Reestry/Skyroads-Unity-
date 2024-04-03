// Copyright (c) 2012-2023 FuryLion Group. All Rights Reserved.

using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace UI
{
    public class SoundOptions : MonoBehaviour
    {
        [SerializeField]
        private Button _musicButton;

        [SerializeField]
        private Button _soundEffectsButton;

        private static bool _musicEnabled = true;
        private static bool _soundEffectsEnabled = true;

        public AudioMixerGroup MixerVolume;

        private const float OnSoundVariable = 0f;
        private const float OffSounVariable = -80;

        private void Awake()
        {
            _musicButton.onClick.AddListener(OnMusicButtonClicked);
            _soundEffectsButton.onClick.AddListener(OnSoundsEffectsButtonClicked);
        }

        private void OnMusicButtonClicked()
        {
            _musicEnabled = !_musicEnabled;

            var volume = _musicEnabled ? OnSoundVariable : OffSounVariable;
            MixerVolume.audioMixer.SetFloat("MusicVolume", volume);
        }

        private void OnSoundsEffectsButtonClicked()
        {
            _soundEffectsEnabled = !_soundEffectsEnabled;

            var volume = _soundEffectsEnabled ? OnSoundVariable : OffSounVariable;
            MixerVolume.audioMixer.SetFloat("SoundEffectsVolume", volume);
        }
    }
}