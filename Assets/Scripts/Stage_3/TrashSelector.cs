using System.Collections;
using System.Collections.Generic;
using Lean.Touch;
using UnityEngine;

public class TrashSelector : MonoBehaviour
{
    public LayerMask trashLayer;
    public Camera raycastCamera;

    private DraggableBody _selectedTrash;

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
        if (_selectedTrash != null)
        {
            _selectedTrash.Free();
            _selectedTrash = null;
        }
    }

    private void TakeCloth(LeanFinger finger)
    {
        RaycastHit hit;
        Ray ray = raycastCamera.ScreenPointToRay(finger.ScreenPosition);

        if (Physics.Raycast(ray, out hit, 1000f, layerMask: trashLayer))
        {
            var body = hit.collider.GetComponent<DraggableBody>();
            if (body != null && body.IsActive)
            {
                _selectedTrash = body;
                body.FollowPointer(raycastCamera);
            }
        }
    }
}
