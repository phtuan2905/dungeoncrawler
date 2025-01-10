using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectExpOrb : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            collision.GetComponent<PlayerStats>().CollectExpOrb(gameObject);
        }
    }
}
