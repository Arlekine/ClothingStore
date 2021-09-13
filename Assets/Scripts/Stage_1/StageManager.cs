using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    [SerializeField] private float _startOffset;
    [SerializeField] private float _clientFinalMoveOffset;

    [Space] 
    [SerializeField] private ClothSelector _clothSelector;
    [SerializeField] private RackRotater _rackRotater;
    [SerializeField] private ResultPanel _resultPanel;
    [SerializeField] private List<Client> _clients = new List<Client>();
    
    [Space]
    [SerializeField] private Transform _rackActivePos;
    [SerializeField] private Transform _rackInactivePos;

    private int _movedClients;
    private int _completedClients;
    private int _score;
    
    private IEnumerator Start()
    {
        _clothSelector.enabled = false;
        _rackRotater.enabled = false;
        
        yield return new WaitForSeconds(_startOffset);

        foreach (var client in _clients)
        {
            client.Init();
            client.OnGotToCab += ClientCompletePathToCab;
            client.OnComplete += ClientCompleteClothing;
        }
    }

    private void ClientCompletePathToCab(Client client)
    {
        client.OnGotToCab -= ClientCompletePathToCab;

        _movedClients++;

        if (_movedClients >= _clients.Count)
        {
            StartStage();
        }
    }

    private void ClientCompleteClothing(Client client, bool isPositive)
    {
        client.OnComplete -= ClientCompleteClothing;

        if (isPositive)
            _score++;

        DOTween.Sequence().AppendInterval(_clientFinalMoveOffset).AppendCallback(() =>
        {
            client.DoFinalMove();
            client.OnFinalMove += ClientFinalMove;
        });
    }

    private void ClientFinalMove(Client client)
    {
        client.OnFinalMove -= ClientFinalMove;

        _completedClients++;

        if (_completedClients >= _clients.Count)
        {
            EndStage(_score);
        }
    }

    private void StartStage()
    {
        _clothSelector.enabled = true;
        _rackRotater.enabled = true;
        _rackRotater.Init();
        _rackRotater.MoveToPoint(_rackActivePos.position);
    }

    private void EndStage(int result)
    {
        _resultPanel.SetScore(result, _clients.Count);
        _resultPanel.Show();
        
        _clothSelector.enabled = false;
        _rackRotater.enabled = false;
        _rackRotater.MoveToPoint(_rackInactivePos.position);
        //show panel
    }
}
