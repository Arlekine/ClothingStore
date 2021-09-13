using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashTypeHolder : MonoBehaviour
{
    [SerializeField] private TrashType _type;

    public TrashType Type => _type;
}