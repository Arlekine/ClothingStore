using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;

public class ClothFolding : MonoBehaviour
{
    public RectTransform Mask;
    public RectTransform FrontImage;
    public RectTransform BackImage;

    [Space] [Range(0, 1)] 
    public float foldingDelta;

    [Space] 
    public Transform startPoint;
    public Transform directionPoint;

    private Vector3 _frontPosition;
    private bool _isMoving;

    private Vector3 clickPosition;

    private void Awake()
    {
        _frontPosition = FrontImage.position;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _isMoving = true;
            clickPosition = Input.mousePosition;
        }

        if (Input.GetMouseButtonUp(0))
            _isMoving = false;
        
        if (!_isMoving)
            return;
        
        
        
        //var direction = (directionPoint.position - startPoint.position).normalized;
        Vector3 direction;
        
        if ((Input.mousePosition - clickPosition).magnitude <= float.Epsilon)
            direction = Vector3.up;
        else
            direction = (Input.mousePosition - clickPosition);

        foldingDelta = Mathf.InverseLerp(0, 300f, direction.magnitude);
        direction = direction.normalized;
        
        print(direction);

        bool xDirPositive = Mathf.Sign(direction.x) == 1;
        bool yDirPositive = Mathf.Sign(direction.y) == 1;

        float startRotationValue = 0f;
        float endRotationValue = 0f;
        
        Vector2 startDirectionPoint = new Vector2();
        Vector2 endDirectionPoint = new Vector2();
        
        if (xDirPositive && yDirPositive)
        {
            startRotationValue = 180f;
            endRotationValue = 0f;
            
            startDirectionPoint = Vector2.up;
            endDirectionPoint = Vector2.right;
        }
        else if (xDirPositive && !yDirPositive)
        {
            startRotationValue = 0;
            endRotationValue = -180f;
            
            startDirectionPoint = Vector2.right;
            endDirectionPoint = Vector2.down;
        }
        else if (!xDirPositive && !yDirPositive)
        {
            startRotationValue = 180;
            endRotationValue = 0;
            
            startDirectionPoint = Vector2.down;
            endDirectionPoint = Vector2.left;
        }
        else
        {
            startRotationValue = 0;
            endRotationValue = -180f;
            
            startDirectionPoint = Vector2.left;
            endDirectionPoint = Vector2.up;
        }

        var directionLerpParam = Mathf.InverseLerp(1, 0, Mathf.Abs(Mathf.Abs(direction.x) - Mathf.Abs(direction.y)));
        direction *= Mathf.Lerp(1, Mathf.Sqrt(2), directionLerpParam);
        
        float angleLerpParam = InverseLerp(startDirectionPoint, endDirectionPoint, direction);
        float backRotation = Mathf.Lerp(startRotationValue, endRotationValue, angleLerpParam);

        float maskRotation = 0f;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
        Quaternion rot = Quaternion.AngleAxis(angle, Vector3.forward);
        Mask.rotation = rot;

        BackImage.eulerAngles = new Vector3(0f, 0f, backRotation);

        Mask.position = Vector3.Lerp(_frontPosition - Vector3.Scale(direction, FrontImage.sizeDelta * 0.5f),_frontPosition + Vector3.Scale(direction, FrontImage.sizeDelta * 0.5f), foldingDelta);
        BackImage.position = Vector3.Lerp(_frontPosition - Vector3.Scale(direction, FrontImage.sizeDelta), _frontPosition + Vector3.Scale(direction, FrontImage.sizeDelta), foldingDelta);

        FrontImage.eulerAngles = Vector3.zero;
        FrontImage.position = _frontPosition;
    }
    
    public static float InverseLerp(Vector3 a, Vector3 b, Vector3 value)
    {
        Vector3 AB = b - a;
        Vector3 AV = value - a;
        return Vector3.Dot(AV, AB) / Vector3.Dot(AB, AB);
    }
    
}
