using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private Animator animator;
    private PlayerStats stats;
    private PlayerHandControl handControl;
    public WeaponStats weaponStats;
    private bool isAttacking = false;
    private void Awake()
    {
        animator = GetComponent<Animator>();    
        stats = GetComponent<PlayerStats>();
        handControl = GetComponent<PlayerHandControl>();
        //weaponStats = transform.Find("Player Hand/Player Hand Flip/Player Hand Position/Player Hand Sprite/Player Weapon").GetComponent<WeaponStats>();
        animator.SetFloat("AttackSpeedMultiplier", weaponStats.speed);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && !isAttacking)
        {
            isAttacking = true;
            animator.SetLayerWeight(1, 1);
            animator.SetBool("IsAttacking", true);
            handControl.canFollow = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            EndAttack();
        }
    }

    void EndAttack()
    {
        animator.SetBool("IsAttacking", false);
        animator.SetLayerWeight(1, 0);
        handControl.canFollow = true;
        isAttacking = false;
    }
}
