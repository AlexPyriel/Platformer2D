using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpForce;

    private Rigidbody2D _rigidBody;
    private Animator _animator;
    private float _velocity;
    private bool _grounded;

    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
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
            _animator.SetBool("isWalking", true);
            transform.rotation = _velocity < 0 ? Quaternion.Euler(0, 180, 0) : Quaternion.identity;
        }
        else
        {
            _animator.SetBool("isWalking", false);
        }
    }

    private void Jump()
    {
        if (Input.GetButtonDown("Jump") && _grounded)
        {
            _rigidBody.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
            _grounded = false;
            _animator.SetBool("isJumping", true);
        }

        if (_rigidBody.velocity.y == 0)
        {
            _grounded = true;
            _animator.SetBool("isJumping", false);
        }
    }
}
