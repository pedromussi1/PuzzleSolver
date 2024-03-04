// GameManager.cs
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static void ResumeOrStartNewGame()
    {
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        PauseMenu.GameIsPaused = false;
    }
}
