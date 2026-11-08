using UnityEngine;

public class Ladder : MonoBehaviour
{
    private EnemySpawner spawner;

    // void Update()
    // {
    //         if (Input.GetKeyDown(KeyCode.L))
    // {
    //     Debug.Log("Manual ladder spawn");
    //     OnFloorCleared();
    // }
    // }



    void Start()
    {
        //spawner = FindObjectOfType<EnemySpawner>();
        spawner = FindFirstObjectByType<EnemySpawner>();
        //gameObject.SetActive(false); 
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {

             if (AudioManager.Instance != null)
        {
            AudioManager.Instance.PlaySound(AudioManager.Instance.Healing);
        }



            if (spawner != null)
            {
                spawner.GoToNextFloor();
            }
        }
    }

private void SpawnLadder()
{
    Debug.Log("SpawnLadder: Searching for Ladder...");
    
    // Try both methods to see what's happening
    GameObject ladderByName = GameObject.Find("Ladder");
    GameObject ladderByTag = GameObject.FindGameObjectWithTag("Ladder");
    
    Debug.Log($"Find by NAME 'Ladder': {(ladderByName != null ? "FOUND" : "NOT FOUND")}");
    Debug.Log($"Find by TAG 'Ladder': {(ladderByTag != null ? "FOUND" : "NOT FOUND")}");
    
    GameObject ladder = ladderByTag; // Use tag method
    
    if (ladder != null)
    {
        Debug.Log("Ladder found! Current active state: " + ladder.activeSelf);
        ladder.SetActive(true);
        Debug.Log("Ladder activated. New active state: " + ladder.activeSelf);
        //ladder.transform.position = centerPoint;
        //Debug.Log("Ladder position set to: " + centerPoint);
    }
    else
    {
        Debug.LogError("Ladder NOT FOUND! Check:");
        Debug.LogError("1. Is there a GameObject with Tag 'Ladder'?");
        Debug.LogError("2. Is the tag spelled exactly 'Ladder'?");
        Debug.LogError("3. Is the Ladder GameObject in the scene (not a prefab)?");
    }
}



}