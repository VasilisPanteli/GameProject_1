using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerMovement : MonoBehaviour
{
public float speed = 5f;
private Vector2 moveInput;


private Animator animator;
private Vector3 originalScale;

void Start()
    {
        animator = GetComponent<Animator>();
        originalScale = transform.localScale;
    }


public void OnMove(InputValue value)
{
moveInput = value.Get<Vector2>();
}
void Update()
{

 if (GameManager.Instance != null && GameManager.Instance.CurrentGameState != GameManager.GameState.Playing)
    {
        return;
    }

Vector3 move = new Vector3(moveInput.x, moveInput.y, 0f);
transform.Translate(move * speed * Time.deltaTime);

bool isRunning = moveInput.magnitude > 0.1f;

animator.SetBool("isRunning", isRunning);


if (moveInput.x > 0.1f)
        {
            transform.localScale = new Vector3(originalScale.x, originalScale.y, originalScale.z);
        }
        else if (moveInput.x < -0.1f)
        {
            transform.localScale = new Vector3(-originalScale.x, originalScale.y, originalScale.z);
        }

}
}