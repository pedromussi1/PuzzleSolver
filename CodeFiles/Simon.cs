using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class SimonCube : MonoBehaviour
{
    // Assign the materials for Red, Blue, Green, Yellow in the Unity Editor
    public Material redMaterial;
    public Material blueMaterial;
    public Material greenMaterial;
    public Material yellowMaterial;

    private Material defaultMaterial;
    public List<Material> colorSequence; // List to store the color sequence
    public int currentColorIndex = 0;

    public float flashDelay = 1f;
    public float sequencePause = 2f; // Longer pause after the entire sequence

   
    private int sequenceLength = 4; // Initial sequence length
    private int setsCompleted = 0; // Track the number of sets completed

    public int playerProgress = 0; // Track player's progress

    public GameObject LevelComp;

    public int currentLevel = 1;
    public int nextLevelSceneIndex = 2; // Replace "NextLevel" with the actual name of your next level scene

    void Start()
    {
        defaultMaterial = GetComponent<Renderer>().material;
        colorSequence = new List<Material>();

        GenerateRandomSequence(); // Generate initial random sequence
        StartCoroutine(FlashSequence());
    }

    void GenerateRandomSequence()
    {
        colorSequence.Clear();
        for (int i = 0; i < sequenceLength; i++)
        {
            Material randomColor = GetRandomColor();
            colorSequence.Add(randomColor);
        }
    }

    Material GetRandomColor()
    {
        Material[] colors = { redMaterial, blueMaterial, greenMaterial, yellowMaterial };
        return colors[Random.Range(0, colors.Length)];
    }

    IEnumerator FlashSequence()
    {
        while (true)
        {
            foreach (Material colorMaterial in colorSequence)
            {
                ShowColor(colorMaterial);
                yield return new WaitForSeconds(flashDelay);
                ShowColor(defaultMaterial); // Use the default material or white for a brief pause
                yield return new WaitForSeconds(0.5f); // Delay between flashes
            }

            yield return new WaitForSeconds(sequencePause); // Longer pause after the entire sequence

            // Check if player has completed the sequence
            if (playerProgress == sequenceLength)
            {
                playerProgress = 0; // Reset player progress
                currentColorIndex = 0; // Reset color index

                // Increase sequence length after each successful completion
                sequenceLength++;
                setsCompleted++;

                // Check if three sets have been completed
                if (setsCompleted >= 3)
                {
                    Debug.Log("Congratulations! You have completed three sets.");

                    LevelComp.SetActive(true);
                    Invoke("LoadNextLevel", 2f);



                    // Add your end game logic here
                    yield break; // Exit coroutine
                }

                GenerateRandomSequence(); // Generate new random sequence
                Debug.Log($"New sequence generated with length {sequenceLength}");
            }
        }
    }

    void ShowColor(Material colorMaterial)
    {
        GetComponent<Renderer>().material = colorMaterial;
    }

    // This method returns the next color material in the sequence
    public Material GetNextColor()
    {
        if (currentColorIndex < colorSequence.Count)
        {
            return colorSequence[currentColorIndex++];
        }
        else
        {
            return null; // Signal that the sequence is complete
        }
    }

    // In the SimonCube class
    public Material PeekNextColor()
    {
        if (currentColorIndex < colorSequence.Count)
        {
            return colorSequence[currentColorIndex];
        }
        else
        {
            return null; // Signal that the sequence is complete
        }
    }

    void LoadNextLevel()
    {
        // Load the next level by scene name
        SceneManager.LoadScene(nextLevelSceneIndex);
    }


}
