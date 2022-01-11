using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    [Header("Coin Prefab")]
    [SerializeField] private Coin _template;

    [Header("Coins container")]
    [SerializeField] private GameObject _coinsContainer;
    [SerializeField] private Camera _camera;

    private Spot[] _spots;

    public int Spots => _spots.Length;

    private void Awake()
    {
        _spots = GetComponentsInChildren<Spot>();
        if (_spots.Length == 0)
        {
            Debug.LogError("Spawn spots game objects missing");
        }
    }

    private void CleanUp()
    {
        Coin[] activeCoins = _coinsContainer.GetComponentsInChildren<Coin>();
        if (activeCoins.Length != 0)
        {
            foreach (Coin coin in activeCoins)
            {
                Destroy(coin.gameObject);
            }
        }
    }

    public void Spawn()
    {
        CleanUp();
        foreach (Spot spot in _spots)
        {
            Coin coin = Instantiate(_template, spot.transform.position, Quaternion.identity);
            coin.transform.SetParent(_coinsContainer.transform);
            coin.Init(_camera.transform.position);
        }
    }
}
