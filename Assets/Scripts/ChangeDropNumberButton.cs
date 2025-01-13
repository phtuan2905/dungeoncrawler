using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeDropNumberButton : MonoBehaviour
{
    public bool state;
    public InventoryManage inventoryManager;
    public void OnClick()
    {
        inventoryManager.ChangeDropNumber(state);
    }
}
