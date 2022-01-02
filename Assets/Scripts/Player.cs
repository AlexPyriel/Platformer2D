using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(AudioSource))]

public class Player : MonoBehaviour
{
    [SerializeField] private AudioClip _dieSound;

    private bool _isAlive = true;
    private Animator _animator;
    private AudioSource _audioSource;
    private Vector3 _startPosition;

    public static Action OnCoinCollected;
    public static Action OnPlayerDead;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();
        _startPosition = transform.position;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.TryGetComponent<Coin>(out Coin coin))
        {
            OnCoinCollected?.Invoke();
        }
        else if (other.transform.TryGetComponent<Enemy>(out Enemy enemy) && _isAlive)
        {
            _isAlive = false;
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
}
