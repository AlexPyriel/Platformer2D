using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(AudioSource))]

public class Player : MonoBehaviour
{
    [SerializeField] private AudioClip _dieSound;

    private Animator _animator;
    private AudioSource _audioSource;
    private Vector3 _startPosition;
    private CapsuleCollider2D _collider;

    public static Action OnCoinCollected;
    public static Action OnPlayerDead;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();
        _startPosition = transform.position;
    }

    private void OnEnable()
    {
        GameState.OnVictory += Win;
    }

    private void OnDisable()
    {
        GameState.OnVictory -= Win;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.TryGetComponent<Coin>(out Coin coin))
        {
            OnCoinCollected?.Invoke();
        }
        else if (other.transform.TryGetComponent<Enemy>(out Enemy enemy))
        {
            transform.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            _animator.SetTrigger("isDead");
            _audioSource.PlayOneShot(_dieSound);
            OnPlayerDead?.Invoke();
            Invoke(nameof(Die), 1f);
        }
    }

    private void Die()
    {
        Destroy(this.gameObject);
    }

    private void Win()
    {
        transform.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        transform.position = new Vector3(0, 2.6f, 0);
        _animator.SetTrigger("hasWon");
    }
}
