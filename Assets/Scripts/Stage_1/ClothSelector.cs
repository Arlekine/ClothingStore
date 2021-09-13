using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Lean.Touch;
using UnityEngine;

public class ClothSelector : MonoBehaviour
{
    public LayerMask clothLayer;
    public LayerMask cabLayer;
    public Camera raycastCamera;
    public RackRotater RackRotater;

    private ClothModel _currentClothModel;

    private void Awake()
    {
        LeanTouch.OnFingerDown += TakeCloth;
        LeanTouch.OnFingerUp += DropCloth;
    }

    private void OnDestroy()
    {
        LeanTouch.OnFingerDown -= TakeCloth;
        LeanTouch.OnFingerUp -= DropCloth;
    }

    private void DropCloth(LeanFinger finger)
    {
        if (_currentClothModel != null)
        {
            RaycastHit hit;
            Ray ray = raycastCamera.ScreenPointToRay(finger.ScreenPosition);

            if (Physics.Raycast(ray, out hit, 1000f, layerMask: cabLayer))
            {
                var cab = hit.collider.GetComponent<ClientCab>();
                if (cab != null)
                {
                    var cloth = _currentClothModel;

                    if (cab.CanGetClose(cloth.ClothData.Type))
                    {
                        _currentClothModel.Deactivate();
                        _currentClothModel.GoToPoint(cab.ClothPoint.position).OnComplete(() => cab.GiveCloth(cloth));
                    }
                    else
                    {
                        _currentClothModel.ReturnToRack();
                    }
                }
            }
            else
            {
                _currentClothModel.ReturnToRack();
            }
                
            RackRotater.enabled = true;
            _currentClothModel = null;
        }
    }

    private void TakeCloth(LeanFinger finger)
    {
        RaycastHit hit;
        Ray ray = raycastCamera.ScreenPointToRay(finger.ScreenPosition);

        if (Physics.Raycast(ray, out hit, 1000f, layerMask: clothLayer))
        {
            var clothModel = hit.collider.GetComponent<ClothModel>();
            if (clothModel != null)
            {
                RackRotater.enabled = false;
                _currentClothModel = clothModel;
                clothModel.FollowPointer(raycastCamera);
            }
        }
    }
}