using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoving : MonoBehaviour
{
    private PlayerStats stats;
    private Rigidbody2D rgbd;
    private SpriteRenderer srdr;
    private Animator animator;

    //private SpriteRenderer playerHead;
    //private SpriteRenderer playerBody;
    //private SpriteRenderer playerLeg1;
    //private SpriteRenderer playerLeg2;

    [Header("Player Stats")]
    [SerializeField] private float moveSpeed;

    [Header("Player Index")]
    [SerializeField] private Vector2 moveDirection;

    private void Awake()
    {
        stats = GetComponent<PlayerStats>();
        rgbd = GetComponent<Rigidbody2D>();
        srdr = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        moveSpeed = stats.moveSpeed;
        //playerHead = transform.Find("Player Head").GetComponent<SpriteRenderer>();
        //playerBody = transform.Find("Player Body").GetComponent<SpriteRenderer>();
        //playerLeg1 = transform.Find("Player Leg 1").GetComponent<SpriteRenderer>();
        //playerLeg2 = transform.Find("Player Leg 2").GetComponent<SpriteRenderer>();

    }
    void Start()
    {
        
    }

    void Update()
    {
        Moving();
        ControlAnimation();
    }

    void Moving()
    {
        moveDirection.x = Input.GetAxisRaw("Horizontal");
        moveDirection.y = Input.GetAxisRaw("Vertical");
        rgbd.velocity = moveDirection.normalized * moveSpeed;
        //if (moveDirection.x > 0)
        //{
        //    playerHead.flipX = false;
        //    playerBody.flipX = false;
        //    playerLeg1.flipX = false;
        //    playerLeg2.flipX = true;
        //}
        //else if (moveDirection.x < 0)
        //{
        //    playerHead.flipX = true;
        //    playerBody.flipX = true;
        //    playerLeg1.flipX = true;
        //    playerLeg2.flipX = false;
        //}
    }

    void ControlAnimation()
    {
        if (rgbd.velocity != Vector2.zero)
        {
            animator.SetBool("IsRunning", true);
        }
        else
        {
            animator.SetBool("IsRunning", false);
        }
    }
}
