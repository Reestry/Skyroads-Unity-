// Copyright (c) 2012-2023 FuryLion Group. All Rights Reserved.

using UnityEngine;

public class GameLogic : MonoBehaviour
{
    public static float Score;
    public static float BestScore;
    public static float Time;

    private const float NormalSpeedPoints = 1; // Количество очков за полёт на обычной скорости
    private const float BoostSpeedPoints = 2;  // Количество очков за полёт в режиме ускорения

    //false -> проигрыш
    public static bool IsGame = true;

    //работа коллизии false -> столкновения
    public static bool IsCollided = false;

    // перезапуск сцены
    public static bool FloorRestart = false;
    public static bool ObstacleRestart = false;
    public static bool PlayerRestart = false;

    public static bool IsPaused = true;

    private void Awake()
    {
        Score = 0;
    }

    public static void AddScore(int points)
    {
        Score += points;
    }

    private void Update()
    {
        if (IsGame && !IsPaused)
        {
            if (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.W))
            {
                Score += BoostSpeedPoints * UnityEngine.Time.deltaTime;
            }
            else
            {
                Score += NormalSpeedPoints * UnityEngine.Time.deltaTime;
            }
        }

        if (IsGame && !IsPaused)
            Time += UnityEngine.Time.deltaTime;
    }
}