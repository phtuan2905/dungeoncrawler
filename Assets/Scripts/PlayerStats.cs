using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

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
    public int maxHealth;
    public int stamina;
    public int maxStamina;
    public float moveSpeed;
    public int exp;
    public int maxExp;

    [Header("GUI")]
    [SerializeField] private Image healthBar;
    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private Image staminaBar;
    [SerializeField] private TextMeshProUGUI staminaText;
    [SerializeField] private Image expBar;
    [SerializeField] private TextMeshProUGUI expText;
    //public int attackSpeed;

    private void Awake()
    {
        SetStats();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && collision.name == "Enemy Weapon")
        {
            SetDamage(collision.GetComponent<ObjectStats>().damage);
        }
    }

    void SetDamage(int damage)
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
        SetStats();
    }
    
    void SetStats()
    {
        healthText.text = health.ToString() + "/" + maxHealth;
        healthBar.fillAmount = health / maxHealth;
        staminaText.text = stamina.ToString() + "/" + maxStamina;
        staminaBar.fillAmount = stamina / maxStamina;
        expText.text = exp.ToString() + "/" + maxExp;
        expBar.fillAmount = exp / maxExp;
    }
}
