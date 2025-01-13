using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class ItemStats : MonoBehaviour
{
    public DraggableItem itemUI;

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

    public void UseUseableItem()
    {
        stack--;
        itemUI.SetCapacity(stack);
        if (stack == 0)
        {
            Destroy(itemUI.gameObject);
            transform.parent.GetComponent<InventoryManage>().UseUseableItem();
            Destroy(gameObject);
        }
    }
}
