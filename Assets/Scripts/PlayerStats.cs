using System.Collections;
using System.Collections.Generic;
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
}
