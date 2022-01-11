using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(AudioSource))]

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpForce;
    [SerializeField] private AudioClip _jump;

    private GameState _gameState;
    private Rigidbody2D _rigidBody;
    private Animator _animator;
    private SpriteRenderer _renderer;
    private AudioSource _audioSource;

    private float _velocity;
    private bool _grounded;

    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _renderer = GetComponent<SpriteRenderer>();
        _audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (_gameState.CurrentState == GameState.State.Playing)
        {
            Move();
            Jump();
        }
    }

    private void LateUpdate()
    {
        if (_gameState.CurrentState == GameState.State.Playing)
        {
            Flip();
        }
    }

    private void Move()
    {
        _velocity = Input.GetAxis("Horizontal");
        transform.Translate(_velocity * _speed * Time.deltaTime, 0, 0);

        if (Mathf.Abs(_velocity) > 0)
        {
            _animator.SetBool("isWalking", true);
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
            _audioSource.PlayOneShot(_jump);
        }

        if (_rigidBody.velocity.y == 0)
        {
            _grounded = true;
            _animator.SetBool("isJumping", false);
        }
    }

    private void Flip()
    {
        if (Mathf.Approximately(0, _velocity) == false)
        {
            _renderer.flipX = _velocity < 0;
        }
    }

    public void Init(GameState gameState)
    {
        _gameState = gameState;
    }
}
