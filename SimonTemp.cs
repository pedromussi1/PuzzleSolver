using UnityEngine;
using System.Collections;

public class SimonCube : MonoBehaviour
{
    // Assign the materials for Red, Blue, Green, Yellow in the Unity Editor
    public Material redMaterial;
    public Material blueMaterial;
    public Material greenMaterial;
    public Material yellowMaterial;

    private Material defaultMaterial;
    public Material[] colorSequence; // Array to store the color sequence
    public int currentColorIndex = 0;

    public float flashDelay = 1f;
    public float sequencePause = 2f; // Longer pause after the entire sequence

    private bool printedSequence = false;

    public int playerProgress = 0; // Track player's progress




    void Start()
    {
        defaultMaterial = GetComponent<Renderer>().material;
        colorSequence = new Material[] { redMaterial, blueMaterial, greenMaterial, yellowMaterial };

        // Check if the sequence has been printed already
        if (!printedSequence)
        {
            // Log the sequence to the console
            Debug.Log("Sequence of colors:");
            foreach (Material colorMaterial in colorSequence)
            {
                Debug.Log(colorMaterial.name);
            }

            printedSequence = true; // Set the flag to true to indicate that the sequence has been printed
        }

        StartCoroutine(FlashSequence());
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
            currentColorIndex = 0; // Reset the color index for the next sequence
        }
    }

    void ShowColor(Material colorMaterial)
    {
        GetComponent<Renderer>().material = colorMaterial;
    }

    // This method returns the next color material in the hard-coded sequence
    public Material GetNextColor()
    {
        if (currentColorIndex < colorSequence.Length)
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
        if (currentColorIndex < colorSequence.Length)
        {
            return colorSequence[currentColorIndex];
        }
        else
        {
            return null; // Signal that the sequence is complete
        }
    }

}