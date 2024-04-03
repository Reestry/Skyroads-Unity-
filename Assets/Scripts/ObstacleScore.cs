// Copyright (c) 2012-2023 FuryLion Group. All Rights Reserved.

using UnityEngine;

public class ObstacleScore : MonoBehaviour
{
    private const int ObstaclePoints = 5;

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) // Проверяем, что столкнулись с игроком
        {
            GameLogic.AddScore(ObstaclePoints); // Вызываем метод для добавления 5 очков
        }
    }
}