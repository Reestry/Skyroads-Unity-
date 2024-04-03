// Copyright (c) 2012-2023 FuryLion Group. All Rights Reserved.

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class FloorGenerator : MonoBehaviour
{
    [FormerlySerializedAs("Player")]
    [SerializeField]
    private GameObject _player;

    [FormerlySerializedAs("parentObject")]
    [SerializeField]
    private GameObject _parentObject;

    [FormerlySerializedAs("platforms")]
    public GameObject Platforms; // массив платформ
    
    private int _lastSpawnPos;
    private const int PlatformLength = 75; // длина платформы
    private const int PlatformCount = 6;

    [FormerlySerializedAs("currentPlatforms")]
    public List<GameObject> CurrentPlatforms; // список текущих платформ
    private GameObject _lastPlatform;

    private void Start()
    {
        for (var i = 0; i < PlatformCount; i++)
        {
            PlatformSpawn();
        }
    }

    private void Update()
    {
        _lastPlatform = CurrentPlatforms[5];

        if (_player.transform.position.z > _lastPlatform.transform.position.z - PlatformLength * 4)
        {
            PlatformMove();
        }

        if (GameLogic.FloorRestart)
        {
            ResetPlatformSpawn();
            GameLogic.FloorRestart = false;
        }
    }

    private void PlatformSpawn()
    {
        var spawnPos = transform.forward * _lastSpawnPos;
        spawnPos.y = -1f;
        var platform = Instantiate(Platforms, spawnPos, transform.rotation);
        platform.transform.parent = _parentObject.transform;

        _lastSpawnPos += PlatformLength;

        CurrentPlatforms.Add(platform);
    }

    private void PlatformMove()
    {
        var platform = CurrentPlatforms[0];

        CurrentPlatforms.RemoveAt(0);

        platform.transform.position =
            new Vector3(0, -1, _lastPlatform.transform.position.z + PlatformLength);

        CurrentPlatforms.Add(platform);
    }

    private void ResetPlatformSpawn()
    {
        _lastSpawnPos = 0;

        foreach (var platform in CurrentPlatforms)
        {
            Destroy(platform);
        }

        CurrentPlatforms.Clear();

        for (var i = 0; i < PlatformCount; i++)
        {
            PlatformSpawn();
        }
    }
}