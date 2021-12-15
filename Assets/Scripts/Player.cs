using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.transform.TryGetComponent<Coin>(out Coin coin))
        {
            Debug.Log("Coin collected");
            Destroy(coin.gameObject);
        }
    }
}
