using System;
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
    public int additionalMaxHealth;
    public int stamina;
    public int maxStamina;
    public float moveSpeed;
    public float additionalMoveSpeed;
    public int exp;
    public int maxExp;
    public int attackSpeed;

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

    [Header("Other")]
    [SerializeField] public bool isUsingUseable;

    private void Awake()
    {
        PlayerPrefs.DeleteAll();
        SetUIAttributes();
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
        int randomArmor = UnityEngine.Random.Range(minArmor, maxArmor);
        if (health + randomArmor - damage > 0)
        {
            health += (randomArmor - damage);
        } 
        else
        {
            health = 0;
            Debug.Log("You Die");
        }
        SetUIAttributes();
    }
    
    public void SetUIAttributes()
    {
        levelText.text = "Lvl: " + PlayerPrefs.GetInt("Level");
        healthText.text = health + "/" + maxHealth;
        if (additionalMaxHealth > 0) healthText.text += " + " + additionalMaxHealth;
        healthBar.fillAmount = (float)health / (float)(maxHealth + additionalMaxHealth);
        staminaText.text = stamina + "/" + maxStamina;
        staminaBar.fillAmount = (float)stamina / (float)maxStamina;
        expText.text = exp + "/" + maxExp;
        expBar.fillAmount = (float)exp / (float)maxExp;
    }

    public void SetAdditionalAttributes(int AdditionalHealth, float AdditionalMoveSpeed, int AdditionalMinArmor, int AdditionalMaxArmor)
    {
        additionalMaxHealth += AdditionalHealth;
        if (health > maxHealth) health = maxHealth;
        additionalMoveSpeed += AdditionalMoveSpeed;
        minArmor += AdditionalMinArmor;
        maxArmor += AdditionalMaxArmor;
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
        SetUIAttributes();
    }
    
    public void SetHealth(int healthAdd)
    {
        if (healthAdd + health > maxHealth + additionalMaxHealth)
        {
            health = maxHealth + additionalMaxHealth;
        }
        else
        {
            health += healthAdd;
        }
        SetUIAttributes();
    }

    public void SetStamina(int  staminaAdd)
    {
        if (staminaAdd + stamina > maxStamina)
        {
            stamina = maxStamina;
        }
        else
        {
            stamina += staminaAdd;
        }
        SetUIAttributes();
    }
}
