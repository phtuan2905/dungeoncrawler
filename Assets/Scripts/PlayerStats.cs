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

    [Header("Player Equipments Attributes")]
    public int minArmor;
    public int maxArmor;

    [Header("GUI")]
    [SerializeField] private Image healthBar;
    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private Image staminaBar;
    [SerializeField] private TextMeshProUGUI staminaText;
    [SerializeField] private Image expBar;
    [SerializeField] private TextMeshProUGUI expText;
    [SerializeField] private TextMeshProUGUI levelText;
    //public int attackSpeed;

    private void Awake()
    {
        PlayerPrefs.DeleteAll();
        SetAttributes();
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
        SetAttributes();
    }
    
    public void SetAttributes()
    {
        levelText.text = "Lvl: " + PlayerPrefs.GetInt("Level");
        healthText.text = health + "/" + maxHealth;
        healthBar.fillAmount = (float)health / (float)maxHealth;
        staminaText.text = stamina + "/" + maxStamina;
        staminaBar.fillAmount = (float)stamina / (float)maxStamina;
        expText.text = exp + "/" + maxExp;
        expBar.fillAmount = (float)exp / (float)maxExp;
    }

    public void CollectExpOrb(GameObject orb)
    {
        exp++;
        if (exp > maxExp)
        {
            exp = 0;
            PlayerPrefs.SetInt("Level", PlayerPrefs.GetInt("Level") + 1);
            PlayerPrefs.SetInt("LevelUpPoint", PlayerPrefs.GetInt("LevelUpPoint") + 1);
        }
        Destroy(orb);
        SetAttributes();
    }
}
