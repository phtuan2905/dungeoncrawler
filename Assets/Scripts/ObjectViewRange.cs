using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectViewRange : MonoBehaviour
{
    public GameObject player;
    private CircleCollider2D circleCollider;

    private bool canCastRay = false;

    public bool canSee = false;

    private RaycastHit2D hit;

    private void Start()
    {
        circleCollider = GetComponent<CircleCollider2D>();
    }

    public void Update()
    {
        if (canCastRay)
        {
            hit = Physics2D.Raycast(transform.position, player.transform.position - transform.position, circleCollider.radius);
            if (hit.collider != null)
            {
                if (hit.transform.gameObject.CompareTag("Player"))
                {
                    canSee = true;
                }
                else
                {
                    canSee = false;
                }
            }
            else
            {
                canSee = false;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && collision.gameObject.name == "Player")
        {
            player = collision.gameObject;
            canCastRay = true;
            canSee = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && collision.gameObject.name == "Player")
        {
            player = null;
            canCastRay = false;
            canSee = false;
        }
    }
}
