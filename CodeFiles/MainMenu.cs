// MainMenu.cs
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        // Ensure that the player is not considered as paused when starting a new game
        PauseMenu.GameIsPaused = false;

        // Call the GameManager method that handles both resuming and starting a new game
        GameManager.ResumeOrStartNewGame();

        // Other logic for starting the game (e.g., loading the scene)
        SceneManager.LoadScene(1); // Load the scene at index 1
    }

    // ... other code ...





    public void QuitGame()
    {
        Debug.Log("QUIT!");
        Application.Quit();
    }

}
