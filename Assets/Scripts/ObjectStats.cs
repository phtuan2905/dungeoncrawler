using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectStats : MonoBehaviour
{
    private GameObject HurtBox;

    [Header("Object Attributes")]
    [SerializeField] private int health;
    public float speed;

    private void Start()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Player Weapon" && collision.transform.CompareTag("Player"))
        {
            GetDamage(collision.transform.GetChild(0).GetComponent<WeaponStats>().damage);
        }
    }

    void GetDamage(int damage)
    {
        if (health - damage > 0)
        {
            health -= damage;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
