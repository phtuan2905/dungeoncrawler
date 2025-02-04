using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class ChestInteract : MonoBehaviour
{
    [SerializeField] private Transform chestUIManager;
    [SerializeField] private GameObject chestUI;
    [SerializeField] private GameObject itemUIPrefab;
    [SerializeField] private ItemsPoolSO itemsPool;


    private void Awake()
    {
        chestUIManager = GameObject.Find("UI/Safe Area/Chest UI Manager").transform;
        AddRandomItems();
        CreateUI();
    }

    void AddRandomItems()
    {
        int randomTotal = Random.Range(1, 6);
        for (int i = 0; i < randomTotal; i++)
        {
            int randomIndex = Random.Range(0, itemsPool.items.Count);
            GameObject itemClone = Instantiate(itemsPool.items[randomIndex], transform);
            itemClone.transform.position = transform.position;
            itemClone.name = itemsPool.items[randomIndex].name;
            itemClone.SetActive(false);
            itemClone.GetComponent<BoxCollider2D>().enabled = false;
            itemClone.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
            itemClone.GetComponent<ItemStats>().stack = Random.Range(1, itemClone.GetComponent<ItemStats>().maxStack + 1);
        }
    }

    void CreateUI()
    {
        chestUI = Instantiate(chestUI, chestUIManager);
        foreach (Transform slotUI in chestUI.transform)
        {
            if (slotUI.CompareTag("Inventory Slot"))
            {
                slotUI.GetComponent<InventorySlot>().inventoryOrigin = gameObject;

            }
        }

        foreach (Transform item in transform)
        {
            CreateItemUI(item.gameObject, FindEmptySlot());
        }
    }

    Transform FindEmptySlot()
    {
        foreach (Transform slotUI in chestUI.transform)
        {
            if (slotUI.childCount == 0 && slotUI.CompareTag("Inventory Slot"))
            {
                return slotUI;
            }
        }
        return null;
    }

    void CreateItemUI(GameObject item, Transform slot)
    {
        GameObject itemUI = Instantiate(itemUIPrefab, slot);
        itemUI.transform.localPosition = Vector3.zero;
        itemUI.GetComponent<DraggableItem>().SetItem(item);
        item.GetComponent<ItemStats>().itemUI = itemUI;
        itemUI.GetComponent<DraggableItem>().SetCapacity();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            chestUI.SetActive(true);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && chestUI.activeSelf)
        {
            chestUI.SetActive(false);
        }
    }
}
