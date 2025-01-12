using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenTabButton : MonoBehaviour
{
    public GameObject tab;
    private PlayerHandControl playerHandControl;
    private PlayerAttack playerAttack;

    private void Start()
    {
        playerAttack = GameObject.Find("Player").GetComponent<PlayerAttack>();
        playerHandControl = GameObject.Find("Player").GetComponent<PlayerHandControl>();
    }

    public void OnClick()
    {
        if (!tab.activeSelf)
        {
            playerAttack.enabled = false;
            playerHandControl.enabled = false;
            tab.SetActive(true);
        }
    }
}
