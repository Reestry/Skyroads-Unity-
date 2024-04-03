// Copyright (c) 2012-2023 FuryLion Group. All Rights Reserved.

using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.Serialization;

public class CameraFollow : MonoBehaviour
{
    [FormerlySerializedAs("target")]
    public Transform Target;

    private PostProcessVolume _postProcessVolume;
    private LensDistortion _lensDistortion;
    private ChromaticAberration _chromaticAberration;

    private bool _isShaking;

    private const float CameraTrackSpeed = 15f;
    private const int CameraDistance = 3;

    private const float DefaultEffectValue = 0;
    private const float EffectSpeed = 2f;

    private const int LensDistValue = -90;
    private const int LensDistSpeed = 10;

    private const int ChromaticAberValue = -90;
    private const int ChromaticAberSpeed = 15;

    private float _initialPositionY;
    private const float ShakeAmount = 0.3f;
    private const float ShakeDuration = 0.1f;
    private const float ShakeDelay = 0.05f;
    private const float ShakeDistance = 0.5f;

    private void Start()
    {
        _initialPositionY = transform.position.y;

        _postProcessVolume = GetComponent<PostProcessVolume>();
        _postProcessVolume.profile.TryGetSettings(out _lensDistortion);
        _postProcessVolume.profile.TryGetSettings(out _chromaticAberration);
    }

    private void FixedUpdate()
    {
        var position = Target.position;

        if ((Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.W)) && GameLogic.IsGame)
        {
            StartCoroutine(Shake());

            _lensDistortion.intensity.value = Mathf.Lerp(_lensDistortion.intensity.value, LensDistValue,
                Time.deltaTime / LensDistSpeed);
            _chromaticAberration.intensity.value =
                Mathf.Lerp(_chromaticAberration.intensity.value, ChromaticAberValue,
                    Time.deltaTime / ChromaticAberSpeed);
        }
        else
        {
            transform.position = Vector3.Lerp(transform.position,
                new Vector3(position.x, transform.position.y, position.z - CameraDistance),
                Time.deltaTime * CameraTrackSpeed);

            _lensDistortion.intensity.value = Mathf.Lerp(_lensDistortion.intensity.value, DefaultEffectValue,
                Time.deltaTime / EffectSpeed);
            _chromaticAberration.intensity.value =
                Mathf.Lerp(_chromaticAberration.intensity.value, DefaultEffectValue, Time.deltaTime / EffectSpeed);
        }
    }

    private IEnumerator Shake()
    {
        var elapsedTime = 0f;
        transform.position = Vector3.Lerp(transform.position,
            new Vector3(Target.position.x, transform.position.y, Target.position.z - CameraDistance),
            Time.deltaTime / 0.05f); //30
        while (elapsedTime < ShakeDuration)
        {
            var randomX = Random.Range(-ShakeDistance, ShakeDistance) * ShakeAmount;
            var randomY = Random.Range(-ShakeDistance, ShakeDistance) * ShakeAmount;

            transform.position = Vector3.Lerp(transform.position,
                new Vector3(Target.position.x + randomX, _initialPositionY + randomY,
                    Target.position.z - CameraDistance),
                Time.deltaTime / ShakeDelay);
            elapsedTime += Time.deltaTime;

            yield return null;
        }

        transform.position = Vector3.Lerp(transform.position,
            new Vector3(Target.position.x, _initialPositionY, transform.position.z), Time.deltaTime / ShakeDelay);
    }
}