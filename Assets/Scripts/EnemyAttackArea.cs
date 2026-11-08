using UnityEngine;

public class EnemyAttackArea : MonoBehaviour
{
    [SerializeField] private int Damage = 10;  
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Health>() != null)
        {
            Health health = collision.GetComponent<Health>();
            health.Damage(Damage);
        }
    }
}