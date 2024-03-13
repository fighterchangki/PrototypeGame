using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackRange : MonoBehaviour
{
    BoxCollider boxCollider;
    public bool isAttack;
    public int damege;
    // Start is called before the first frame update
    void Start()
    {
        boxCollider = GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!boxCollider.enabled)
        {
            isAttack = false;
        }
        else
        {
            isAttack = true;

        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (isAttack)
            {
                isAttack = false;
                other.gameObject.GetComponent<PlayerStatus>().Damege(damege);
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (!isAttack)
            {
                isAttack = true;
                Debug.Log("��������!!!!");
            }
        }
    }
}
