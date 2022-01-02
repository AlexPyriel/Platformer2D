using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using TMPro;

public class GameState : MonoBehaviour
{
    [SerializeField] private GameObject _coinContainer;
    [SerializeField] private GameObject _gameOverPanel;
    [SerializeField] private TextMeshProUGUI _gameOverUI;

    private State _state;

    private int _totalCoins;
    private int _collectedCoins;
    private bool _gameOver;

    public enum State { Playing, GameOver }
    public static Action<int, int> OnAmountUpdated;
    public static Action OnVictory;

    public State CurrentState => _state;

    private void Awake()
    {

    }

    private void OnEnable()
    {
        Player.OnCoinCollected += HandleCoinCollected;
        Player.OnPlayerDead += HandlePlayerDead;
    }

    private void OnDisable()
    {
        Player.OnCoinCollected -= HandleCoinCollected;
        Player.OnPlayerDead -= HandlePlayerDead;
    }

    private void Start()
    {
        _state = State.Playing;
        _totalCoins = _coinContainer.GetComponentsInChildren<Coin>().Length;
        OnAmountUpdated?.Invoke(_collectedCoins, _totalCoins);
    }

    private void HandleCoinCollected()
    {
        _collectedCoins++;
        OnAmountUpdated?.Invoke(_collectedCoins, _totalCoins);

        if (_collectedCoins == _totalCoins)
        {
            OnVictory?.Invoke();
            GameOver("Victory!");
        }
    }

    private void HandlePlayerDead()
    {
        GameOver("Game Over!");
    }

    private void GameOver(string text)
    {
        _state = State.GameOver;
        _gameOverUI.text = text;
        Invoke(nameof(ShowGameOverPanel), 1f);
    }

    private void ShowGameOverPanel()
    {
        _gameOverPanel.SetActive(true);
    }

    public void Restart()
    {
        SceneManager.LoadScene("Platformer2D");
    }
}
