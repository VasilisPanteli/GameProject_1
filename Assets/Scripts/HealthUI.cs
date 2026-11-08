using UnityEngine;
using TMPro;

public class HealthUI : MonoBehaviour
{
    [SerializeField] private TMP_Text healthText;
    [SerializeField] private int maxHealth = 100;
    
    void Start()
    {
        UpdateHealthDisplay();
    }
    
    void Update()
    {
        UpdateHealthDisplay();
    }
    
    private void UpdateHealthDisplay()
    {
        if (GameManager.Instance != null)
        {
            int currentHealth = GameManager.Instance.PlayerHealth;
            healthText.text = $"{currentHealth}/{maxHealth}";
        }
    }
}