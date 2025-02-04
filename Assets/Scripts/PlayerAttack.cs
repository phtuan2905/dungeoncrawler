using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private Animator animator;
    private PlayerStats stats;
    private PlayerHandControl handControl;
    private int attackType;
    public BoxCollider2D playerWeapon;
    public WeaponStats weaponStats;
    private bool isAttacking = false;
    private void Awake()
    {
        animator = GetComponent<Animator>();    
        stats = GetComponent<PlayerStats>();
        handControl = GetComponent<PlayerHandControl>();
        //weaponStats = transform.Find("Player Hand/Player Hand Flip/Player Hand Position/Player Hand Sprite/Player Weapon").GetComponent<WeaponStats>();
        SetWeapon(null);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && !isAttacking)
        {
            isAttacking = true;
            animator.SetLayerWeight(attackType, 1);
            animator.SetBool("IsAttacking", true);
            handControl.canFollow = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if (collision.gameObject.CompareTag("Obstacle"))
        //{
        //    EndAttack();
        //}
        if (!collision.gameObject.CompareTag("Enemy") && !collision.gameObject.CompareTag("Object"))
        {
            EndAttack();
        }
    }

    void EndAttack()
    {
        handControl.canFollow = true;
        animator.SetBool("IsAttacking", false);
        isAttacking = false;
        for (int i = 1; i < animator.layerCount; i++) animator.SetLayerWeight(i, 0);
    }

    public void SetWeapon(GameObject item)
    {
        if (item != null)
        {
            WeaponStats itemStats = item.GetComponent<WeaponStats>();
            attackType = itemStats.attackType;
            animator.SetFloat("AttackSpeedMultiplier", itemStats.speed * stats.attackSpeed);
            playerWeapon.size = new Vector2(itemStats.hitBoxWidth, itemStats.hitBoxHeight);
            playerWeapon.offset = new Vector2(itemStats.hitBoxX, itemStats.hitBoxY);
        }
        else
        {
            attackType = 1;
            animator.SetFloat("AttackSpeedMultiplier", stats.attackSpeed);
            playerWeapon.size = new Vector2(0.25f, 0.25f);
            playerWeapon.offset = Vector2.zero;
        }
    }

    /*
     * hand attack = 1
     * sword attack = 2
     * spear attack = 3
     */
}
