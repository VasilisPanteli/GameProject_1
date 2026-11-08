using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenu : MonoBehaviour
{
    
    public void PlayGame()
    {
                if (AudioManager.Instance != null)
        {
            AudioManager.Instance.PlaySound(AudioManager.Instance.ButtonClick);
        }

        SceneManager.LoadSceneAsync("Game");


    }





    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
