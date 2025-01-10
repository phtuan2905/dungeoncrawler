using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DraggableItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler
{
    private GameObject holdItemTemp;

    public Type type;
    public EquipmentType equipmentType;

    [SerializeField] public GameObject item;
    private void Awake()
    {
        holdItemTemp = GameObject.Find("UI/Safe Area/Hold Item Temporary");
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        holdItemTemp.GetComponent<HoldItemTemporary>().slotBefore = transform.parent.gameObject;
        holdItemTemp.GetComponent<HoldItemTemporary>().slotAfter = null;
        GetComponent<Image>().raycastTarget = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.SetParent(holdItemTemp.transform);
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (holdItemTemp.GetComponent<HoldItemTemporary>().slotAfter == null)
        {
            transform.SetParent(holdItemTemp.GetComponent<HoldItemTemporary>().slotBefore.transform);
            transform.position = transform.parent.position;
        }
        GetComponent<Image>().raycastTarget = true;
    }

    public void OnDrop(PointerEventData eventData)
    {
        holdItemTemp.GetComponent<HoldItemTemporary>().slotAfter = transform.parent.gameObject;
        holdItemTemp.GetComponent<HoldItemTemporary>().ChangeSlot();
    }
    
    public void SetItem(GameObject addItem, Type itemType, EquipmentType itemEqpType)
    {
        item = addItem;
        type = itemType;
        equipmentType = itemEqpType;
        GetComponent<Image>().sprite = item.GetComponent<SpriteRenderer>().sprite;
    }
}
