using UnityEngine;
using System;
using TMPro;

public class GameState : MonoBehaviour
{
    [SerializeField] private GameObject _gameOverPanel;
    [SerializeField] private TextMeshProUGUI _gameOverUI;
    [SerializeField] private CoinSpawner _coinSpawner;
    [SerializeField] private PlayerSpawner _playerSpawner;

    private State _state;

    private int _totalCoins;
    private int _collectedCoins;
    private bool _gameOver;
    private Player _player;

    public enum State { Init, Playing, GameOver }
    public static Action<int, int> OnAmountUpdated;
    public static Action OnVictory;

    public State CurrentState => _state;

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
        InitGame();
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

    private void InitGame()
    {
        _state = State.Init;
        if (_player)
        {
            Destroy(_player.gameObject);
        }
        _gameOverPanel.SetActive(false);
        _totalCoins = _coinSpawner.Spots;
        _collectedCoins = 0;
        OnAmountUpdated?.Invoke(_collectedCoins, _totalCoins);
        _coinSpawner.Spawn();
        _player = _playerSpawner.Spawn();
        _player.Init(() =>
        {
            _state = State.Playing;
        });
    }

    public void Restart()
    {
        InitGame();
    }
}
