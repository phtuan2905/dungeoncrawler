using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemStats : MonoBehaviour
{
    [Header("Stats")]
    public int health;
    public int stamina;
    public int moveSpeed;
    public int attackSpeed;
    public int minArmor;
    public int maxArmor;
    public Type type;
    public EquipmentType equipmentType;

    [Header("Requiments")]
    public int STR;
    public int DEX;
    public int INT;
    public int VGR;
    public int END;
    public int AGL;

    [Header("Stack")]
    public int stack = 1;
    public int maxStack = 1;
}
