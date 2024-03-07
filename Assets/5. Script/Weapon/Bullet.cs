using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float timeToDestory;
    public WeaponManager weapon;
    public float damage = 20;
    public Vector3 dir;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(this.gameObject, timeToDestory);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponentInParent<EnemyHealth>())
        {
            EnemyHealth enemyHealth = collision.gameObject.GetComponentInParent<EnemyHealth>();
            enemyHealth.TakeDamage(damage);

            //ÀûÀÌ Á×À»¶§
            if (enemyHealth.health <= 0 && enemyHealth.isDead == false)
            {
                Rigidbody rb = collision.gameObject.GetComponent<Rigidbody>();
                rb.AddForce(dir * weapon.enemyKickbackForce, ForceMode.Impulse);//Á×À¸¸é µÚ·Î ¹Ð¾î³¿
                enemyHealth.isDead = true;
            }
        }
        Destroy(this.gameObject);
    }
}
