using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    private GameObject attackArea = default;
    private Animator animator;
    private Transform player;

    bool attacking = false;
    private float attackTime = 0.25f;
    private float timer = 0f;

    [SerializeField] private float detectionRange = 2f;

    [SerializeField] private float attackCooldown = 1.5f;
    private float cooldownTimer = 0f;

    void Start()
    {
        attackArea = transform.Find("EnemyAttackArea").gameObject;
        animator = GetComponent<Animator>();
        attackArea.SetActive(false);

        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.transform;
        }
    }

    void Update()
    {
        if (player == null) return;

        if (cooldownTimer > 0)
        {
            cooldownTimer -= Time.deltaTime;
        }

        float distance =
            Vector2.Distance(transform.position, player.position);

        if (distance <= detectionRange &&
            !attacking &&
            cooldownTimer <= 0)
        {
            Attack();
        }

        if (attacking)
        {
            timer += Time.deltaTime;

            if (timer >= attackTime)
            {
                attacking = false;
                attackArea.SetActive(false);
                timer = 0f;
                cooldownTimer = attackCooldown;
            }
        }
    }

    private void Attack()
    {
        attacking = true;
        attackArea.SetActive(true);
        timer = 0f;

        if (animator != null)
        {
            animator.SetBool("isRunning", false);
            Debug.Log("Sending EnemyAttack trigger");
            animator.SetTrigger("EnemyAttack");
        }
    }

    public bool IsAttacking()
    {
        return attacking;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }
}