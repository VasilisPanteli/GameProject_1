using UnityEngine;

public enum EnemyState
{
    Following,
    Knockback
}

public class EnemyFollow : MonoBehaviour
{
    public Transform player;
    public float speed = 2f;

    private Animator animator;
    private Vector3 originalScale;

    public EnemyState currentState = EnemyState.Following;

    private EnemyAttack enemyAttack;

    void Start()
    {
        animator = GetComponent<Animator>();
        enemyAttack = GetComponent<EnemyAttack>();
        originalScale = transform.localScale;

    if (player == null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null)
            {
                player = playerObj.transform;
                Debug.Log("EnemyFollow: Auto-assigned player - " + player.name);
            }
            else
            {
                Debug.LogError("EnemyFollow: No Player found! Make sure Player has 'Player' tag.");
            }
        }



    }

    void Update()
    {
        if (player == null) return;

        // Stop movement during knockback
        if (currentState == EnemyState.Knockback) return;

        // Stop movement while attacking
        if (enemyAttack != null && enemyAttack.IsAttacking()) return;

        Vector3 direction = (player.position - transform.position).normalized;

        Vector3 move = new Vector3(direction.x, direction.y, 0f);
        transform.Translate(move * speed * Time.deltaTime);

        bool isMoving =
            Mathf.Abs(direction.x) > 0.1f ||
            Mathf.Abs(direction.y) > 0.1f;

        if (animator != null)
        {
            animator.SetBool("isRunning", isMoving);
        }

        if (direction.x > 0.1f)
        {
            transform.localScale = new Vector3(
                Mathf.Abs(originalScale.x),
                originalScale.y,
                originalScale.z
            );
        }
        else if (direction.x < -0.1f)
        {
            transform.localScale = new Vector3(
                -Mathf.Abs(originalScale.x),
                originalScale.y,
                originalScale.z
            );
        }
    }
}