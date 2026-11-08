using UnityEngine;

public class AttackArea : MonoBehaviour
{

private int Damage =10;
public float knockbackForce = 50f;

// private void OnTriggerEnter2D(Collider2D collision)
//     {
//        if(collision.GetComponent<Health>() != null)
//         {
//             Health health = collision.GetComponent<Health>();

//             //health.Damage(Damage);
        
//         }
//     }


private void OnTriggerEnter2D(Collider2D collision)
{
    EnemyHealth enemyHealth = collision.GetComponent<EnemyHealth>();
    
    if (enemyHealth != null)
    {
        enemyHealth.EnemyDamage(Damage);
    }

EnemyKnockback enemyKnockback = collision.GetComponent<EnemyKnockback>();
        if (enemyKnockback != null)
        {
            
            Transform playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
            enemyKnockback.Knockback(playerTransform, knockbackForce);
        }


}


}
