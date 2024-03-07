using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float health;
    Animator animator;
    RagdollManager ragdollManager;
    public UIManager uiManager;
    public SoundManager soundManager;
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
            uiManager.Hit();
            soundManager.Hit();
            health -= damage;
            if (health <= 0) EnemyDeath();
        }
    }
    void EnemyDeath()
    {
        animator.enabled = false;
        ragdollManager.TriggerRagdoll();
    }
    
}
