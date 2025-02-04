using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemsPool", menuName = "ScriptableObjects/Items Pool")]
public class ItemsPoolSO : ScriptableObject
{
    public List<GameObject> items;
}
