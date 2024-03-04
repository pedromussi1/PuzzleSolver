using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CubeBehavior : MonoBehaviour
{

    private static int matchedPairs = 0; // Track the number of matched pairs

    private bool isFlipped = false;
    private Color originalColor;
    private static CubeBehavior firstCube;
    private static CubeBehavior secondCube;

    public int currentLevel = 1;
    public int nextLevelSceneIndex = 2; // Replace "NextLevel" with the actual name of your next level scene

    public AudioSource ChallengeComplete;  // Add this line

    public AudioSource FlipSound;

    public AudioSource MatchSound;

    public GameObject LevelComp;



    // Define an array of predetermined colors using RGB values
    private static Color[] predeterminedColors = new Color[]
    {
    new Color(1f, 0f, 0f),    // Red
    new Color(0f, 1f, 0f),    // Green
    new Color(0f, 0f, 1f),    // Blue
    new Color(1f, 1f, 0f),    // Yellow
    new Color(1f, 0f, 1f),    // Magenta
    new Color(0f, 1f, 1f),    // Cyan
    new Color(0.5f, 0.5f, 0.5f),  // Gray
    new Color(0f, 0f, 0f),    // Black
    new Color(1f, 0.5f, 0f),   // Orange-like color
    new Color(0.5f, 0f, 0.5f), // Purple
    };


    private static int colorIndex = 0; // Used to cycle through the predetermined colors

    void Start()
    {
        originalColor = GetNextColor();
    }

    void OnMouseDown()
    {
        if (!isFlipped && (firstCube == null || secondCube == null))
        {
            FlipCube();
            CheckMatch();
            FlipSound.Play();
        }
    }

    void FlipCube()
    {
        GetComponent<Renderer>().material.color = originalColor; // Use the original color
        isFlipped = true;

        if (firstCube == null)
        {
            firstCube = this;
        }
        else
        {
            secondCube = this;
        }
    }

    void RevertCube()
    {
        GetComponent<Renderer>().material.color = Color.white; // Set color to white
        isFlipped = false;
    }

    void CheckMatch()
    {
        if (firstCube != null && secondCube != null)
        {
            if (firstCube.originalColor == secondCube.originalColor)
            {
                // Matched
                matchedPairs++;
                firstCube = null;
                secondCube = null;
                MatchSound.Play();

                // Check if all pairs are matched
                if (matchedPairs == 10)
                {
                    // All pairs matched, trigger next level
                    if (ChallengeComplete != null)
                    {
                        ChallengeComplete.Play();
                    }

                    LevelComp.SetActive(true);
                    Invoke("LoadNextLevel", 2f);
                    //HideObject(); // Call your method to hide an object

                    // You can add more actions for transitioning to the next level here
                }
            }
            else
            {
                // Not matched, invoke the RevertCubesWithDelay method after a delay
                Invoke("RevertCubesWithDelay", 2f);
            }
        }
    }

    void RevertCubesWithDelay()
    {
        firstCube.RevertCube();
        secondCube.RevertCube();
        firstCube = null;
        secondCube = null;
    }
    /*
    void HideObject()
    {
        // Find all objects with the tag 'Hidden' and disable them
        GameObject[] hiddenObjects = GameObject.FindGameObjectsWithTag("Hidden");

        foreach (GameObject hiddenObject in hiddenObjects)
        {
            hiddenObject.SetActive(false);
        }
    }
    */


    Color GetNextColor()
    {
        // Get the next color from the predetermined array
        Color nextColor = predeterminedColors[colorIndex];

        // Cycle to the next color index
        colorIndex = (colorIndex + 1) % predeterminedColors.Length;

        // Skip white as a predetermined color
        // if (nextColor == Color.white)
        // {
        //     nextColor = GetNextColor(); // Recursive call to get the next color
        // }

        return nextColor;
    }

    void LoadNextLevel()
    {
        // Load the next level by scene name
        SceneManager.LoadScene(nextLevelSceneIndex);
    }
}