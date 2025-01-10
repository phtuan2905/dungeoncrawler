using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManage : MonoBehaviour
{
    public GameObject inventoryUI;
    public GameObject itemUIPrefab;

    [Header("Equipment")]
    public GameObject Helmet;
    public GameObject Amror;
    public GameObject Glove;
    public GameObject Shoe1;
    public GameObject Shoe2;
    public GameObject Weapon;
    public GameObject Necklace;
    public GameObject Ring;

    [Header("QuickSlot")]
    public List<GameObject> quickSlots;

    [Header("Inventory")]
    public List<GameObject> items;

    public void EquipItem(GameObject item, Type type, EquipmentType equipmentType)
    {
        if (type == Type.Equipment)
        {
            switch (equipmentType)
            {
                case EquipmentType.Helmet:
                    
                    break;
                case EquipmentType.Armor:
                    break;
                case EquipmentType.Glove:
                    break;
                case EquipmentType.Shoe:
                    break;
                case EquipmentType.Weapon:
                    break;
                case EquipmentType.Ring:
                    break;
                case EquipmentType.Necklace:
                    break;
            }
        }
    }

    void AddItemUI(GameObject item)
    {
        foreach (Transform slot in inventoryUI.transform)
        {
            if (slot.gameObject.CompareTag("Inventory Slot") && slot.childCount == 0)
            {
                GameObject itemUIClone = Instantiate(itemUIPrefab, slot);
                itemUIClone.GetComponent<DraggableItem>().SetItem(item, item.GetComponent<ItemStats>().type, item.GetComponent<ItemStats>().equipmentType);
                break;
            }   
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Item"))
        {
            collision.GetComponent<BoxCollider2D>().enabled = false;
            collision.gameObject.transform.SetParent(transform);
            collision.gameObject.SetActive(false);
            AddItemUI(collision.gameObject);
        }
    }
}
