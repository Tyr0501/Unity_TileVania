using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : MonoBehaviour
{
    private bool isTrigger;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag
        == "Player")
        {
            if (!isTrigger)
            {
                isTrigger = true;
                Destroy(gameObject);
                FindObjectOfType<GameSession>().AddLife();
            }
        }
    }
}