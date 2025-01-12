using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class InventoryManage : MonoBehaviour
{
    public GameObject inventoryUI;
    public GameObject itemUIPrefab;
    public GameObject quickSlotUI;
    public GameObject quickSlotSelectUI;
    private PlayerStats stats;
    private PlayerAttack playerAttack;

    [Header("Equipment")]
    public GameObject Helmet;
    public GameObject Armor;
    public GameObject Glove;
    public GameObject Shoe1;
    public GameObject Shoe2;
    public GameObject Weapon;
    public GameObject Necklace;
    public GameObject Ring;

    [Header("QuickSlot")]
    public List<GameObject> quickSlots;
    public int currentQuickSlot;

    [Header("Inventory")]
    public List<GameObject> items;

    private void Awake()
    {
        stats = transform.parent.GetComponent<PlayerStats>();
        playerAttack = transform.parent.GetComponent<PlayerAttack>();
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
            collision.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
            if (collision.GetComponent<ItemStats>().type == Type.Useable)
            {
                collision.GetComponent<SpriteRenderer>().enabled = false;
            }
            collision.gameObject.transform.SetParent(transform);
            collision.gameObject.SetActive(false);
            AddItemUI(collision.gameObject);
        }
    }

    public void SetEquipment(GameObject item, Type itemType, EquipmentType itemEqpType)
    {
        if (item != null)
        {
            if (itemType == Type.Equipment)
            {
                GameObject cloneItem = Instantiate(item);
                switch (itemEqpType)
                {
                    case EquipmentType.Helmet:
                        cloneItem.transform.SetParent(Helmet.transform);
                        cloneItem.transform.localPosition = Vector2.zero;
                        cloneItem.SetActive(true);
                        stats.SetAdditionalAttributes(item.GetComponent<ItemStats>().health, item.GetComponent<ItemStats>().moveSpeed, item.GetComponent<ItemStats>().minArmor, item.GetComponent<ItemStats>().maxArmor);
                        break;
                    case EquipmentType.Armor:
                        cloneItem.transform.SetParent(Armor.transform);
                        cloneItem.transform.localPosition = Vector2.zero;
                        cloneItem.SetActive(true);
                        stats.SetAdditionalAttributes(item.GetComponent<ItemStats>().health, item.GetComponent<ItemStats>().moveSpeed, item.GetComponent<ItemStats>().minArmor, item.GetComponent<ItemStats>().maxArmor);
                        break;
                    case EquipmentType.Glove:
                        cloneItem.transform.SetParent(Glove.transform);
                        cloneItem.transform.localPosition = Vector2.zero;
                        cloneItem.transform.localEulerAngles = Vector3.zero;
                        cloneItem.SetActive(true);
                        stats.SetAdditionalAttributes(item.GetComponent<ItemStats>().health, item.GetComponent<ItemStats>().moveSpeed, item.GetComponent<ItemStats>().minArmor, item.GetComponent<ItemStats>().maxArmor);
                        break;
                    case EquipmentType.Shoe:
                        cloneItem.transform.SetParent(Shoe1.transform);
                        cloneItem.transform.localPosition = Vector2.zero;
                        cloneItem.SetActive(true);
                        GameObject shoeClone = Instantiate(item, Shoe2.transform);
                        shoeClone.transform.localPosition = Vector2.zero;
                        shoeClone.GetComponent<SpriteRenderer>().flipX = true;
                        shoeClone.SetActive(true);
                        stats.SetAdditionalAttributes(item.GetComponent<ItemStats>().health, item.GetComponent<ItemStats>().moveSpeed, item.GetComponent<ItemStats>().minArmor, item.GetComponent<ItemStats>().maxArmor);
                        break;
                    case EquipmentType.Weapon:
                        cloneItem.transform.SetParent(Weapon.transform);
                        cloneItem.transform.localPosition = Vector2.zero;
                        cloneItem.transform.localEulerAngles = Vector3.zero;
                        cloneItem.SetActive(true);
                        playerAttack.SetWeapon(cloneItem);
                        stats.SetAdditionalAttributes(item.GetComponent<ItemStats>().health, item.GetComponent<ItemStats>().moveSpeed, item.GetComponent<ItemStats>().minArmor, item.GetComponent<ItemStats>().maxArmor);
                        break;
                    case EquipmentType.Ring:
                        break;
                    case EquipmentType.Necklace:
                        break;
                }
            }
        }
        else
        {
            if (itemType == Type.Equipment)
            {
                GameObject deleteItem;
                switch (itemEqpType)
                {
                    case EquipmentType.Helmet:
                        deleteItem = Helmet.transform.GetChild(0).gameObject;
                        stats.SetAdditionalAttributes(deleteItem.GetComponent<ItemStats>().health * -1, deleteItem.GetComponent<ItemStats>().moveSpeed * -1, deleteItem.GetComponent<ItemStats>().minArmor * -1, deleteItem.GetComponent<ItemStats>().maxArmor * -1);
                        Destroy(deleteItem);
                        break;
                    case EquipmentType.Armor:
                        deleteItem = Armor.transform.GetChild(0).gameObject;
                        stats.SetAdditionalAttributes(deleteItem.GetComponent<ItemStats>().health * -1, deleteItem.GetComponent<ItemStats>().moveSpeed * -1, deleteItem.GetComponent<ItemStats>().minArmor * -1, deleteItem.GetComponent<ItemStats>().maxArmor * -1);
                        Destroy(deleteItem);
                        break;
                    case EquipmentType.Glove:
                        deleteItem = Glove.transform.GetChild(0).gameObject;
                        stats.SetAdditionalAttributes(deleteItem.GetComponent<ItemStats>().health * -1, deleteItem.GetComponent<ItemStats>().moveSpeed * -1, deleteItem.GetComponent<ItemStats>().minArmor * -1, deleteItem.GetComponent<ItemStats>().maxArmor * -1);
                        Destroy(deleteItem);
                        break;
                    case EquipmentType.Shoe:
                        deleteItem = Shoe1.transform.GetChild(0).gameObject;
                        stats.SetAdditionalAttributes(deleteItem.GetComponent<ItemStats>().health * -1, deleteItem.GetComponent<ItemStats>().moveSpeed * -1, deleteItem.GetComponent<ItemStats>().minArmor * -1, deleteItem.GetComponent<ItemStats>().maxArmor * -1);
                        Destroy(deleteItem);
                        deleteItem = Shoe2.transform.GetChild(0).gameObject;
                        Destroy(deleteItem);
                        break;
                    case EquipmentType.Weapon:
                        deleteItem = Weapon.transform.GetChild(0).gameObject;
                        playerAttack.SetWeapon(null);
                        stats.SetAdditionalAttributes(deleteItem.GetComponent<ItemStats>().health * -1, deleteItem.GetComponent<ItemStats>().moveSpeed * -1, deleteItem.GetComponent<ItemStats>().minArmor * -1, deleteItem.GetComponent<ItemStats>().maxArmor * -1);
                        Destroy(deleteItem);
                        break;
                    case EquipmentType.Ring:
                        break;
                    case EquipmentType.Necklace:
                        break;
                }
            }
        }
        stats.SetUIAttributes();
    }

    public void SetUseable()
    {
        for (int i = 0; i < 6; i++)
        {
            if (quickSlotUI.transform.GetChild(i).childCount > 0)
            {
                if (quickSlots[i] != null) quickSlots[i].SetActive(false);
                quickSlots[i] = quickSlotUI.transform.GetChild(i).GetChild(0).GetComponent<DraggableItem>().item;
            }
            else
            {
                quickSlots[i] = null;
            }
        }
        SetSelectQuickSlots(true);
    }

    public void DropItem(GameObject item, Type slotType, EquipmentType slotEqpType)
    {
        if (slotType == Type.Equipment)
        {
            SetEquipment(null, slotType, slotEqpType);
            if (slotEqpType == EquipmentType.Weapon)
            {
                playerAttack.SetWeapon(null);
            }
        }
        item.GetComponent<SpriteRenderer>().enabled = true;
        item.transform.SetParent(null);
        item.transform.position = transform.position + new Vector3(0, item.GetComponent<BoxCollider2D>().size.y + 0.1f + (GetComponent<BoxCollider2D>().size.y / 2f), 0);
        item.GetComponent<BoxCollider2D>().enabled = true;
        item.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        item.SetActive(true);
    }

    private void Update()
    {
        SelectQuickSlots();
    }

    void SelectQuickSlots()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SetSelectQuickSlots(false);
            currentQuickSlot = 0;
            SetSelectQuickSlots(true);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SetSelectQuickSlots(false);
            currentQuickSlot = 1;
            SetSelectQuickSlots(true);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SetSelectQuickSlots(false);
            currentQuickSlot = 2;
            SetSelectQuickSlots(true);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            SetSelectQuickSlots(false);
            currentQuickSlot = 3;
            SetSelectQuickSlots(true);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            SetSelectQuickSlots(false);
            currentQuickSlot = 4;
            SetSelectQuickSlots(true);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            SetSelectQuickSlots(false);
            currentQuickSlot = 5;
            SetSelectQuickSlots(true);
        }
        SetUISelectQuickSlots();
    }

    void SetSelectQuickSlots(bool option)
    {
        if (quickSlots[currentQuickSlot] != null)
        {
            //quickSlots[currentQuickSlot].GetComponent<UseableItem>().enabled = option;
            quickSlots[currentQuickSlot].SetActive(option);
        }
    }

    void SetUISelectQuickSlots()
    {
        quickSlotSelectUI.transform.position = quickSlotUI.transform.GetChild(currentQuickSlot).position;
    }
}
