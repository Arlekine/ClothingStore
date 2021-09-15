using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashCan : MonoBehaviour
{
    public Action<Trash, bool> onTrashGot;
    
    [SerializeField] private TrashType _trashType;
    [SerializeField] private ParticleSystem _correctMoveFX;
    [SerializeField] private ParticleSystem _incorrectMoveFX;

    public void OnTriggerEnter(Collider other)
    {
        var trash = other.GetComponent<Trash>();

        if (trash != null && trash.DraggableBody.IsActive)
        {
            trash.DraggableBody.IsActive = false;
            bool correctTrashType = trash.Type == _trashType;

            if (correctTrashType)
            {
                _correctMoveFX.gameObject.SetActive(true);
                _correctMoveFX.Play();
            }
            else
            {
                _incorrectMoveFX.gameObject.SetActive(true);
                _incorrectMoveFX.Play();
            }

            onTrashGot?.Invoke(trash, correctTrashType);
        }
    }
}
