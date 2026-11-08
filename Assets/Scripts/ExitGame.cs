using UnityEngine;
using UnityEngine.UI; // Needed if you're hooking up the button in code

public class ExitGame : MonoBehaviour
{
    // This is the function your button will call.
    public void QuitGame()
    {

                if (AudioManager.Instance != null)
        {
            AudioManager.Instance.PlaySound(AudioManager.Instance.ButtonClick);
        }
        // The '#if UNITY_EDITOR' line is a preprocessor directive.
        // It tells Unity to ONLY include the code between it and the '#else' or '#endif'
        // when the game is running inside the Unity Editor.
        #if UNITY_EDITOR
            // This line stops Play Mode in the Editor [citation:1][citation:5].
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            // This line quits the application once it's been built [citation:4][citation:6].
            Application.Quit();
        #endif
    }
}