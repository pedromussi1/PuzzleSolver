using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelector : MonoBehaviour
{
    public int sceneNum = 0;

    private void Start()
    {
        // Enable the button for level 1, otherwise check if the player has completed the previous level
        if (sceneNum == 1 || HasCompletedPreviousLevel())
        {
            GetComponent<UnityEngine.UI.Button>().interactable = true;
        }
        else
        {
            GetComponent<UnityEngine.UI.Button>().interactable = false;
        }
    }

    public void OpenScene()
    {
        GameManager.ResumeOrStartNewGame();
        SceneManager.LoadScene(sceneNum);
    }

    private bool HasCompletedPreviousLevel()
    {
        // Check if the player has completed the previous level, except for level 1
        if (sceneNum > 1)
        {
            int previousLevel = sceneNum - 1;
            return PlayerPrefs.GetInt("Level_" + previousLevel.ToString(), 0) == 1;
        }

        return false; // Level 1 is always considered completed
    }
}
