// Copyright (c) 2012-2023 FuryLion Group. All Rights Reserved.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class ObstacleGenerator : MonoBehaviour
{
    [FormerlySerializedAs("Player")]
    [SerializeField]
    private GameObject _player;

    [FormerlySerializedAs("parentObject")]
    [SerializeField]
    private GameObject _parentObject;

    [FormerlySerializedAs("obstacles")]
    public GameObject Obstacles; // Массив препятствий, которые нужно создать

    [FormerlySerializedAs("spawnPoints")]
    public Vector3[] SpawnPoints; // Точки спавна, где могут создаваться препятствия

    [FormerlySerializedAs("spawnDelay")]
    public float SpawnDelay = 30f; // Задержка между спавном препятствий

    [FormerlySerializedAs("currentObstacles")]
    public List<GameObject> CurrentObstacles; // список текущих платформ

    private float _spawnDirZ = 100;
    private readonly int[] _spawnDelays = {60, 55, 50, 45, 40, 35, 30, 25, 20};
    private const int ObstaclesCount = 10;

    private const int MovePosition = 20;

    private void Start()
    {
        StartCoroutine(SpawnFrequency());

        for (var i = 0; i < ObstaclesCount; i++)
        {
            ObstacleSpawn();
        }
    }

    private void Update()
    {
        if (_player.transform.position.z > CurrentObstacles[0].transform.position.z + MovePosition)
            ObstacleMove();

        if (GameLogic.ObstacleRestart)
        {
            ResetObstacleSpawn();
            GameLogic.ObstacleRestart = false;
        }
    }

    private void ObstacleSpawn()
    {
        var spawnIndexPos = Random.Range(0, SpawnPoints.Length);

        var spawnPos = SpawnPoints[spawnIndexPos];
        spawnPos.z += _spawnDirZ;

        var obstacle = Instantiate(Obstacles, spawnPos, transform.rotation);
        obstacle.transform.parent = _parentObject.transform;

        CurrentObstacles.Add(obstacle);
        _spawnDirZ += SpawnDelay;
    }

    private void ObstacleMove()
    {
        var obstacle = CurrentObstacles[0];

        CurrentObstacles.RemoveAt(0);

        var spawnIndexPos = Random.Range(0, SpawnPoints.Length);

        var spawnPos = SpawnPoints[spawnIndexPos];
        spawnPos.z += _spawnDirZ;

        obstacle.transform.position =
            spawnPos;

        CurrentObstacles.Add(obstacle);
        _spawnDirZ += SpawnDelay;
    }

    private IEnumerator SpawnFrequency()
    {
        foreach (var currentSpawnDelay in _spawnDelays)
        {
            SpawnDelay = currentSpawnDelay;
            yield return new WaitForSeconds(15f);
        }
    }

    private void ResetObstacleSpawn()
    {
        StopCoroutine(SpawnFrequency());
        StartCoroutine(SpawnFrequency());
        _spawnDirZ = 100f;

        foreach (var obstacle in CurrentObstacles)
        {
            Destroy(obstacle);
        }

        CurrentObstacles.Clear();

        for (var i = 0; i < ObstaclesCount; i++)
        {
            ObstacleSpawn();
        }
    }
}