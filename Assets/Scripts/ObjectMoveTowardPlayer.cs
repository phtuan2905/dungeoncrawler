using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMoveTowardPlayer : MonoBehaviour
{
    private ObjectStats stats;
    private ObjectViewRange viewRange;
    private Rigidbody2D rgbd;

    private Vector2 currentPoint;

    bool canMove = true;
    bool isPlayer = false;

    [Header("Object Attributes")]
    private float speed;

    private Vector2 direction;

    private void Awake()
    {
        stats = GetComponent<ObjectStats>();
        rgbd = GetComponent<Rigidbody2D>();
        viewRange = transform.Find("ViewRange").GetComponent<ObjectViewRange>();
    }

    private void Update()
    {
        MoveToPoint();
    }

    void MoveToPoint()
    {
        if (canMove && viewRange.canSee)
        {
            if (!isPlayer)
            {
                direction = viewRange.player.transform.position - transform.position;
            }
            else
            {
                direction = Vector2.zero;
            }
        }
        else
        {
            if (viewRange.canSee)
            {
                direction = viewRange.player.transform.position - transform.position;
            }
            else
            {
                direction = Vector2.zero;
            }
        }
        rgbd.velocity = direction.normalized * stats.speed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            canMove = false;
        }
        if (collision.gameObject.name == "Player")
        {
            isPlayer = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            canMove = true;
        }
        if (collision.gameObject.name == "Player")
        {
            isPlayer = false;
        }
    }
}
