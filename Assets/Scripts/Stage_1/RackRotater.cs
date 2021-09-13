using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Lean.Touch;
using UnityEngine;

public class RackRotater : MonoBehaviour
{
    [Space]
    [SerializeField] private float _lerpSpeed = 3f;
    [SerializeField] private float _rotateSpeed = 1f;
    [SerializeField] private float _moveTime = 1f;
    
    Vector3 _targetRotation, _currentRotation;

    private Tween _moveTween;

    public void Init()
    {
        _targetRotation = _currentRotation = new Vector3(0f, -60f, 0f);
    }

    public void MoveToPoint(Vector3 point)
    {
        _moveTween?.Kill();
        _moveTween = transform.DOMove(point, _moveTime);
    }

    private void Update()
    {
        float lerpParameter = Time.deltaTime * 2f * _lerpSpeed;
        var fingers = LeanTouch.GetFingers(true, false);

        Rotate(fingers);

        _currentRotation.y = Mathf.LerpAngle(_currentRotation.y, _targetRotation.y, lerpParameter);
        transform.rotation = Quaternion.Euler(_currentRotation);
    }
    
    private void Rotate(List<LeanFinger> fingers)
    {
        if (fingers.Count == 1)
        {
            var screenDelta = LeanGesture.GetScreenDelta(new List<LeanFinger>() { fingers[0] });

            _targetRotation.y -= screenDelta.x * _rotateSpeed;
        }
    }
}
