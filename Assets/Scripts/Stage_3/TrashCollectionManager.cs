using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class TrashCollectionManager : MonoBehaviour
{
    [SerializeField] private List<TrashCan> _trashCans = new List<TrashCan>();
    [SerializeField] private TextMeshProUGUI _scoreCounter;
   
    [Space]
    [SerializeField] private ResultPanel _resultPanel;
    [SerializeField] private float _resultPanelShowOffset;

    [Space] 
    [SerializeField] private LayerMask _floorMask;
    [SerializeField] private float _trashDraggingHeight;

    [Header("Trash Move Borders")] 
    [SerializeField] private Transform _leftTopBorder;
    [SerializeField] private Transform _rightDownBorder;

    private int _score;
    private int _maxScore;
    private List<Trash> _uncollectedTrash = new List<Trash>();

    private void Start()
    {
        _scoreCounter.text = "0";
        
        _uncollectedTrash = FindObjectsOfType<Trash>().ToList();
        _maxScore = _uncollectedTrash.Count;

        foreach (var trash in _uncollectedTrash)
        {
            trash.DraggableBody.Init(_floorMask, _trashDraggingHeight);
            trash.DraggableBody.SetMoveBorder(new Vector2(_leftTopBorder.position.x, _rightDownBorder.position.x), new Vector2(_rightDownBorder.position.z, _leftTopBorder.position.z));
        }

        foreach (var trashCan in _trashCans)
        {
            trashCan.onTrashGot += OnTrashCollected;
        }
    }

    private void OnTrashCollected(Trash trash, bool rightCan)
    {
        if (rightCan)
        {
            _score++;
            _scoreCounter.text = _score.ToString();
        }

        _uncollectedTrash.Remove(trash);

        if (_uncollectedTrash.Count == 0)
            StartCoroutine(EndGame());
    }

    private IEnumerator EndGame()
    {
        yield return new WaitForSeconds(_resultPanelShowOffset);
        
        _scoreCounter.gameObject.SetActive(false);
        _resultPanel.SetScore(_score, _maxScore);
        _resultPanel.Show();
    }
}
