using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(TextMeshProUGUI))]

public class CoinAmountView : MonoBehaviour
{
    private TextMeshProUGUI _collectedCoinsUGUI;
    private int _totalCoins;
    private int _currentAmount;

    private void Awake()
    {
        _collectedCoinsUGUI = GetComponent<TextMeshProUGUI>();
    }

    private void OnEnable()
    {
        GameState.OnAmountUpdated += UpdateView;
    }

    private void OnDisable()
    {
        GameState.OnAmountUpdated -= UpdateView;
    }

    private void Start()
    {
        _collectedCoinsUGUI.text = $"{_currentAmount}/{_totalCoins}";
    }

    private void UpdateView(int amount, int total)
    {
        _currentAmount = amount;
        _totalCoins = total;
        _collectedCoinsUGUI.text = $"{_currentAmount}/{_totalCoins}";
    }
}
