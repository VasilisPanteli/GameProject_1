using UnityEngine;

public class EnemyKnockback : MonoBehaviour
{
 
private Rigidbody2D rb;
private EnemyFollow enemyFollow;
private float knockbackTimer = 0f;
private float knockbackDuration = 0.15f;


private float stunTimer = 0f;
private float stunDuration = 0.2f;


private SpriteRenderer spriteRenderer;
private Color originalColor;
private float flashTimer = 0f;
private float flashDuration = 0.1f;


private void Start()
{
    rb = GetComponent<Rigidbody2D>();
    enemyFollow = GetComponent<EnemyFollow>();


     spriteRenderer = GetComponent<SpriteRenderer>();
    if (spriteRenderer != null)
    {
        originalColor = spriteRenderer.color;
    }
}

private void Update()
{
    
    if (knockbackTimer > 0)
    {
        knockbackTimer -= Time.deltaTime;
        
        if (knockbackTimer <= 0 && enemyFollow != null)
        {
            
            enemyFollow.currentState = EnemyState.Following;
            rb.linearVelocity = Vector2.zero;  

            stunTimer = stunDuration;
        }
    }

if (stunTimer > 0)
    {
        stunTimer -= Time.deltaTime;
        
        if (stunTimer <= 0 && enemyFollow != null)
        {
            enemyFollow.currentState = EnemyState.Following;
        }
    }
    
   
    if (flashTimer > 0)
    {
        flashTimer -= Time.deltaTime;
        
        if (flashTimer <= 0 && spriteRenderer != null)
        {
            spriteRenderer.color = originalColor;
        }
    }



}



public void Knockback(Transform playerTransform, float knockbackForce)
{
    
    if (enemyFollow != null)
    {
        enemyFollow.currentState = EnemyState.Knockback;
        knockbackTimer = knockbackDuration;
    }

    Vector2 direction = (transform.position - playerTransform.position).normalized;
    rb.linearVelocity = direction * knockbackForce;

    if (spriteRenderer != null)
    {
        spriteRenderer.color = Color.white;
        flashTimer = flashDuration;
    }



    Debug.Log("Knockback Applied.");
}
}