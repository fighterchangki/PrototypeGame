using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyAI : MonoBehaviour
{
    public enum State
    {
        Idle,
        Running,
        Hit,
        Attack,
        Dead
    }
    public State state = State.Idle;
    public State stateChange
    {
        get
        {
            return state;
        }
        set
        {
            if (state == value) return;
            state = value;
            StateBehaviour();
        }
    }
    EnemyAttackRange enemyAttackRange;
    EnemyHealth enemyHealth;
    public Transform target;
    Vector3 destination;
    NavMeshAgent agent;
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        enemyAttackRange = GetComponentInChildren<EnemyAttackRange>();
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        agent.speed = 0;
        destination = GameObject.FindWithTag("Player").gameObject.transform.position;
    }
    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(destination, target.position) > 1.0f)
        {
            destination = target.position;
            agent.destination = destination;
        }
        
    }
    void StateBehaviour()
    {
        switch (state)
        {
            case State.Idle:
                animator.SetBool("Running", false);
                break;
            case State.Running:
                animator.SetBool("Running", true);
                break;
            case State.Dead:
                Dead();
                break;
            case State.Attack:
                Attack();
                break;
            case State.Hit:
                animator.SetTrigger("Hit");
                break;
        }
    }
    public void Running()
    {
        agent.speed = 1;
    }
    public void Idle()
    {
        agent.speed = 0;
    }
    public void Dead()
    {
        agent.speed = 0;
    }
    public void Attack()
    {
        enemyAttackRange.GetComponent<BoxCollider>().enabled = false;
        animator.SetTrigger("Attack");
        
    }
    public void returnAttack()
    {
        if (stateChange == State.Attack)
        {
            enemyAttackRange.GetComponent<BoxCollider>().enabled = true;
            Invoke("Attack", 1.5f);
        }
    }
    public void returnState()
    {
        if (agent.speed > 0)
        {
            stateChange = State.Running;
        }
        else
        {
            stateChange = State.Idle;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("ÇÃ·¹ÀÌ¾î¶û ¸ÂºÎµúÈû");
                stateChange = State.Attack;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            returnState();
        }
    }
}
