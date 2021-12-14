using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _speed = 1.5f;
    [SerializeField] private float _jumpForce = 3f;

    private Rigidbody2D _rigidBody;
    private float _velocity;

    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Move();
        Jump();
    }

    private void Move()
    {
        _velocity = Input.GetAxis("Horizontal");
        transform.position += new Vector3(_velocity * _speed * Time.deltaTime, 0, 0);

        if (Mathf.Approximately(0, _velocity) == false)
        {
            transform.rotation = _velocity < 0 ? Quaternion.Euler(0, 180, 0) : Quaternion.identity;
        }
    }

    private void Jump()
    {
        if (Input.GetButtonDown("Jump") && Mathf.Abs(_rigidBody.velocity.y) < 0.001f)
        {
            _rigidBody.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
        }
    }
}
