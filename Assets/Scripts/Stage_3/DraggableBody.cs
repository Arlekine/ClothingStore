using Lean.Touch;
using UnityEngine;

public class DraggableBody : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private float _floorHeight = 2f;

    private Camera _pointerCamera;
    private bool _isFollowingPointer;
    
    public void Free()
    {
        _isFollowingPointer = false;
        _rigidbody.useGravity = true;
        _rigidbody.isKinematic = false;
    }
    
    public void FollowPointer(Camera pointerCamera)
    {
        _pointerCamera = pointerCamera;
        _isFollowingPointer = true;
        _rigidbody.useGravity = false;
        _rigidbody.isKinematic = true;
    }

    private void Update()
    {
        if (_pointerCamera == null || !_isFollowingPointer)
            return;
        
        var fingers = LeanTouch.GetFingers(true, false);
        
        if (fingers.Count == 0)
            return;

        // RaycastHit hit;
        // Ray ray = _pointerCamera.ScreenPointToRay(fingers[0].ScreenPosition);
        //
        // if (Physics.Raycast(ray, out hit, 1000f, layerMask: trashLayer))
        // {
        //
        //     var pos = _pointerCamera.ScreenToWorldPoint(new Vector3(fingers[0].ScreenPosition.x,
        //         fingers[0].ScreenPosition.y, _cameraDistance));
        //
        //     _rigidbody.MovePosition(pos);
        // }
    }
}