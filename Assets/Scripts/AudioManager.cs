using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }
    
    [Header("Audio Sources")]
    //[SerializeField] private AudioSource sfxSource;
    [SerializeField] private AudioSource musicSource;  


    [Header("Audio Sources")]
    [SerializeField] private AudioSource sfxSource;
    
    [Header("Sound Effects")]
    [SerializeField] public AudioClip SwordSlash;
    [SerializeField] public AudioClip Damage;
    [SerializeField] public AudioClip EnemyDeath;
    [SerializeField] public AudioClip Healing;
    [SerializeField] public AudioClip ButtonClick;

    [Header("Music")]  // ADD THIS SECTION
    [SerializeField] public AudioClip MenuMusic;  // ADD THIS
    [SerializeField] public AudioClip gameMusic;
    
    void Awake()
    {
        // Singleton setup
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        
        // Create audio source if not assigned
        if (sfxSource == null)
        {
            sfxSource = gameObject.AddComponent<AudioSource>();
        }

        if (musicSource == null)
        {
            musicSource = gameObject.AddComponent<AudioSource>();
            musicSource.loop = true;
            musicSource.volume = 1f;
        }

    }

    void Start()  // ADD THIS METHOD
    {
            Scene currentScene = SceneManager.GetActiveScene();
        if (currentScene.name == "Main Menu")
            PlayMenuMusic();
        else if (currentScene.name == "Game")
            PlayGameMusic();
    }
    
    // public void PlayBackgroundMusic()  // ADD THIS METHOD
    // {
    //     if (backgroundMusic != null && musicSource != null)
    //     {
    //         musicSource.clip = backgroundMusic;
    //         musicSource.loop = true;
    //         musicSource.Play();
    //     }
    // }

    
    public void PlaySound(AudioClip clip)
    {
        if (clip != null && sfxSource != null)
        {
            sfxSource.PlayOneShot(clip);
        }
    }


    void OnEnable()
{
    SceneManager.sceneLoaded += OnSceneLoaded;
}

void OnDisable()
{
    SceneManager.sceneLoaded -= OnSceneLoaded;
}

void OnSceneLoaded(Scene scene, LoadSceneMode mode)
{
    // Change music based on scene name
    if (scene.name == "Main Menu")
    {
        PlayMenuMusic();
    }
    else if (scene.name == "Game")
    {
        PlayGameMusic();
    }
}

public void PlayMenuMusic()
{
    // Load your menu music clip here
    if (MenuMusic != null && musicSource != null)
    {
        musicSource.clip = MenuMusic;
        musicSource.Play();
    }
}

public void PlayGameMusic()
{
    // Load your game music clip here
    if (gameMusic != null && musicSource != null)
    {
        musicSource.clip = gameMusic;
        musicSource.Play();
    }
}
}