using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(AudioSource))]

public class Player : MonoBehaviour
{
    [SerializeField] private AudioClip _dieSound;

    private bool _isDead = false;
    private Animator _animator;
    private AudioSource _audioSource;

    public bool IsDead => _isDead;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.TryGetComponent<Coin>(out Coin coin))
        {
            Debug.Log("Coin collected");
        }
        else if (other.transform.TryGetComponent<Enemy>(out Enemy enemy))
        {
            _isDead = true;
            _animator.SetTrigger("isDead");
            _audioSource.PlayOneShot(_dieSound);
            Invoke(nameof(Die), 1f);
        }
    }

    private void Die()
    {
        gameObject.SetActive(false);
    }
}
