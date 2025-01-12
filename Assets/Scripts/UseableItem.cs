using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UseableItem : MonoBehaviour
{
    [SerializeField] public GameObject effect;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            GameObject effectClone = Instantiate(effect);
            effectClone.transform.SetParent(transform.parent.parent);
            //Debug.Log("Effect from " + gameObject.name);
        }
    }
}
