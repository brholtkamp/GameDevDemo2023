using System;
using DG.Tweening;
using UnityEngine;

public class ScaleTween : MonoBehaviour {
    [SerializeField]
    private float _scaleAmount = 1.5f;

    [SerializeField]
    private float _scaleTime = 0.5f;

    [SerializeField]
    private bool _shouldLoop = true;

    private Tween _tween;
    
    private void Start() => PlayTween();

    private void OnDestroy() => _tween?.Kill();

    private void PlayTween() => _tween = transform.DOScale(_scaleAmount, _scaleTime).SetLoops(_shouldLoop ? -1 : 1, LoopType.Yoyo).Play();
}
