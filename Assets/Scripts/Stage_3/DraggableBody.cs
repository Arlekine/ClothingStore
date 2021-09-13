using Lean.Touch;
using UnityEngine;

public class DraggableBody : MonoBehaviour
{
    public bool IsActive { get; set; } = true;

    [SerializeField] private LayerMask _floorMask;
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

        RaycastHit hit;
        Ray ray = _pointerCamera.ScreenPointToRay(fingers[0].ScreenPosition);

        if (Physics.Raycast(ray, out hit, 1000f, layerMask: _floorMask))
        {
            var cameraFloorDistance = hit.distance;
            var cameraHeight = _pointerCamera.transform.position.y;
            var cameraToBodyDistance = cameraFloorDistance - (cameraFloorDistance * _floorHeight) / cameraHeight;

            var pos = _pointerCamera.ScreenToWorldPoint(new Vector3(fingers[0].ScreenPosition.x,
                fingers[0].ScreenPosition.y, cameraToBodyDistance));

            _rigidbody.MovePosition(pos);
        }
    }
}