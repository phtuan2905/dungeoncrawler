using UnityEngine;

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

            if (CompareType(transform.GetChild(0).gameObject, item1, slot2, itemeqp1, sloteqp2) && CompareType(slotAfter.transform.GetChild(0).gameObject, item2, slot1, itemeqp2, sloteqp1))
            {
                slotAfter.transform.GetChild(0).position = slotBefore.transform.position;
                slotAfter.transform.GetChild(0).SetParent(slotBefore.transform);
                transform.GetChild(0).position = slotAfter.transform.position;
                transform.GetChild(0).SetParent(slotAfter.transform);

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
            if (CompareType(transform.GetChild(0).gameObject, item1, slot2, itemeqp1, sloteqp2))
            {
                transform.GetChild(0).position = slotAfter.transform.position;
                transform.GetChild(0).SetParent(slotAfter.transform);

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
        inventory.DropItem(item, slotType, slotEqpType);
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
