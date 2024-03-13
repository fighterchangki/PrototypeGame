using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRange : MonoBehaviour
{
    EnemyAI enemyAI;
    public BoxCollider boxCollider;

    // Start is called before the first frame update
    void Start()
    {
        boxCollider = GetComponent<BoxCollider>();
        enemyAI = GetComponentInParent<EnemyAI>();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (enemyAI.state != EnemyAI.State.Dead)
            {
                enemyAI.stateChange = EnemyAI.State.Running;
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            if (enemyAI.state != EnemyAI.State.Dead)
            {
                enemyAI.stateChange = EnemyAI.State.Idle;
            }
        }
    }
}
