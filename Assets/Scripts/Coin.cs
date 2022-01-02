using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] AudioClip _collectSound;

    private Vector3 _audioListenerPosition;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.TryGetComponent<Player>(out Player player))
        {
            PlaySound();
            Destroy(gameObject);
        }
    }

    private void PlaySound()
    {
        AudioSource.PlayClipAtPoint(_collectSound, _audioListenerPosition);
    }

    public void Init(Vector3 audioListenerPosition)
    {
        _audioListenerPosition = audioListenerPosition;
    }
}
