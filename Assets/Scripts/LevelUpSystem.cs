using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelUpSystem : MonoBehaviour
{
    private PlayerStats playerStats;

    [Header("Stats")]
    [SerializeField] private int point;
    [SerializeField] private int strPoint;
    [SerializeField] private int dexPoint;
    [SerializeField] private int intPoint;
    [SerializeField] private int vgrPoint;
    [SerializeField] private int endPoint;
    [SerializeField] private int aglPoint;

    [SerializeField] private TextMeshProUGUI levelUpText;
    [SerializeField] private TextMeshProUGUI strText;
    [SerializeField] private TextMeshProUGUI dexText;
    [SerializeField] private TextMeshProUGUI intText;
    [SerializeField] private TextMeshProUGUI vgrText;
    [SerializeField] private TextMeshProUGUI endText;
    [SerializeField] private TextMeshProUGUI aglText;

    [Header("Attributes")]
    [SerializeField] private int healthTemp;
    [SerializeField] private int staminaTemp;
    [SerializeField] private float moveSpeedTemp;

    [SerializeField] private TextMeshProUGUI levelStatusText;
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private TextMeshProUGUI expText;
    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private TextMeshProUGUI staminaText;
    [SerializeField] private TextMeshProUGUI moveSpeedText;


    private void Awake()
    {
        playerStats = GameObject.Find("Player").GetComponent<PlayerStats>();
        //PlayerPrefs.SetInt("Level", 1);
        //PlayerPrefs.SetInt("LevelUpPoint", 0);
        //PlayerPrefs.SetInt("STR", 0);
        //PlayerPrefs.SetInt("DEX", 0);
        //PlayerPrefs.SetInt("INT", 0);
        //PlayerPrefs.SetInt("VGR", 0);
        //PlayerPrefs.SetInt("END", 0);
        //PlayerPrefs.SetInt("AGL", 0);
        levelStatusText.text = "Lvl: " + PlayerPrefs.GetInt("Level");
    }

    private void OnEnable()
    {
        point = PlayerPrefs.GetInt("LevelUpPoint");
        strPoint = PlayerPrefs.GetInt("STR");
        dexPoint = PlayerPrefs.GetInt("DEX");
        intPoint = PlayerPrefs.GetInt("INT");
        vgrPoint = PlayerPrefs.GetInt("VGR");
        endPoint = PlayerPrefs.GetInt("END");
        aglPoint = PlayerPrefs.GetInt("AGL");

        healthTemp = playerStats.health;
        staminaTemp = playerStats.stamina;
        moveSpeedTemp = playerStats.moveSpeed;
        SetStats();
    }


    void SetStats()
    {
        levelUpText.text = point.ToString();
        strText.text = strPoint.ToString();
        dexText.text = dexPoint.ToString();
        intText.text = intPoint.ToString();
        vgrText.text = vgrPoint.ToString();
        endText.text = endPoint.ToString();
        aglText.text = aglPoint.ToString();

        levelText.text = PlayerPrefs.GetInt("Level").ToString();
        expText.text = playerStats.exp + "/" + playerStats.maxExp;
        healthText.text = playerStats.health + "/" + healthTemp;
        staminaText.text = playerStats.stamina + "/" + staminaTemp;
        moveSpeedText.text = moveSpeedTemp.ToString();
    }

    public void SetPermStats()
    {
        PlayerPrefs.SetInt("LevelUpPoint", point);
        PlayerPrefs.SetInt("STR", strPoint);
        PlayerPrefs.SetInt("DEX", dexPoint);
        PlayerPrefs.SetInt("INT", intPoint);
        PlayerPrefs.SetInt("VGR", vgrPoint);
        PlayerPrefs.SetInt("END", endPoint);
        PlayerPrefs.SetInt("AGL", aglPoint);

        playerStats.maxHealth = healthTemp;
        playerStats.maxStamina = staminaTemp;
        playerStats.moveSpeed = moveSpeedTemp;
        playerStats.SetAttributes();
    }
    public void LevelUpPoint(bool state, int pointIndex)
    {
        if (pointIndex == 1)
        {
            if (state)
            {
                if (point > 0)
                {
                    point--;
                    strPoint++;
                    SetStats();
                }
            }
            else
            {
                if (strPoint != PlayerPrefs.GetInt("STR"))
                {
                    point++;
                    strPoint--;
                    SetStats();
                }
            }

        }
        else if (pointIndex == 2)
        {
            if (state)
            {
                if (point > 0)
                {
                    point--;
                    dexPoint++;
                    SetStats();
                }
            }
            else
            {
                if (dexPoint != PlayerPrefs.GetInt("DEX"))
                {
                    point++;
                    dexPoint--;
                    SetStats();
                }
            }
        }
        else if (pointIndex == 3)
        {
            if (state)
            {
                if (point > 0)
                {
                    point--;
                    intPoint++;
                    SetStats();
                }
            }
            else
            {
                if (intPoint != PlayerPrefs.GetInt("INT"))
                {
                    point++;
                    intPoint--;
                    SetStats();
                }
            }
        }
        else if (pointIndex == 4)
        {
            if (state)
            {
                if (point > 0)
                {
                    point--;
                    vgrPoint++;
                    healthTemp++;
                    SetStats();
                }
            }
            else
            {
                if (vgrPoint != PlayerPrefs.GetInt("VGR"))
                {
                    point++;
                    vgrPoint--;
                    healthTemp--;
                    SetStats();
                }
            }
        }
        else if (pointIndex == 5)
        {
            if (state)
            {
                if (point > 0)
                {
                    point--;
                    endPoint++;
                    staminaTemp += 10;
                    SetStats();
                }
            }
            else
            {
                if (endPoint != PlayerPrefs.GetInt("END"))
                {
                    point++;
                    endPoint--;
                    staminaTemp -= 10;
                    SetStats();
                }
            }
        }
        else if (pointIndex == 6)
        {
            if (state)
            {
                if (point > 0)
                {
                    point--;
                    aglPoint++;
                    moveSpeedTemp += 0.05f;
                    SetStats();
                }
            }
            else
            {
                if (aglPoint != PlayerPrefs.GetInt("AGL"))
                {
                    point++;
                    aglPoint--;
                    moveSpeedTemp -= 0.05f;
                    SetStats();
                }
            }
        }
        SetStats();
    }

}
