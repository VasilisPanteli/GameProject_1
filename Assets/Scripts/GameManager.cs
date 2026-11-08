using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Singleton pattern - only one GameManager exists
    public static GameManager Instance { get; private set; }
    
    [Header("Game State")]
    private GameState currentState = GameState.Playing;
    public enum GameState
    {
        Playing,
        Paused,
        GameOver,
        Win
    }

    // private GameObject defeatCanvas;
    // private GameObject victoryCanvas;
    // private GameObject pauseCanvas;


    [SerializeField] private GameObject defeatCanvas;
    [SerializeField] private GameObject victoryCanvas;

    [Header("Pause")]
    [SerializeField] private GameObject pauseCanvas;  
    private bool isPaused = false;
    
    [Header("Player Stats")]
    [SerializeField] private int playerHealth = 100;
    [SerializeField] private int playerMaxHealth = 100;
    [SerializeField] private int currentFloor = 1;
    [SerializeField] private int maxFloors = 5;
    
    [Header("Score")]
    [SerializeField] private int totalScore = 0;
    [SerializeField] private int enemiesDefeated = 0;
    
    [Header("References")]
    private GameObject player;
    private PlayerAttack playerAttack;
    private Health playerHealthScript;
    private EnemySpawner enemySpawner;
    public delegate void ScoreChanged(int newScore);
    public static event ScoreChanged OnScoreChanged;
    

    // Properties for other scripts to access
    public int PlayerHealth => playerHealth;
    public int CurrentFloor => currentFloor;
    public int TotalScore => totalScore;
    public int EnemiesDefeated => enemiesDefeated;
    public GameState CurrentGameState => currentState;
    public delegate void HealthChanged(int currentHealth);
    public static event HealthChanged OnHealthChanged;

    
    // void Awake()
    // {
    //     // Singleton setup
    //     if (Instance == null)
    //     {
    //         Instance = this;
    //         DontDestroyOnLoad(gameObject);
    //     }
    //     else
    //     {
    //         Destroy(gameObject);
    //         return;
    //     }
    // }


    void Awake()
{
    // Singleton setup
    if (Instance == null)
    {
        Instance = this;
        //DontDestroyOnLoad(gameObject);
    }
    else
    {
        Destroy(gameObject);
        return;
    }
}

/*void OnEnable()
{
    // Subscribe to scene load event
    SceneManager.sceneLoaded += OnSceneLoaded;
}

void OnDisable()
{
    // Unsubscribe from scene load event
    SceneManager.sceneLoaded -= OnSceneLoaded;
}

void OnSceneLoaded(Scene scene, LoadSceneMode mode)
{
    // defeatCanvas = GameObject.Find("DefeatCanvas");
    // victoryCanvas = GameObject.Find("VictoryCanvas");
    // pauseCanvas = GameObject.Find("PauseCanvas");

    // Reset game state when scene reloads
    InitializeGame();
    
    // Find new references in the new scene
    FindReferences();
    
    // Hide end game canvases
    if (defeatCanvas != null) defeatCanvas.SetActive(false);
    if (victoryCanvas != null) victoryCanvas.SetActive(false);
    if (pauseCanvas != null) pauseCanvas.SetActive(false);
    
    // Reset pause flag
    isPaused = false;
}*/
    
    void Start()
    {

        defeatCanvas = GameObject.Find("DefeatCanvas");
        victoryCanvas = GameObject.Find("VictoryCanvas");
        pauseCanvas = GameObject.Find("PauseCanvas");

        FindReferences();
        InitializeGame();

        if (defeatCanvas != null) defeatCanvas.SetActive(false);
        if (victoryCanvas != null) victoryCanvas.SetActive(false);
        if (pauseCanvas != null) pauseCanvas.SetActive(false);        
    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
        
        
        // if (Input.GetKeyDown(KeyCode.N))
        // {
        //     NextFloor();
        // }
    }
    
    void FindReferences()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerAttack = player.GetComponent<PlayerAttack>();
            playerHealthScript = player.GetComponent<Health>();
        }
        
        enemySpawner = FindFirstObjectByType<EnemySpawner>();
    }
    
    void InitializeGame()
    {
        playerHealth = playerMaxHealth;
        totalScore = 0;
        enemiesDefeated = 0;
        currentFloor = 1;
        currentState = GameState.Playing;
        Time.timeScale = 1f;

        OnHealthChanged?.Invoke(playerHealth);
        OnScoreChanged?.Invoke(totalScore);
        
        Debug.Log("Game Initialized!");
    }
    
    public void AddScore(int amount)
    {
        totalScore += amount;
        Debug.Log($"Score: {totalScore}");
        OnScoreChanged?.Invoke(totalScore);
        
    }
    
    public void EnemyDefeated()
    {
        enemiesDefeated++;
        AddScore(100); // 100 points per enemy
        Debug.Log($"Enemies defeated: {enemiesDefeated}");
    }
    
    public void UpdatePlayerHealth(int currentHealth)
    {
        playerHealth = currentHealth;
        OnHealthChanged?.Invoke(playerHealth);
        
        if (playerHealth <= 0)
        {
            GameOver();
        }
        
        // Update UI here if you have one
    }

    
    
    public void NextFloor()
    {
        if (currentFloor < maxFloors)
        {
            currentFloor++;
            Debug.Log($"Moving to Floor {currentFloor}");
            
            // Heal player on floor completion
            // if (playerHealthScript != null)
            // {
            //     playerHealthScript.Heal(10); // Heal 10 HP per floor
            // }
        }
        else
        {
            GameWin();
        }
    }
    
    public void GameOver()
    {
        currentState = GameState.GameOver;
        Time.timeScale = 0f;
        Debug.Log("GAME OVER!");
        // Show Game Over UI here


        if (defeatCanvas != null)
        {
            defeatCanvas.SetActive(true);
        }
    }
    
    public void GameWin()
    {
        currentState = GameState.Win;
        Time.timeScale = 0f;
        Debug.Log("YOU WIN!");
        // Show Win UI here


        if (victoryCanvas != null)
        {
            victoryCanvas.SetActive(true);
        }

    }
    
    public void TogglePause()
    {
        isPaused = !isPaused;
        
        if (isPaused)
        {
            Time.timeScale = 0f;
            if (pauseCanvas != null) pauseCanvas.SetActive(true);
            Debug.Log("Game Paused");
        }
        else
        {
            Time.timeScale = 1f;
            if (pauseCanvas != null) pauseCanvas.SetActive(false);
            Debug.Log("Game Resumed");
        }
    }

    public void ResumeGame()
    {
        if (isPaused)
        {

        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.PlaySound(AudioManager.Instance.ButtonClick);
        }

            TogglePause();
        }
    }

    public void RestartGame()
    {

    if (AudioManager.Instance != null)
    {
        AudioManager.Instance.PlaySound(AudioManager.Instance.ButtonClick);
    }

    Time.timeScale = 1f;
    SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    }

    public void QuitGame()
    {
        //if (isPaused)
        //{}

        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.PlaySound(AudioManager.Instance.ButtonClick);
        }

            Time.timeScale = 1f;
            SceneManager.LoadSceneAsync("Main Menu");
        
    }

    
    // public void RestartGame()
    // {
    //     Time.timeScale = 1f;
    //     UnityEngine.SceneManagement.SceneManager.LoadScene(
    //         UnityEngine.SceneManagement.SceneManager.GetActiveScene().name
    //     );
    // }
}