using UnityEngine;

public class ButtonCube : MonoBehaviour
{
    public Material buttonMaterial; // Assign this in the Inspector

    private SimonCube simonCube;

    public AudioSource Correct;

    public AudioSource Incorrect;

    public AudioSource Win;

    void Start()
    {
        simonCube = GameObject.Find("Simon").GetComponent<SimonCube>();
    }

    void OnMouseDown()
    {
        // Handle player input
        CheckPlayerInput();
    }

    void CheckPlayerInput()
    {
        Material expectedColorMaterial = simonCube.PeekNextColor();

        if (expectedColorMaterial != null && buttonMaterial == expectedColorMaterial)
        {
            // Player's input is correct
            Debug.Log($"Correct! Expected: {expectedColorMaterial.name}, Actual: {buttonMaterial.name}. Next color...");
            Correct.Play();

            simonCube.GetNextColor(); // Advance the sequence
                                      // Update player's progress
            simonCube.playerProgress++;

            // Check if player has completed the full sequence
            if (simonCube.playerProgress == simonCube.colorSequence.Count)
            {
                // Player has completed the full sequence
                Debug.Log("Congratulations! You win!");
                Win.Play();
                // Add your win condition actions here (e.g., display win message, reset game, etc.)
            }
            // Add your logic for correct input (e.g., scoring, advancing to the next round)
        }
        else
        {
            // Player made a mistake - reset the progress and sequence
            simonCube.playerProgress = 0;
            simonCube.currentColorIndex = 0; // Reset the sequence index
            Debug.Log($"Incorrect! Expected: {expectedColorMaterial.name}, Actual: {buttonMaterial.name}. Restarting...");
            Incorrect.Play();
            // Add your logic for incorrect input (e.g., restarting the game)
        }
    }





}