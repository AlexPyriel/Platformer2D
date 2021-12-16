using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [Header("Coin Prefab")]
    [SerializeField] private Coin _template;

    [Header("Coins container")]
    [SerializeField] private GameObject _coinsContainer;

    private Spot[] _spots;

    private void Awake()
    {
        _spots = GetComponentsInChildren<Spot>();
        if (_spots.Length == 0)
        {
            Debug.LogError("Spawn spots game objects missing");
        }
    }

    private void Start()
    {
        SpawnCoins();
    }

    private void SpawnCoins()
    {
        foreach (Spot spot in _spots)
        {
            Coin coin = Instantiate(_template, spot.transform.position, Quaternion.identity);
            coin.transform.SetParent(_coinsContainer.transform);
        }
    }
}
