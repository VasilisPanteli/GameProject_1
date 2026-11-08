using UnityEngine;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour
{
    [Header("Enemy Prefab")]
    [SerializeField] private GameObject enemyPrefab;
    
    [Header("Spawn Settings")]
    [SerializeField] private int currentFloor = 1;
    [SerializeField] private int maxFloor = 5;
    
    [Header("Spawn Area")]
    [SerializeField] private Vector2 spawnAreaSize = new Vector2(5f, 5f);
    [SerializeField] private Vector2 centerPoint = Vector2.zero;

    [Header("Ladder")]
    [SerializeField] private GameObject ladder;
    
    private List<GameObject> activeEnemies = new List<GameObject>();
    private int enemiesToSpawn = 0;
    private bool floorCleared = false;
    
    private void Start()
    {
        if (ladder != null)
        {
            ladder.SetActive(false);
            Debug.Log("Ladder hidden at start");
        }
        else
        {
            Debug.LogError("Ladder reference missing");
        }


        // Start with floor 1
        LoadFloor(currentFloor);
    }

    void Update()
{
    // TEST: Press L key to manually spawn ladder
    if (Input.GetKeyDown(KeyCode.L))
    {
        Debug.Log("MANUAL TEST: L key pressed - trying to spawn ladder");
        SpawnLadder();
    }
}


    public void LoadFloor(int floorNumber)
    {
        currentFloor = floorNumber;
        floorCleared = false;
        
        // Clear any existing enemies
        ClearAllEnemies();
        
        // Calculate how many enemies to spawn (1 enemy per floor)
        enemiesToSpawn = Mathf.Min(currentFloor, maxFloor);
        
        // Spawn enemies
        SpawnEnemies(enemiesToSpawn);
        
        Debug.Log($"Floor {currentFloor} loaded. Spawning {enemiesToSpawn} enemies.");
    }
    
    private void SpawnEnemies(int count)
    {
        for (int i = 0; i < count; i++)
        {
            Vector3 spawnPosition = GetRandomSpawnPosition();
            GameObject newEnemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
            activeEnemies.Add(newEnemy);
            
            // Add reference to spawner so enemy can report death
            EnemyHealth EnemyHealth = newEnemy.GetComponent<EnemyHealth>();
            if (EnemyHealth != null)
            {
                EnemyHealth.SetSpawner(this);
            }
        }
    }
    
    private Vector3 GetRandomSpawnPosition()
    {
        int maxAttempts = 50;
        float minDistanceBetweenEnemies = 1.5f;
        
        for (int attempt = 0; attempt < maxAttempts; attempt++)
        {
            float x = centerPoint.x + Random.Range(-spawnAreaSize.x / 2, spawnAreaSize.x / 2);
            float y = centerPoint.y + Random.Range(-spawnAreaSize.y / 2, spawnAreaSize.y / 2);
            Vector3 candidatePosition = new Vector3(x, y, 0f);
            
            bool tooClose = false;
            foreach (GameObject existingEnemy in activeEnemies)
            {
                if (existingEnemy != null && Vector3.Distance(candidatePosition, existingEnemy.transform.position) < minDistanceBetweenEnemies)
                {
                    tooClose = true;
                    break;
                }
            }
            
            if (!tooClose)
            {
                return candidatePosition;
            }
        }
        
        // Fallback - return random position anyway
        return new Vector3(
            centerPoint.x + Random.Range(-spawnAreaSize.x / 2, spawnAreaSize.x / 2),
            centerPoint.y + Random.Range(-spawnAreaSize.y / 2, spawnAreaSize.y / 2),
            0f
        );
    }
    
    public void EnemyDied(GameObject deadEnemy)
    {
        if (activeEnemies.Contains(deadEnemy))
        {
            activeEnemies.Remove(deadEnemy);
            Debug.Log($"Enemy removed. Remaining enemies: {activeEnemies.Count}");
        }

        else
        {
        Debug.Log($"Enemy not found in activeEnemies list!");
        }
        
        // Check if floor is cleared
        if (activeEnemies.Count == 0 && !floorCleared)
        {
            Debug.Log("Floor cleared! Calling OnFloorCleared()");
            OnFloorCleared();
        }
    }
    
    private void OnFloorCleared()
    {
        floorCleared = true;
        Debug.Log($"Floor {currentFloor} cleared!");
        
        if (GameManager.Instance != null)
    {
        GameManager.Instance.NextFloor();
    }


        // Spawn ladder
        SpawnLadder();
    }
    
    private void SpawnLadder()
    {

        if (ladder != null)
    {
        Debug.Log("Ladder found. Current active state: " + ladder.activeSelf);
        
        ladder.SetActive(true);
        
        Debug.Log("After SetActive(true): " + ladder.activeSelf);
        
        // Check sprite renderer
        SpriteRenderer sr = ladder.GetComponent<SpriteRenderer>();
        if (sr != null)
        {
            Debug.Log("SpriteRenderer enabled: " + sr.enabled);
            Debug.Log("Sprite: " + sr.sprite);
        }
        else
        {
            Debug.LogError("Ladder has no SpriteRenderer!");
        }
        
        Debug.Log("Ladder position: " + ladder.transform.position);
    }
    else
    {
        Debug.LogError("Ladder reference is missing!");
    }
       
        //GameObject ladder = GameObject.Find("Ladder");

        // GameObject ladder = GameObject.FindGameObjectWithTag("Ladder");

        // if (ladder != null)
        // {
        //     Debug.Log("Ladder Spawned");
        //     ladder.SetActive(true);
            
            
        //     ladder.transform.position = centerPoint;
        // }
        // else
        // {
        //     Debug.Log("Ladder not found - create a Ladder GameObject");
        // }

    /*if (ladder != null)
        {
            Debug.Log("Ladder Spawned");
            ladder.SetActive(true);
            //ladder.transform.position = centerPoint;
        }
        else
        {
            Debug.LogError("Ladder reference is missing! Drag the Ladder GameObject into the Inspector.");
        }*//////////


    }
    
    public void GoToNextFloor()
    {
        if (currentFloor < maxFloor)
        {
            currentFloor++;
            LoadFloor(currentFloor);
            
            // Hide ladder
            //GameObject ladder = GameObject.Find("Ladder");
            // GameObject ladder = GameObject.FindGameObjectWithTag("Ladder");
            // if (ladder != null)
            // {
            //     ladder.SetActive(false);
            // }

             if (ladder != null)
            {
                ladder.SetActive(false);
            }

            
            // Heal player (optional)
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                Health playerHealth = player.GetComponent<Health>();
                if (playerHealth != null)
                {
                    playerHealth.Heal(10);
                }
            }
        }
        else
        {
            // Max floor completed - Game Win!
            Debug.Log("CONGRATULATIONS! You beat all floors!");
            // Call win screen here
        }
    }
    
    private void ClearAllEnemies()
    {
        foreach (GameObject enemy in activeEnemies)
        {
            if (enemy != null)
                Destroy(enemy);
        }
        activeEnemies.Clear();
    }
    
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(centerPoint, spawnAreaSize);
    }
}