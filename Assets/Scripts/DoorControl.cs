using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorControl : MonoBehaviour
{
    [SerializeField] private Sprite closeSprite;
    [SerializeField] private Sprite openSprite;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            ControlDoor(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            ControlDoor(false);
        }
    }

    public void ControlDoor(bool state)
    {
        GetComponent<BoxCollider2D>().isTrigger = state;
        if (state)
        {
            GetComponent<SpriteRenderer>().sprite = openSprite;
        }
        else
        {
            GetComponent<SpriteRenderer>().sprite = closeSprite;
        }
    }
}
