using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfirmLevelUpButton : MonoBehaviour
{
    private LevelUpSystem levelUpSystem;
    private void Awake()
    {
        levelUpSystem = transform.parent.parent.parent.GetComponent<LevelUpSystem>();
    }
    public void OnClick()
    {
        levelUpSystem.SetPermStats();
    }
}
