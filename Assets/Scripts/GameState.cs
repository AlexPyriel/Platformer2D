using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// using UnityEngine.SceneManagement;
using System;

public class GameState : MonoBehaviour
{
    [SerializeField] private GameObject _coinContainer;
    private State _state;

    private int _totalCoins;
    private int _collectedCoins;
    private bool _gameOver;

    public enum State { Playing, GameOver }
    public static Action<int, int> OnAmountUpdated;

    public State CurrentState => _state;

    private void Awake()
    {

    }

    private void OnEnable()
    {
        Player.OnCoinCollected += IncrementAmount;
        Player.OnPlayerDead += GameOver;
    }

    private void OnDisable()
    {
        Player.OnCoinCollected -= IncrementAmount;
        Player.OnPlayerDead -= GameOver;
    }

    private void Start()
    {
        _state = State.Playing;
        _totalCoins = _coinContainer.GetComponentsInChildren<Coin>().Length;
        OnAmountUpdated?.Invoke(_collectedCoins, _totalCoins);
    }

    private void IncrementAmount()
    {
        _collectedCoins++;
        OnAmountUpdated?.Invoke(_collectedCoins, _totalCoins);
    }

    private void GameOver()
    {
        Debug.Log("received");
        _state = State.GameOver;
        // SceneManager.LoadScene("Platformer2D");
    }
}
