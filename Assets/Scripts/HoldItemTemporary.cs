using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Progress;

public class HoldItemTemporary : MonoBehaviour
{
    public GameObject slotBefore;
    public GameObject slotAfter;

    public InventoryManage inventory;
    public LevelUpSystem levelUpSystem;

    public void ChangeSlot()
    {
        Type item1 = transform.GetChild(0).GetComponent<DraggableItem>().type;
        EquipmentType itemeqp1 = transform.GetChild(0).GetComponent<DraggableItem>().equipmentType;
        Type slot2 = slotAfter.GetComponent<InventorySlot>().type;
        EquipmentType sloteqp2 = slotAfter.GetComponent<InventorySlot>().equipmentType;

        if (slotAfter.transform.childCount > 0)
        {
            Type item2 = slotAfter.transform.GetChild(0).GetComponent<DraggableItem>().type;
            EquipmentType itemeqp2 = slotAfter.transform.GetChild(0).GetComponent<DraggableItem>().equipmentType;
            Type slot1 = slotBefore.GetComponent<InventorySlot>().type;
            EquipmentType sloteqp1 = slotBefore.GetComponent<InventorySlot>().equipmentType;

            GameObject item1Stats = transform.GetChild(0).GetComponent<DraggableItem>().item;
            GameObject item2Stats = slotAfter.transform.GetChild(0).GetComponent<DraggableItem>().item;

            if (item1Stats.name == item2Stats.name)
            {
                if (item1Stats.GetComponent<ItemStats>().stack + item2Stats.GetComponent<ItemStats>().stack <= item2Stats.GetComponent<ItemStats>().maxStack)
                {
                    item2Stats.GetComponent<ItemStats>().stack += item1Stats.GetComponent<ItemStats>().stack;
                    item2Stats.GetComponent<ItemStats>().itemUI.SetCapacity(item2Stats.GetComponent<ItemStats>().stack);
                    Destroy(item1Stats.gameObject);
                    Destroy(item1Stats.GetComponent<ItemStats>().itemUI);
                    return;
                }
                else
                {
                    item1Stats.GetComponent<ItemStats>().stack -= (item2Stats.GetComponent<ItemStats>().maxStack - item2Stats.GetComponent<ItemStats>().stack);
                    item1Stats.GetComponent <ItemStats>().itemUI.SetCapacity(item1Stats.GetComponent<ItemStats>().stack);
                    item2Stats.GetComponent<ItemStats>().stack = item2Stats.GetComponent<ItemStats>().maxStack;
                    item2Stats.GetComponent<ItemStats>().itemUI.SetCapacity(item2Stats.GetComponent<ItemStats>().stack);
                    transform.GetChild(0).position = slotBefore.transform.position;
                    transform.GetChild(0).SetParent(slotBefore.transform);
                    return;
                }
            }

            if (CompareType(transform.GetChild(0).gameObject, item1, slot2, itemeqp1, sloteqp2) && CompareType(slotAfter.transform.GetChild(0).gameObject, item2, slot1, itemeqp2, sloteqp1))
            {
                slotAfter.transform.GetChild(0).position = slotBefore.transform.position;
                slotAfter.transform.GetChild(0).SetParent(slotBefore.transform);
                transform.GetChild(0).position = slotAfter.transform.position;
                transform.GetChild(0).SetParent(slotAfter.transform);

                if (slotBefore.GetComponent<InventorySlot>().inventoryOrigin != slotAfter.GetComponent<InventorySlot>().inventoryOrigin)
                {
                    item1Stats.transform.position = slotAfter.GetComponent<InventorySlot>().inventoryOrigin.transform.position;
                    item1Stats.transform.SetParent(slotAfter.GetComponent<InventorySlot>().inventoryOrigin.transform);
                    item2Stats.transform.position = slotBefore.GetComponent<InventorySlot>().inventoryOrigin.transform.position;
                    item2Stats.transform.SetParent(slotBefore.GetComponent<InventorySlot>().inventoryOrigin.transform);
                }

                if (slot1 == Type.Useable)
                {
                    inventory.SetUseable();
                }
                if (slot2 == Type.Useable)
                {
                    inventory.SetUseable();
                }

                if (slot1 == Type.Equipment)
                {
                    inventory.SetEquipment(null, item1, itemeqp1);
                    inventory.SetEquipment(slotBefore.transform.GetChild(0).gameObject, item2, itemeqp2);
                }
                if (slot2 == Type.Equipment)
                {
                    inventory.SetEquipment(null, item2, itemeqp2);
                    inventory.SetEquipment(slotAfter.transform.GetChild(0).GetComponent<DraggableItem>().item, item1, itemeqp1);
                }
            }
            else
            {
                transform.GetChild(0).position = slotBefore.transform.position;
                transform.GetChild(0).SetParent(slotBefore.transform);
            }
        }
        else
        {
            GameObject item1Stats = transform.GetChild(0).GetComponent<DraggableItem>().item;
            if (CompareType(transform.GetChild(0).gameObject, item1, slot2, itemeqp1, sloteqp2))
            {
                transform.GetChild(0).position = slotAfter.transform.position;
                transform.GetChild(0).SetParent(slotAfter.transform);

                if (slotBefore.GetComponent<InventorySlot>().inventoryOrigin != slotAfter.GetComponent<InventorySlot>().inventoryOrigin)
                {
                    item1Stats.transform.position = slotAfter.GetComponent<InventorySlot>().inventoryOrigin.transform.position;
                    item1Stats.transform.SetParent(slotAfter.GetComponent<InventorySlot>().inventoryOrigin.transform);
                }

                if (slotBefore.GetComponent<InventorySlot>().type == Type.Useable)
                {
                    inventory.SetUseable();
                }
                if (slot2 == Type.Useable)
                {
                    inventory.SetUseable();
                }

                if (slot2 == Type.Equipment)
                {
                    inventory.SetEquipment(slotAfter.transform.GetChild(0).GetComponent<DraggableItem>().item, item1, itemeqp1);
                }
                if (slotBefore.GetComponent<InventorySlot>().type == Type.Equipment)
                {
                    inventory.SetEquipment(null, item1, itemeqp1);
                }
            }
            else
            {
                transform.GetChild(0).position = slotBefore.transform.position;
                transform.GetChild(0).SetParent(slotBefore.transform);
            }
        }
    }

    public void DropUIItem(GameObject item)
    {
        Type slotType = slotBefore.GetComponent<InventorySlot>().type;
        EquipmentType slotEqpType = slotBefore.GetComponent<InventorySlot>().equipmentType;
        inventory.StartDropItem(item, slotType, slotEqpType);
    }

    bool CompareType(GameObject item, Type itemType, Type slotType, EquipmentType itemEqpType, EquipmentType slotEqpType)
    {
        if (itemType == Type.Item)
        {
            if (slotType == Type.Item)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else if (itemType == Type.Useable)
        {
            if (slotType == Type.Item || slotType == Type.Useable)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            if (slotType == Type.Item)
            {
                return true;
            }
            else if (slotType == Type.Equipment)
            {
                if (itemEqpType == slotEqpType)
                {
                    return CheckRequiments(item);
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
    }
    
    bool CheckRequiments(GameObject item)
    {
        ItemStats stats = item.GetComponent<DraggableItem>().item.GetComponent<ItemStats>();
        return levelUpSystem.CompareStats(stats.STR, stats.DEX, stats.INT, stats.VGR, stats.END, stats.AGL);
    }
}
