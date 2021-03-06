using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]

public class Enemy : MonoBehaviour
{
    [SerializeField] private Transform _path;
    [SerializeField] private float _speed;
    [SerializeField] private GameState _gameState;

    private Transform[] _points;
    private int _currentPoint;
    private SpriteRenderer _renderer;


    private void Awake()
    {
        _renderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        _points = new Transform[_path.childCount];

        for (int i = 0; i < _path.childCount; i++)
        {
            _points[i] = _path.GetChild(i);
        }
    }

    private void Update()
    {
        if (_gameState.CurrentState == GameState.State.Playing)
        {
            Move();
        }
    }

    private void Move()
    {
        Transform target = _points[_currentPoint];

        Vector3 direction = (target.position - transform.position).normalized;

        transform.position = Vector3.MoveTowards(transform.position, target.position, _speed * Time.deltaTime);

        if (transform.position == target.position)
        {
            _currentPoint++;

            if (_currentPoint >= _points.Length)
            {
                _currentPoint = 0;
            }
        }

        _renderer.flipX = direction.x > 0;
    }
}
