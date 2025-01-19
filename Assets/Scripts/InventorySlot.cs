using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public enum Type 
{
    Equipment,
    Useable,
    Item
}

public enum EquipmentType
{
    None,
    Helmet,
    Armor,
    Glove,
    Shoe,
    Weapon,
    Necklace,
    Ring
}

public class InventorySlot : MonoBehaviour, IDropHandler
{
    private GameObject holdItemTemp;

    public Type type;
    public EquipmentType equipmentType;
    public GameObject inventoryOrigin;
    private void Awake()
    {
        holdItemTemp = GameObject.Find("UI/Safe Area/Hold Item Temporary");
    }

    public void OnDrop(PointerEventData eventData)
    {
        holdItemTemp.GetComponent<HoldItemTemporary>().slotAfter = gameObject;
        //holdItemTemp.GetComponent<HoldItemTemporary>().ChangeSlot();
    }
}
