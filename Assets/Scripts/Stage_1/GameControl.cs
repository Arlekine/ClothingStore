using Lean.Touch;
using UnityEngine;

public class GameControl
{
    public Vector3 GetPointerPosition()
    {
#if (UNITY_STANDALONE_WIN || UNITY_EDITOR)
        return Input.mousePosition;
#else
        return Input.GetTouch(0).position;
#endif
    }
}