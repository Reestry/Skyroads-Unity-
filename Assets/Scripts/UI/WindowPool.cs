// Copyright (c) 2012-2023 FuryLion Group. All Rights Reserved.

using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    /// <summary>
    /// Список рабочих окон для менеджера
    /// </summary>
    public class WindowPool : MonoBehaviour
    {
        [SerializeField]
        private List<Window> _prefabs;

        private readonly List<Window> _windows = new();
        private static WindowPool _instance;

        private void Awake()
        {
            if (_instance != null && _instance != this)
                Destroy(gameObject);
            else
                _instance = this;

            _windows.Clear();
            InitPrefabs();
        }

        private void InitPrefabs()
        {
            foreach (var windowPrefab in _prefabs)
            {
                var window = Instantiate(windowPrefab, transform);
                window.gameObject.SetActive(false);
                _windows.Add(window);
            }
        }

        public static T Get<T>() where T : Window
        {
            foreach (var window in _instance._windows)
            {
                if (window is T typedObj)
                    return typedObj;
            }

            return null;
        }

        public static void Release(GameObject window)
        {
            window.SetActive(false);
            window.transform.SetParent(_instance.transform, false);
        }
    }
}