using UnityEngine;

public class Health : MonoBehaviour
{

    [SerializeField] private int health = 100;

    private int MAX_HEALTH = 100;

    private Animator animator;
    
    
    private bool isDead = false;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        /*if(Input.GetKeyDown(KeyCode.G))
        {
            Damage(10);
        }

        if(Input.GetKeyDown(KeyCode.H))
        {
            Heal(10);
        }*/


    }

    public void Damage(int amount)
    {

        if (isDead) return;

        if(amount <0)
        {

            throw new System.ArgumentOutOfRangeException("Cannot have negative damage");
        }

        this.health -= amount;

    if (GameManager.Instance != null && gameObject.CompareTag("Player"))
    {
        GameManager.Instance.UpdatePlayerHealth(this.health);
    }


    if (AudioManager.Instance != null && gameObject.CompareTag("Player"))
    {
        AudioManager.Instance.PlaySound(AudioManager.Instance.Damage);
    }

    if (animator != null)
    {
        animator.SetTrigger("Damage");
    }

        if(health <= 0)
        {
            Die();
        }

    }

    public void Heal(int amount)
    {

         if (isDead) return;

        if(amount <0)
        {

            throw new System.ArgumentOutOfRangeException("Cannot have negative healing");
        }

        bool woulndBeOverMaxHealth = health + amount > MAX_HEALTH;

        if(health + amount > MAX_HEALTH)
        {
            this.health = MAX_HEALTH;
           
        }

        else
        {
            this.health += amount;
        }
        
    if (GameManager.Instance != null && gameObject.CompareTag("Player"))
    {
        GameManager.Instance.UpdatePlayerHealth(this.health);
    }


    }

    private void Die()
    {

        if (isDead) return;


        isDead = true;

        if (GameManager.Instance != null && gameObject.CompareTag("Player"))
    {
        GameManager.Instance.UpdatePlayerHealth(this.health);
        
        
        GameManager.Instance.GameOver();
    }

        animator.SetTrigger("Death");
        Debug.Log("Player is dead");
        //Destroy(gameObject);

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
