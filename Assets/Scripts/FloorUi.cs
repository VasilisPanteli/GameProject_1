using UnityEngine;
using TMPro;

public class FloorUI : MonoBehaviour
{
    [SerializeField] private TMP_Text FloorText;
    
    void Start()
    {
        UpdateFloorDisplay();
    }
    
    void Update()
    {
        UpdateFloorDisplay();
    }
    
    private void UpdateFloorDisplay()
    {
        if (GameManager.Instance != null)
        {
            FloorText.text = $"Floor \n{GameManager.Instance.CurrentFloor}";
        }
    }
}