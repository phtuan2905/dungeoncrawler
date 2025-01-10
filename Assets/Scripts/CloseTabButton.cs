using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseTabButton : MonoBehaviour
{
    private GameObject tab;
    private PlayerHandControl playerHandControl;
    private PlayerAttack playerAttack;
    private void Awake()
    {
        tab = transform.parent.gameObject;
        playerAttack = GameObject.Find("Player").GetComponent<PlayerAttack>();
        playerHandControl = GameObject.Find("Player").GetComponent<PlayerHandControl>();
    }
    public void OnClick()
    {
        if (tab.activeSelf)
        {
            playerAttack.enabled = true;
            playerHandControl.enabled = true;
            tab.SetActive(false);
        }
    }
}
