using System.Collections;
using System.Collections.Generic;
using TMPro;
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

    [Header("Inventory")]
    [SerializeField] private int maxSlotsCapacity;
    public List<GameObject> items;

    private void Awake()
    {
        stats = transform.parent.GetComponent<PlayerStats>();
        playerAttack = transform.parent.GetComponent<PlayerAttack>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Item"))
        {
            PickupItem(collision.gameObject);
        }
    }

    public void PickupItem(GameObject item)
    {
        if (FindItemInInventory(item) != null) 
        {
            GameObject itemInInventory = FindItemInInventory(item);
            if (itemInInventory.GetComponent<ItemStats>().stack < item.GetComponent<ItemStats>().maxStack)
            {
                itemInInventory.GetComponent<ItemStats>().stack += item.GetComponent<ItemStats>().stack;
                item.GetComponent<ItemStats>().stack = itemInInventory.GetComponent<ItemStats>().stack - itemInInventory.GetComponent<ItemStats>().maxStack;
                if (itemInInventory.GetComponent<ItemStats>().stack > itemInInventory.GetComponent<ItemStats>().maxStack)
                {
                    itemInInventory.GetComponent<ItemStats>().stack = itemInInventory.GetComponent<ItemStats>().maxStack;
                }
                itemInInventory.GetComponent<ItemStats>().itemUI.GetComponent<DraggableItem>().SetCapacity();
                if (item.GetComponent<ItemStats>().stack <= 0)
                {
                    Destroy(item);
                    return;
                }
            }
        }

        if (FindEmptyInventorySlotUI() != null)
        {
            item.transform.SetParent(transform, false);
            item.transform.position = transform.position;

            item.gameObject.SetActive(false);
            item.GetComponent<BoxCollider2D>().enabled = false;
            item.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;

            AddItemUI(item);
        }
    }

    GameObject FindItemInInventory(GameObject findItem)
    {
        foreach (Transform item in transform) 
        {
            if (item.name == findItem.name)
            {
                return item.gameObject;
            }
        }
        return null;
    }

    public void AddItemUI(GameObject addItem)
    {
        Transform slotUI = FindEmptyInventorySlotUI().transform;
        GameObject itemUI = Instantiate(itemUIPrefab, slotUI);
        addItem.GetComponent<ItemStats>().itemUI = itemUI;
        itemUI.transform.position = slotUI.position;
        itemUI.GetComponent<DraggableItem>().SetItem(addItem);
        itemUI.GetComponent<DraggableItem>().SetCapacity();
    }

    GameObject FindEmptyInventorySlotUI()
    {
        foreach (Transform slotUI in inventoryUI.transform)
        {
            if (slotUI.childCount == 0 && slotUI.CompareTag("Inventory Slot"))
            {
                return slotUI.gameObject;
            }
        }
        return null;
    }

    [Header("Drop Item")]
    [SerializeField] private int dropNumber;
    [SerializeField] private GameObject dropPanel;
    private Type dropSlotType;
    private EquipmentType dropSlotEqpType;
    private GameObject dropItem;
    [SerializeField] private TextMeshProUGUI dropNumberUI;
    [SerializeField] private TextMeshProUGUI maxDropNumberUI;

    public void DropItem(GameObject item, GameObject itemUI)
    {
        item.transform.SetParent(null);
        item.transform.position = transform.position + Vector3.up * 1.5f;

        item.gameObject.SetActive(true);
        item.GetComponent<BoxCollider2D>().enabled = true;
        item.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        DeleteItemUI(itemUI);
    }

    void DeleteItemUI(GameObject deleteItemUI)
    {
        Destroy(deleteItemUI);
    }

    public void StackupItem(GameObject item1, GameObject item2)
    {
        if (item1.GetComponent<ItemStats>().stack < item2.GetComponent<ItemStats>().maxStack)
        {
            item1.GetComponent<ItemStats>().stack += item2.GetComponent<ItemStats>().stack;
            item2.GetComponent<ItemStats>().stack = item1.GetComponent<ItemStats>().stack - item1.GetComponent<ItemStats>().maxStack;
            if (item1.GetComponent<ItemStats>().stack > item1.GetComponent<ItemStats>().maxStack)
            {
                item1.GetComponent<ItemStats>().stack = item1.GetComponent<ItemStats>().maxStack;
            }
            if (item2.GetComponent<ItemStats>().stack <= 0)
            {
                Destroy(item2);
                DeleteItemUI(item2.GetComponent<ItemStats>().itemUI);
            }
            item1.GetComponent<ItemStats>().itemUI.GetComponent<DraggableItem>().SetCapacity();
            if (item2 != null) item2.GetComponent<ItemStats>().itemUI.GetComponent<DraggableItem>().SetCapacity();
        }
    }

    public void UseEquipment(GameObject itemEqp)
    {
        itemEqp.SetActive(true);
        stats.SetAdditionalAttributes(itemEqp.GetComponent<ItemStats>().health, itemEqp.GetComponent<ItemStats>().moveSpeed, itemEqp.GetComponent<ItemStats>().minArmor, itemEqp.GetComponent<ItemStats>().maxArmor);
        switch (itemEqp.GetComponent<ItemStats>().equipmentType)
        {
            case EquipmentType.Helmet:
                itemEqp.transform.SetParent(Helmet.transform, false);
                itemEqp.transform.position = Helmet.transform.position;
                break;
            case EquipmentType.Armor:
                itemEqp.transform.SetParent(Armor.transform, false);
                itemEqp.transform.position = Armor.transform.position;
                break;
            case EquipmentType.Glove:
                itemEqp.transform.SetParent(Glove.transform, false);
                itemEqp.transform.position = Glove.transform.position;
                break;
            case EquipmentType.Shoe:
                itemEqp.transform.SetParent(Shoe1.transform, false);
                itemEqp.transform.position = Shoe1.transform.position;
                GameObject shoeClone = Instantiate(itemEqp);
                shoeClone.transform.SetParent(Shoe2.transform, false);
                shoeClone.transform.position = Shoe2.transform.position;
                shoeClone.GetComponent<SpriteRenderer>().flipX = true;
                break;
            case EquipmentType.Weapon:
                itemEqp.transform.SetParent(Weapon.transform, false);
                itemEqp.transform.position = Weapon.transform.position;
                playerAttack.SetWeapon(itemEqp);
                break;
            case EquipmentType.Necklace:

                break;
            case EquipmentType.Ring:

                break;
        }
    }

    public void UnuseEquipment(GameObject itemUneqp)
    {
        stats.SetAdditionalAttributes(itemUneqp.GetComponent<ItemStats>().health * -1, itemUneqp.GetComponent<ItemStats>().moveSpeed * -1f, itemUneqp.GetComponent<ItemStats>().minArmor * -1, itemUneqp.GetComponent<ItemStats>().maxArmor * -1);
        itemUneqp.transform.SetParent(transform);
        itemUneqp.transform.position = transform.position;
        itemUneqp.SetActive(false);
        switch (itemUneqp.GetComponent<ItemStats>().equipmentType)
        {
            case EquipmentType.Helmet:
                break;
            case EquipmentType.Armor:
                break;
            case EquipmentType.Glove:
                break;
            case EquipmentType.Shoe:
                Destroy(Shoe2.transform.gameObject);
                break;
            case EquipmentType.Weapon:
                playerAttack.SetWeapon(null);
                break;
            case EquipmentType.Necklace:

                break;
            case EquipmentType.Ring:

                break;
        }
    }

    [SerializeField] private int currentUseableItemIndex;
    [SerializeField] private GameObject currentUseableItem;
    [SerializeField] private Transform selectedUseableItemUI;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            currentUseableItemIndex = 0;
            SetSelectedUseableItem();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            currentUseableItemIndex = 1;
            SetSelectedUseableItem();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            currentUseableItemIndex = 2;
            SetSelectedUseableItem();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            currentUseableItemIndex = 3;
            SetSelectedUseableItem();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            currentUseableItemIndex = 4;
            SetSelectedUseableItem();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            currentUseableItemIndex = 5;
            SetSelectedUseableItem();
        }
    }

    void SetSelectedUseableItemUI()
    {
        selectedUseableItemUI.position = quickSlotUI.transform.GetChild(currentUseableItemIndex).position;
    }

    public void SetSelectedUseableItem()
    {
        if (currentUseableItem != null)
        {
            currentUseableItem.GetComponent<UseableItem>().enabled = false;
        }
        if (quickSlotUI.transform.GetChild(currentUseableItemIndex).childCount > 0)
        {
            currentUseableItem = quickSlotUI.transform.GetChild(currentUseableItemIndex).GetChild(0).GetComponent<DraggableItem>().item;
            currentUseableItem.GetComponent<UseableItem>().enabled = true;
        }
        else
        {
            currentUseableItem = null;
        }
        SetSelectedUseableItemUI();
    }
   
    public void UseUseable(GameObject item)
    {
        item.GetComponent<SpriteRenderer>().enabled = false;
        item.SetActive(true);
    }

    public void UnuseUseable(GameObject item)
    {
        item.GetComponent<SpriteRenderer>().enabled = true;
        item.GetComponent<UseableItem>().enabled = false;
        item.SetActive(false);
    }
}
