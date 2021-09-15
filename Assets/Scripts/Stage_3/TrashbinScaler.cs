using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class TrashbinScaler : MonoBehaviour
{
    [SerializeField] private Transform _scaleTarget;
    [SerializeField] private Vector3 _selectedScale;
    [SerializeField] private Vector3 _unselectedScale;
    [SerializeField] private float _scaleTime = 0.2f;

    private bool _hasTrash;
    private bool _isTriggerReaction;
    private Tween _currentScaleTween;

    public void OnTriggerStay(Collider other)
    {
        var trash = other.GetComponent<Trash>();

        if (trash != null && trash.DraggableBody.IsActive && trash.DraggableBody.IsDragging)
        {
            _hasTrash = true;
        }
    }

    private void FixedUpdate()
    {
        _isTriggerReaction = true;
    }

    private void LateUpdate()
    {
        if (!_isTriggerReaction)
            return;
        
        if (_hasTrash)
        {
            if (_scaleTarget.localScale == _unselectedScale)
            {
                _currentScaleTween?.Kill();
                _currentScaleTween = _scaleTarget.DOScale(_selectedScale, _scaleTime);
            }
        }
        else
        {
            if (_scaleTarget.localScale == _selectedScale)
            {
                _currentScaleTween?.Kill();
                _currentScaleTween = _scaleTarget.DOScale(_unselectedScale, _scaleTime);
            }
        }

        _hasTrash = false;
        _isTriggerReaction = false;
    }
}
