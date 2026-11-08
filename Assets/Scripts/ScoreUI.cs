using UnityEngine;
using TMPro;

public class ScoreUI : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreText;
    
    void Start()
    {
        UpdateScoreDisplay();
    }
    
    void Update()
    {
        UpdateScoreDisplay();
    }
    
    private void UpdateScoreDisplay()
    {
        if (GameManager.Instance != null)
        {
            scoreText.text = $"Score: {GameManager.Instance.TotalScore}";
        }
    }
}