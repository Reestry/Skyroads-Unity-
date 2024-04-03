// Copyright (c) 2012-2023 FuryLion Group. All Rights Reserved.

using UnityEngine;

namespace UI
{
    /// <summary>
    /// Менеджер окон
    /// </summary>
    public class WindowManager : MonoBehaviour
    {
        private static WindowManager _instance;

        private void Awake()
        {
            if (_instance != null && _instance != this)
                Destroy(gameObject);
            else
                _instance = this;
        }

        public static T OpenWindow<T>(Window sender = null) where T : Window
        {
            if (sender != null)
            {
                sender.Hide();
                Close(sender.gameObject);
            }

            var window = WindowPool.Get<T>();
            window.transform.SetParent(_instance.transform, false);
            window.Show();
            return window;
        }

        public static void Close(GameObject window)
        {
            WindowPool.Release(window);
        }
    }
}