using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DraggableItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler
{
    private HoldItemTemporary holdItemTemp;
    public TextMeshProUGUI capacityText;

    public Type type;
    public EquipmentType equipmentType;

    [SerializeField] public GameObject item;
    private void Awake()
    {
        holdItemTemp = GameObject.Find("UI/Safe Area/Hold Item Temporary").GetComponent<HoldItemTemporary>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        holdItemTemp.slotBefore = transform.parent.gameObject;
        holdItemTemp.slotAfter = null;
        GetComponent<Image>().raycastTarget = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.SetParent(holdItemTemp.transform);
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        //if (holdItemTemp.GetComponent<HoldItemTemporary>().slotAfter == null)
        //{
        //    transform.position = holdItemTemp.GetComponent<HoldItemTemporary>().slotBefore.transform.position;
        //    transform.SetParent(holdItemTemp.GetComponent<HoldItemTemporary>().slotBefore.transform);
        //    holdItemTemp.GetComponent<HoldItemTemporary>().DropUIItem(item);
        //}
        holdItemTemp.ChangeItemUISlot();
        GetComponent<Image>().raycastTarget = true;
    }

    public void OnDrop(PointerEventData eventData)
    {
        holdItemTemp.GetComponent<HoldItemTemporary>().slotAfter = transform.parent.gameObject;
    }
    
    public void SetItem(GameObject addItem)
    {
        item = addItem;
        type = addItem.GetComponent<ItemStats>().type;
        equipmentType = addItem.GetComponent<ItemStats>().equipmentType;
        GetComponent<Image>().sprite = item.GetComponent<SpriteRenderer>().sprite;
    }

    public void SetCapacity()
    {
        capacityText.text = item.GetComponent<ItemStats>().stack.ToString();
    }
}
