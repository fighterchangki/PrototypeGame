using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float health;
    Animator animator;
    RagdollManager ragdollManager;
    public EnemyAI enemyAI;
    public UIManager uiManager;
    public SoundManager soundManager;
    [HideInInspector] public bool isDead;
    private void Start()
    {
        ragdollManager = GetComponent<RagdollManager>();
        animator = GetComponent<Animator>();
        enemyAI = GetComponent<EnemyAI>();
    }
    public void TakeDamage(float damage)
    {
        if (health > 0)
        {
            enemyAI.stateChange = EnemyAI.State.Hit;
            uiManager.Hit();
            soundManager.Hit();
            health -= damage;
            if (health <= 0)
            {
                enemyAI.stateChange = EnemyAI.State.Dead;
                animator.enabled = false;
                ragdollManager.TriggerRagdoll();
                Invoke("EnemyDestory", 2f);
            } 
        }
    }
    void EnemyDestory()
    {
        Destroy(gameObject);
    }
    
}
