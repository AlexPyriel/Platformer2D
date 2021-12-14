using UnityEngine;

public class SkyMovement : MonoBehaviour
{
    [SerializeField] private float _speed;

    private Vector3 _startPosition;
    private float _offset = -5f;

    private void Start()
    {
        _startPosition = transform.position;
    }

    private void Update()
    {
        transform.Translate(Vector3.left * _speed, Space.World);
        if (transform.position.x < _offset)
        {
            transform.position = _startPosition;
        }
    }
}
