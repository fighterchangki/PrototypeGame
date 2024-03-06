using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float health;
    Animator animator;
    RagdollManager ragdollManager;
    [HideInInspector] public bool isDead;
    private void Start()
    {
        ragdollManager = GetComponent<RagdollManager>();
        animator = GetComponent<Animator>();
    }
    public void TakeDamage(float damage)
    {
        if (health > 0)
        {
            health -= damage;
            if (health <= 0) EnemyDeath();
            else Debug.Log("Hit");
        }
    }
    void EnemyDeath()
    {
        animator.enabled = false;
        ragdollManager.TriggerRagdoll();
        Debug.Log("Die");
    }
}
