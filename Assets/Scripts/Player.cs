using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(Rigidbody2D))]

public class Player : MonoBehaviour
{
    [SerializeField] private AudioClip _die;

    private Animator _animator;
    private AudioSource _audioSource;
    private Vector3 _startPosition;
    private Rigidbody2D _rigidBody;

    public static Action OnCoinCollected;
    public static Action OnPlayerDead;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();
        _rigidBody = GetComponent<Rigidbody2D>();
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
            _rigidBody.bodyType = RigidbodyType2D.Static;
            _animator.SetTrigger("isDead");
            _audioSource.PlayOneShot(_die);
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
        _rigidBody.bodyType = RigidbodyType2D.Static;
        transform.position = new Vector3(0, 2.6f, 0);
        _animator.SetTrigger("hasWon");
    }

    private IEnumerator StartDelay(Action Oncomplete)
    {
        _rigidBody.bodyType = RigidbodyType2D.Static;
        _animator.SetTrigger("justSpawned");
        yield return new WaitForSeconds(2f);
        _rigidBody.bodyType = RigidbodyType2D.Dynamic;
        _animator.SetTrigger("gameStarted");
        Oncomplete();
    }

    public void Init(Action OnComplete)
    {
        StartCoroutine(StartDelay(OnComplete));
    }
}
