using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashCan : MonoBehaviour
{
    public Action<TrashCan, bool> onTrashGot;
    
    [SerializeField] private TrashType _trashType;

    public void OnTriggerEnter(Collider other)
    {
        var trash = other.GetComponent<TrashTypeHolder>();

        if (trash != null)
        {
            other.GetComponent<DraggableBody>().IsActive = false;
            print(trash.Type == _trashType);
            onTrashGot?.Invoke(this, trash.Type == _trashType);
        }
    }
}
