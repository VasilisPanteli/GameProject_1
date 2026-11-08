using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int health = 30;
    //private int MAX_HEALTH = 30;
    private Animator animator;
    private bool isDead = false;
    private EnemySpawner spawner;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

     public void SetSpawner(EnemySpawner spawnerRef)  
    {
        spawner = spawnerRef;
        Debug.Log("Spawner assigned to enemy: " + gameObject.name);
    }

    public void EnemyDamage(int amount)
    {
        if (isDead) return;

        if (amount < 0)
        {
            throw new System.ArgumentOutOfRangeException("Cannot have negative damage");
        }

        this.health -= amount;

        if (animator != null)
        {
            animator.SetTrigger("EnemyDamage");
        }

        if (health <= 0)
        {
            EnemyDie();
        }
    }

    private void EnemyDie()
    {
        if (isDead) return;

        Debug.Log("EnemyDie called on: " + gameObject.name); 

        isDead = true;


        if (AudioManager.Instance != null)
    {
        AudioManager.Instance.PlaySound(AudioManager.Instance.EnemyDeath);
    }    

        if (GameManager.Instance != null)
    {
        GameManager.Instance.EnemyDefeated();
    }


        animator.SetTrigger("EnemyDeath");
        Debug.Log("Enemy is dead");
        //Destroy(gameObject);


    //GetComponent<EnemyFollow>().enabled = false;
    //GetComponent<EnemyKnockback>().enabled = false;

 EnemyFollow follow = GetComponent<EnemyFollow>();
    if (follow != null) follow.enabled = false;
    
    EnemyAttack attack = GetComponent<EnemyAttack>();
    if (attack != null) attack.enabled = false;


 if (spawner != null)
        {
            Debug.Log("Notifying spawner that enemy died");  // ADD THIS
            spawner.EnemyDied(gameObject);
        }
        else
        {
            Debug.LogError("Spawner is NULL on enemy: " + gameObject.name);  // ADD THIS
        }



        Destroy(gameObject, 1f);
    }

    public int GetCurrentHealth()
    {
        return health;
    }

    public bool IsDead()
    {
        return isDead;
    }
}