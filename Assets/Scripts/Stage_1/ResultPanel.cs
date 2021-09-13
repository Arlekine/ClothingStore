using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class ResultPanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private CanvasGroup _back;
    [SerializeField] private RectTransform _mainPanel;

    [Space] 
    [SerializeField] private float _tweenTime;
    
    public void Show()
    {
        _back.DOFade(1f, _tweenTime);
        _mainPanel.DOScale(1f, _tweenTime);
    }

    public void Hide()
    {
        _back.DOFade(0f, _tweenTime);
        _mainPanel.DOScale(0f, _tweenTime);
    }

    public void SetScore(int score, int maxScore)
    {
        _scoreText.text = $"{score} / {maxScore}";
    }
}
