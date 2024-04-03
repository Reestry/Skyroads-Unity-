// Copyright (c) 2012-2023 FuryLion Group. All Rights Reserved.

using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private const float Speed = 10f;

    private float _leftBound = -5f;
    private float _rightBound = 5f;
    private float _maxRotationAngel = 20f;
    private float _rotationSpeed = 5f;
    
    private void FixedUpdate()
    {
        if(GameLogic.IsPaused)
            return;
        
        var horizontalInput = Input.GetAxis("Horizontal");

        if (GameLogic.IsGame)
            transform.position += new Vector3(horizontalInput * Speed * Time.deltaTime, 0, 0);

        // Ограничение движения по оси X
        var currentPosition = transform.position;
        currentPosition.x = Mathf.Clamp(currentPosition.x, _leftBound, _rightBound);
        transform.position = currentPosition;

        var targetRotation = -horizontalInput * _maxRotationAngel;

        var targetQuaternion = Quaternion.Euler(0, 0, targetRotation);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetQuaternion, Time.deltaTime * _rotationSpeed);
    }
}