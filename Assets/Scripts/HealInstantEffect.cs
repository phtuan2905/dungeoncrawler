using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealInstantEffect : MonoBehaviour
{
    [SerializeField] private int healthHeal;
    void Start()
    {
        if (transform.parent.CompareTag("Player"))
        {
            transform.parent.GetComponent<PlayerStats>().SetHealth(healthHeal);
            Destroy(gameObject);
        }
        else
        {

        }
    }
}
