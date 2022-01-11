using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private GameState _gameState;

    private Vector3 _spawnPosition = new Vector3(-7.5f, -0.5f, 0);

    public Player Spawn()
    {
        Player player = Instantiate(_player, _spawnPosition, Quaternion.identity);
        if (player.TryGetComponent<PlayerMovement>(out PlayerMovement playerMovement))
        {
            playerMovement.Init(_gameState);
        }
        return player;
    }
}
