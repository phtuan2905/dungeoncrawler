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

    [Header("QuickSlot")]
    public List<GameObject> quickSlots;
    public int currentQuickSlot;

    [Header("Inventory")]
    [SerializeField] private int maxSlotsCapacity;
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
                item.GetComponent<ItemStats>().itemUI = itemUIClone.GetComponent<DraggableItem>();
                itemUIClone.GetComponent<DraggableItem>().SetCapacity(item.GetComponent<ItemStats>().stack);
                return;
            }   
        }
    }

    public void AddItem(GameObject itemAdd)
    {
        if (itemAdd.gameObject.CompareTag("Item"))
        {
            foreach (Transform item in transform)
            {
                if (item.name == itemAdd.name)
                {
                    if (itemAdd.GetComponent<ItemStats>().stack + item.GetComponent<ItemStats>().stack <= item.GetComponent<ItemStats>().maxStack)
                    {
                        item.GetComponent<ItemStats>().stack += itemAdd.GetComponent<ItemStats>().stack;
                        item.GetComponent<ItemStats>().itemUI.SetCapacity(item.GetComponent<ItemStats>().stack);
                        Destroy(itemAdd.gameObject);
                        return;
                    }
                    else
                    {
                        itemAdd.GetComponent<ItemStats>().stack -= (item.GetComponent<ItemStats>().maxStack - item.GetComponent<ItemStats>().stack);
                        item.GetComponent<ItemStats>().stack = item.GetComponent<ItemStats>().maxStack;
                        item.GetComponent<ItemStats>().itemUI.SetCapacity(item.GetComponent<ItemStats>().stack);
                    }
                }
            }
            if (CheckEmptySlot())
            {
                itemAdd.GetComponent<BoxCollider2D>().enabled = false;
                itemAdd.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
                if (itemAdd.GetComponent<ItemStats>().type == Type.Useable)
                {
                    itemAdd.GetComponent<SpriteRenderer>().enabled = false;
                }
                itemAdd.gameObject.transform.SetParent(transform);
                itemAdd.gameObject.SetActive(false);
                AddItemUI(itemAdd.gameObject);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        AddItem(collision.gameObject);
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
                if (quickSlots[i] != null)
                {
                    quickSlots[i].SetActive(false);
                }
                quickSlots[i] = quickSlotUI.transform.GetChild(i).GetChild(0).GetComponent<DraggableItem>().item;
                
                //quickSlots[i].GetComponent<UseableItem>().enabled = false;
            }
            else
            {
                quickSlots[i] = null;
            }
        }
        SetSelectQuickSlots(true);
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
            quickSlots[currentQuickSlot].GetComponent<UseableItem>().enabled = option;
            quickSlots[currentQuickSlot].SetActive(option);
        }
    }

    void SetUISelectQuickSlots()
    {
        quickSlotSelectUI.transform.position = quickSlotUI.transform.GetChild(currentQuickSlot).position;
    }

    bool CheckEmptySlot()
    {
        foreach (Transform itemUI in inventoryUI.transform)
        {
            if (itemUI.childCount == 0 && itemUI.CompareTag("Inventory Slot"))
            {
                return true;
            }
        }
        return false;
    }

    public void UseUseableItem()
    {
        SetUseable();
    }

    [Header("Drop Item")]
    [SerializeField] private int dropNumber;
    [SerializeField] private GameObject dropPanel;
    private Type dropSlotType;
    private EquipmentType dropSlotEqpType;
    private GameObject dropItem;
    [SerializeField] private TextMeshProUGUI dropNumberUI;
    [SerializeField] private TextMeshProUGUI maxDropNumberUI;

    public void StartDropItem(GameObject item, Type slotType, EquipmentType slotEqpType)
    {
        ItemStats itemStats = item.GetComponent<ItemStats>();
        dropNumber = itemStats.stack;
        if (dropNumber == 1)
        {
            if (slotType == Type.Equipment)
            {
                SetEquipment(null, slotType, slotEqpType);
                if (slotEqpType == EquipmentType.Weapon)
                {
                    playerAttack.SetWeapon(null);
                }
            }
            if (item.GetComponent<ItemStats>().type == Type.Useable)
            {
                item.GetComponent<UseableItem>().enabled = false;
            }
            item.GetComponent<SpriteRenderer>().enabled = true;
            item.transform.SetParent(null);
            item.transform.position = transform.position + new Vector3(0, item.GetComponent<BoxCollider2D>().size.y + 0.1f + (GetComponent<BoxCollider2D>().size.y / 2f), 0);
            item.GetComponent<BoxCollider2D>().enabled = true;
            item.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            Destroy(item.GetComponent<ItemStats>().itemUI.gameObject);
            item.SetActive(true);
            dropPanel.SetActive(false);
            return;
        }
        dropNumberUI.text = dropNumber.ToString();
        maxDropNumberUI.text = dropNumber.ToString();
        dropPanel.SetActive(true);
        dropItem = item;
        dropSlotType = slotType;
        dropSlotEqpType = slotEqpType;
    }

    public void ConfirmDrop()
    {
        GameObject dropItemClone = Instantiate(dropItem);
        dropItemClone.name = dropItem.name;
        if (dropSlotType == Type.Equipment)
        {
            SetEquipment(null, dropSlotType, dropSlotEqpType);
            if (dropSlotEqpType == EquipmentType.Weapon)
            {
                playerAttack.SetWeapon(null);
            }
        }
        if (dropItemClone.GetComponent<ItemStats>().type == Type.Useable)
        {
            dropItemClone.GetComponent<UseableItem>().enabled = false;
        }
        dropItemClone.GetComponent<SpriteRenderer>().enabled = true;
        dropItemClone.transform.SetParent(null);
        dropItemClone.transform.position = transform.position + new Vector3(0, dropItemClone.GetComponent<BoxCollider2D>().size.y + 0.1f + (GetComponent<BoxCollider2D>().size.y / 2f), 0);
        dropItemClone.GetComponent<BoxCollider2D>().enabled = true;
        dropItemClone.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        dropItemClone.GetComponent<ItemStats>().stack = dropNumber;
        dropItemClone.SetActive(true);
        SetActiveDropPanel();

        dropItem.GetComponent<ItemStats>().stack -= dropNumber;
        dropItem.GetComponent <ItemStats>().itemUI.SetCapacity(dropItem.GetComponent<ItemStats>().stack);
        if (dropItem.GetComponent<ItemStats>().stack == 0)
        {
            Destroy(dropItem.GetComponent<ItemStats>().itemUI.gameObject);
            Destroy(dropItem);
        }
    }

    public void SetActiveDropPanel()
    {
        dropPanel.SetActive(false);
    }

    public void ChangeDropNumber(bool state)
    {
        if (state && dropNumber < dropItem.GetComponent<ItemStats>().stack)
        {
            dropNumber++;
        }
        else if (!state && dropNumber > 1)
        {
            dropNumber--;
        }
        dropNumberUI.text = dropNumber.ToString();
    }
}
