using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class ClientCab : MonoBehaviour
{
    public Action<ClothModel> OnClothGetted;
    
    public List<Transform> WayIn = new List<Transform>();
    public List<Transform> WayOut = new List<Transform>();
    public List<Transform> FinalWay= new List<Transform>();
    public Transform ClothPoint;

    [Space] 
    [SerializeField] private WishView _wishView;
    [SerializeField] private Collider _collider;

    [Space] 
    [SerializeField] private Transform _door;
    [SerializeField] private Vector3 _closedDoorRotation;
    [SerializeField] private Vector3 _openedDoorRotation;
    [SerializeField] private ParticleSystem _doorSlapFX;
    [SerializeField] private float _doorTime; 

    private HashSet<ClothType> _clothType = new HashSet<ClothType>();
    
    public void OpenDoor()
    {
        _door.DOLocalRotate(_openedDoorRotation, _doorTime);
    }

    public void CloseDoor()
    {
        _door.DOLocalRotate(_closedDoorRotation, _doorTime).OnComplete(() =>
        {
            _doorSlapFX.gameObject.SetActive(true);
            _doorSlapFX.Play();
        });
    }

    public void ShowWish(ClothPack pack)
    {
        _wishView.ShowWish(pack);
    }

    public void HideWish()
    {
        _wishView.HideWith();
    }

    public void Activate()
    {
        _collider.enabled = true;
    }

    public void Deactivate()
    {
        _collider.enabled = false;
    }

    public bool CanGetCloth(ClothType type)
    {
        return !_clothType.Contains(type);
    }

    public void GiveCloth(ClothModel model)
    {
        _clothType.Add(model.ClothData.Type);
        OnClothGetted?.Invoke(model);
    }
}