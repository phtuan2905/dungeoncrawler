using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlLevelButton : MonoBehaviour
{
    private LevelUpSystem levelUpSystem;

    private void Awake()
    {
        levelUpSystem = transform.parent.parent.parent.GetComponent<LevelUpSystem>();
    }
    public bool buttonState;
    public int buttonIndex;
    public void OnClick()
    {
        levelUpSystem.LevelUpPoint(buttonState, buttonIndex);
    }
}
