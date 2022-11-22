using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickup : MonoBehaviour
{
    [SerializeField] AudioClip coinPickupSFX;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            PlayClipAtPoint();
            Destroy(gameObject);

        }
    }
    void PlayClipAtPoint()
    {
      AudioSource.PlayClipAtPoint(coinPickupSFX,Camera.main.transform.position);
    }
}
