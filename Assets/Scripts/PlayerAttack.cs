using UnityEngine;

public class PlayerAttack : MonoBehaviour
{

    private GameObject attackArea= default;

    private Animator animator;

    bool attacking = false;

    private float attackTime = 0.25f;
    private float timer = 0f;

    private int Attack2Step = 0;
    private float Attack2Timer = 0f;
    private float Attack2Window = 0.5f;
    


   
    void Start()
    {
        attackArea = transform.GetChild(0).gameObject;
        animator = GetComponent<Animator>();
        attackArea.SetActive(false);
        
    }

    
    void Update()
    {

   if (GameManager.Instance != null && GameManager.Instance.CurrentGameState != GameManager.GameState.Playing)
    {
        return;
    }



    if (Input.GetKeyDown(KeyCode.Space))
        {
            
            if (!attacking && Attack2Step == 0)
            {
                Attack(1);
            }
           
            else if (Attack2Step == 1 && Attack2Timer <= Attack2Window)
            {
                Attack(2);
            }
        }

        if(attacking)
        {
            timer += Time.deltaTime;
            if(timer >= attackTime)
            {
                attacking = false;
                attackArea.SetActive(false);
                timer = 0;
            
        

        
            if(Attack2Step == 1)
            {
                Attack2Timer = 0f;
            }
        
        }
    }

if (Attack2Step == 1)
        {
            Attack2Timer += Time.deltaTime;  
            if (Attack2Timer > Attack2Window)
            {
                Attack2Step = 0;  
                Attack2Timer = 0f;
            }
        }
        
    }


    

   /* private void Attack()
    {
        attacking = true;
        attackArea.SetActive(attacking);

        if (animator != null)
    {
        animator.SetTrigger("Attacking");
    }
    
    }*/

private void Attack(int step)
    {
        attacking = true;
        attackArea.SetActive(true);
        timer = 0f;

    if (AudioManager.Instance != null)
    {
        AudioManager.Instance.PlaySound(AudioManager.Instance.SwordSlash);
    }



if (step == 1)
        {
            animator.SetTrigger("Attacking");
            Attack2Step = 1;
            Attack2Timer = 0f;  // Reset timer to start combo window countdown
        }
        else if (step == 2)
        {
            animator.SetTrigger("Attacking2");
            Attack2Step = 2;  // Combo complete
            
            // Optional: Reset after a delay
            Invoke(nameof(ResetCombo), attackTime);
        }
    }
    
    private void ResetCombo()
    {
        Attack2Step = 0;
        Attack2Timer = 0f;
    }
   
}







