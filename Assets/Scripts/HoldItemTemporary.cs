using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class HoldItemTemporary : MonoBehaviour
{
    public GameObject slotBefore;
    public GameObject slotAfter;

    private InventoryManage inventory;

    public void ChangeSlot()
    {
        Type item1 = transform.GetChild(0).GetComponent<DraggableItem>().type;
        EquipmentType itemeqp1 = transform.GetChild(0).GetComponent<DraggableItem>().equipmentType;
        Type slot2 = slotAfter.GetComponent<InventorySlot>().type;
        EquipmentType sloteqp2 = slotAfter.GetComponent<InventorySlot>().equipmentType;

        if (slotAfter.transform.childCount > 0) {
            Type item2 = slotAfter.transform.GetChild(0).GetComponent<DraggableItem>().type;
            EquipmentType itemeqp2 = slotAfter.transform.GetChild(0).GetComponent<DraggableItem>().equipmentType;
            Type slot1 = slotBefore.GetComponent<InventorySlot>().type;
            EquipmentType sloteqp1 = slotBefore.GetComponent<InventorySlot>().equipmentType;

            if (CompareType(item1, slot2, itemeqp1, sloteqp2) && CompareType(item2, slot1, itemeqp2, sloteqp1))
            {
                slotAfter.transform.GetChild(0).position = slotBefore.transform.position;
                slotAfter.transform.GetChild(0).SetParent(slotBefore.transform);
                transform.GetChild(0).position = slotAfter.transform.position;
                transform.GetChild(0).SetParent(slotAfter.transform);
            }
            else
            {
                transform.GetChild(0).position = slotBefore.transform.position;
                transform.GetChild(0).SetParent(slotBefore.transform);
            }
        }
        else
        {
            if (CompareType(item1, slot2, itemeqp1, sloteqp2))
            {
                transform.GetChild(0).position = slotAfter.transform.position;
                transform.GetChild(0).SetParent(slotAfter.transform);
            }
            else
            {
                transform.GetChild(0).position = slotBefore.transform.position;
                transform.GetChild(0).SetParent(slotBefore.transform);
            }
        }
    }

    bool CompareType(Type itemType, Type slotType, EquipmentType itemEqpType, EquipmentType slotEqpType)
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
                    return true;
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
}
