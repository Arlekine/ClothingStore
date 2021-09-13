using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Client : MonoBehaviour
{
    public Action<Client> OnGotToCab;
    public Action<Client, bool> OnComplete;
    public Action<Client> OnFinalMove;
    
    [Serializable]
    public class ClothPositionByType
    {
        public ClothType type;
        public GameObject standartCloth;
        public GameObject[] obj;
        public Cloth[] cloth;
    }

    public List<ClothPositionByType> ClothPositions = new List<ClothPositionByType>();
    public List<Cloth> wishClothes = new List<Cloth>();
    public ClientCab ClientCab;
    public float _moveSpeed;

    [Space]
    [SerializeField] private Animator _animator;
    [SerializeField] private GameObject _positiveReaction;
    [SerializeField] private GameObject _negativeReaction;
    
    private List<Cloth> _puttedClothes = new List<Cloth>();

    private ClothPack _currentWish;
    private ClientCab _cab;

    private Sequence _moveSequence;

    private void Awake()
    {
        foreach (var clothPosition in ClothPositions)
        {
            for (int i = 0; i < clothPosition.obj.Length; i++)
            {
                clothPosition.standartCloth.SetActive(true);
                clothPosition.obj[i].SetActive(false);
            }
        }
    }

    public void Init()
    {
        ClothPack pack = new ClothPack();

        foreach (var wishClothe in wishClothes)
        {
            pack.AddClothToPack(wishClothe);
        }
        
        _currentWish = pack;
        _cab = ClientCab;
        
        _cab.Activate();
        Move(_cab.WayIn).OnComplete(() =>
        {
            _animator.SetBool("Move", false);
            _cab.ShowWish(_currentWish);
            _cab.CloseDoor();
            OnGotToCab?.Invoke(this);
        });
        _cab.OnClothGetted += PutOnCloth;
    }

    public void PutOnCloth(ClothModel cloth)
    {
        cloth.gameObject.SetActive(false);
        
        var clothPos = ClothPositions.Find(pos => pos.type == cloth.ClothData.Type);
        
        clothPos.standartCloth.SetActive(false);  

        for (int i = 0; i < clothPos.obj.Length; i++)
        {
            clothPos.obj[i].SetActive(clothPos.cloth[i] == cloth.ClothData);
        }
        
        _puttedClothes.Add(cloth.ClothData);

        if (_puttedClothes.Count == _currentWish.Clothes.Count)
        {
            EndFitting();
        }
    }

    public void DoFinalMove()
    {
        Move(_cab.FinalWay).OnComplete(() =>
        {
            _animator.SetBool("Move", false);
            OnFinalMove?.Invoke(this);
        });
    }

    private void Update()
    {
        _animator.transform.localPosition = Vector3.zero;
    }

    private Tween Move(List<Transform> points)
    {
        _moveSequence?.Kill();
        _moveSequence = DOTween.Sequence();

        transform.forward = points[0].position - transform.position;

        _animator.SetBool("Move", true);
        
        _animator.SetBool("Joy", false);
        _animator.SetBool("Sad", false);
        
        for (int i = 0; i < points.Count; i++)
        {
            var startPoint = i == 0 ? transform.position : points[i - 1].position;
            var distance = (points[i].position - startPoint).magnitude;
            _moveSequence.Append(transform.DOMove(points[i].position, distance / _moveSpeed).SetEase(Ease.Linear));
        }
        
        return _moveSequence;
    }

    private bool IsWishComplete()
    {
        bool isComplete = true;

        foreach (var wish in _currentWish.Clothes.Keys)
        {
            isComplete = isComplete && _puttedClothes.Contains(_currentWish.Clothes[wish]);
        }

        return isComplete;
    }
    
    private void EndFitting()
    {
        Move(_cab.WayOut).OnComplete(() =>
            {
                _animator.SetBool("Move", false);
                ShowReaction(IsWishComplete());
            }
        );
        _cab.OpenDoor();
        _cab.Deactivate();
        _cab.HideWish();
    }

    private void ShowReaction(bool isPositive)
    {
        if (isPositive)
            _animator.SetBool("Joy", true);
        else
            _animator.SetBool("Sad", true);
        
        OnComplete?.Invoke(this, isPositive);
    }
}