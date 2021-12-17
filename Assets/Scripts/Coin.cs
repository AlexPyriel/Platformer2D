using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] AudioClip _collectSound;

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
        AudioSource.PlayClipAtPoint(_collectSound, new Vector3(0, 0, -10));
    }
}
