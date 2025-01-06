using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [Header("Player Stats")]
    public int strengthPoint;
    public int dexterityPoint;
    public int intelligencePoint;
    public int endurancePoint;
    public int vigorPoint;
    public int wisdomPoint;

    [Header("Player Attributes")]
    public int health;
    public int stamina;
    public float moveSpeed;
    //public int attackSpeed;

    private void Awake()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && collision.name == "Enemy Weapon")
        {
            GetDamage(collision.GetComponent<ObjectStats>().damage);
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
            health = 0;
            Debug.Log("You Die");
        }
    }
}
