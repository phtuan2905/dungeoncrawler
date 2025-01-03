using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private Animator animator;
    private void Awake()
    {
        animator = GetComponent<Animator>();    
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            animator.SetLayerWeight(1, 1);
            animator.SetBool("IsAttacking", true);
        }
    }

    void EndAttack()
    {
        animator.SetBool("IsAttacking", false);
        animator.SetLayerWeight(1, 0);
    }
}
