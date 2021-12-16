using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// [RequireComponent(typeof(AudioSource))]

public class Coin : MonoBehaviour
{
    [SerializeField] AudioClip _collectSound;
    //     [SerializeField] private UnityEvent _collected;

    // private AudioSource _audiosource;

    // public event UnityAction Collected
    // {
    //     add => _collected.AddListener(value);
    //     remove => _collected.RemoveListener(value);
    // }

    // private void Awake()
    // {
    //     _audiosource = GetComponent<AudioSource>();
    // }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.TryGetComponent<Player>(out Player player))
        {
            Debug.Log("Player engaged");
            // _collected?.Invoke();
            PlaySound();
            Destroy(gameObject);
        }
    }

    private void PlaySound()
    {
        AudioSource.PlayClipAtPoint(_collectSound, new Vector3(0, 0, -10));
    }
}
