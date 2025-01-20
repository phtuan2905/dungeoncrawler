using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Progress;

public class HoldItemTemporary : MonoBehaviour
{
    public GameObject slotBefore;
    public GameObject slotAfter;

    public InventoryManage inventory;
    public LevelUpSystem levelUpSystem;

    public void ChangeItemUISlot()
    {
        GameObject item1 = transform.GetChild(0).GetComponent<DraggableItem>().item;
        GameObject item1UI = transform.GetChild(0).gameObject;
        if (slotAfter == null)
        {
            if (slotBefore.GetComponent<InventorySlot>().type == Type.Equipment)
            {
                inventory.UnuseEquipment(item1);
            }
            else if (slotBefore.GetComponent<InventorySlot>().type == Type.Useable)
            {
                inventory.UnuseUseable(item1);
            }

            inventory.DropItem(item1, item1UI);
            inventory.SetSelectedUseableItem();
            return;
        }

        if (slotAfter.transform.childCount > 0) 
        {
            GameObject item2 = slotAfter.transform.GetChild(0).GetComponent<DraggableItem>().item;
            GameObject item2UI = slotAfter.transform.GetChild(0).gameObject;

            if (item1.name == item2.name)
            {
                inventory.StackupItem(item2, item1);
                item1UI.transform.SetParent(slotBefore.transform, false);
                item1UI.transform.position = slotBefore.transform.position;
                return;
            }

            if (CompareType(item1, item1UI.GetComponent<DraggableItem>().type, slotAfter.GetComponent<InventorySlot>().type, item1UI.GetComponent<DraggableItem>().equipmentType, slotAfter.GetComponent<InventorySlot>().equipmentType) && CompareType(item2, item2UI.GetComponent<DraggableItem>().type, slotBefore.GetComponent<InventorySlot>().type, item2UI.GetComponent<DraggableItem>().equipmentType, slotBefore.GetComponent<InventorySlot>().equipmentType))
            {
                item1UI.transform.SetParent(slotAfter.transform, false);
                item1UI.transform.position = slotAfter.transform.position;
                item2UI.transform.SetParent(slotBefore.transform, false);
                item2UI.transform.position = slotBefore.transform.position;

                if (slotBefore.GetComponent<InventorySlot>().inventoryOrigin != slotAfter.GetComponent<InventorySlot>().inventoryOrigin)
                {
                    item1.transform.SetParent(slotAfter.GetComponent<InventorySlot>().inventoryOrigin.transform, false);
                    item1.transform.localPosition = Vector3.zero;
                    item2.transform.SetParent(slotBefore.GetComponent<InventorySlot>().inventoryOrigin.transform, false);
                    item2.transform.localPosition = Vector3.zero;
                }

                if (slotBefore.GetComponent<InventorySlot>().type == Type.Equipment)
                {
                    inventory.UnuseEquipment(item1);
                    inventory.UseEquipment(item2);
                }
                else if (slotBefore.GetComponent<InventorySlot>().type == Type.Useable)
                {
                    inventory.UnuseUseable(item1);
                    inventory.UseUseable(item2);
                }

                if (slotAfter.GetComponent<InventorySlot>().type == Type.Equipment)
                {
                    inventory.UnuseEquipment(item2);
                    inventory.UseEquipment(item1);
                }
                else if (slotAfter.GetComponent<InventorySlot>().type == Type.Useable)
                {
                    inventory.UnuseUseable(item2);
                    inventory.UseUseable(item1);
                }
                inventory.SetSelectedUseableItem();
            }
            else
            {
                item1UI.transform.SetParent(slotBefore.transform, false);
                item1UI.transform.position = slotBefore.transform.position;
            }

        }
        else
        {
            if (CompareType(item1, item1UI.GetComponent<DraggableItem>().type, slotAfter.GetComponent<InventorySlot>().type, item1UI.GetComponent<DraggableItem>().equipmentType, slotAfter.GetComponent<InventorySlot>().equipmentType))
            {
                item1UI.transform.SetParent(slotAfter.transform, false);
                item1UI.transform.position = slotAfter.transform.position;

                if (slotBefore.GetComponent<InventorySlot>().inventoryOrigin != slotAfter.GetComponent<InventorySlot>().inventoryOrigin)
                {
                    item1.transform.SetParent(slotAfter.GetComponent<InventorySlot>().inventoryOrigin.transform, false);
                    item1.transform.localPosition = Vector3.zero;
                }

                if (slotBefore.GetComponent<InventorySlot>().type == Type.Equipment)
                {
                    inventory.UnuseEquipment(item1);
                }
                else if (slotBefore.GetComponent<InventorySlot>().type == Type.Useable)
                {
                    inventory.UnuseUseable(item1);
                }

                if (slotAfter.GetComponent<InventorySlot>().type == Type.Equipment)
                {
                    inventory.UseEquipment(item1);
                }
                else if (slotAfter.GetComponent<InventorySlot>().type == Type.Useable)
                {
                    inventory.UseUseable(item1);
                }
                inventory.SetSelectedUseableItem();
            }
            else
            {
                item1UI.transform.SetParent(slotBefore.transform, false);
                item1UI.transform.position = slotBefore.transform.position;
            }
        }
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
        ItemStats stats = item.GetComponent<ItemStats>();
        return levelUpSystem.CompareStats(stats.STR, stats.DEX, stats.INT, stats.VGR, stats.END, stats.AGL);
    }
}
