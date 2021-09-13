using System;
using DG.Tweening;
using UnityEngine;

public class ClothModel : MonoBehaviour
{
    public Cloth ClothData;
    
    public float cameraDistance;
    public float moveTime;

    public Collider _collider;
    
    private Vector3 _localRackPosition;
    private Vector3 _localRackRotation;
    
    private bool _followPointer;
    private Camera _followCamera;

    private Tween _currentMovingTween;
    
    private void Start()
    {
        _localRackPosition = transform.localPosition;
        _localRackRotation = transform.localRotation.eulerAngles;
    }

    public void FollowPointer(Camera followCamera)
    {
        _followCamera = followCamera;
        _followPointer = true;
    }

    private void Update()
    {
        if (_followCamera != null && _followPointer)
        {
            var mousePos = Input.mousePosition;
            transform.position = _followCamera.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, cameraDistance));
            transform.LookAt(_followCamera.transform);
        }
    }

    public void Activate()
    {
        _collider.enabled = true;
    }

    public void Deactivate()
    {
        _collider.enabled = false;
    }

    public Tween GoToPoint(Vector3 point)
    {
        _followPointer = false;
        
        _currentMovingTween?.Kill();
        _currentMovingTween = DOTween.Sequence().Append(transform.DOMove(point, moveTime));

        return _currentMovingTween;
    }

    public Tween ReturnToRack()
    {
        _followPointer = false;

        _currentMovingTween?.Kill();
        _currentMovingTween = DOTween.Sequence().Append(transform.DOLocalMove(_localRackPosition, moveTime)).Join(transform.DOLocalRotate(_localRackRotation, moveTime));
        return _currentMovingTween;
    }
}