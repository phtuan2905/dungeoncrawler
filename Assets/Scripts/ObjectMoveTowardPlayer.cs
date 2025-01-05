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
        if (viewRange.canSee && canMove)
        {

            direction = viewRange.player.transform.position - transform.position;
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
        if (collision.gameObject.name == "Player" || collision.gameObject.CompareTag("Obstacle"))
        {
            canMove = false;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Player" || collision.gameObject.CompareTag("Obstacle"))
        {
            canMove = true;
        }
    }
}
