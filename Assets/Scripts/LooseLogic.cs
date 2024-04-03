// Copyright (c) 2012-2023 FuryLion Group. All Rights Reserved.

using System.Collections;
using DG.Tweening;
using UI;
using UI.Leaderboard;
using UnityEngine;
using UnityEngine.Serialization;

public class LooseLogic : MonoBehaviour
{
    [FormerlySerializedAs("_AnimationHeight")]
    [SerializeField]
    private float _animationHeight;

    [FormerlySerializedAs("rotationAngle")]
    public float RotationAngle = 360f;
    [FormerlySerializedAs("duration")]
    public float Duration = 2;

    [SerializeField]
    private AudioSource _explosionEffect;

    [SerializeField]
    private GameObject _explosion;

    private void Start()
    {
        transform.DOLocalMoveY(_animationHeight, 1).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutQuart);
        transform.DORotate(new Vector3(0, RotationAngle, 0), Duration, RotateMode.LocalAxisAdd)
            .SetEase(Ease.Linear)
            .SetLoops(-1, LoopType.Restart);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!GameLogic.IsCollided)
        {
            var particles = Instantiate(_explosion, other.transform.position, other.transform.rotation);
            particles.GetComponent<ParticleSystem>().Play();
            Destroy(particles, particles.GetComponent<ParticleSystem>().main.duration);
            _explosionEffect.Play();
            
            GameLogic.IsGame = false;
            GameLogic.IsCollided = true;

            StartCoroutine(AnimationDelay());
        }
    }

    private IEnumerator AnimationDelay()
    {
        yield return new WaitForSeconds(2);

        if (GameLogic.Score > GameLogic.BestScore)
        {
            GameLogic.BestScore = GameLogic.Score;

            WindowManager.OpenWindow<WinWindow>();

            ScoreDataManager.Load();
            ScoreDataManager.Save();

            GameLogic.Score = 0;
            GameLogic.Time = 0;
        }
        else
        {
            WindowManager.OpenWindow<LooseMenu>();

            GameLogic.Score = 0;
            GameLogic.Time = 0;
        }
    }
}