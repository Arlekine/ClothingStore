using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trash : MonoBehaviour
{
    [SerializeField] private TrashType _type;
    
    private DraggableBody _draggableBody;

    public TrashType Type => _type;
    public DraggableBody DraggableBody
    {
        get
        {
            if (_draggableBody == null)
                _draggableBody = GetComponentInParent<DraggableBody>();
            
            return _draggableBody;
        }
    }
}