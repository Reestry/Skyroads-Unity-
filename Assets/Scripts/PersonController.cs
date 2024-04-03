// Copyright (c) 2012-2023 FuryLion Group. All Rights Reserved.

using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;

public class PersonController : MonoBehaviour
{
    [FormerlySerializedAs("_dir")]
    public Vector3 Dir = Vector3.zero;

    private const float StartSpeed = 5f;
    private const float MaxSpeed = 100f; // Максимальная скорость
    private float _maxSpeedDelay = 100f;
    private float _boostSpeedDelay = 1.5f;
    public static float CurrentSpeed; // Текущая скорость игрока 

    [SerializeField]
    private AudioSource _boostEffect;

    private static Vector3 _initialPosition = Vector3.zero; // Начальная позиция объекта
    private float _elapsedTime;                             // Прошедшее время

    private void Start()
    {
        transform.DOLocalMoveY(0.2f, 1).SetLoops(-1, LoopType.Yoyo);
        _initialPosition = transform.position;
    }

    private void Update()
    {
        _elapsedTime += Time.deltaTime;
        CurrentSpeed = Mathf.Lerp(StartSpeed, MaxSpeed, _elapsedTime / _maxSpeedDelay);

        var boostSpeed = CurrentSpeed * 2;

        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space))
            _boostEffect.Play();

        if (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.W))
            Dir.z = Mathf.Lerp(Dir.z, boostSpeed, Time.deltaTime / _boostSpeedDelay);
        else
            Dir.z = Mathf.Lerp(Dir.z, CurrentSpeed, Time.deltaTime / _boostSpeedDelay);

        if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.Space))
            _boostEffect.Stop();

        if (GameLogic.PlayerRestart)
        {
            ResetLevel();
            GameLogic.PlayerRestart = false;
        }
    }

    private void FixedUpdate()
    {
        if (GameLogic.IsPaused)
            return;
        
        if (GameLogic.IsGame)
            transform.position += new Vector3(0, 0, Dir.z * Time.fixedDeltaTime);
    }

    private void ResetLevel()
    {
        _elapsedTime = 0f;
        transform.position = _initialPosition;
        Dir = Vector3.zero;
        CurrentSpeed = StartSpeed;
    }
}