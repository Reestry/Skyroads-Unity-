// Copyright (c) 2012-2023 FuryLion Group. All Rights Reserved.

using UnityEngine;

namespace UI
{
    /// <summary>
    /// Родительский класс окон
    /// </summary>
    public class Window : MonoBehaviour
    {
        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}