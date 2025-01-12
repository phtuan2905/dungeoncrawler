using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EatingEffect : MonoBehaviour
{
    [SerializeField] private int staminaGain;
    void Start()
    {
        if (transform.parent.CompareTag("Player"))
        {
            transform.parent.GetComponent<PlayerStats>().SetStamina(staminaGain);
            Destroy(gameObject);
        }
        else
        {

        }
    }
}
